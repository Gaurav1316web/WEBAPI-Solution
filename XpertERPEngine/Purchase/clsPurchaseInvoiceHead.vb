Imports common
Imports System.Data.SqlClient
''KDI/18/05/18-000320 by balwinder on 18/05/2018 tran bulk milk purchase invoice pick purchase account.
Public Class clsPurchaseInvoiceHead
#Region "Variables"
    Public isJobWorkOutward As Integer = 0
    Public PROJECT_ID As String = Nothing
    Public PI_No As String = Nothing
    Public Vendor_Invoice_No As String = Nothing
    Public PI_Date As String = Nothing
    Public Vendor_Code As String = Nothing
    Public DateAndTime As DateTime?
    Public TapalNo As String = String.Empty
    Public Vendor_Name As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public Ref_No As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Document_Type As String = Nothing
    Public Bill_To_Location As String = Nothing
    Public BillToLocationName As String = Nothing
    Public Ship_To_Location As String = Nothing
    Public ShipToLocationName As String = Nothing
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
    Public TDS_Provision As Boolean = False
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
    Public Total_Taxable_Amount As Decimal = 0
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
    Public Arr As List(Of clsPurchaseInvoiceDetail) = Nothing
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

    Public Total_Accepted_Amount As Double = 0
    Public Total_Rejected_Amount As Double = 0
    Public Total_Shortage_Amount As Double = 0
    Public Total_Leak_Amount As Double = 0
    Public Total_Burst_Amount As Double = 0
    Public Is_Shortage_Include_In_Landed_Cost As Boolean = False
    Public GSTRegistered As Integer = 0
    Public ITC_Elibible As Integer = 0
    Public ITC_Type As String = Nothing
    Public ITC_Type_Category As String = Nothing
    Public Port As String = Nothing
    Public Import_Entry_No As String = Nothing
    Public Import_Entry_Date As String = Nothing
    Public PI_Type As String = Nothing
    Public Sublocation_Code As String = String.Empty
    Public SubLocationName As String = String.Empty
    Public RoundOffAmt As Decimal
    Public ActualTCSBaseAmount As Double = 0
    Public ChangedTCSBaseAmount As Double = 0
    Public Total_Item_Insurance_Amt As Decimal = 0
    Public Total_Add_Charge_Insurance As Decimal = 0
    Public Arr_ACInsurance As List(Of clsPIAdditionChargeInsurance) = Nothing
#End Region

    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PI_HEAD", "PI_No", "TSPL_PI_DETAIL", "PI_No", "TSPL_PI_REMITTANCE", "Document_No", trans)
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsPurchaseInvoiceHead, ByVal isNewEntry As Boolean) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Invoice", obj.Bill_To_Location, clsCommon.myCDate(obj.PI_Date), trans)
            If Not isNewEntry Then
                HistoryUpdate(obj.PI_No, trans)
            End If
            clsPIAdditionChargeInsurance.DeleteData(obj.PI_No, trans)
            Dim qry As String = "delete from TSPL_PI_DETAIL where PI_No='" + obj.PI_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_PI_REMITTANCE where Document_No='" + obj.PI_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If obj.isJobWorkOutward = 1 Then
                    obj.PI_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PI_Date), clsDocType.POInvoice, clsDocTransactionType.POJobWorkOutward, obj.Sublocation_Code)
                Else
                    If clsCommon.CompairString(obj.Document_Type, "PI") = CompairStringResult.Equal Then
                        Dim TransType = clsDBFuncationality.getSingleValue("SELECT PREFIX_CODE FROM TSPL_ITEM_TYPE_MASTER WHERE ITEM_TYPE_CODE='" + obj.Item_Type + "'", trans)
                        obj.PI_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PI_Date), clsDocType.POInvoice, TransType, obj.Bill_To_Location)
                        If clsCommon.CompairString(obj.PI_No, String.Empty) = CompairStringResult.Equal Then
                            Throw New Exception("Item Type is Not Correct To Generate the Transaction Code")
                        End If
                    ElseIf clsCommon.CompairString(obj.Document_Type, "MT") = CompairStringResult.Equal Then
                        Dim TransType = clsDBFuncationality.getSingleValue("SELECT PREFIX_CODE FROM TSPL_ITEM_TYPE_MASTER WHERE ITEM_TYPE_CODE='" + obj.Item_Type + "'", trans)
                        obj.PI_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PI_Date), clsDocType.MT_POInvoice, TransType, obj.Bill_To_Location)
                        If clsCommon.CompairString(obj.PI_No, String.Empty) = CompairStringResult.Equal Then
                            Throw New Exception("Item Type is Not Correct To Generate the Transaction Code")
                        End If
                    End If
                End If
            End If
            If (clsCommon.myLen(obj.PI_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PI_Date", clsCommon.GetPrintDate(obj.PI_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Vendor_Invoice_No", obj.Vendor_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
            clsCommon.AddColumnsForChange(coll, "GSTRegistered", obj.GSTRegistered)
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Location", obj.Ship_To_Location)
            clsCommon.AddColumnsForChange(coll, "Sublocation_Code", obj.Sublocation_Code)
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
            clsCommon.AddColumnsForChange(coll, "Total_Taxable_Amount", obj.Total_Taxable_Amount)
            clsCommon.AddColumnsForChange(coll, "PI_Total_Amt", obj.PI_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "TDS_Provision", IIf(obj.TDS_Provision, 1, 0))

            clsCommon.AddColumnsForChange(coll, "RoundOffAmt", obj.RoundOffAmt)
            clsCommon.AddColumnsForChange(coll, "Against_Requisition", obj.Against_Requisition, True)
            clsCommon.AddColumnsForChange(coll, "Against_PO", obj.Against_PO, True)
            clsCommon.AddColumnsForChange(coll, "Against_GRN", obj.Against_GRN, True)
            clsCommon.AddColumnsForChange(coll, "Against_MRN", obj.Against_MRN, True)
            clsCommon.AddColumnsForChange(coll, "Against_SRN", obj.Against_SRN, True)
            clsCommon.AddColumnsForChange(coll, "Tot_Empty_Amount", obj.Tot_Empty_Amount)
            clsCommon.AddColumnsForChange(coll, "Against_C_Form", IIf(obj.Against_C_Form, 1, 0))
            clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)
            clsCommon.AddColumnsForChange(coll, "LR_No", obj.LRNo, True)
            clsCommon.AddColumnsForChange(coll, "isJobWorkOutward", obj.isJobWorkOutward)
            If obj.Invdate IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "InvoiceDate", clsCommon.GetPrintDate(obj.Invdate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "InvoiceDate", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "TapalNo", obj.TapalNo, True)
            If clsCommon.myLen(obj.DateAndTime) > 0 Then
                clsCommon.AddColumnsForChange(coll, "DateAndTime", clsCommon.GetPrintDate(obj.DateAndTime, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "DateAndTime", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Total_Accepted_Amount", obj.Total_Accepted_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Rejected_Amount", obj.Total_Rejected_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Shortage_Amount", obj.Total_Shortage_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Leak_Amount", obj.Total_Leak_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Burst_Amount", obj.Total_Burst_Amount)
            clsCommon.AddColumnsForChange(coll, "Is_Shortage_Include_In_Landed_Cost", IIf(obj.Is_Shortage_Include_In_Landed_Cost, 1, 0))


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
            If clsCommon.myLen(obj.ApplicableFrom) > 0 Then
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", Nothing, True)
            End If
            '' End currencyconversion
            clsCommon.AddColumnsForChange(coll, "IsAbatementPO", obj.IsAbatementPO)
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

            clsCommon.AddColumnsForChange(coll, "ITC_Elibible", IIf(obj.ITC_Elibible, 1, 0))
            clsCommon.AddColumnsForChange(coll, "ITC_Type", obj.ITC_Type)
            clsCommon.AddColumnsForChange(coll, "ITC_Type_Category", obj.ITC_Type_Category)

            clsCommon.AddColumnsForChange(coll, "PI_Type", obj.PI_Type)
            clsCommon.AddColumnsForChange(coll, "Import_Entry_No", obj.Import_Entry_No)
            clsCommon.AddColumnsForChange(coll, "Port", obj.Port)
            If clsCommon.myLen(obj.Import_Entry_No) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Import_Entry_Date", clsCommon.GetPrintDate(obj.Import_Entry_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "Total_Add_Charge_Insurance", obj.Total_Add_Charge_Insurance)
            clsCommon.AddColumnsForChange(coll, "Total_Item_Insurance_Amt", obj.Total_Item_Insurance_Amt)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "PI_No", obj.PI_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PI_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PI_HEAD", OMInsertOrUpdate.Update, "TSPL_PI_HEAD.PI_No='" + obj.PI_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsPurchaseInvoiceDetail.SaveData(obj.PI_No, obj.Bill_To_Location, Arr, trans)
            isSaved = isSaved AndAlso clsPIRemittance.SaveData(obj.objPIRemittance, obj.PI_No, obj.PI_Date, trans)
            Dim strPJVNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PJV_No from TSPL_PJV_HEAD where Invoice_No='" + obj.PI_No + "' ", trans))
            Dim objJVC As New clsPJVHead
            objJVC = objJVC.SetPJVData(strPJVNo, obj, trans)
            isSaved = isSaved AndAlso objJVC.SaveData(objJVC, obj.PI_No, obj.Bill_To_Location, isNewEntry, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.PI_No, obj.arrCustomFields, trans)
            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "PI_No", obj.PI_No, "TSPL_PI_HEAD", trans)
            RequiredMgmtApprovalIfRateIncrease(obj, trans)
            isSaved = isSaved AndAlso clsPIAdditionChargeInsurance.SaveData(obj.PI_No, obj.PI_Date, obj.Arr_ACInsurance, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Private Function RequiredMgmtApprovalIfRateIncrease(ByVal obj As clsPurchaseInvoiceHead, ByVal trans As SqlTransaction) As Boolean
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RequiredMgmtApprovalForRateIncrease, clsFixedParameterCode.RequiredMgmtApprovalForRateIncrease, trans)) = 1 Then
            Dim qry As String = "delete from TSPL_TRANSACTION_APPROVAL where Document_No='" & obj.PI_No & "' and Screen_Name='" + clsUserMgtCode.mbtnPurchaseInvoice + "'"
            clsDBFuncationality.GetDataTable(qry, trans)

            qry = "select TSPL_PI_DETAIL.PI_No,TSPL_PI_DETAIL.PO_ID,TSPL_PI_DETAIL.Item_Code,TSPL_PI_DETAIL.item_cost ,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost as POCost" + Environment.NewLine +
            "from TSPL_PI_DETAIL" + Environment.NewLine +
            "left outer join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID and TSPL_PURCHASE_ORDER_DETAIL.Item_Code=TSPL_PI_DETAIL.Item_Code" + Environment.NewLine +
            "where TSPL_PI_DETAIL.PI_No ='" + obj.PI_No + "' and " + Environment.NewLine +
            "TSPL_PI_DETAIL.item_cost >TSPL_PURCHASE_ORDER_DETAIL.Item_Cost"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " &
                    "values ('" + clsUserMgtCode.mbtnPurchaseInvoice + "','" & clsUserMgtCode.mbtnPurchaseInvoice & "','" & obj.PI_No & "','" & clsCommon.GetPrintDate(obj.PI_Date, "dd-MMM-yyyy hh:mm:ss") & "','Rate',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
        End If
        Return True
    End Function

    Public Shared Function UpdateSecondaryInfo(ByVal obj As clsPurchaseInvoiceHead, ByVal trans As SqlTransaction) As Boolean
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
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PI_HEAD", OMInsertOrUpdate.Update, "TSPL_PI_HEAD.PI_No='" + obj.PI_No + "'", trans)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal arrLoc As String) As clsPurchaseInvoiceHead
        Return GetData(strDocumentNo, NavType, Nothing, arrLoc)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal arrLoc As String) As clsPurchaseInvoiceHead
        Dim obj As clsPurchaseInvoiceHead = Nothing
        Dim qry As String = "SELECT TSPL_PI_HEAD.*, " &
        " TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName, " &
        " TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName," &
        " TSPL_LOCATION_MASTER_SubLocation.Location_Desc as SubLocationName " &
        " FROM TSPL_PI_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PI_HEAD.Bill_To_Location " &
        " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_SubLocation  on TSPL_LOCATION_MASTER_SubLocation.Location_Code=TSPL_PI_HEAD.Sublocation_Code " &
        " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_PI_HEAD.Ship_To_Location " &
        " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_PI_HEAD.Tax_Group " &
        " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_PI_HEAD.Terms_Code where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrCls = " and Bill_To_Location in (" + arrLoc + ") "
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PI_HEAD.PI_No = (select MIN(PI_No) from TSPL_PI_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_PI_HEAD.PI_No = (select Max(PI_No) from TSPL_PI_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Next
                qry += " and TSPL_PI_HEAD.PI_No = (select Min(PI_No) from TSPL_PI_HEAD where PI_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_PI_HEAD.PI_No = (select Max(PI_No) from TSPL_PI_HEAD where PI_No<'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_PI_HEAD.PI_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPurchaseInvoiceHead()
            obj.isJobWorkOutward = clsCommon.myCstr(dt.Rows(0)("isJobWorkOutward"))
            obj.PI_No = clsCommon.myCstr(dt.Rows(0)("PI_No"))
            obj.PI_Date = clsCommon.myCstr(dt.Rows(0)("PI_Date"))
            obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("Document_Type"))
            obj.Vendor_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Vendor_Invoice_No"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.GSTRegistered = clsCommon.myCdbl(dt.Rows(0)("GSTRegistered"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
            obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
            obj.Sublocation_Code = clsCommon.myCstr(dt.Rows(0)("Sublocation_Code"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.TDS_Provision = IIf(clsCommon.myCdbl(dt.Rows(0)("TDS_Provision")) = 1, True, False)

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
            obj.Total_Taxable_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Taxable_Amount"))
            obj.PI_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("PI_Total_Amt"))
            obj.RoundOffAmt = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmt"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))
            If IsDBNull(dt.Rows(0)("DateAndTime")) = True Then
                obj.DateAndTime = Nothing
            Else
                obj.DateAndTime = clsCommon.myCstr(dt.Rows(0)("DateAndTime"))
            End If
            obj.TapalNo = clsCommon.myCstr(dt.Rows(0)("TapalNo"))
            obj.ITC_Type_Category = clsCommon.myCstr(dt.Rows(0)("ITC_Type_Category"))
            obj.ITC_Elibible = IIf(clsCommon.myCdbl(dt.Rows(0)("ITC_Elibible")) = 1, 1, 0)
            obj.ITC_Type = clsCommon.myCdbl(dt.Rows(0)("ITC_Type"))

            obj.Port = clsCommon.myCstr(dt.Rows(0)("Port"))
            obj.PI_Type = clsCommon.myCstr(dt.Rows(0)("PI_Type"))
            obj.Import_Entry_No = clsCommon.myCstr(dt.Rows(0)("Import_Entry_No"))
            If dt.Rows(0)("Import_Entry_Date") IsNot DBNull.Value Then
                obj.Import_Entry_Date = clsCommon.myCstr(dt.Rows(0)("Import_Entry_Date"))
            End If

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
            obj.SubLocationName = clsCommon.myCstr(dt.Rows(0)("SubLocationName"))
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

            obj.Total_Accepted_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Accepted_Amount"))
            obj.Total_Rejected_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Rejected_Amount"))
            obj.Total_Shortage_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Shortage_Amount"))
            obj.Total_Leak_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Leak_Amount"))
            obj.Total_Burst_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Burst_Amount"))
            obj.Is_Shortage_Include_In_Landed_Cost = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Shortage_Include_In_Landed_Cost")) = 1, True, False)
            obj.ChangedTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ChangedTCSBaseAmount"))
            obj.ActualTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ActualTCSBaseAmount"))
            obj.Total_Add_Charge_Insurance = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge_Insurance"))
            obj.Total_Item_Insurance_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Item_Insurance_Amt"))
            qry = "SELECT TSPL_PI_DETAIL.*,TSPL_LOCATION_MASTER.Location_Desc as LocationName,(case when len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 then (select MAX(SRN_Qty) from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.SRN_No=TSPL_PI_DETAIL.SRN_Id and TSPL_SRN_DETAIL.Item_Code=TSPL_PI_DETAIL.Item_Code and TSPL_SRN_DETAIL.Unit_code=TSPL_PI_DETAIL.Unit_code and ISNULL(TSPL_SRN_DETAIL.MRP,0)=isnull(TSPL_PI_DETAIL.MRP,0) and isnull(TSPL_SRN_DETAIL.Assessable,0)=isnull(TSPL_PI_DETAIL.Assessable,0))  else 0 end) as OrgSRNQty FROM TSPL_PI_DETAIL left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PI_DETAIL.Location where TSPL_PI_DETAIL.PI_No='" + obj.PI_No + "' ORDER BY TSPL_PI_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsPurchaseInvoiceDetail)
                Dim objTr As clsPurchaseInvoiceDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsPurchaseInvoiceDetail
                    objTr.PI_No = clsCommon.myCstr(dr("PI_No"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.PI_Qty = clsCommon.myCdbl(dr("PI_Qty"))
                    objTr.SRN_Id = clsCommon.myCstr(dr("SRN_Id"))
                    objTr.PO_ID = clsCommon.myCstr(dr("PO_ID"))
                    objTr.GRN_ID = clsCommon.myCstr(dr("GRN_ID"))
                    objTr.MRN_ID = clsCommon.myCstr(dr("MRN_ID"))
                    objTr.OrgSRNQty = clsCommon.myCdbl(dr("OrgSRNQty"))
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
                    objTr.Header_Discount_Per = clsCommon.myCdbl(dr("Header_Discount_Per"))
                    objTr.Header_Discount_Amount = clsCommon.myCdbl(dr("Header_Discount_Amount"))
                    objTr.Disc_Per = clsCommon.myCdbl(dr("Disc_Per"))
                    objTr.Detail_Discount_Amount = clsCommon.myCdbl(dr("Detail_Discount_Amount"))
                    objTr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amt_Less_Discount"))

                    objTr.Taxable_Amount_Per = clsCommon.myCdbl(dr("Taxable_Amount_Per"))
                    objTr.Taxable_Amount = clsCommon.myCdbl(dr("Taxable_Amount"))

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

                    objTr.Accepted_Amount = clsCommon.myCdbl(dr("Accepted_Amount"))
                    objTr.Rejected_Amount = clsCommon.myCdbl(dr("Rejected_Amount"))
                    objTr.Shortage_Amount = clsCommon.myCdbl(dr("Shortage_Amount"))
                    objTr.Leak_Amount = clsCommon.myCdbl(dr("Leak_Amount"))
                    objTr.Burst_Amount = clsCommon.myCdbl(dr("Burst_Amount"))
                    objTr.Amt_Less_Discount_Without_Shortage = clsCommon.myCdbl(dr("Amt_Less_Discount_Without_Shortage"))

                    ''------------------19/10/2016=======================
                    objTr.ItemAdd_Charge_Code1 = clsCommon.myCstr(dr("ItemAdd_Charge_Code1"))
                    objTr.ItemAdd_Charge_Code2 = clsCommon.myCstr(dr("ItemAdd_Charge_Code2"))
                    objTr.ItemAdd_Charge_Code3 = clsCommon.myCstr(dr("ItemAdd_Charge_Code3"))
                    objTr.ItemAdd_Charge_Code4 = clsCommon.myCstr(dr("ItemAdd_Charge_Code4"))
                    objTr.ItemAdd_Charge_Code5 = clsCommon.myCstr(dr("ItemAdd_Charge_Code5"))
                    objTr.ItemAdd_Charge_Code6 = clsCommon.myCstr(dr("ItemAdd_Charge_Code6"))
                    objTr.ItemAdd_Charge_Code7 = clsCommon.myCstr(dr("ItemAdd_Charge_Code7"))
                    objTr.ItemAdd_Charge_Code8 = clsCommon.myCstr(dr("ItemAdd_Charge_Code8"))
                    objTr.ItemAdd_Charge_Code9 = clsCommon.myCstr(dr("ItemAdd_Charge_Code9"))
                    objTr.ItemAdd_Charge_Code10 = clsCommon.myCstr(dr("ItemAdd_Charge_Code10"))
                    objTr.ItemAdd_Org_Charge_Amt1 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt1"))
                    objTr.ItemAdd_Org_Charge_Amt2 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt2"))
                    objTr.ItemAdd_Org_Charge_Amt3 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt3"))
                    objTr.ItemAdd_Org_Charge_Amt4 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt4"))
                    objTr.ItemAdd_Org_Charge_Amt5 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt5"))
                    objTr.ItemAdd_Org_Charge_Amt6 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt6"))
                    objTr.ItemAdd_Org_Charge_Amt7 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt7"))
                    objTr.ItemAdd_Org_Charge_Amt8 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt8"))
                    objTr.ItemAdd_Org_Charge_Amt9 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt9"))
                    objTr.ItemAdd_Org_Charge_Amt10 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt10"))
                    objTr.ItemAdd_Calc_Charge_Amt1 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt1"))
                    objTr.ItemAdd_Calc_Charge_Amt2 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt2"))
                    objTr.ItemAdd_Calc_Charge_Amt3 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt3"))
                    objTr.ItemAdd_Calc_Charge_Amt4 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt4"))
                    objTr.ItemAdd_Calc_Charge_Amt5 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt5"))
                    objTr.ItemAdd_Calc_Charge_Amt6 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt6"))
                    objTr.ItemAdd_Calc_Charge_Amt7 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt7"))
                    objTr.ItemAdd_Calc_Charge_Amt8 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt8"))
                    objTr.ItemAdd_Calc_Charge_Amt9 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt9"))
                    objTr.ItemAdd_Calc_Charge_Amt10 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt10"))
                    objTr.Total_ItemAdd_Charge = clsCommon.myCdbl(dr("Total_ItemAdd_Charge"))
                    ''=======================================================
                    objTr.Category = clsCommon.myCstr(dr("Category"))
                    objTr.Emergency = IIf(clsCommon.myCdbl(dr("Emergency")) = 1, True, False)
                    objTr.Capex_Code = clsCommon.myCstr(dr("Capex_Code"))
                    objTr.Capex_SubCode = clsCommon.myCstr(dr("Capex_SubCode"))
                    objTr.Against_Item_Wise_Tax_Rate = clsCommon.myCstr(dr("Against_Item_Wise_Tax_Rate"))

                    objTr.Insurance_Per = clsCommon.myCdbl(dr("Insurance_Per"))
                    objTr.Insurance_Base_Amt = clsCommon.myCdbl(dr("Insurance_Base_Amt"))

                    objTr.Item_Insurance_Base_Amt = clsCommon.myCdbl(dr("Item_Insurance_Base_Amt"))
                    objTr.Item_Insurance_Apply_On = clsCommon.myCstr(dr("Item_Insurance_Apply_On"))
                    objTr.Item_Insurance_Rate = clsCommon.myCdbl(dr("Item_Insurance_Rate"))
                    objTr.Item_Insurance_Amt = clsCommon.myCdbl(dr("Item_Insurance_Amt"))
                    objTr.Item_Amt_After_Insurance = clsCommon.myCdbl(dr("Item_Amt_After_Insurance"))

                    obj.Arr.Add(objTr)
                Next
            End If
            obj.objJVC = clsPJVHead.GetData(obj.PI_No, NavigatorType.Current, trans)
            obj.Arr_ACInsurance = clsPIAdditionChargeInsurance.GetData(obj.PI_No, trans)
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal arrLoc As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, "", "", trans, arrLoc)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal strAPInvNo As String, ByVal strAPInvVoucherNo As String, ByVal trans As SqlTransaction, ByVal arrLoc As String) As Boolean
        Dim isSaved As Boolean = True
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Purchase Invoice No not found to Post")
            End If
            ''Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

            Dim obj As clsPurchaseInvoiceHead = clsPurchaseInvoiceHead.GetData(strDocNo, NavigatorType.Current, trans, arrLoc)
            '' Dim strPostDate As String = clsCommon.GetPrintDate(obj.PI_Date, "dd/MM/yyyy")
            If (obj Is Nothing OrElse clsCommon.myLen(obj.PI_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Invoice", obj.Bill_To_Location, clsCommon.myCDate(obj.PI_Date), trans)

            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Purchase Invoice No " + obj.PI_No + " Is On Hold.Can't Post it")
            End If

            Dim qry As String = ""

            ''BHA/09/05/18-000008 by balwinder on 09/05/2018
            ''GKD/09/05/18-000129 by balwinder write query and give to rupesh on 09/05/2018 
            Dim dttemp As DataTable = clsDBFuncationality.GetDataTable("select Approve from TSPL_TRANSACTION_APPROVAL where Program_Code='" & clsUserMgtCode.mbtnPurchaseInvoice & "' and Document_No='" & obj.PI_No & "'", trans)
            If dttemp IsNot Nothing AndAlso dttemp.Rows.Count > 0 Then
                If clsCommon.myCdbl(dttemp.Rows(0)("Approve")) = 0 Then
                    Throw New Exception("Required Transaction Approval for Document No-" + obj.PI_No)
                End If
            End If
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_PI_HEAD", "PI_No", obj.PI_No, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If
            Dim isAgainstTender As Boolean = clsPurchaseOrderHead.AgainstTender(obj.Against_SRN, 2, trans)

            Dim isCreateDebitNotForReject As Boolean = False
            Dim isCreateDebitNotForShort As Boolean = False
            Dim isCreateDebitNotForDiscountDeduct As Boolean = False

            For Each objTr As clsPurchaseInvoiceDetail In obj.Arr
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
                        If Not isAgainstTender Then
                            isCreateDebitNotForShort = True
                        End If
                    End If
                    If objTr.Reject_Qty > 0 Then
                        If Not isAgainstTender Then
                            isCreateDebitNotForReject = True
                        End If
                    End If
                    If objTr.Disc_Type = 1 AndAlso objTr.Disc_Amt > 0 Then
                        isCreateDebitNotForDiscountDeduct = True
                    End If
                End If
            Next



            Dim objVendorInvHead As New clsVedorInvoiceHead()
            objVendorInvHead.Document_No = strAPInvNo
            objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.PI_Date, "dd/MM/yyyy")
            objVendorInvHead.Vendor_Code = obj.Vendor_Code
            objVendorInvHead.Vendor_Name = obj.Vendor_Name
            objVendorInvHead.Vendor_Invoice_No = obj.Vendor_Invoice_No
            objVendorInvHead.Invoice_Type = "AP"
            If obj.Invdate Is Nothing Then ''changed by Panchraj on 16/02/2016 login Balwinder
                objVendorInvHead.Vendor_Invoice_Date = obj.PI_Date
            Else
                objVendorInvHead.Vendor_Invoice_Date = obj.Invdate
            End If

            '' added by parteek 17/08/2017
            objVendorInvHead.ITC_Elibible = IIf(obj.ITC_Elibible = 1, 1, 0)
            objVendorInvHead.ITC_Type = clsCommon.myCdbl(obj.ITC_Type)
            objVendorInvHead.ITC_Type_Category = clsCommon.myCstr(obj.ITC_Type_Category)
            objVendorInvHead.TapalNo = obj.TapalNo
            If clsCommon.myLen(obj.DateAndTime) > 0 Then
                objVendorInvHead.DateAndTime = obj.DateAndTime
            End If
            ''---end

            objVendorInvHead.loc_code = obj.loc_code
            objVendorInvHead.PROJECT_ID = obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "I" ''For Purchase Invoice Type
            objVendorInvHead.TDS_Provision = obj.TDS_Provision ''For Purchase Invoice Type

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
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX1, trans) Then
                    objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                    objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
                objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            End If
            If (clsCommon.myLen(obj.TAX2) > 0) Then
                objVendorInvHead.TAX2 = obj.TAX2
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX2, trans) Then
                    objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                    objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
                objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            End If
            If (clsCommon.myLen(obj.TAX3) > 0) Then
                objVendorInvHead.TAX3 = obj.TAX3
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX3, trans) Then
                    objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                    objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
                objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            End If
            If (clsCommon.myLen(obj.TAX4) > 0) Then
                objVendorInvHead.TAX4 = obj.TAX4
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX4, trans) Then
                    objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                    objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
                objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            End If
            If (clsCommon.myLen(obj.TAX5) > 0) Then
                objVendorInvHead.TAX5 = obj.TAX5
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX5, trans) Then
                    objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                    objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.Bill_To_Location, trans)

                End If
                objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
                objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            End If
            If (clsCommon.myLen(obj.TAX6) > 0) Then
                objVendorInvHead.TAX6 = obj.TAX6
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX6, trans) Then
                    objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                    objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
                objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            End If
            If (clsCommon.myLen(obj.TAX7) > 0) Then
                objVendorInvHead.TAX7 = obj.TAX7
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX7, trans) Then
                    objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                    objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.Bill_To_Location, trans)

                End If
                objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
                objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            End If
            If (clsCommon.myLen(obj.TAX8) > 0) Then
                objVendorInvHead.TAX8 = obj.TAX8
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX8, trans) Then
                    objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                    objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
                objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            End If
            If (clsCommon.myLen(obj.TAX9) > 0) Then
                objVendorInvHead.TAX9 = obj.TAX9
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX9, trans) Then
                    objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                    objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.Bill_To_Location, trans)
                End If
                objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
                objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            End If
            If (clsCommon.myLen(obj.TAX10) > 0) Then
                objVendorInvHead.TAX10 = obj.TAX10
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX10, trans) Then
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
            objVendorInvHead.RoundOffAmount = obj.RoundOffAmt
            objVendorInvHead.Document_Total = obj.PI_Total_Amt
            objVendorInvHead.Balance_Amt = obj.PI_Total_Amt
            objVendorInvHead.Against_POInvoice_No = obj.PI_No
            objVendorInvHead.RoundOffAmount = obj.RoundOffAmt
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
            ''===============21/12/2016 Parteek Excel Format point=====
            objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10

            objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge
            ''===================End=================================

            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            Dim strFirstItemCode As String = GetFirstItemCode(obj.Arr)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0
            For Each objPIDetail As clsPurchaseInvoiceDetail In obj.Arr
                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                Dim strICode As String = objPIDetail.Item_Code
                If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                    strICode = strFirstItemCode
                End If

                ''Fill VendorInvoice details Data
                qry = "select TSPL_PURCHASE_ACCOUNTS.Purchase_JobWork,TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
                End If
                ''Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                ''    Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
                ''End If
                Dim strPaybleCleanigCtrlAC As String = ""
                If obj.isJobWorkOutward = 1 Then
                    If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Purchase_JobWork"))) = 0 Then
                        Throw New Exception("Please set Purchase Job Work Account for Account set " + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")))
                    End If
                    strPaybleCleanigCtrlAC = clsCommon.myCstr(dt.Rows(0)("Purchase_JobWork"))
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Bill_To_Location, trans)
                Else
                    strPaybleCleanigCtrlAC = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Bill_To_Location, trans)
                    objVendorInvDetail.Comments = "Y"
                End If

                Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
                    objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.Bill_To_Location, trans)
                End If


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
                If Not obj.Is_Shortage_Include_In_Landed_Cost Then
                    objVendorInvDetail.Landed_Amount += objPIDetail.Shortage_Amount
                End If

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

            objVendorInvHead.GSTRegistered = obj.GSTRegistered

            isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans, strAPInvVoucherNo)
            '============Updated By Rohit on March 24,2015 (BM00000005987)
            'isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.PI_Date)
            isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.PI_Date, strAPInvVoucherNo, False)
            '======================================================================
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
                objVendorInvHead.TapalNo = obj.TapalNo
                If clsCommon.myLen(obj.DateAndTime) > 0 Then
                    objVendorInvHead.DateAndTime = obj.DateAndTime
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
                For Each objPIDetail As clsPurchaseInvoiceDetail In obj.Arr
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

                objVendorInvHead.GSTRegistered = obj.GSTRegistered

                isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, clsCommon.myCDate(obj.PI_Date))
                ''End of create debit note for shortage
            End If

            Dim straddqtyuomanditemwithdescription As String = String.Empty
            Dim strQtyUOM As String = String.Empty
            Dim strItem As String = String.Empty

            If isCreateDebitNotForShort AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateDbitNoteForShortPI, clsFixedParameterCode.CreateDbitNoteForShortPI, trans)) = 1 Then
                Dim objPurRet As New clsPurchasReturnHead()
                'objPurRetPurRet.PR_No = txtDocNo.Value
                objPurRet.PR_Date = clsCommon.myCDate(obj.PI_Date, "dd/MM/yyyy")
                objPurRet.Vendor_Code = obj.Vendor_Code
                objPurRet.Vendor_Name = obj.Vendor_Name
                objPurRet.Ref_No = obj.Vendor_Name
                objPurRet.Total_Tax_Amt = 0
                objPurRet.Remarks = obj.Remarks
                objPurRet.Bill_To_Location = obj.Bill_To_Location
                objPurRet.Ship_To_Location = obj.Ship_To_Location
                objPurRet.Comments = obj.Comments
                objPurRet.On_Hold = False
                objPurRet.GSTRegistered = obj.GSTRegistered
                ' objPurRet.Description = "Auto Generated Debit Note against Invoice no- " + obj.PI_No + " due to shoratage qty."
                objPurRet.Vendor_Invoice_No = obj.Vendor_Invoice_No
                objPurRet.Tax_Group = obj.Tax_Group
                objPurRet.Item_Type = obj.Item_Type
                objPurRet.is_Reject_Item = False
                objPurRet.Against_PI = obj.PI_No
                objPurRet.Auto_Gen_Againnt_PI_No = obj.PI_No
                objPurRet.Project_Id = obj.PROJECT_ID
                If clsCommon.myLen(objPurRet.Against_PI) > 0 Then
                    objPurRet.Against_SRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_SRN FROM TSPL_PI_HEAD WHERE PI_No='" + objPurRet.Against_PI + "'", trans))
                End If
                If clsCommon.myLen(objPurRet.Against_SRN) > 0 Then
                    objPurRet.Against_MRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_MRN FROM TSPL_SRN_HEAD WHERE SRN_No='" + objPurRet.Against_SRN + "'", trans))
                End If
                If clsCommon.myLen(objPurRet.Against_MRN) > 0 Then
                    objPurRet.Against_GRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_GRN FROM TSPL_MRN_HEAD WHERE MRN_No='" + objPurRet.Against_MRN + "'", trans))
                End If
                If clsCommon.myLen(objPurRet.Against_GRN) > 0 Then
                    objPurRet.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_PO FROM TSPL_GRN_HEAD WHERE GRN_No='" + objPurRet.Against_GRN + "'", trans))
                End If
                If clsCommon.myLen(objPurRet.Against_PO) > 0 Then
                    objPurRet.Against_Requisition = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + objPurRet.Against_PO + "'", trans))
                End If
                objPurRet.AssessableAmt = 0
                If clsCommon.myLen(obj.TAX1) > 0 Then
                    objPurRet.TAX1 = obj.TAX1
                    objPurRet.TAX1_Rate = obj.TAX1_Rate
                    objPurRet.TAX1_Base_Amt = 0
                    objPurRet.TAX1_Amt = 0
                End If
                If clsCommon.myLen(obj.TAX2) > 0 Then
                    objPurRet.TAX2 = obj.TAX2
                    objPurRet.TAX2_Rate = obj.TAX1_Rate
                    objPurRet.TAX2_Base_Amt = 0
                    objPurRet.TAX2_Amt = 0
                End If
                If clsCommon.myLen(obj.TAX3) > 0 Then
                    objPurRet.TAX3 = obj.TAX3
                    objPurRet.TAX3_Rate = obj.TAX3_Rate
                    objPurRet.TAX3_Base_Amt = 0
                    objPurRet.TAX3_Amt = 0
                End If
                If clsCommon.myLen(obj.TAX4) > 0 Then
                    objPurRet.TAX4 = obj.TAX4
                    objPurRet.TAX4_Rate = obj.TAX4_Rate
                    objPurRet.TAX4_Base_Amt = 0
                    objPurRet.TAX4_Amt = 0
                End If
                If clsCommon.myLen(obj.TAX5) > 0 Then
                    objPurRet.TAX5 = obj.TAX5
                    objPurRet.TAX5_Rate = obj.TAX5_Rate
                    objPurRet.TAX5_Base_Amt = 0
                    objPurRet.TAX5_Amt = 0
                End If
                If clsCommon.myLen(obj.TAX6) > 0 Then
                    objPurRet.TAX6 = obj.TAX6
                    objPurRet.TAX6_Rate = obj.TAX6_Rate
                    objPurRet.TAX6_Base_Amt = 0
                    objPurRet.TAX6_Amt = 0
                End If
                If clsCommon.myLen(obj.TAX7) > 0 Then
                    objPurRet.TAX7 = obj.TAX7
                    objPurRet.TAX7_Rate = obj.TAX7_Rate
                    objPurRet.TAX7_Base_Amt = 0
                    objPurRet.TAX7_Amt = 0
                End If
                If clsCommon.myLen(obj.TAX8) > 0 Then
                    objPurRet.TAX8 = obj.TAX8
                    objPurRet.TAX8_Rate = obj.TAX8_Rate
                    objPurRet.TAX8_Base_Amt = 0
                    objPurRet.TAX8_Amt = 0
                End If
                If clsCommon.myLen(obj.TAX9) > 0 Then
                    objPurRet.TAX9 = obj.TAX9
                    objPurRet.TAX9_Rate = obj.TAX9_Rate
                    objPurRet.TAX9_Base_Amt = 0
                    objPurRet.TAX9_Amt = 0
                End If
                If clsCommon.myLen(obj.TAX10) > 0 Then
                    objPurRet.TAX10 = obj.TAX10
                    objPurRet.TAX10_Rate = obj.TAX10_Rate
                    objPurRet.TAX10_Base_Amt = 0
                    objPurRet.TAX10_Amt = 0
                End If
                objPurRet.Tax_Calculation_Type = obj.Tax_Calculation_Type

                objPurRet.Terms_Code = obj.Terms_Code
                objPurRet.Due_Date = obj.Due_Date
                objPurRet.Carrier = obj.Carrier
                objPurRet.VehicleNo = obj.VehicleNo
                objPurRet.GRNo = obj.GRNo
                objPurRet.GENo = obj.GENo
                If obj.GEDate IsNot Nothing Then
                    objPurRet.GEDate = obj.GEDate
                End If

                objPurRet.Total_Add_Charge = 0
                objPurRet.is_Excise_On_Qty = obj.is_Excise_On_Qty

                'objPurRet.Discount_Base = clsCommon.myCdbl(lblAmtWithDiscount.Text)
                'objPurRet.Discount_Amt = clsCommon.myCdbl(lblDiscountAmt.Text)
                'objPurRet.Amount_Less_Discount = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
                'objPurRet.PR_Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)

                objPurRet.Discount_Base = 0
                objPurRet.Discount_Amt = 0
                objPurRet.Amount_Less_Discount = 0
                objPurRet.Total_Tax_Amt = 0
                objPurRet.PR_Total_Amt = 0

                objPurRet.Arr = New List(Of clsPurchasReturnDetail)
                Dim RowNo As Integer = 1
                Dim dclMiscChargesShortAmount As Decimal = 0
                Dim dclratio As Decimal
                Dim objPurRetTr As New clsPurchasReturnDetail()
                For Each objPIDetail As clsPurchaseInvoiceDetail In obj.Arr
                    If (objPIDetail.PI_Qty + objPIDetail.Leak_Qty + objPIDetail.Short_Qty + objPIDetail.Burst_Qty + objPIDetail.Reject_Qty) = 0 Then
                        Continue For
                    End If
                    dclratio = (objPIDetail.Short_Qty) / (objPIDetail.PI_Qty + objPIDetail.Leak_Qty + objPIDetail.Short_Qty + objPIDetail.Burst_Qty + objPIDetail.Reject_Qty)
                    If dclratio = 0 Then
                        Continue For
                    End If
                    If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        Continue For
                    End If

                    objPurRetTr = New clsPurchasReturnDetail()
                    objPurRetTr.Line_No = RowNo
                    RowNo += 1
                    objPurRetTr.Row_Type = objPIDetail.Row_Type
                    objPurRetTr.Item_Code = objPIDetail.Item_Code
                    objPurRetTr.Item_Desc = objPIDetail.Item_Desc
                    objPurRetTr.PR_Qty = objPIDetail.Short_Qty
                    ''objPurRetTr.Rejected_Qty = clsCommon.myCdbl(grow.Cells(colRejectedQty).Value)
                    objPurRetTr.Balance_Qty = objPIDetail.Short_Qty
                    objPurRetTr.Unit_code = objPIDetail.Unit_code
                    objPurRetTr.PI_Id = objPIDetail.PI_No

                    ''richa agarwal 3 Apr,2017
                    straddqtyuomanditemwithdescription += clsCommon.myCstr(objPIDetail.Short_Qty) + " " + clsCommon.myCstr(objPIDetail.Unit_code) + " of " + clsCommon.myCstr(objPIDetail.Item_Code) + ", "
                    strQtyUOM += clsCommon.myCstr(objPIDetail.Short_Qty) + " " + clsCommon.myCstr(objPIDetail.Unit_code) + ", "
                    strItem += clsCommon.myCstr(objPIDetail.Item_Code) + ", "
                    ''--------------

                    objPurRetTr.PO_ID = objPIDetail.PO_ID
                    objPurRetTr.GRN_ID = objPIDetail.GRN_ID
                    objPurRetTr.MRN_ID = objPIDetail.MRN_ID
                    objPurRetTr.SRN_Id = objPIDetail.SRN_Id


                    'objPurRetTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
                    objPurRetTr.Item_Cost = objPIDetail.Item_Cost
                    objPurRetTr.Amount = objPIDetail.Amount * dclratio
                    objPurRetTr.Disc_Per = objPIDetail.Disc_Per
                    objPurRetTr.Disc_Amt = objPIDetail.Disc_Amt * dclratio
                    objPurRetTr.Amt_Less_Discount = objPIDetail.Amt_Less_Discount * dclratio

                    objPurRetTr.TAX1 = objPIDetail.TAX1
                    objPurRetTr.TAX1_Base_Amt = objPIDetail.TAX1_Base_Amt * dclratio
                    objPurRetTr.TAX1_Rate = objPIDetail.TAX1_Rate
                    objPurRetTr.TAX1_Amt = objPIDetail.TAX1_Amt * dclratio

                    objPurRetTr.TAX2 = objPIDetail.TAX2
                    objPurRetTr.TAX2_Base_Amt = objPIDetail.TAX2_Base_Amt * dclratio
                    objPurRetTr.TAX2_Rate = objPIDetail.TAX2_Rate
                    objPurRetTr.TAX2_Amt = objPIDetail.TAX2_Amt * dclratio

                    objPurRetTr.TAX3 = objPIDetail.TAX3
                    objPurRetTr.TAX3_Base_Amt = objPIDetail.TAX3_Base_Amt * dclratio
                    objPurRetTr.TAX3_Rate = objPIDetail.TAX3_Rate
                    objPurRetTr.TAX3_Amt = objPIDetail.TAX3_Amt * dclratio

                    objPurRetTr.TAX4 = objPIDetail.TAX4
                    objPurRetTr.TAX4_Base_Amt = objPIDetail.TAX4_Base_Amt * dclratio
                    objPurRetTr.TAX4_Rate = objPIDetail.TAX4_Rate
                    objPurRetTr.TAX4_Amt = objPIDetail.TAX4_Amt * dclratio

                    objPurRetTr.TAX5 = objPIDetail.TAX5
                    objPurRetTr.TAX5_Base_Amt = objPIDetail.TAX5_Base_Amt * dclratio
                    objPurRetTr.TAX5_Rate = objPIDetail.TAX5_Rate
                    objPurRetTr.TAX5_Amt = objPIDetail.TAX5_Amt * dclratio

                    objPurRetTr.TAX6 = objPIDetail.TAX6
                    objPurRetTr.TAX6_Base_Amt = objPIDetail.TAX6_Base_Amt * dclratio
                    objPurRetTr.TAX6_Rate = objPIDetail.TAX6_Rate
                    objPurRetTr.TAX6_Amt = objPIDetail.TAX6_Amt * dclratio

                    objPurRetTr.TAX7 = objPIDetail.TAX7
                    objPurRetTr.TAX7_Base_Amt = objPIDetail.TAX7_Base_Amt * dclratio
                    objPurRetTr.TAX7_Rate = objPIDetail.TAX7_Rate
                    objPurRetTr.TAX7_Amt = objPIDetail.TAX7_Amt * dclratio

                    objPurRetTr.TAX8 = objPIDetail.TAX8
                    objPurRetTr.TAX8_Base_Amt = objPIDetail.TAX8_Base_Amt * dclratio
                    objPurRetTr.TAX8_Rate = objPIDetail.TAX8_Rate
                    objPurRetTr.TAX8_Amt = objPIDetail.TAX8_Amt * dclratio

                    objPurRetTr.TAX9 = objPIDetail.TAX9
                    objPurRetTr.TAX9_Base_Amt = objPIDetail.TAX9_Base_Amt * dclratio
                    objPurRetTr.TAX9_Rate = objPIDetail.TAX9_Rate
                    objPurRetTr.TAX9_Amt = objPIDetail.TAX9_Amt * dclratio

                    objPurRetTr.TAX10 = objPIDetail.TAX10
                    objPurRetTr.TAX10_Base_Amt = objPIDetail.TAX10_Base_Amt * dclratio
                    objPurRetTr.TAX10_Rate = objPIDetail.TAX10_Rate
                    objPurRetTr.TAX10_Amt = objPIDetail.TAX10_Amt * dclratio


                    objPurRetTr.Total_Tax_Amt = objPIDetail.Total_Tax_Amt * dclratio
                    objPurRetTr.Taxable_Amount_Per = objPIDetail.Taxable_Amount_Per
                    objPurRetTr.Taxable_Amount = objPIDetail.Taxable_Amount * dclratio
                    objPurRetTr.Item_Net_Amt = objPIDetail.Item_Net_Amt * dclratio
                    objPurRetTr.Location = objPIDetail.Location
                    objPurRetTr.MRP = objPIDetail.MRP

                    objPurRet.TAX1_Base_Amt += objPurRetTr.TAX1_Base_Amt
                    objPurRet.TAX1_Amt += objPurRetTr.TAX1_Amt
                    objPurRet.TAX2_Base_Amt += objPurRetTr.TAX2_Base_Amt
                    objPurRet.TAX2_Amt += objPurRetTr.TAX2_Amt
                    objPurRet.TAX3_Base_Amt += objPurRetTr.TAX3_Base_Amt
                    objPurRet.TAX3_Amt += objPurRetTr.TAX3_Amt
                    objPurRet.TAX4_Base_Amt += objPurRetTr.TAX4_Base_Amt
                    objPurRet.TAX4_Amt += objPurRetTr.TAX4_Amt
                    objPurRet.TAX5_Base_Amt += objPurRetTr.TAX5_Base_Amt
                    objPurRet.TAX5_Amt += objPurRetTr.TAX5_Amt
                    objPurRet.TAX6_Base_Amt += objPurRetTr.TAX6_Base_Amt
                    objPurRet.TAX6_Amt += objPurRetTr.TAX6_Amt
                    objPurRet.TAX7_Base_Amt += objPurRetTr.TAX7_Base_Amt
                    objPurRet.TAX7_Amt += objPurRetTr.TAX7_Amt
                    objPurRet.TAX8_Base_Amt += objPurRetTr.TAX8_Base_Amt
                    objPurRet.TAX8_Amt += objPurRetTr.TAX8_Amt
                    objPurRet.TAX9_Base_Amt += objPurRetTr.TAX9_Base_Amt
                    objPurRet.TAX9_Amt += objPurRetTr.TAX9_Amt
                    objPurRet.TAX10_Base_Amt += objPurRetTr.TAX10_Base_Amt
                    objPurRet.TAX10_Amt += objPurRetTr.TAX10_Amt

                    objPurRet.Discount_Base += objPurRetTr.Amount
                    objPurRet.Discount_Amt += objPurRetTr.Disc_Amt
                    objPurRet.Amount_Less_Discount += objPurRetTr.Amt_Less_Discount
                    objPurRet.Total_Tax_Amt += (objPurRetTr.TAX1_Amt + objPurRetTr.TAX2_Amt + objPurRetTr.TAX3_Amt + objPurRetTr.TAX4_Amt + objPurRetTr.TAX5_Amt + objPurRetTr.TAX6_Amt + objPurRetTr.TAX7_Amt + objPurRetTr.TAX8_Amt + objPurRetTr.TAX9_Amt + objPurRetTr.TAX10_Amt) ' tax1 amount add double times
                    objPurRet.PR_Total_Amt += objPurRetTr.Item_Net_Amt


                    objPurRetTr.AssessableAmt = objPIDetail.AssessableAmt * dclratio
                    objPurRetTr.Batch_No = objPIDetail.Batch_No
                    objPurRetTr.Bin_No = objPIDetail.Bin_No
                    If objPIDetail.Expiry_Date IsNot Nothing Then
                        objPurRetTr.Expiry_Date = objPIDetail.Expiry_Date
                    End If
                    If objPIDetail.MFG_Date IsNot Nothing Then
                        objPurRetTr.MFG_Date = objPIDetail.MFG_Date
                    End If
                    objPurRetTr.Specification = objPIDetail.Specification
                    objPurRetTr.Remarks = objPIDetail.Remarks

                    objPurRetTr.Landed_Cost_Amount = objPIDetail.Shortage_Amount
                    objPurRetTr.Landed_Cost_Rate = objPIDetail.Shortage_Amount / objPurRetTr.PR_Qty

                    Dim dclCurrMiscChargesShortAmount As Decimal = objPIDetail.Shortage_Amount - objPurRetTr.Item_Net_Amt
                    If dclCurrMiscChargesShortAmount < 0 Then
                        ''If Tax is revocerable then it will come -ve so use amt after discount
                        dclCurrMiscChargesShortAmount = objPIDetail.Shortage_Amount - objPurRetTr.Amt_Less_Discount

                        ''Add Tax Amount
                        Dim TaxPer As Decimal = objPurRetTr.Total_Tax_Amt * 100 / objPurRetTr.Taxable_Amount
                        dclCurrMiscChargesShortAmount = dclCurrMiscChargesShortAmount * (100 + TaxPer) / 100

                    End If

                    dclMiscChargesShortAmount += dclCurrMiscChargesShortAmount


                    objPurRetTr.Total_AddtionalCost_PerUnit = objPIDetail.Total_AddtionalCost_PerUnit * dclratio
                    objPurRetTr.Total_NonRecTax_PerUnit = objPIDetail.Total_NonRecTax_PerUnit * dclratio
                    objPurRetTr.Total_RecTax_PerUnit = objPIDetail.Total_RecTax_PerUnit * dclratio


                    If (clsCommon.myLen(objPurRetTr.Item_Code) > 0) Then
                        objPurRet.Arr.Add(objPurRetTr)
                    End If
                Next

                ''richa agarwal 3 Apr,2017
                If clsCommon.myLen(straddqtyuomanditemwithdescription) > 0 Then
                    straddqtyuomanditemwithdescription = straddqtyuomanditemwithdescription.Substring(0, straddqtyuomanditemwithdescription.Length() - 2)
                End If
                If clsCommon.myLen(strQtyUOM) > 0 Then
                    strQtyUOM = strQtyUOM.Substring(0, strQtyUOM.Length() - 2)
                End If
                If clsCommon.myLen(strItem) > 0 Then
                    strItem = strItem.Substring(0, strItem.Length() - 2)
                End If
                'objPurRet.Description = "Auto Generated Debit Note against Invoice no- " + obj.PI_No + " due to " + straddqtyuomanditemwithdescription + " shortage qty."
                objPurRet.Description = "Auto Generated Debit Note against Invoice no- " + obj.PI_No + " due to " + strQtyUOM + " shortage qty of " + strItem + "."
                ''-----------------

                If (objPurRet.Arr Is Nothing OrElse objPurRet.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                If obj.Amount_Less_Discount > 0 AndAlso obj.Is_Shortage_Include_In_Landed_Cost Then
                    objPurRet.Total_Add_Charge = obj.Total_Add_Charge * objPurRet.Amount_Less_Discount / obj.Amount_Less_Discount
                    If objPurRet.Total_Add_Charge <> 0 Then
                        objPurRet.PR_Total_Amt += objPurRet.Total_Add_Charge

                        objPurRet.Add_Charge_Code1 = obj.Add_Charge_Code1
                        objPurRet.Add_Charge_Code2 = obj.Add_Charge_Code2
                        objPurRet.Add_Charge_Code3 = obj.Add_Charge_Code3
                        objPurRet.Add_Charge_Code4 = obj.Add_Charge_Code4
                        objPurRet.Add_Charge_Code5 = obj.Add_Charge_Code5
                        objPurRet.Add_Charge_Code6 = obj.Add_Charge_Code6
                        objPurRet.Add_Charge_Code7 = obj.Add_Charge_Code7
                        objPurRet.Add_Charge_Code8 = obj.Add_Charge_Code8
                        objPurRet.Add_Charge_Code9 = obj.Add_Charge_Code9
                        objPurRet.Add_Charge_Code10 = obj.Add_Charge_Code10

                        objPurRet.Add_Charge_Amt1 = objPurRet.Total_Add_Charge * obj.Add_Charge_Amt1 / obj.Total_Add_Charge
                        objPurRet.Add_Charge_Amt2 = objPurRet.Total_Add_Charge * obj.Add_Charge_Amt2 / obj.Total_Add_Charge
                        objPurRet.Add_Charge_Amt3 = objPurRet.Total_Add_Charge * obj.Add_Charge_Amt3 / obj.Total_Add_Charge
                        objPurRet.Add_Charge_Amt4 = objPurRet.Total_Add_Charge * obj.Add_Charge_Amt4 / obj.Total_Add_Charge
                        objPurRet.Add_Charge_Amt5 = objPurRet.Total_Add_Charge * obj.Add_Charge_Amt5 / obj.Total_Add_Charge
                        objPurRet.Add_Charge_Amt6 = objPurRet.Total_Add_Charge * obj.Add_Charge_Amt6 / obj.Total_Add_Charge
                        objPurRet.Add_Charge_Amt7 = objPurRet.Total_Add_Charge * obj.Add_Charge_Amt7 / obj.Total_Add_Charge
                        objPurRet.Add_Charge_Amt8 = objPurRet.Total_Add_Charge * obj.Add_Charge_Amt8 / obj.Total_Add_Charge
                        objPurRet.Add_Charge_Amt9 = objPurRet.Total_Add_Charge * obj.Add_Charge_Amt9 / obj.Total_Add_Charge
                        objPurRet.Add_Charge_Amt10 = objPurRet.Total_Add_Charge * obj.Add_Charge_Amt10 / obj.Total_Add_Charge
                    End If
                End If

                ''For handle addition charges
                dclMiscChargesShortAmount = dclMiscChargesShortAmount - objPurRet.Total_Add_Charge
                If dclMiscChargesShortAmount > 0 Then
                    Dim dclTotalMisCharAmount As Decimal = 0
                    For Each objPIDetail As clsPurchaseInvoiceDetail In obj.Arr
                        If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                            dclTotalMisCharAmount += objPIDetail.Item_Net_Amt
                        End If
                    Next
                    If dclTotalMisCharAmount > 0 Then
                        For Each objPIDetail As clsPurchaseInvoiceDetail In obj.Arr
                            If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                                dclratio = objPIDetail.Item_Net_Amt / dclTotalMisCharAmount
                                If dclratio = 0 Then
                                    Continue For
                                End If
                                Dim dclMiscChargesShortAmountToDistribute As Decimal = dclMiscChargesShortAmount * dclratio

                                RowNo += 1
                                objPurRetTr = New clsPurchasReturnDetail()
                                objPurRetTr.Line_No = RowNo
                                objPurRetTr.Row_Type = objPIDetail.Row_Type
                                objPurRetTr.Item_Code = objPIDetail.Item_Code
                                objPurRetTr.Item_Desc = objPIDetail.Item_Desc
                                objPurRetTr.PI_Id = objPIDetail.PI_No
                                objPurRetTr.PO_ID = objPIDetail.PO_ID
                                objPurRetTr.GRN_ID = objPIDetail.GRN_ID
                                objPurRetTr.MRN_ID = objPIDetail.MRN_ID
                                objPurRetTr.SRN_Id = objPIDetail.SRN_Id

                                objPurRetTr.Amount = dclMiscChargesShortAmountToDistribute * objPIDetail.Amount / objPIDetail.Item_Net_Amt
                                objPurRetTr.Disc_Per = objPIDetail.Disc_Per
                                objPurRetTr.Disc_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.Disc_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.Amt_Less_Discount = dclMiscChargesShortAmountToDistribute * objPIDetail.Amt_Less_Discount / objPIDetail.Item_Net_Amt

                                objPurRetTr.Taxable_Amount = dclMiscChargesShortAmountToDistribute * objPIDetail.Taxable_Amount / objPIDetail.Item_Net_Amt
                                objPurRetTr.Taxable_Amount_Per = objPIDetail.Taxable_Amount_Per

                                objPurRetTr.TAX1 = objPIDetail.TAX1
                                objPurRetTr.TAX1_Base_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX1_Base_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.TAX1_Rate = objPIDetail.TAX1_Rate
                                objPurRetTr.TAX1_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX1_Amt / objPIDetail.Item_Net_Amt

                                objPurRetTr.TAX2 = objPIDetail.TAX2
                                objPurRetTr.TAX2_Base_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX2_Base_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.TAX2_Rate = objPIDetail.TAX2_Rate
                                objPurRetTr.TAX2_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX2_Amt / objPIDetail.Item_Net_Amt

                                objPurRetTr.TAX3 = objPIDetail.TAX3
                                objPurRetTr.TAX3_Base_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX3_Base_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.TAX3_Rate = objPIDetail.TAX3_Rate
                                objPurRetTr.TAX3_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX3_Amt / objPIDetail.Item_Net_Amt

                                objPurRetTr.TAX4 = objPIDetail.TAX4
                                objPurRetTr.TAX4_Base_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX4_Base_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.TAX4_Rate = objPIDetail.TAX4_Rate
                                objPurRetTr.TAX4_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX4_Amt / objPIDetail.Item_Net_Amt

                                objPurRetTr.TAX5 = objPIDetail.TAX5
                                objPurRetTr.TAX5_Base_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX5_Base_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.TAX5_Rate = objPIDetail.TAX5_Rate
                                objPurRetTr.TAX5_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX5_Amt / objPIDetail.Item_Net_Amt

                                objPurRetTr.TAX6 = objPIDetail.TAX6
                                objPurRetTr.TAX6_Base_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX6_Base_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.TAX6_Rate = objPIDetail.TAX6_Rate
                                objPurRetTr.TAX6_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX6_Amt / objPIDetail.Item_Net_Amt

                                objPurRetTr.TAX7 = objPIDetail.TAX7
                                objPurRetTr.TAX7_Base_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX7_Base_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.TAX7_Rate = objPIDetail.TAX7_Rate
                                objPurRetTr.TAX7_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX7_Amt / objPIDetail.Item_Net_Amt

                                objPurRetTr.TAX8 = objPIDetail.TAX8
                                objPurRetTr.TAX8_Base_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX8_Base_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.TAX8_Rate = objPIDetail.TAX8_Rate
                                objPurRetTr.TAX8_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX8_Amt / objPIDetail.Item_Net_Amt

                                objPurRetTr.TAX9 = objPIDetail.TAX9
                                objPurRetTr.TAX9_Base_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX9_Base_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.TAX9_Rate = objPIDetail.TAX9_Rate
                                objPurRetTr.TAX9_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX9_Amt / objPIDetail.Item_Net_Amt

                                objPurRetTr.TAX10 = objPIDetail.TAX10
                                objPurRetTr.TAX10_Base_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX10_Base_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.TAX10_Rate = objPIDetail.TAX10_Rate
                                objPurRetTr.TAX10_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.TAX10_Amt / objPIDetail.Item_Net_Amt

                                objPurRetTr.Total_Tax_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.Total_Tax_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.Item_Net_Amt = dclMiscChargesShortAmountToDistribute * objPIDetail.Item_Net_Amt / objPIDetail.Item_Net_Amt
                                objPurRetTr.Location = objPIDetail.Location
                                objPurRet.TAX1_Base_Amt += objPurRetTr.TAX1_Base_Amt
                                objPurRet.TAX1_Amt += objPurRetTr.TAX1_Amt
                                objPurRet.TAX2_Base_Amt += objPurRetTr.TAX2_Base_Amt
                                objPurRet.TAX2_Amt += objPurRetTr.TAX2_Amt
                                objPurRet.TAX3_Base_Amt += objPurRetTr.TAX3_Base_Amt
                                objPurRet.TAX3_Amt += objPurRetTr.TAX3_Amt
                                objPurRet.TAX4_Base_Amt += objPurRetTr.TAX4_Base_Amt
                                objPurRet.TAX4_Amt += objPurRetTr.TAX4_Amt
                                objPurRet.TAX5_Base_Amt += objPurRetTr.TAX5_Base_Amt
                                objPurRet.TAX5_Amt += objPurRetTr.TAX5_Amt
                                objPurRet.TAX6_Base_Amt += objPurRetTr.TAX6_Base_Amt
                                objPurRet.TAX6_Amt += objPurRetTr.TAX6_Amt
                                objPurRet.TAX7_Base_Amt += objPurRetTr.TAX7_Base_Amt
                                objPurRet.TAX7_Amt += objPurRetTr.TAX7_Amt
                                objPurRet.TAX8_Base_Amt += objPurRetTr.TAX8_Base_Amt
                                objPurRet.TAX8_Amt += objPurRetTr.TAX8_Amt
                                objPurRet.TAX9_Base_Amt += objPurRetTr.TAX9_Base_Amt
                                objPurRet.TAX9_Amt += objPurRetTr.TAX9_Amt
                                objPurRet.TAX10_Base_Amt += objPurRetTr.TAX10_Base_Amt
                                objPurRet.TAX10_Amt += objPurRetTr.TAX10_Amt

                                objPurRet.Discount_Base += objPurRetTr.Amount
                                objPurRet.Discount_Amt += objPurRetTr.Disc_Amt
                                objPurRet.Amount_Less_Discount += objPurRetTr.Amt_Less_Discount
                                objPurRet.Total_Tax_Amt += (objPurRetTr.TAX1_Amt + objPurRetTr.TAX2_Amt + objPurRetTr.TAX3_Amt + objPurRetTr.TAX4_Amt + objPurRetTr.TAX5_Amt + objPurRetTr.TAX6_Amt + objPurRetTr.TAX7_Amt + objPurRetTr.TAX8_Amt + objPurRetTr.TAX9_Amt + objPurRetTr.TAX10_Amt) ' tax1 amount add double times
                                objPurRet.PR_Total_Amt += objPurRetTr.Item_Net_Amt
                                objPurRetTr.AssessableAmt = dclMiscChargesShortAmountToDistribute * objPIDetail.AssessableAmt / objPIDetail.Item_Net_Amt
                                objPurRetTr.Batch_No = objPIDetail.Batch_No
                                objPurRetTr.Bin_No = objPIDetail.Bin_No
                                If objPIDetail.Expiry_Date IsNot Nothing Then
                                    objPurRetTr.Expiry_Date = objPIDetail.Expiry_Date
                                End If
                                If objPIDetail.MFG_Date IsNot Nothing Then
                                    objPurRetTr.MFG_Date = objPIDetail.MFG_Date
                                End If
                                objPurRetTr.Specification = objPIDetail.Specification
                                objPurRetTr.Remarks = objPIDetail.Remarks
                                If (clsCommon.myLen(objPurRetTr.Item_Code) > 0) Then
                                    objPurRet.Arr.Add(objPurRetTr)
                                End If
                            End If
                        Next
                    End If
                End If
                ''End of For handle addition charges




                '' CurrencConversion
                objPurRet.CURRENCY_CODE = obj.CURRENCY_CODE
                objPurRet.ConvRate = obj.ConvRate

                'If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
                '    objPurRet.CURRENCY_CODE = Me.txtCurrencyCode.Value
                '    objPurRet.ConvRate = clsCommon.myCdbl(Me.txtConversionRate.Text)
                '    If clsCommon.myLen(txtApplicableFrom.Text) > 0 Then
                '        objPurRet.ApplicableFrom = Me.txtApplicableFrom.Text
                '    Else
                '        objPurRet.ApplicableFrom = Nothing
                '    End If
                'Else
                '    objPurRet.CURRENCY_CODE = Nothing
                '    objPurRet.ConvRate = 1
                '    objPurRet.ApplicableFrom = Nothing
                'End If
                'If CboNoteType.SelectedValue = "D" Then
                'Else
                '    objPurRet.TrType = ""
                'End If
                objPurRet.NoteType = "D"
                objPurRet.TrType = "P"
                '' end CurrencyConversion
                objPurRet.SaveData(objPurRet, True, trans)
                clsPurchasReturnHead.PostData(clsUserMgtCode.mbtnPurchaseReturn, objPurRet.PR_No, trans)
            End If

            CreatePurchaseReturnIfCostIncrease(obj, trans)

            ''-------------------------


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
                objVendorInvHead.TapalNo = obj.TapalNo
                If clsCommon.myLen(obj.DateAndTime) > 0 Then
                    objVendorInvHead.DateAndTime = obj.DateAndTime
                End If
                objVendorInvHead.On_Hold = False

                query = "select SRN_Date  from TSPL_SRN_HEAD where SRN_No ='" + obj.Against_SRN + "' "
                srndate = clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MM/yyyy")

                objVendorInvHead.Description = "Vendor " + obj.Vendor_Code + "/" + obj.Vendor_Name + " .Against PO Invoice No " + obj.PI_No + "-" + obj.Against_SRN + "-" + srndate
                objVendorInvHead.Tax_Calculation_Type = obj.Tax_Calculation_Type
                objVendorInvHead.Tax_Group = obj.Tax_Group
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    objVendorInvHead.TAX1 = obj.TAX1
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX1, trans) Then
                        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                    objVendorInvHead.Tax1_BAmount = 0 ' obj.TAX1_Base_Amt
                    objVendorInvHead.TAX1_Amt = 0 'obj.TAX1_Amt
                End If
                If (clsCommon.myLen(obj.TAX2) > 0) Then
                    objVendorInvHead.TAX2 = obj.TAX2
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX2, trans) Then
                        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                    objVendorInvHead.Tax2_BAmount = 0 ' obj.TAX2_Base_Amt
                    objVendorInvHead.TAX2_Amt = 0 'obj.TAX2_Amt
                End If
                If (clsCommon.myLen(obj.TAX3) > 0) Then
                    objVendorInvHead.TAX3 = obj.TAX3
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX3, trans) Then
                        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                    objVendorInvHead.Tax3_BAmount = 0 ' obj.TAX3_Base_Amt
                    objVendorInvHead.TAX3_Amt = 0 'obj.TAX3_Amt
                End If
                If (clsCommon.myLen(obj.TAX4) > 0) Then
                    objVendorInvHead.TAX4 = obj.TAX4
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX4, trans) Then
                        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                    objVendorInvHead.Tax4_BAmount = 0 ' obj.TAX4_Base_Amt
                    objVendorInvHead.TAX4_Amt = 0 'obj.TAX4_Amt
                End If
                If (clsCommon.myLen(obj.TAX5) > 0) Then
                    objVendorInvHead.TAX5 = obj.TAX5
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX5, trans) Then
                        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.Bill_To_Location, trans)

                    End If
                    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                    objVendorInvHead.Tax5_BAmount = 0 ' obj.TAX5_Base_Amt
                    objVendorInvHead.TAX5_Amt = 0 'obj.TAX5_Amt
                End If
                If (clsCommon.myLen(obj.TAX6) > 0) Then
                    objVendorInvHead.TAX6 = obj.TAX6
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX6, trans) Then
                        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                    objVendorInvHead.Tax6_BAmount = 0 ' obj.TAX6_Base_Amt
                    objVendorInvHead.TAX6_Amt = 0 'obj.TAX6_Amt
                End If
                If (clsCommon.myLen(obj.TAX7) > 0) Then
                    objVendorInvHead.TAX7 = obj.TAX7
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX7, trans) Then
                        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.Bill_To_Location, trans)

                    End If
                    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                    objVendorInvHead.Tax7_BAmount = 0 ' obj.TAX7_Base_Amt
                    objVendorInvHead.TAX7_Amt = 0 'obj.TAX7_Amt
                End If
                If (clsCommon.myLen(obj.TAX8) > 0) Then
                    objVendorInvHead.TAX8 = obj.TAX8
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX8, trans) Then
                        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                    objVendorInvHead.Tax8_BAmount = 0 ' obj.TAX8_Base_Amt
                    objVendorInvHead.TAX8_Amt = 0 ' obj.TAX8_Amt
                End If
                If (clsCommon.myLen(obj.TAX9) > 0) Then
                    objVendorInvHead.TAX9 = obj.TAX9
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX9, trans) Then
                        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                    objVendorInvHead.Tax9_BAmount = 0 ' obj.TAX9_Base_Amt
                    objVendorInvHead.TAX9_Amt = 0 'obj.TAX9_Amt
                End If
                If (clsCommon.myLen(obj.TAX10) > 0) Then
                    objVendorInvHead.TAX10 = obj.TAX10
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX10, trans) Then
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
                objVendorInvHead.Total_Add_Charge = 0
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
                'Dim isMiscRowExist As Boolean = False
                'For Each objPIDetail As clsPurchaseInvoiceDetail In obj.Arr
                '    If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                '        isMiscRowExist = True
                '        Exit For
                '    End If
                'Next
                Dim dclratio As Decimal = 0
                For Each objPIDetail As clsPurchaseInvoiceDetail In obj.Arr
                    Dim strICode As String = objPIDetail.Item_Code
                    dclratio = 0
                    If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        'Continue For ''By Balwinder on 29/08/2022 Dr/Cr Mismatch of PI No-PIO-001/22-23/00728 SPMMD
                        strICode = strFirstItemCode
                        dclratio = obj.Total_Rejected_Amount / (obj.Total_Rejected_Amount + obj.Total_Accepted_Amount + obj.Total_Shortage_Amount) '' BHA/21/11/18-000693 by balwinder on 04/12/2018
                    Else
                        If Not (objPIDetail.PI_Qty + objPIDetail.Leak_Qty + objPIDetail.Short_Qty + objPIDetail.Burst_Qty + objPIDetail.Reject_Qty) = 0 Then
                            dclratio = (objPIDetail.Reject_Qty) / (objPIDetail.PI_Qty + objPIDetail.Leak_Qty + objPIDetail.Short_Qty + objPIDetail.Burst_Qty + objPIDetail.Reject_Qty)
                        End If
                    End If
                    If dclratio = 0 Then
                        Continue For
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

                    ''Calculate Landed Amt Because Grid

                    objVendorInvDetail.Landed_Amount = (objPIDetail.Landed_Cost_Amount * dclratio) - (objPIDetail.Amt_Less_Discount * dclratio)
                    If objVendorInvDetail.Landed_Amount < 0 Then ''Added by balwinder on 28/02/2022 becuase it Give JE Mismatch by ashok
                        If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then '' ''By Balwinder on 29/08/2022 Dr/Cr Mismatch of PI No-PIO-001/22-23/00728 SPMMD 
                            objVendorInvDetail.Landed_Amount = 0
                        End If
                    End If
                    'objVendorInvHead.Total_Add_Charge += objVendorInvDetail.Landed_Amount ''Comment by balwinder on 19/11/2018 cause of JE mismatch in bharat.

                    objVendorInvHead.Discount_Base += objVendorInvDetail.Amount
                    objVendorInvHead.Discount_Amount += objVendorInvDetail.Discount
                    objVendorInvHead.Amount_Less_Discount += objVendorInvDetail.Amount_less_Discount
                    objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
                    '==Update by preeti Gupta Against Ticket No[BHA/15/11/18-000684]'29/10/2018
                    objVendorInvHead.Total_Tax += (objVendorInvDetail.TAX1_Amt + objVendorInvDetail.TAX2_Amt + objVendorInvDetail.TAX3_Amt + objVendorInvDetail.TAX4_Amt + objVendorInvDetail.TAX5_Amt + objVendorInvDetail.TAX6_Amt + objVendorInvDetail.TAX7_Amt + objVendorInvDetail.TAX8_Amt + objVendorInvDetail.TAX9_Amt + objVendorInvDetail.TAX10_Amt)
                    objVendorInvHead.Document_Total += objVendorInvDetail.Total_Amount
                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                    End If
                    ''End of Fill Vendor Invoice Detail Data
                Next
                dclratio = 0

                If obj.Amount_Less_Discount > 0 Then
                    dclratio = objVendorInvHead.Amount_Less_Discount / obj.Amount_Less_Discount
                    If dclratio > 0 Then
                        objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
                        objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
                        objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1 * dclratio

                        objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
                        objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
                        objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2 * dclratio

                        objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
                        objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
                        objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3 * dclratio

                        objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
                        objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
                        objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4 * dclratio

                        objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
                        objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
                        objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5 * dclratio

                        objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
                        objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
                        objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6 * dclratio

                        objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
                        objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
                        objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7 * dclratio

                        objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
                        objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
                        objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8 * dclratio

                        objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
                        objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
                        objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9 * dclratio

                        objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
                        objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
                        objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10 * dclratio

                        objVendorInvHead.Total_Add_Charge = objVendorInvHead.Add_Charge_Amt1 + objVendorInvHead.Add_Charge_Amt2 + objVendorInvHead.Add_Charge_Amt3 + objVendorInvHead.Add_Charge_Amt4 + objVendorInvHead.Add_Charge_Amt5 + objVendorInvHead.Add_Charge_Amt6 + objVendorInvHead.Add_Charge_Amt7 + objVendorInvHead.Add_Charge_Amt8 + objVendorInvHead.Add_Charge_Amt9 + objVendorInvHead.Add_Charge_Amt10
                    End If
                End If





                objVendorInvHead.Document_Total += objVendorInvHead.Total_Add_Charge
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
                objVendorInvHead.GSTRegistered = obj.GSTRegistered

                isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.PI_Date, Nothing, False)
                ''End of create debit note for Rejected
            End If

            ''##############################################################################################################################



#Region "Adjustment"
            '' Anubhooti 04-Nov-2014 Difference Entry (SRN-PI)
            Dim PIAmount As Double = 0
            Dim PayClrAmount As Double = 0
            Dim DiffAmount As Double = 0
            Dim SRNAmount As Double = 0
            Dim SRNRejectAmount As Double = 0
            Dim SRNShortAmount As Double = 0
            Dim counter As Integer = 0
            Dim counterReject As Integer = 1
            Dim ArryLst1 As ArrayList = New ArrayList()
            Dim objAdj As New ClsAdjustments()
            objAdj.Arr = New List(Of ClsAdjustmentsDetails)
            Dim objAdjRej As New ClsAdjustments()
            objAdjRej.Arr = New List(Of ClsAdjustmentsDetails)
            For Each objTr As clsPurchaseInvoiceDetail In obj.Arr
                If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    counter = counter + 1
                    Dim intCount As Integer = obj.Arr.Count
                    Dim dtSRNAmt As DataTable = clsDBFuncationality.GetDataTable("Select Accepted_Amount,Shortage_Amount,Rejected_Amount From TSPL_SRN_DETAIL Where SRN_No ='" & objTr.SRN_Id & "' And Item_Code ='" & objTr.Item_Code & "' " + IIf(clsCommon.myLen(objTr.PO_ID) > 0, " and po_ID='" & objTr.PO_ID & "'", "") + " ", trans)
                    If dtSRNAmt Is Nothing OrElse dtSRNAmt.Rows.Count <= 0 Then
                        Continue For
                    End If
                    SRNAmount = clsCommon.myCdbl(dtSRNAmt.Rows(0)("Accepted_Amount"))
                    DiffAmount = SRNAmount - objTr.Accepted_Amount

                    If DiffAmount <> 0 Then
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                            qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.RM_Consumption,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount,TSPL_PURCHASE_ACCOUNTS.Other_1,TSPL_PURCHASE_ACCOUNTS.Other_2 from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "'"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("Please set Purchase Account set for item " + objTr.Item_Code + "(" + objTr.Item_Code + ")")
                            End If
                            Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
                            If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("RM_Consumption"))) <= 0 Then
                                Throw New Exception("Please set RM consumption account in purchase account set for item " + objTr.Item_Code + "(" + objTr.Item_Code + ")")
                            End If

                            Dim RejAmount As Double = Math.Abs((clsCommon.myCdbl(dtSRNAmt.Rows(0)("Rejected_Amount")) - objTr.Rejected_Amount))
                            Dim ShortAmount As Double = Math.Abs((clsCommon.myCdbl(dtSRNAmt.Rows(0)("Shortage_Amount")) - objTr.Shortage_Amount))

                            Dim strRMConsumAC As String = clsCommon.myCstr(dt.Rows(0)("RM_Consumption"))
                            strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, IIf(clsCommon.myLen(obj.Ship_To_Location) > 0, obj.Ship_To_Location, obj.Bill_To_Location), trans)
                            strRMConsumAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strRMConsumAC, IIf(clsCommon.myLen(obj.Ship_To_Location) > 0, obj.Ship_To_Location, obj.Bill_To_Location), trans)
                            Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))
                            Dim strRMConsumACDesp As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strRMConsumAC + "'", trans))
                            Dim AccPayClrCr() As String = {strPaybleCleanigCtrlAC, (DiffAmount - IIf(DiffAmount > 0, 1, -1) * RejAmount - IIf(DiffAmount > 0, 1, -1) * ShortAmount), "", "", "", "", "", "", "Y"}
                            Dim AccRMConsumDr() As String = {strRMConsumAC, -1 * (DiffAmount - IIf(DiffAmount > 0, 1, -1) * RejAmount - IIf(DiffAmount > 0, 1, -1) * ShortAmount)}
                            ArryLst1.Add(AccPayClrCr)
                            ArryLst1.Add(AccRMConsumDr)
                            Dim strBrachAC As String = String.Empty

                            '' done by Panch Raj as suggested by Amit Sir on date 27-07-2016
                            If clsCommon.myLen(obj.Ship_To_Location) > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(obj.Ship_To_Location), clsCommon.myCstr(obj.Bill_To_Location)) <> CompairStringResult.Equal Then
                                    qry = "select Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING where From_Location='" + clsLocation.GetSegmentCode(obj.Bill_To_Location, trans) + "' and To_Location='" + clsLocation.GetSegmentCode(obj.Ship_To_Location, trans) + "'"
                                    strBrachAC = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                                    If clsCommon.myLen(strBrachAC) <= 0 Then
                                        Throw New Exception("Plase set Brach account with From_Location=" + clsLocation.GetSegmentCode(obj.Bill_To_Location, trans) + " and To_Location=" + clsLocation.GetSegmentCode(obj.Ship_To_Location, trans) + "")
                                    End If
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(obj.Ship_To_Location), clsCommon.myCstr(obj.Bill_To_Location)) <> CompairStringResult.Equal Then
                                    If clsCommon.CompairString(clsCommon.myCstr(obj.Ship_To_Location), clsCommon.myCstr(obj.Bill_To_Location)) = CompairStringResult.Equal Then
                                        strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Ship_To_Location, trans)
                                    Else
                                        strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strPaybleCleanigCtrlAC.Substring(0, strPaybleCleanigCtrlAC.Length - 4), obj.Bill_To_Location, False, trans)
                                    End If
                                    Dim AccCr2() As String = {strPaybleCleanigCtrlAC, Math.Round((DiffAmount - IIf(DiffAmount > 0, 1, -1) * RejAmount - IIf(DiffAmount > 0, 1, -1) * ShortAmount), 2, MidpointRounding.ToEven), "", "", "", "", "", "", "Y"}
                                    ArryLst1.Add(AccCr2)

                                    Dim AccCr3() As String = {strBrachAC, Math.Round(-1 * (DiffAmount - IIf(DiffAmount > 0, 1, -1) * RejAmount - IIf(DiffAmount > 0, 1, -1) * ShortAmount), 2, MidpointRounding.ToEven)}
                                    ArryLst1.Add(AccCr3)
                                End If
                            End If
                            ''end  done by Panch Raj as suggested by Amit Sir on date 27-07-2016

                            If objTr.Reject_Qty > 0 Then
                                'Dim RejAmount As Double = Math.Abs((clsCommon.myCdbl(dtSRNAmt.Rows(0)("Rejected_Amount")) - objTr.Rejected_Amount))
                                strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, IIf(clsCommon.myLen(obj.Ship_To_Location) > 0, obj.Ship_To_Location, obj.Bill_To_Location), trans)
                                Dim AccPayClrCrRej() As String = {strPaybleCleanigCtrlAC, IIf(DiffAmount > 0, 1, -1) * RejAmount, "", "", "", "", "", "", "Y"}
                                ArryLst1.Add(AccPayClrCrRej)
                                Dim strRejectAC As String = clsCommon.myCstr(dt.Rows(0)("Other_1"))
                                If clsCommon.myLen(strRejectAC) <= 0 Then
                                    Throw New Exception("Please set Reject account in purchase account set for item " + objTr.Item_Code + "(" + objTr.Item_Code + ")")
                                End If
                                strRejectAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strRejectAC, IIf(clsCommon.myLen(obj.Ship_To_Location) > 0, obj.Ship_To_Location, obj.Bill_To_Location), trans)
                                Dim AccRej() As String = {strRejectAC, IIf(DiffAmount > 0, -1, 1) * RejAmount}
                                ArryLst1.Add(AccRej)
                            End If
                            If objTr.Short_Qty > 0 Then
                                'Dim ShortAmount As Double = Math.Abs((clsCommon.myCdbl(dtSRNAmt.Rows(0)("Shortage_Amount")) - objTr.Shortage_Amount))
                                strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, IIf(clsCommon.myLen(obj.Ship_To_Location) > 0, obj.Ship_To_Location, obj.Bill_To_Location), trans)
                                Dim AccPayClrCrRej() As String = {strPaybleCleanigCtrlAC, IIf(DiffAmount > 0, 1, -1) * ShortAmount, "", "", "", "", "", "", "Y"}
                                ArryLst1.Add(AccPayClrCrRej)
                                Dim strRejectAC As String = clsCommon.myCstr(dt.Rows(0)("Other_2"))
                                If clsCommon.myLen(strRejectAC) <= 0 Then
                                    Throw New Exception("Please set Shortage account in purchase account set for item " + objTr.Item_Code + "(" + objTr.Item_Code + ")")
                                End If
                                strRejectAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strRejectAC, IIf(clsCommon.myLen(obj.Ship_To_Location) > 0, obj.Ship_To_Location, obj.Bill_To_Location), trans)
                                Dim AccRej() As String = {strRejectAC, IIf(DiffAmount > 0, -1, 1) * ShortAmount}
                                ArryLst1.Add(AccRej)
                            End If
                        End If

                        ''Adjustment Entry
                        objAdj.Adjustment_No = ""
                        objAdj.Adjustment_Date = clsCommon.GetPrintDate(obj.PI_Date, "dd/MMM/yyyy hh:mm tt")
                        objAdj.Description = "Auto Adjustment Against PI -" & clsCommon.myCstr(obj.PI_No) & " And SRN -" & clsCommon.myCstr(obj.Against_SRN) & ""
                        objAdj.Unit_Code = "ALL"
                        If clsCommon.myLen(obj.Ship_To_Location) > 0 Then
                            objAdj.Loc_Code = obj.Ship_To_Location
                            objAdj.Loc_Desc = obj.ShipToLocationName
                        Else
                            objAdj.Loc_Code = obj.Bill_To_Location
                            objAdj.Loc_Desc = obj.BillToLocationName
                        End If


                        If DiffAmount < 0 Then
                            objAdj.Trans_Type = clsCommon.myCstr("In")
                        Else
                            objAdj.Trans_Type = clsCommon.myCstr("Out")
                        End If
                        objAdj.chklocation = "N"
                        objAdj.IsMilkType = 0
                        objAdj.MainLocationCode = ""
                        objAdj.MainLocationDesc = ""
                        objAdj.Against_PI_No_Difference = obj.PI_No
                        Dim isFirstTimeSA As Boolean = True
                        If clsCommon.myLen(objTr.Item_Code) > 0 Then
                            Dim objAdTr As New ClsAdjustmentsDetails()
                            objAdTr.Adjustment_Line_No = clsCommon.myCstr(clsCommon.myCdbl(counter))
                            objAdTr.Item_Code = clsCommon.myCstr(objTr.Item_Code)
                            If clsCommon.myLen(objAdTr.Item_Code) > 0 Then
                                objAdTr.Item_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Item_Desc,'') AS Item_Desc From TSPL_ITEM_MASTER Where Item_Code ='" & clsCommon.myCstr(objAdTr.Item_Code) & "'", trans))
                            Else
                                objAdTr.Item_Description = ""
                            End If
                            objAdTr.Adjustment_Type = clsCommon.myCstr("Cost").Substring(0, 1) + IIf(clsCommon.CompairString(objAdj.Trans_Type, "In") = CompairStringResult.Equal, "I", "D")
                            objAdTr.Item_Quantity = 0
                            If DiffAmount < 0 Then
                                objAdTr.Item_Cost = clsCommon.myCdbl(-1 * DiffAmount) * IIf(obj.ConvRate = 0, 1, obj.ConvRate)
                            Else
                                objAdTr.Item_Cost = DiffAmount * IIf(obj.ConvRate = 0, 1, obj.ConvRate)
                            End If

                            objAdTr.Unit_Code = clsCommon.myCstr(objTr.Unit_code)
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select TSPL_PURCHASE_ACCOUNTS.Adjustment_Account ,TSPL_GL_ACCOUNTS.Description  from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Adjustment_Account where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "'", trans)
                            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                                Throw New Exception("Please set the Purchase Account set or its Adjustment Writeoff Account for item " + objAdTr.Item_Code)
                            End If
                            objAdTr.Account_Code = clsCommon.myCstr(dt1.Rows(0)("Adjustment_Account"))
                            objAdTr.Account_Description = clsCommon.myCstr(dt1.Rows(0)("Description"))
                            objAdTr.Remarks = ""
                            objAdTr.Comments = ""
                            objAdTr.mrp = clsCommon.myCdbl(0)
                            objAdTr.Batch_No = ""
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
                            objAdTr.Itemstatus = "NEW"
                            If (clsCommon.myLen(objAdTr.Item_Code) > 0) Then
                                objAdj.Arr.Add(objAdTr)
                            End If
                        End If
                        ''End of Adjustment Entry


                        ''Adjustment Entry for Rejected qty
                        objAdjRej.Adjustment_No = ""
                        objAdjRej.Adjustment_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
                        objAdjRej.Description = "Rejected Auto Adjustment Against PI -" & clsCommon.myCstr(obj.PI_No) & " And SRN -" & clsCommon.myCstr(obj.Against_SRN) & ""
                        objAdjRej.Unit_Code = "ALL"
                        objAdjRej.Loc_Code = clsLocation.GetRejectedLocation(obj.Bill_To_Location, trans)


                        objAdjRej.Loc_Desc = clsLocation.GetName(objAdjRej.Loc_Code, trans)
                        Dim RejectedAmount As Double = ((clsCommon.myCdbl(dtSRNAmt.Rows(0)("Rejected_Amount"))) - (objTr.Rejected_Amount))
                        If RejectedAmount < 0 Then
                            objAdjRej.Trans_Type = clsCommon.myCstr("In")
                        Else
                            objAdjRej.Trans_Type = clsCommon.myCstr("Out")
                        End If
                        objAdjRej.chklocation = "N"
                        objAdjRej.IsMilkType = 0
                        objAdjRej.MainLocationCode = ""
                        objAdjRej.MainLocationDesc = ""
                        objAdjRej.Against_PI_No_Difference_Rejected = obj.PI_No
                        If clsCommon.myLen(objTr.Item_Code) > 0 AndAlso objTr.Reject_Qty > 0 Then
                            If clsCommon.myLen(objAdjRej.Loc_Code) <= 0 Then
                                Throw New Exception("Please set the rejected location for " + obj.Bill_To_Location)
                            End If
                            Dim objAdTr As New ClsAdjustmentsDetails()
                            objAdTr.Adjustment_Line_No = counterReject
                            counterReject += 1
                            objAdTr.Item_Code = clsCommon.myCstr(objTr.Item_Code)
                            If clsCommon.myLen(objAdTr.Item_Code) > 0 Then
                                objAdTr.Item_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Item_Desc,'') AS Item_Desc From TSPL_ITEM_MASTER Where Item_Code ='" & clsCommon.myCstr(objAdTr.Item_Code) & "'", trans))
                            Else
                                objAdTr.Item_Description = ""
                            End If
                            objAdTr.Adjustment_Type = clsCommon.myCstr("Cost").Substring(0, 1) + IIf(clsCommon.CompairString(objAdjRej.Trans_Type, "In") = CompairStringResult.Equal, "I", "D")
                            objAdTr.Item_Quantity = 0
                            If RejectedAmount < 0 Then
                                objAdTr.Item_Cost = clsCommon.myCdbl(-1 * RejectedAmount)
                            Else
                                objAdTr.Item_Cost = RejectedAmount
                            End If

                            objAdTr.Unit_Code = clsCommon.myCstr(objTr.Unit_code)
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select TSPL_PURCHASE_ACCOUNTS.Adjustment_Account ,TSPL_GL_ACCOUNTS.Description  from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Adjustment_Account where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "'", trans)
                            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                                Throw New Exception("Please set the Purchase Account set or its Adjustment Writeoff Account for item " + objAdTr.Item_Code)
                            End If
                            objAdTr.Account_Code = clsCommon.myCstr(dt1.Rows(0)("Adjustment_Account"))
                            objAdTr.Account_Description = clsCommon.myCstr(dt1.Rows(0)("Description"))
                            objAdTr.Remarks = ""
                            objAdTr.Comments = ""
                            objAdTr.mrp = clsCommon.myCdbl(0)
                            objAdTr.Batch_No = ""
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
                            objAdTr.Itemstatus = "NEW"
                            If (clsCommon.myLen(objAdTr.Item_Code) > 0) Then
                                objAdjRej.Arr.Add(objAdTr)
                            End If
                        End If
                        ''End of Adjustment Entry
                    End If
                End If
            Next
            '' After Loop Saved all difference items
            If objAdj IsNot Nothing AndAlso objAdj.Arr.Count > 0 Then
                Dim isSavedAdj As Boolean = objAdj.SaveData(objAdj, True, "", trans)
                ClsAdjustments.PostData(objAdj.Adjustment_No, "Store Adjustment", trans, False)
            End If
            If objAdjRej IsNot Nothing AndAlso objAdjRej.Arr.Count > 0 Then
                Dim isSavedAdj As Boolean = objAdjRej.SaveData(objAdjRej, True, "", trans)
                ClsAdjustments.PostData(objAdjRej.Adjustment_No, "Store Adjustment", trans, False)
            End If

            '''' To be skipped based on setting

            'If ArryLst1 IsNot Nothing AndAlso ArryLst1.Count > 0 AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipDiffGLOnPI, clsFixedParameterCode.SkipDiffGLOnPI, trans)) = 0 Then
            If ArryLst1 IsNot Nothing AndAlso ArryLst1.Count > 0 AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                transportSql.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, trans, obj.PI_Date, "Difference Entry Against PI-" & obj.PI_No & " SRN-" & obj.Against_SRN & "", "PI-CM", "PI Consumption", obj.PI_No, obj.Description, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst1)
            End If
#End Region

            If objCommonVar.RCDFCFP Then
#Region "CreateDebitNotForQCDeduction"
                qry = "select sum(Ded_Amt) as Amt  from TSPL_SRN_DEDUCTION where SRN_No in (select SRN_ID from tspl_PI_Detail where PI_No='" + obj.PI_No + "')"
                Dim dtAmt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
                    Dim dblAmount As Decimal = Math.Abs(clsCommon.myCDecimal(dtAmt.Rows(0)("Amt")))
                    If dblAmount > 0 Then
                        If True Then
                            objVendorInvHead = New clsVedorInvoiceHead()
                            objVendorInvHead.isDeduction = 1
                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.PI_Date, "dd/MMM/yyyy")
                            objVendorInvHead.Vendor_Code = obj.Vendor_Code
                            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.Bill_To_Location, trans)
                            objVendorInvHead.Description = "AP Debit Note Against QC Deduction"
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If
                            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                            objVendorInvHead.RefDocType = "QC-DED"
                            objVendorInvHead.RefDocNo = obj.PI_No
                            objVendorInvHead.On_Hold = False
                            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
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
                            objVendorInvHead.Total_Landed_Amt = 0
                            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

                            If True Then
                                ii = ii + 1
                                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                objVendorInvDetail.Detail_Line_No = ii
                                'objVendorInvDetail.DCS_Addition_Deduction = clsCommon.myCstr(drAmt("Against_DCS_ADDITION_DEDUCTION"))
                                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Deduction_ACCOUNT"))
                                If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                                    Throw New Exception("Please Set Deduction Account of Vendor Account set [" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")) + "] and Vendor [" + obj.Vendor_Code + "]")
                                End If
                                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, obj.Bill_To_Location, trans)
                                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)
                                objVendorInvDetail.Amount = dblAmount
                                objVendorInvDetail.Discount_Per = 0
                                objVendorInvDetail.Discount = 0
                                objVendorInvDetail.Amount_less_Discount = dblAmount
                                objVendorInvDetail.Total_Tax = 0
                                objVendorInvDetail.Total_Amount = dblAmount
                                objVendorInvDetail.Landed_Amount = dblAmount
                                ''End of Set AP Invvoice Detail Table
                                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                                End If

                                ''Set AP Invvoice Header Table
                                objVendorInvHead.Total_Landed_Amt += dblAmount
                                objVendorInvHead.Discount_Base += dblAmount
                                objVendorInvHead.Discount_Amount += 0
                                objVendorInvHead.Amount_Less_Discount += dblAmount
                                objVendorInvHead.Document_Total += dblAmount
                                objVendorInvHead.Balance_Amt += dblAmount
                                ''End of Set AP Invvoice Header Table

                                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                If objVendorInvHead.Empty_Amount > 0 Then
                                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                        Throw New Exception("Please set Inventory Control Empties")
                                    End If
                                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                End If
                            End If
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                        End If
                    End If
                End If
#End Region

#Region "Debit Note for Secuity Deduction"
                qry = "select sum(Ded_Amt) as Amt  from TSPL_SRN_DEDUCTION_SECURITY where SRN_No in (select SRN_ID from TSPL_PI_DETAIL where PI_No='" + obj.PI_No + "')"
                dtAmt = clsDBFuncationality.GetDataTable(qry, trans)
                If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
                    Dim dblAmount As Decimal = Math.Abs(clsCommon.myCDecimal(dtAmt.Rows(0)("Amt")))
                    If dblAmount > 0 Then
                        If True Then
                            objVendorInvHead = New clsVedorInvoiceHead()
                            objVendorInvHead.isDeduction = 1
                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.PI_Date, "dd/MMM/yyyy")
                            objVendorInvHead.Vendor_Code = obj.Vendor_Code
                            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.Bill_To_Location, trans)
                            objVendorInvHead.Description = "AP Debit Note Against Secuirty Deduction"
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If
                            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                            objVendorInvHead.RefDocType = "SEC-DED"
                            objVendorInvHead.RefDocNo = obj.PI_No
                            objVendorInvHead.On_Hold = False
                            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,SECURITY_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
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
                            objVendorInvHead.Total_Landed_Amt = 0
                            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

                            If True Then
                                ii = ii + 1
                                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                objVendorInvDetail.Detail_Line_No = ii
                                'objVendorInvDetail.DCS_Addition_Deduction = clsCommon.myCstr(drAmt("Against_DCS_ADDITION_DEDUCTION"))
                                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("SECURITY_ACCOUNT"))
                                If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                                    Throw New Exception("Please Set Deduction Account of Vendor Account set [" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")) + "] and Vendor [" + obj.Vendor_Code + "]")
                                End If
                                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, obj.Bill_To_Location, trans)
                                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)
                                objVendorInvDetail.Amount = dblAmount
                                objVendorInvDetail.Discount_Per = 0
                                objVendorInvDetail.Discount = 0
                                objVendorInvDetail.Amount_less_Discount = dblAmount
                                objVendorInvDetail.Total_Tax = 0
                                objVendorInvDetail.Total_Amount = dblAmount
                                objVendorInvDetail.Landed_Amount = dblAmount
                                ''End of Set AP Invvoice Detail Table
                                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                                End If

                                ''Set AP Invvoice Header Table
                                objVendorInvHead.Total_Landed_Amt += dblAmount
                                objVendorInvHead.Discount_Base += dblAmount
                                objVendorInvHead.Discount_Amount += 0
                                objVendorInvHead.Amount_Less_Discount += dblAmount
                                objVendorInvHead.Document_Total += dblAmount
                                objVendorInvHead.Balance_Amt += dblAmount
                                ''End of Set AP Invvoice Header Table

                                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                If objVendorInvHead.Empty_Amount > 0 Then
                                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                        Throw New Exception("Please set Inventory Control Empties")
                                    End If
                                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                End If
                            End If
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                        End If
                    End If
                End If
#End Region

#Region "CreateDebitNotForSchedulePenalty"
                qry = "select sum(Penalty) as Amt  from TSPL_SRN_TENDER where isnull(Penalty,0)>0 and  SRN_No in (select SRN_ID from tspl_PI_Detail where PI_No='" + obj.PI_No + "')"
                dtAmt = clsDBFuncationality.GetDataTable(qry, trans)
                If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
                    Dim dblAmount As Decimal = Math.Abs(clsCommon.myCDecimal(dtAmt.Rows(0)("Amt")))
                    If dblAmount > 0 Then
                        If True Then
                            objVendorInvHead = New clsVedorInvoiceHead()
                            objVendorInvHead.isDeduction = 1
                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.PI_Date, "dd/MMM/yyyy")
                            objVendorInvHead.Vendor_Code = obj.Vendor_Code
                            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.Bill_To_Location, trans)
                            objVendorInvHead.Description = "AP Debit Note Against Schedule Penalty"
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If
                            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                            objVendorInvHead.RefDocType = "SCH-PNT"
                            objVendorInvHead.RefDocNo = obj.PI_No
                            objVendorInvHead.On_Hold = False
                            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,PRO_DATA_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
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
                            objVendorInvHead.Total_Landed_Amt = 0
                            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

                            If True Then
                                ii = ii + 1
                                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                objVendorInvDetail.Detail_Line_No = ii
                                'objVendorInvDetail.DCS_Addition_Deduction = clsCommon.myCstr(drAmt("Against_DCS_ADDITION_DEDUCTION"))
                                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("PRO_DATA_ACCOUNT"))
                                If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                                    Throw New Exception("Please Set Pro Data Account of Vendor Account set [" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")) + "] and Vendor [" + obj.Vendor_Code + "]")
                                End If
                                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, obj.Bill_To_Location, trans)
                                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)
                                objVendorInvDetail.Amount = dblAmount
                                objVendorInvDetail.Discount_Per = 0
                                objVendorInvDetail.Discount = 0
                                objVendorInvDetail.Amount_less_Discount = dblAmount
                                objVendorInvDetail.Total_Tax = 0
                                objVendorInvDetail.Total_Amount = dblAmount
                                objVendorInvDetail.Landed_Amount = dblAmount
                                ''End of Set AP Invvoice Detail Table
                                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                                End If

                                ''Set AP Invvoice Header Table
                                objVendorInvHead.Total_Landed_Amt += dblAmount
                                objVendorInvHead.Discount_Base += dblAmount
                                objVendorInvHead.Discount_Amount += 0
                                objVendorInvHead.Amount_Less_Discount += dblAmount
                                objVendorInvHead.Document_Total += dblAmount
                                objVendorInvHead.Balance_Amt += dblAmount
                                ''End of Set AP Invvoice Header Table

                                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                If objVendorInvHead.Empty_Amount > 0 Then
                                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                        Throw New Exception("Please set Inventory Control Empties")
                                    End If
                                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                End If
                            End If
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                        End If
                    End If
                End If
#End Region
            End If

            qry = "Update TSPL_PI_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.PI_Date, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "' "
            qry += " where PI_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_PI_HEAD", "PI_No", trans)
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function CreatePurchaseReturnIfCostIncrease(ByVal obj As clsPurchaseInvoiceHead, ByVal trans As SqlTransaction) As Boolean
        ''richa agarwal 5 Mar,2017 dur to variation in amount
        Dim straddqtyuomanditemwithdescription As String = String.Empty
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateDebitNoteForUnitCost, clsFixedParameterCode.CreateDebitNoteForUnitCost, trans)) = 1 Then
            Dim objPurRet As New clsPurchasReturnHead()
            'objPurRetPurRet.PR_No = txtDocNo.Value
            objPurRet.PR_Date = clsCommon.myCDate(obj.PI_Date, "dd/MM/yyyy")
            objPurRet.Vendor_Code = obj.Vendor_Code
            objPurRet.Vendor_Name = obj.Vendor_Name
            objPurRet.Vendor_Invoice_No = obj.Vendor_Invoice_No
            objPurRet.Ref_No = obj.Ref_No
            objPurRet.GSTRegistered = obj.GSTRegistered
            objPurRet.Total_Tax_Amt = 0
            objPurRet.Remarks = obj.Remarks
            objPurRet.Bill_To_Location = obj.Bill_To_Location
            objPurRet.Ship_To_Location = obj.Ship_To_Location
            objPurRet.Comments = obj.Comments
            objPurRet.On_Hold = False
            'objPurRet.Description = "Auto Generated Debit Note against Invoice no- " + obj.PI_No + " due to variation in Unit Cost."

            objPurRet.Tax_Group = obj.Tax_Group
            objPurRet.Item_Type = obj.Item_Type
            objPurRet.is_Reject_Item = False
            objPurRet.Against_PI = obj.PI_No
            objPurRet.Auto_Gen_Againnt_PI_No = obj.PI_No
            objPurRet.Project_Id = obj.PROJECT_ID
            If clsCommon.myLen(objPurRet.Against_PI) > 0 Then
                objPurRet.Against_SRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_SRN FROM TSPL_PI_HEAD WHERE PI_No='" + objPurRet.Against_PI + "'", trans))
            End If
            If clsCommon.myLen(objPurRet.Against_SRN) > 0 Then
                objPurRet.Against_MRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_MRN FROM TSPL_SRN_HEAD WHERE SRN_No='" + objPurRet.Against_SRN + "'", trans))
            End If
            If clsCommon.myLen(objPurRet.Against_MRN) > 0 Then
                objPurRet.Against_GRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_GRN FROM TSPL_MRN_HEAD WHERE MRN_No='" + objPurRet.Against_MRN + "'", trans))
            End If
            If clsCommon.myLen(objPurRet.Against_GRN) > 0 Then
                objPurRet.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_PO FROM TSPL_GRN_HEAD WHERE GRN_No='" + objPurRet.Against_GRN + "'", trans))
            End If
            If clsCommon.myLen(objPurRet.Against_PO) > 0 Then
                objPurRet.Against_Requisition = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + objPurRet.Against_PO + "'", trans))
            End If

            If clsCommon.myLen(obj.TAX1) > 0 Then
                objPurRet.TAX1 = obj.TAX1
                objPurRet.TAX1_Rate = obj.TAX1_Rate
                objPurRet.TAX1_Base_Amt = 0
                objPurRet.TAX1_Amt = 0
            End If
            If clsCommon.myLen(obj.TAX2) > 0 Then
                objPurRet.TAX2 = obj.TAX2
                objPurRet.TAX2_Rate = obj.TAX2_Rate
                objPurRet.TAX2_Base_Amt = 0
                objPurRet.TAX2_Amt = 0
            End If
            If clsCommon.myLen(obj.TAX3) > 0 Then
                objPurRet.TAX3 = obj.TAX3
                objPurRet.TAX3_Rate = obj.TAX3_Rate
                objPurRet.TAX3_Base_Amt = 0
                objPurRet.TAX3_Amt = 0
            End If
            If clsCommon.myLen(obj.TAX4) > 0 Then
                objPurRet.TAX4 = obj.TAX4
                objPurRet.TAX4_Rate = obj.TAX4_Rate
                objPurRet.TAX4_Base_Amt = 0
                objPurRet.TAX4_Amt = 0
            End If
            If clsCommon.myLen(obj.TAX5) > 0 Then
                objPurRet.TAX5 = obj.TAX5
                objPurRet.TAX5_Rate = obj.TAX5_Rate
                objPurRet.TAX5_Base_Amt = 0
                objPurRet.TAX5_Amt = 0
            End If
            If clsCommon.myLen(obj.TAX6) > 0 Then
                objPurRet.TAX6 = obj.TAX6
                objPurRet.TAX6_Rate = obj.TAX6_Rate
                objPurRet.TAX6_Base_Amt = 0
                objPurRet.TAX6_Amt = 0
            End If
            If clsCommon.myLen(obj.TAX7) > 0 Then
                objPurRet.TAX7 = obj.TAX7
                objPurRet.TAX7_Rate = obj.TAX7_Rate
                objPurRet.TAX7_Base_Amt = 0
                objPurRet.TAX7_Amt = 0
            End If
            If clsCommon.myLen(obj.TAX8) > 0 Then
                objPurRet.TAX8 = obj.TAX8
                objPurRet.TAX8_Rate = obj.TAX8_Rate
                objPurRet.TAX8_Base_Amt = 0
                objPurRet.TAX8_Amt = 0
            End If
            If clsCommon.myLen(obj.TAX9) > 0 Then
                objPurRet.TAX9 = obj.TAX9
                objPurRet.TAX9_Rate = obj.TAX9_Rate
                objPurRet.TAX9_Base_Amt = 0
                objPurRet.TAX9_Amt = 0
            End If
            If clsCommon.myLen(obj.TAX10) > 0 Then
                objPurRet.TAX10 = obj.TAX10
                objPurRet.TAX10_Rate = obj.TAX10_Rate
                objPurRet.TAX10_Base_Amt = 0
                objPurRet.TAX10_Amt = 0
            End If
            objPurRet.Tax_Calculation_Type = obj.Tax_Calculation_Type

            objPurRet.Terms_Code = obj.Terms_Code
            objPurRet.Due_Date = obj.Due_Date
            objPurRet.Carrier = obj.Carrier
            objPurRet.VehicleNo = obj.VehicleNo
            objPurRet.GRNo = obj.GRNo
            objPurRet.GENo = obj.GENo
            If obj.GEDate IsNot Nothing Then
                objPurRet.GEDate = obj.GEDate
            End If
            objPurRet.Total_Add_Charge = 0
            objPurRet.is_Excise_On_Qty = obj.is_Excise_On_Qty
            objPurRet.Discount_Base = 0
            objPurRet.Discount_Amt = 0
            objPurRet.Amount_Less_Discount = 0
            objPurRet.Total_Tax_Amt = 0
            objPurRet.PR_Total_Amt = 0
            objPurRet.AssessableAmt = 0

            objPurRet.Add_Charge_Code1 = obj.Add_Charge_Code1
            objPurRet.Add_Charge_Code2 = obj.Add_Charge_Code2
            objPurRet.Add_Charge_Code3 = obj.Add_Charge_Code3
            objPurRet.Add_Charge_Code4 = obj.Add_Charge_Code4
            objPurRet.Add_Charge_Code5 = obj.Add_Charge_Code5
            objPurRet.Add_Charge_Code6 = obj.Add_Charge_Code6
            objPurRet.Add_Charge_Code7 = obj.Add_Charge_Code7
            objPurRet.Add_Charge_Code8 = obj.Add_Charge_Code8
            objPurRet.Add_Charge_Code9 = obj.Add_Charge_Code9
            objPurRet.Add_Charge_Code10 = obj.Add_Charge_Code10

            objPurRet.Add_Charge_Name1 = obj.Add_Charge_Name1
            objPurRet.Add_Charge_Name2 = obj.Add_Charge_Name2
            objPurRet.Add_Charge_Name3 = obj.Add_Charge_Name3
            objPurRet.Add_Charge_Name4 = obj.Add_Charge_Name4
            objPurRet.Add_Charge_Name5 = obj.Add_Charge_Name5
            objPurRet.Add_Charge_Name6 = obj.Add_Charge_Name6
            objPurRet.Add_Charge_Name7 = obj.Add_Charge_Name7
            objPurRet.Add_Charge_Name8 = obj.Add_Charge_Name8
            objPurRet.Add_Charge_Name9 = obj.Add_Charge_Name9
            objPurRet.Add_Charge_Name10 = obj.Add_Charge_Name10

            objPurRet.Arr = New List(Of clsPurchasReturnDetail)
            Dim RowNo As Integer = 1
            For Each objPIDetail As clsPurchaseInvoiceDetail In obj.Arr
                Dim objPurRetTr As New clsPurchasReturnDetail()

                ''find diffrence in unit cost
                objPurRetTr.Item_Cost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Item_Cost from TSPL_SRN_DETAIL where SRN_No='" & clsCommon.myCstr(obj.Against_SRN) & "' and Item_Code ='" & clsCommon.myCstr(objPIDetail.Item_Code) & "' and Unit_code = '" & clsCommon.myCstr(objPIDetail.Unit_code) & "' ", trans))
                Dim dclratio As Decimal = objPIDetail.Item_Cost - objPurRetTr.Item_Cost
                If dclratio <= 0 Then
                    Continue For
                End If
                objPurRetTr.Item_Cost = dclratio
                objPurRetTr.Line_No = RowNo
                RowNo += 1
                objPurRetTr.Row_Type = objPIDetail.Row_Type
                objPurRetTr.Item_Code = objPIDetail.Item_Code
                objPurRetTr.Item_Desc = objPIDetail.Item_Desc
                objPurRetTr.PR_Qty = objPIDetail.PI_Qty
                objPurRetTr.Balance_Qty = objPIDetail.PI_Qty
                objPurRetTr.Unit_code = objPIDetail.Unit_code
                objPurRetTr.PI_Id = objPIDetail.PI_No
                objPurRetTr.PO_ID = objPIDetail.PO_ID
                objPurRetTr.GRN_ID = objPIDetail.GRN_ID
                objPurRetTr.MRN_ID = objPIDetail.MRN_ID
                objPurRetTr.SRN_Id = objPIDetail.SRN_Id
                objPurRetTr.Amount = objPIDetail.PI_Qty * dclratio
                objPurRetTr.Disc_Per = objPIDetail.Disc_Per
                objPurRetTr.Disc_Amt = (objPurRetTr.Amount * objPIDetail.Disc_Per) / 100
                objPurRetTr.Amt_Less_Discount = objPurRetTr.Amount - objPurRetTr.Disc_Amt

                ''richa agarwal 18 Apr,2017
                straddqtyuomanditemwithdescription += clsCommon.myCstr(objPIDetail.PI_Qty) + " " + clsCommon.myCstr(objPIDetail.Unit_code) + " of " + clsCommon.myCstr(objPIDetail.Item_Code) + ", "
                ''--------------

                objPurRetTr.TAX1 = objPIDetail.TAX1
                If objPIDetail.Amt_Less_Discount <> 0 Then
                    objPurRetTr.TAX1_Amt = (objPIDetail.TAX1_Amt * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                Else
                    objPurRetTr.TAX1_Amt = 0
                End If
                objPurRetTr.TAX1_Rate = objPIDetail.TAX1_Rate
                If objPurRetTr.TAX1_Rate <> 0 Then
                    objPurRetTr.TAX1_Base_Amt = (objPurRetTr.TAX1_Amt * 100) / objPurRetTr.TAX1_Rate
                Else
                    objPurRetTr.TAX1_Base_Amt = 0
                End If


                objPurRetTr.TAX2 = objPIDetail.TAX2
                If objPIDetail.Amt_Less_Discount <> 0 Then
                    objPurRetTr.TAX2_Amt = (objPIDetail.TAX2_Amt * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                Else
                    objPurRetTr.TAX2_Amt = 0
                End If
                objPurRetTr.TAX2_Rate = objPIDetail.TAX2_Rate
                If objPurRetTr.TAX2_Rate <> 0 Then
                    objPurRetTr.TAX2_Base_Amt = (objPurRetTr.TAX2_Amt * 100) / objPurRetTr.TAX2_Rate
                Else
                    objPurRetTr.TAX2_Base_Amt = 0
                End If


                objPurRetTr.TAX3 = objPIDetail.TAX3
                If objPIDetail.Amt_Less_Discount <> 0 Then
                    objPurRetTr.TAX3_Amt = (objPIDetail.TAX3_Amt * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                Else
                    objPurRetTr.TAX3_Amt = 0
                End If
                objPurRetTr.TAX3_Rate = objPIDetail.TAX3_Rate
                If objPurRetTr.TAX3_Rate <> 0 Then
                    objPurRetTr.TAX3_Base_Amt = (objPurRetTr.TAX3_Amt * 100) / objPurRetTr.TAX3_Rate
                Else
                    objPurRetTr.TAX3_Base_Amt = 0
                End If


                objPurRetTr.TAX4 = objPIDetail.TAX4
                If objPIDetail.Amt_Less_Discount <> 0 Then
                    objPurRetTr.TAX4_Amt = (objPIDetail.TAX4_Amt * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                Else
                    objPurRetTr.TAX4_Amt = 0
                End If
                objPurRetTr.TAX4_Rate = objPIDetail.TAX4_Rate
                If objPurRetTr.TAX4_Rate <> 0 Then
                    objPurRetTr.TAX4_Base_Amt = (objPurRetTr.TAX4_Amt * 100) / objPurRetTr.TAX4_Rate
                Else
                    objPurRetTr.TAX4_Base_Amt = 0
                End If



                objPurRetTr.TAX5 = objPIDetail.TAX5
                If objPIDetail.Amt_Less_Discount <> 0 Then
                    objPurRetTr.TAX5_Amt = (objPIDetail.TAX5_Amt * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                Else
                    objPurRetTr.TAX5_Amt = 0
                End If
                objPurRetTr.TAX5_Rate = objPIDetail.TAX5_Rate
                If objPurRetTr.TAX5_Rate <> 0 Then
                    objPurRetTr.TAX5_Base_Amt = (objPurRetTr.TAX5_Amt * 100) / objPurRetTr.TAX5_Rate
                Else
                    objPurRetTr.TAX5_Base_Amt = 0
                End If



                objPurRetTr.TAX6 = objPIDetail.TAX6
                If objPIDetail.Amt_Less_Discount <> 0 Then
                    objPurRetTr.TAX6_Amt = (objPIDetail.TAX6_Amt * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                Else
                    objPurRetTr.TAX6_Amt = 0
                End If
                objPurRetTr.TAX6_Rate = objPIDetail.TAX6_Rate
                If objPurRetTr.TAX6_Rate <> 0 Then
                    objPurRetTr.TAX6_Base_Amt = (objPurRetTr.TAX6_Amt * 100) / objPurRetTr.TAX6_Rate
                Else
                    objPurRetTr.TAX6_Base_Amt = 0
                End If




                objPurRetTr.TAX7 = objPIDetail.TAX7
                If objPIDetail.Amt_Less_Discount <> 0 Then
                    objPurRetTr.TAX7_Amt = (objPIDetail.TAX7_Amt * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                Else
                    objPurRetTr.TAX7_Amt = 0
                End If
                objPurRetTr.TAX7_Rate = objPIDetail.TAX7_Rate
                If objPurRetTr.TAX7_Rate <> 0 Then
                    objPurRetTr.TAX7_Base_Amt = (objPurRetTr.TAX7_Amt * 100) / objPurRetTr.TAX7_Rate
                Else
                    objPurRetTr.TAX7_Base_Amt = 0
                End If



                objPurRetTr.TAX8 = objPIDetail.TAX8
                If objPIDetail.Amt_Less_Discount <> 0 Then
                    objPurRetTr.TAX8_Amt = (objPIDetail.TAX8_Amt * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                Else
                    objPurRetTr.TAX8_Amt = 0
                End If
                objPurRetTr.TAX8_Rate = objPIDetail.TAX8_Rate
                If objPurRetTr.TAX8_Rate <> 0 Then
                    objPurRetTr.TAX8_Base_Amt = (objPurRetTr.TAX8_Amt * 100) / objPurRetTr.TAX8_Rate
                Else
                    objPurRetTr.TAX8_Base_Amt = 0
                End If



                objPurRetTr.TAX9 = objPIDetail.TAX9
                If objPIDetail.Amt_Less_Discount <> 0 Then
                    objPurRetTr.TAX9_Amt = (objPIDetail.TAX9_Amt * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                Else
                    objPurRetTr.TAX9_Amt = 0
                End If

                objPurRetTr.TAX9_Rate = objPIDetail.TAX9_Rate
                If objPurRetTr.TAX9_Rate Then
                    objPurRetTr.TAX9_Base_Amt = (objPurRetTr.TAX9_Amt * 100) / objPurRetTr.TAX9_Rate
                Else
                    objPurRetTr.TAX9_Base_Amt = 0
                End If


                objPurRetTr.TAX10 = objPIDetail.TAX10
                If objPIDetail.Amt_Less_Discount <> 0 Then
                    objPurRetTr.TAX10_Amt = (objPIDetail.TAX10_Amt * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                Else
                    objPurRetTr.TAX10_Amt = 0
                End If
                objPurRetTr.TAX10_Rate = objPIDetail.TAX10_Rate
                If objPurRetTr.TAX10_Rate Then
                    objPurRetTr.TAX10_Base_Amt = (objPurRetTr.TAX10_Amt * 100) / objPurRetTr.TAX10_Rate
                Else
                    objPurRetTr.TAX10_Base_Amt = 0
                End If

                objPurRetTr.Total_Tax_Amt = (objPurRetTr.TAX1_Amt + objPurRetTr.TAX2_Amt + objPurRetTr.TAX3_Amt + objPurRetTr.TAX4_Amt + objPurRetTr.TAX5_Amt + objPurRetTr.TAX6_Amt + objPurRetTr.TAX7_Amt + objPurRetTr.TAX8_Amt + objPurRetTr.TAX9_Amt + objPurRetTr.TAX10_Amt)
                objPurRetTr.Item_Net_Amt = objPurRetTr.Amt_Less_Discount + objPurRetTr.Total_Tax_Amt
                objPurRetTr.Location = objPIDetail.Location
                objPurRetTr.MRP = objPIDetail.MRP

                objPurRet.TAX1_Base_Amt += objPurRetTr.TAX1_Base_Amt
                objPurRet.TAX1_Amt += objPurRetTr.TAX1_Amt
                objPurRet.TAX2_Base_Amt += objPurRetTr.TAX2_Base_Amt
                objPurRet.TAX2_Amt += objPurRetTr.TAX2_Amt
                objPurRet.TAX3_Base_Amt += objPurRetTr.TAX3_Base_Amt
                objPurRet.TAX3_Amt += objPurRetTr.TAX3_Amt
                objPurRet.TAX4_Base_Amt += objPurRetTr.TAX4_Base_Amt
                objPurRet.TAX4_Amt += objPurRetTr.TAX4_Amt
                objPurRet.TAX5_Base_Amt += objPurRetTr.TAX5_Base_Amt
                objPurRet.TAX5_Amt += objPurRetTr.TAX5_Amt
                objPurRet.TAX6_Base_Amt += objPurRetTr.TAX6_Base_Amt
                objPurRet.TAX6_Amt += objPurRetTr.TAX6_Amt
                objPurRet.TAX7_Base_Amt += objPurRetTr.TAX7_Base_Amt
                objPurRet.TAX7_Amt += objPurRetTr.TAX7_Amt
                objPurRet.TAX8_Base_Amt += objPurRetTr.TAX8_Base_Amt
                objPurRet.TAX8_Amt += objPurRetTr.TAX8_Amt
                objPurRet.TAX9_Base_Amt += objPurRetTr.TAX9_Base_Amt
                objPurRet.TAX9_Amt += objPurRetTr.TAX9_Amt
                objPurRet.TAX10_Base_Amt += objPurRetTr.TAX10_Base_Amt
                objPurRet.TAX10_Amt += objPurRetTr.TAX10_Amt
                objPurRet.Discount_Base += objPurRetTr.Amount
                objPurRet.Discount_Amt += objPurRetTr.Disc_Amt
                objPurRet.Amount_Less_Discount += objPurRetTr.Amt_Less_Discount
                objPurRet.Total_Tax_Amt += (objPurRetTr.TAX1_Amt + objPurRetTr.TAX2_Amt + objPurRetTr.TAX3_Amt + objPurRetTr.TAX4_Amt + objPurRetTr.TAX5_Amt + objPurRetTr.TAX6_Amt + objPurRetTr.TAX7_Amt + objPurRetTr.TAX8_Amt + objPurRetTr.TAX9_Amt + objPurRetTr.TAX10_Amt) ' tax1 amount add double times
                objPurRet.PR_Total_Amt += objPurRetTr.Item_Net_Amt

                objPurRetTr.AssessableAmt = objPurRetTr.Amt_Less_Discount
                objPurRetTr.Batch_No = objPIDetail.Batch_No
                objPurRetTr.Bin_No = objPIDetail.Bin_No
                If objPIDetail.Expiry_Date IsNot Nothing Then
                    objPurRetTr.Expiry_Date = objPIDetail.Expiry_Date
                End If
                If objPIDetail.MFG_Date IsNot Nothing Then
                    objPurRetTr.MFG_Date = objPIDetail.MFG_Date
                End If
                objPurRetTr.Specification = objPIDetail.Specification
                objPurRetTr.Remarks = objPIDetail.Remarks

                If obj.Amount_Less_Discount <> 0 Then
                    objPurRetTr.Landed_Cost_Amount = (objPIDetail.Landed_Cost_Amount * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                    If objPurRetTr.PR_Qty > 0 Then
                        objPurRetTr.Landed_Cost_Rate = objPurRetTr.Landed_Cost_Amount / objPurRetTr.PR_Qty
                    End If
                    objPurRetTr.Total_AddtionalCost_PerUnit = (objPIDetail.Total_AddtionalCost_PerUnit * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                    objPurRetTr.Total_NonRecTax_PerUnit = (objPIDetail.Total_NonRecTax_PerUnit * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                    objPurRetTr.Total_RecTax_PerUnit = (objPIDetail.Total_RecTax_PerUnit * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount

                    objPurRet.Add_Charge_Amt1 += (objPIDetail.ItemAdd_Calc_Charge_Amt1 * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                    objPurRet.Add_Charge_Amt2 += (objPIDetail.ItemAdd_Calc_Charge_Amt2 * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                    objPurRet.Add_Charge_Amt3 += (objPIDetail.ItemAdd_Calc_Charge_Amt3 * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                    objPurRet.Add_Charge_Amt4 += (objPIDetail.ItemAdd_Calc_Charge_Amt4 * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                    objPurRet.Add_Charge_Amt5 += (objPIDetail.ItemAdd_Calc_Charge_Amt5 * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                    objPurRet.Add_Charge_Amt6 += (objPIDetail.ItemAdd_Calc_Charge_Amt6 * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                    objPurRet.Add_Charge_Amt7 += (objPIDetail.ItemAdd_Calc_Charge_Amt7 * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                    objPurRet.Add_Charge_Amt8 += (objPIDetail.ItemAdd_Calc_Charge_Amt8 * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                    objPurRet.Add_Charge_Amt9 += (objPIDetail.ItemAdd_Calc_Charge_Amt9 * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                    objPurRet.Add_Charge_Amt10 += (objPIDetail.ItemAdd_Calc_Charge_Amt10 * objPurRetTr.Amt_Less_Discount) / objPIDetail.Amt_Less_Discount
                Else
                    objPurRetTr.Landed_Cost_Amount = 0
                    objPurRetTr.Landed_Cost_Rate = 0
                    objPurRetTr.Total_AddtionalCost_PerUnit = 0
                    objPurRetTr.Total_NonRecTax_PerUnit = 0
                    objPurRetTr.Total_RecTax_PerUnit = 0
                End If
                If (clsCommon.myLen(objPurRetTr.Item_Code) > 0) Then
                    objPurRet.Arr.Add(objPurRetTr)
                End If
            Next
            objPurRet.Total_Add_Charge = objPurRet.Add_Charge_Amt1 + objPurRet.Add_Charge_Amt2 + objPurRet.Add_Charge_Amt3 + objPurRet.Add_Charge_Amt4 + objPurRet.Add_Charge_Amt5 + objPurRet.Add_Charge_Amt6 + objPurRet.Add_Charge_Amt7 + objPurRet.Add_Charge_Amt8 + objPurRet.Add_Charge_Amt9 + objPurRet.Add_Charge_Amt10
            objPurRet.PR_Total_Amt += objPurRet.Total_Add_Charge




            '' CurrencConversion
            objPurRet.CURRENCY_CODE = obj.CURRENCY_CODE
            objPurRet.ConvRate = obj.ConvRate
            objPurRet.NoteType = "D"
            objPurRet.TrType = "P"
            '' end CurrencyConversion

            ''richa agarwal 18 Apr,2017
            If clsCommon.myLen(straddqtyuomanditemwithdescription) > 0 Then
                straddqtyuomanditemwithdescription = straddqtyuomanditemwithdescription.Substring(0, straddqtyuomanditemwithdescription.Length() - 2)
            End If
            objPurRet.Description = "Auto Generated Debit Note against Invoice no- " + obj.PI_No + " due to " + straddqtyuomanditemwithdescription + " variation in Unit Cost."

            ''-----------------

            If (objPurRet.Arr IsNot Nothing AndAlso objPurRet.Arr.Count > 0) Then
                objPurRet.SaveData(objPurRet, True, trans)
                clsPurchasReturnHead.PostData(clsUserMgtCode.mbtnPurchaseReturn, objPurRet.PR_No, trans)
            End If
        End If
        'Throw New Exception("Balwinder singh premi")
        Return True
    End Function

    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsPurchaseInvoiceDetail)) As String
        For Each objtr As clsPurchaseInvoiceDetail In Arr
            If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Return objtr.Item_Code
            End If
        Next
        Return ""
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim qry As String = "select pr_no from TSPL_PR_HEAD where Against_PI in ('" + strDocNo + "')"
            Dim strpi As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(strpi) > 0 Then
                Throw New Exception("Purchase Invoice is used in Purchase Return No- " + strpi + " ")
            End If

            qry = "select TSPL_PI_HEAD.PI_No,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_PJV_HEAD.PJV_No from TSPL_PI_HEAD "
            qry += "  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No"
            qry += "  left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_JOURNAL_MASTER.Source_Code in ('AP-IN','AP-DN','PI-CM')"
            qry += "  left outer join TSPL_PJV_HEAD on TSPL_PJV_HEAD.Invoice_No=TSPL_PI_HEAD.PI_No "
            qry += "  where PI_No='" + strDocNo + "' and TSPL_PI_HEAD.Status=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows
                    ''Delete AP Journal Entry 
                    Dim docNo As String = clsCommon.myCstr(dr("Voucher_No"))
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, docNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                    qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No  in ('" + docNo + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No in ('" + docNo + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)


                    ''Delete AP Invoice ( changes by richa AGARWAL use Purchase Invoice No. in place of vendor Invoice no 08/07/2015)

                    docNo = clsCommon.myCstr(dr("Document_No"))
                    qry = "select TSPL_PAYMENT_DETAIL.Payment_No from TSPL_PAYMENT_DETAIL inner Join TSPL_PAYMENT_hEADER on TSPL_PAYMENT_DETAIL.Payment_no=TSPL_PAYMENT_hEADER.Payment_no where TSPL_PAYMENT_DETAIL.Document_No in ('" + docNo + "') AND ISNULL(TSPL_PAYMENT_hEADER.iscHKrEVERSE,'')<>'Y' "
                    Dim dtAP As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                        qry = "AP-Invoice " + docNo + " is used in following Payment -"
                        For Each drAP As DataRow In dtAP.Rows
                            qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                        Next
                        Throw New Exception(qry)
                    End If


                    ''richa BHA/04/09/18-000505
                    docNo = clsCommon.myCstr(dr("PI_No"))
                    qry = "Delete from TSPL_REMITTANCE WHERE Document_No in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_POInvoice_No in ('" + docNo + "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)


                    'qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No='" + docNo + "'"
                    'clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    'qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Document_No='" + docNo + "'"
                    'clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    ''''''''''''''''''
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, docNo, "TSPL_VENDOR_INVOICE_HEAD", "Against_POInvoice_No", trans)
                    qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_POInvoice_No in ('" + docNo + "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Against_POInvoice_No in ('" + docNo + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No =(select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_POInvoice_No in ('" + docNo + "')))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_POInvoice_No in ('" + docNo + "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_PR_DETAIL where PR_No in (select PR_No from TSPL_PR_HEAD where Auto_Gen_Againnt_PI_No in ('" + docNo + "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "delete from TSPL_PR_HEAD where Auto_Gen_Againnt_PI_No in ('" + docNo + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_PurchaseReturn_No=(select PR_No from TSPL_PR_HEAD where Auto_Gen_Againnt_PI_No in ('" + docNo + "')))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Against_PurchaseReturn_No in (select PR_No from TSPL_PR_HEAD where Auto_Gen_Againnt_PI_No in ('" + docNo + "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No=(select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_PurchaseReturn_No=(select PR_No from TSPL_PR_HEAD where Auto_Gen_Againnt_PI_No in ('" + docNo + "'))))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No in  (select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_PurchaseReturn_No=(select PR_No from TSPL_PR_HEAD where Auto_Gen_Againnt_PI_No in ('" + docNo + "')))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PI-CM' and Source_Doc_No in ('" + docNo + "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='PI-CM' and Source_Doc_No in ('" + docNo + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '==========BM00000008086
                    qry = "delete from TSPL_Inventory_Movement where Trans_Type='IC-AD' and Source_Doc_No in ( select Adjustment_No  from TSPL_ADJUSTMENT_HEADER where Against_PI_No_Difference in ('" + docNo + "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_ADJUSTMENT_DETAIL where  TSPL_ADJUSTMENT_DETAIL.Adjustment_No in ( select Adjustment_No  from TSPL_ADJUSTMENT_HEADER where Against_PI_No_Difference in ('" + docNo + "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "delete from TSPL_ADJUSTMENT_HEADER where Against_PI_No_Difference in ('" + docNo + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    ''''''''''''''''''
                    ''Change status to unpost of PJV
                    docNo = clsCommon.myCstr(dr("PJV_No"))
                    qry = "update TSPL_PJV_HEAD set Status=0,Posting_Date=null where PJV_No in ('" + docNo + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    ''Change status to unpost
                    docNo = clsCommon.myCstr(dr("PI_No"))
                    qry = "update TSPL_PI_HEAD set Status=0 where PI_No in ('" + docNo + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "Delete from TSPL_PO_ADVANCE_ADJUSTMENT_KNOCKOFF where PI_No='" + docNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(docNo), "TSPL_PI_HEAD", "PI_No", trans)
                Next
            End If

            qry = "select Document_No From TSPL_VENDOR_INVOICE_HEAD where RefDocType in('QC-DED','SEC-DED','SCH-PNT') and RefDocNo='" + strDocNo + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    qry = clsCommon.myCstr(dr("Document_No"))
                    clsVedorInvoiceHead.ReverseAndUnpost(qry, trans)
                    clsVedorInvoiceHead.DeleteData(qry, trans)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsPurchaseInvoiceHead = clsPurchaseInvoiceHead.GetData(strCode, NavigatorType.Current, trans, "")
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PI_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Invoice", obj.Bill_To_Location, clsCommon.myCDate(obj.PI_Date), trans)
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsPIAdditionChargeInsurance.DeleteData(strCode, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PI_HEAD", "PI_No", "TSPL_PI_DETAIL", "PI_No", "TSPL_PI_REMITTANCE", "Document_No", trans)
                Dim qry As String = "delete from TSPL_PI_DETAIL where PI_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "Delete from TSPL_PO_ADVANCE_ADJUSTMENT_KNOCKOFF where PI_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


                qry = " delete from TSPL_PI_REMITTANCE where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PJV_Detail where PJV_No in (select TSPL_PJV_HEAD.PJV_No from TSPL_PJV_HEAD where TSPL_PJV_HEAD.Invoice_No='" + strCode + "')"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = " delete from TSPL_PJV_HEAD where Invoice_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PI_HEAD where PI_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_TRANSACTION_APPROVAL where Document_No='" & obj.PI_No & "' and Screen_Name='" + clsUserMgtCode.mbtnPurchaseInvoice + "'"
                clsDBFuncationality.GetDataTable(qry, trans)


                'If (isSaved) Then
                '    trans.Commit()
                'Else
                '    trans.Rollback()
                'End If
            Catch ex As Exception
                'trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Public Shared Function IsValidVendorForPI(ByVal Arr As List(Of String), ByVal strVendorCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select PI_No,Vendor_Code,Vendor_Name from TSPL_PI_HEAD where PI_No in (" + clsCommon.GetMulcallString(Arr) + ") and Vendor_Code not in ('" + strVendorCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "PI No:" + clsCommon.myCstr(dr("PI_No")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Vendor_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Vendor_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function IsValidJobWorkOutwordForPR(ByVal Arr As List(Of String), ByVal isJobWorkOutward As Boolean) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select PI_No,Vendor_Code,Vendor_Name from TSPL_PI_HEAD where PI_No in (" + clsCommon.GetMulcallString(Arr) + ") and isJobWorkOutward not in ('" + IIf(isJobWorkOutward, "1", "0") + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "PI No:" + clsCommon.myCstr(dr("PI_No")) + " should be of " + IIf(isJobWorkOutward, "", " non ") + " job work outward"
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function ReturnQryAllPurchase(ByVal obj As clsPurchaseReco) As String
        Dim qry As String = " SELECT DISTINCT 'PI' As Code , 'Purchase Invoice' As Name,SIH.Bill_To_Location AS Location_Code ,SIH.PI_No as [Document Code],SIH.PI_Date  as [Document Date]," &
                            " SIH.Vendor_Code as [Vendor Code],AP.Document_No AS AP_DOC_NO,SIH.Status " &
                            " From TSPL_PI_DETAIL SID INNER JOIN TSPL_PI_HEAD SIH ON SID.PI_No=SIH.PI_No " &
                            " LEFT JOIN TSPL_VENDOR_INVOICE_HEAD AP ON SIH.PI_No=AP.Against_POInvoice_No " &
                            " WHERE SIH.Status=1 AND COALESCE(SIH.Document_Type,'')<>'MT' AND  CAST(SIH.PI_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' "
        If Not obj.Transaction Is Nothing AndAlso obj.Transaction.Count > 0 Then
            qry = qry & " and 'PI' in (" & clsCommon.GetMulcallString(obj.Transaction) & ")"
        End If
        If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
            qry = qry & " and SIH.Bill_To_Location in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        End If
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry = qry & " and SIH.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        If Not obj.Item_Code Is Nothing AndAlso obj.Item_Code.Count > 0 Then
            qry = qry & " and SID.Item_Code in (" & clsCommon.GetMulcallString(obj.Item_Code) & ")"
        End If
        If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
            qry = qry & " and SIH.PI_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        End If
        qry = qry & Environment.NewLine & " UNION ALL " &
                            " Select DISTINCT 'MCC-PI' As Code , 'Milk Purchase Invoice' As Name,SIH.MCC_CODE as Location_Code ,SIH.DOC_CODE as [Document Code],SIH.DOC_DATE  as [Document Date], " &
                            " SIH.VSP_CODE as [Vendor Code],AP.Document_No AS AP_DOC_NO,SIH.Posted as Status " &
                            " From TSPL_MILK_PURCHASE_INVOICE_DETAIL SID INNER JOIN TSPL_MILK_PURCHASE_INVOICE_HEAD SIH ON SID.DOC_CODE=SIH.DOC_CODE " &
                            " LEFT JOIN TSPL_VENDOR_INVOICE_HEAD AP ON SIH.DOC_CODE=AP.Against_MillkPurchaseInvoice_No " &
                            " WHERE SIH.Posted=1 AND  CAST(SIH.DOC_DATE AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' "
        If Not obj.Transaction Is Nothing AndAlso obj.Transaction.Count > 0 Then
            qry = qry & " and 'MCC-PI' in (" & clsCommon.GetMulcallString(obj.Transaction) & ")"
        End If
        If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
            qry = qry & " and SIH.MCC_CODE in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        End If
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry = qry & " and SIH.VSP_CODE in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        If Not obj.Item_Code Is Nothing AndAlso obj.Item_Code.Count > 0 Then
            qry = qry & " and SID.Item_Code in (" & clsCommon.GetMulcallString(obj.Item_Code) & ")"
        End If
        If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
            qry = qry & " and SIH.DOC_CODE in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        End If

        qry = qry & Environment.NewLine & " union all " &
                            " SELECT DISTINCT 'Bulk-PI' As Code , 'Bulk Milk Purchase' As Name,SIH.Loc_Code as Location_Code ,SIH.DOC_NO as [Document Code],SIH.DOC_DATE  as [Document Date]," &
                            " SIH.vendor_code as [Vendor Code],AP.Document_No AS AP_DOC_NO,SIH.isPosted as Status " &
                            " From tspl_Bulk_milk_purchase_Invoice_Detail SID INNER JOIN tspl_Bulk_milk_purchase_Invoice_head SIH ON SID.DOC_NO=SIH.DOC_NO " &
                            " LEFT JOIN TSPL_VENDOR_INVOICE_HEAD AP ON SIH.DOC_NO=AP.Against_BulkMillkPurchaseInvoice_No " &
                            " WHERE SIH.isPosted=1 AND  CAST(SIH.DOC_DATE AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' "

        If Not obj.Transaction Is Nothing AndAlso obj.Transaction.Count > 0 Then
            qry = qry & " and 'Bulk-PI' in (" & clsCommon.GetMulcallString(obj.Transaction) & ")"
        End If
        If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
            qry = qry & " and SIH.Loc_Code in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        End If
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry = qry & " and SIH.vendor_code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        If Not obj.Item_Code Is Nothing AndAlso obj.Item_Code.Count > 0 Then
            qry = qry & " and SID.Item_Code in (" & clsCommon.GetMulcallString(obj.Item_Code) & ")"
        End If
        If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
            qry = qry & " and SIH.DOC_NO in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        End If

        qry = qry & Environment.NewLine & " union all " &
                            " SELECT DISTINCT 'Bulk-PI-Ret' As Code , 'Bulk Milk Purchase Return' As Name,SIH.Loc_Code as Location_Code ,SIH.Pur_Return_No as [Document Code],SIH.Pur_Return_Date  as [Document Date]," &
                            " SIH.vendor_code as [Vendor Code],AP.Document_No AS AP_DOC_NO,SIH.isPosted as Status " &
                            " From TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL SID INNER JOIN TSPL_BULK_MILK_PURCHASE_RETURN_HEAD SIH ON SID.Pur_Return_No=SIH.Pur_Return_No " &
                            " LEFT JOIN TSPL_VENDOR_INVOICE_HEAD AP ON SIH.Pur_Return_No=AP.RefDocNo " &
                            " WHERE SIH.isPosted=1 AND  CAST(SIH.Pur_Return_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' "

        If Not obj.Transaction Is Nothing AndAlso obj.Transaction.Count > 0 Then
            qry = qry & " and 'Bulk-PI-Ret' in (" & clsCommon.GetMulcallString(obj.Transaction) & ")"
        End If
        If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
            qry = qry & " and SIH.Loc_Code in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        End If
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry = qry & " and SIH.vendor_code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        If Not obj.Item_Code Is Nothing AndAlso obj.Item_Code.Count > 0 Then
            qry = qry & " and SID.Item_Code in (" & clsCommon.GetMulcallString(obj.Item_Code) & ")"
        End If
        If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
            qry = qry & " and SIH.Pur_Return_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        End If
        If obj.IncludeAllDoc = False Then
            qry = qry & Environment.NewLine & " union all " &
                           " SELECT DISTINCT 'MCC Transfer' As Code , 'MCC Transfer' As Name,SIH.location_code as Location_Code ,SIH.Receipt_Challan_No as [Document Code],SIH.Receipt_Challan_Date  as [Document Date]," &
                           " '' as [Vendor Code],NULL AS AP_DOC_NO,SIH.isPosted as Status From TSPL_MILK_TRANSFER_IN SIH left join TSPL_MCC_Dispatch_Challan SID on SIH.Dispatch_Challan_No=SID.Chalan_NO " &
                           " WHERE SIH.isPosted=1 AND  CAST(SIH.Receipt_Challan_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"

            If Not obj.Transaction Is Nothing AndAlso obj.Transaction.Count > 0 Then
                qry = qry & " and 'MCC Transfer' in (" & clsCommon.GetMulcallString(obj.Transaction) & ")"
            End If
            If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
                qry = qry & " and SIH.location_code in (" & clsCommon.GetMulcallString(obj.Location) & ")"
            End If
            If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
                qry = qry & " and 1=2 "
            End If
            If Not obj.Item_Code Is Nothing AndAlso obj.Item_Code.Count > 0 Then
                qry = qry & " and SID.Item_Code in (" & clsCommon.GetMulcallString(obj.Item_Code) & ")"
            End If
            If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
                qry = qry & " and SIH.Receipt_Challan_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
            End If

        End If

        If obj.IncludeAllDoc = False Then
            qry = qry & Environment.NewLine & " union all " &
                           " SELECT DISTINCT 'Transfer' As Code , 'Transfer' As Name,SIH.From_Location as Location_Code ,SIH.Document_No as [Document Code],SIH.Document_Date  as [Document Date]," &
                           " SIH.To_Location as [Vendor Code],NULL AS AP_DOC_NO,SIH.Status as Status From TSPL_TRANSFER_ORDER_HEAD SIH  INNER JOIN TSPL_TRANSFER_ORDER_DETAIL SID ON SIH.Document_No=SID.Document_No" &
                           " WHERE SIH.Status =1 AND  CAST(SIH.Document_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' and LEN(COALESCE(SIH.TransferOutNo ,''))>0"

            If Not obj.Transaction Is Nothing AndAlso obj.Transaction.Count > 0 Then
                qry = qry & " and 'Transfer' in (" & clsCommon.GetMulcallString(obj.Transaction) & ")"
            End If
            If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
                qry = qry & " and SIH.From_Location in (" & clsCommon.GetMulcallString(obj.Location) & ")"
            End If
            If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
                qry = qry & " and 1=2 "
            End If
            If Not obj.Item_Code Is Nothing AndAlso obj.Item_Code.Count > 0 Then
                qry = qry & " and SID.Item_Code in (" & clsCommon.GetMulcallString(obj.Item_Code) & ")"
            End If
            If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
                qry = qry & " and SIH.Document_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
            End If
        End If

        'qry = qry & Environment.NewLine & " union all " & _
        '                    " SELECT DISTINCT 'Transfer Return' As Code , 'Transfer Return' As Name ,SIH.Document_No as [Document Code],SIH.Document_Date  as [Document Date]," & _
        '                    " SIH.To_Location as [Vendor Code],NULL AS AP_DOC_NO,SIH.Status as Status From TSPL_TRANSFER_ORDER_HEAD SIH " & _
        '                    " WHERE  CAST(SIH.Document_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' and LEN(COALESCE(SIH.TransferOutNo ,''))>0"

        'If Not obj.Transaction Is Nothing AndAlso obj.Transaction.Count > 0 Then
        '    qry = qry & " and 'Transfer Return' in (" & clsCommon.GetMulcallString(obj.Transaction) & ")"
        'End If
        'If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
        '    qry = qry & " and SIH.From_Location in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        'End If
        'If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
        '    qry = qry & " and 1=2 "
        'End If
        'If Not obj.Item_Code Is Nothing AndAlso obj.Item_Code.Count > 0 Then
        '    qry = qry & " and SID.Item_Code in (" & clsCommon.GetMulcallString(obj.Item_Code) & ")"
        'End If
        If obj.IncludeAllDoc = False Then
            qry = qry & Environment.NewLine & " UNION ALL " &
                                        " SELECT DISTINCT 'Transfer Return'  As Code ,'Transfer Return' As Name,SID.From_Location as Location_Code ,SIH.Document_No as [Document Code],SIH.Document_Date  as [Document Date],SID.To_Location as [Customer Code]," &
                                        " NULL AS AP_DOC_NO,1 AS Status FROM TSPL_TRANSFER_RETURN SIH inner join (select SIH.Document_No,SID.Item_Code,SIH.From_Location,SIH.To_Location from TSPL_TRANSFER_ORDER_DETAIL SID " &
                                        " inner join TSPL_TRANSFER_ORDER_HEAD SIH ON SID.Document_No=SIH.Document_No) SID ON SIH.Transfer_No=SID.Document_No where 2=2 and CAST(SIH.Document_Date AS DATE) " &
                                        " BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' "

            If Not obj.Transaction Is Nothing AndAlso obj.Transaction.Count > 0 Then
                qry = qry & " and 'Transfer Return' in (" & clsCommon.GetMulcallString(obj.Transaction) & ")"
            End If
            If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
                qry = qry & " and SID.From_Location in (" & clsCommon.GetMulcallString(obj.Location) & ")"
            End If
            If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
                qry = qry & " and 1=2 "
            End If
            If Not obj.Item_Code Is Nothing AndAlso obj.Item_Code.Count > 0 Then
                qry = qry & " and SID.Item_Code in (" & clsCommon.GetMulcallString(obj.Item_Code) & ")"
            End If
            If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
                qry = qry & " and SIH.Document_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
            End If
        End If


        qry = qry & Environment.NewLine & " UNION ALL " &
                            " SELECT DISTINCT 'Return' As Code , 'Purchase Return' As Name,SIH.Bill_To_Location as Location_Code ,SIH.PR_No as [Document Code],SIH.PR_Date  as [Document Date]," &
                            " SIH.vendor_code as [Vendor Code],AP.Document_No AS AP_DOC_NO,SIH.Status " &
                            " From TSPL_PR_DETAIL SID INNER JOIN TSPL_PR_HEAD SIH ON SID.PR_No=SIH.PR_No " &
                            " LEFT JOIN TSPL_VENDOR_INVOICE_HEAD AP ON SIH.PR_No=AP.Against_PurchaseReturn_No " &
                            " WHERE SIH.Status=1 AND  CAST(SIH.PR_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' "

        If Not obj.Transaction Is Nothing AndAlso obj.Transaction.Count > 0 Then
            qry = qry & " and 'Return' in (" & clsCommon.GetMulcallString(obj.Transaction) & ")"
        End If
        If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
            qry = qry & " and SIH.Bill_To_Location in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        End If
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry = qry & " and SIH.vendor_code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        If Not obj.Item_Code Is Nothing AndAlso obj.Item_Code.Count > 0 Then
            qry = qry & " and SID.Item_Code in (" & clsCommon.GetMulcallString(obj.Item_Code) & ")"
        End If
        If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
            qry = qry & " and SIH.PR_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        End If

        qry = qry & Environment.NewLine & " UNION ALL " &
                            " SELECT DISTINCT 'MT' As Code , 'Merchant Trade' As Name,SIH.Bill_To_Location as Location_Code ,SIH.PI_No as [Document Code],SIH.PI_Date  as [Document Date]," &
                            " SIH.vendor_code as [Vendor Code],AP.Document_No AS AP_DOC_NO,SIH.Status " &
                            " From TSPL_PI_DETAIL SID INNER JOIN TSPL_PI_HEAD SIH ON SID.PI_No=SIH.PI_No " &
                            " LEFT JOIN TSPL_VENDOR_INVOICE_HEAD AP ON SIH.PI_No=AP.Against_POInvoice_No " &
                            " WHERE SIH.Status=1 AND  COALESCE(SIH.Document_Type,'')='MT' AND CAST(SIH.PI_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' "

        If Not obj.Transaction Is Nothing AndAlso obj.Transaction.Count > 0 Then
            qry = qry & " and 'MT' in (" & clsCommon.GetMulcallString(obj.Transaction) & ")"
        End If
        If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
            qry = qry & " and SIH.Bill_To_Location in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        End If
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry = qry & " and SIH.vendor_code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        If Not obj.Item_Code Is Nothing AndAlso obj.Item_Code.Count > 0 Then
            qry = qry & " and SID.Item_Code in (" & clsCommon.GetMulcallString(obj.Item_Code) & ")"
        End If
        If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
            qry = qry & " and SIH.PI_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        End If
        If obj.IncludeAllDoc Then
            qry = qry & Environment.NewLine & " UNION ALL " & GetVendorQry(obj)
        End If
        Return qry
    End Function

    Public Shared Function GetPurchaseRecoQry(ByVal obj As clsPurchaseReco) As String
        Dim strRunFinalQry As String = ""
        Dim qry As String = ReturnQryAllPurchase(obj)
        Dim APQry As String = " select coalesce(AP_Doc_No,GL_SOURCE_DOC_NO) AS AP_Doc_No, Vendor_Code ,AP_Date,AP_Account_Code,AP_Account_Desc,AP_Account_Amount," &
                              " GL_VoucherNo, GL_Source_Doc_No, GL_Account_No, GL_Account_Desc, GL_Account_Amount" &
                              " from (select ARI.Document_No as AP_Doc_No,coalesce(ARI.Vendor_Code,JE.CustVend_Code) as Vendor_Code,COALESCE(ARI.Document_Date,JE.Voucher_Date) as AP_Date,ARI.GL_Account_Code as AP_Account_Code,ARI.GL_Account_Desc as AP_Account_Desc," &
                              " ARI.Total_Amount as AP_Account_Amount,JE.Voucher_No as GL_VoucherNo,JE.Source_Doc_No as GL_Source_Doc_No,JE.Account_code as GL_Account_No," &
                              " JE.Account_Desc as GL_Account_Desc,JE.Amount as GL_Account_Amount from ( " &
                              " select JEH.Source_Doc_No,JEH.CustVend_Code,JEH.Voucher_No,JEH.Voucher_Date,JED.Account_code,JED.Detail_Line_No,JED.Account_Desc,JED.Amount,JEH.Segment_code from TSPL_JOURNAL_DETAILS JED INNER JOIN TSPL_JOURNAL_MASTER JEH ON JEH.VOUCHER_NO=JED.VOUCHER_NO) as JE " &
                              " LEFT JOIN (select ARID.Document_No,ARIH.Posting_Date as Document_Date,ARIH.Vendor_Code,ARID.Detail_Line_No,ARID.GL_Account_Code,ARID.GL_Account_Desc,ARID.Amount,ARID.Discount, " &
                              " ARID.Amount_less_Discount,ARID.Total_Tax,ARID.Total_Amount from TSPL_VENDOR_INVOICE_DETAIL ARID " &
                              " INNER JOIN TSPL_VENDOR_INVOICE_HEAD ARIH ON ARID.Document_No=ARIH.Document_No ) as ARI on ARI.Document_No=JE.Source_Doc_No AND ARI.GL_Account_Code=JE.Account_code and ARI.Detail_Line_No=JE.Detail_Line_No) as Fin"

        If obj.Acc_Code IsNot Nothing AndAlso obj.Acc_Code.Count > 0 Then
            APQry = APQry & " AND (coalesce(Fin.AP_Account_Code,'') in (" & clsCommon.GetMulcallString(obj.Acc_Code) & ") or coalesce(Fin.GL_Account_No,'') in (" & clsCommon.GetMulcallString(obj.Acc_Code) & " ) "
        End If

        strRunFinalQry = "select Purchase.[Document Code],convert(varchar,Purchase.[Document Date],103) as [Document Date],Purchase.Name as Trans_Type,AP.Vendor_Code,TSPL_VENDOR_MASTER.vendor_name,AP.AP_Doc_No,AP.AP_Account_Code,AP.AP_Account_Desc,AP.AP_Account_Amount,AP.GL_VoucherNo,AP.GL_Source_Doc_No,AP.GL_Account_No,AP.GL_Account_Desc,AP.GL_Account_Amount,(abs(coalesce(AP.AP_Account_Amount,0))-abs(coalesce(AP.GL_Account_Amount,0))) as [Diff Amount] from (" & qry & ") Purchase " &
           " left join (" & APQry & ") AP on Purchase.AP_Doc_No=coalesce(AP.AP_Doc_No,GL_SOURCE_DOC_NO) " &
           " left join TSPL_VENDOR_MASTER on AP.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code "

        '''' LEFT JOIN TSPL_PAYMENT_DETAIL TPD ON TPH.Payment_No=TPD.Payment_No
        If obj.IncludeAllDoc Then
            strRunFinalQry = strRunFinalQry & Environment.NewLine & " UNION ALL " &
                        " SELECT TPH.Payment_No,convert(varchar,TPH.Payment_Date) as Document_Date,'Payment' AS Trans_Type,TPH.Vendor_Code,TPH.Vendor_Name,Null AS AP_DOC_NO," &
                        " (CASE WHEN JED.Reco_Control_Account='V'  THEN JED.Account_code ELSE NULL END) AS Vendor_Account_Code, " &
                        " (CASE WHEN JED.Reco_Control_Account='V'  THEN JED.Account_Desc ELSE NULL END) AS Vendor_Account_Desc, " &
                        " (CASE WHEN JED.Reco_Control_Account='V'  THEN JED.Amount ELSE NULL END) AS Vendor_Account_Amount, " &
                        " JE.Voucher_No as GL_VoucherNo,JE.Source_Doc_No AS GL_Source_Doc_No,JED.Account_code AS GL_Account_No, " &
                        " JED.Account_Desc AS GL_Account_Desc,JED.Amount AS GL_Account_Amount,(abs(coalesce((CASE WHEN JED.Reco_Control_Account='V'  THEN JED.Amount ELSE NULL END),0))-abs(coalesce(JED.Amount,0))) as [Diff Amount] " &
                        " FROM TSPL_PAYMENT_HEADER TPH  " &
                        " left join TSPL_JOURNAL_MASTER JE on JE.Source_Doc_No=TPH.Payment_No " &
                        " LEFT JOIN TSPL_JOURNAL_DETAILS JED ON JED.Voucher_No=JE.Voucher_No " &
                        " WHERE TPH.Posted='1' AND LEN(COALESCE(TPH.Vendor_Code,''))>0 " &
                        " and  CAST(TPH.Payment_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' "

            If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " and JE.Segment_code in  (select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (" & clsCommon.GetMulcallString(obj.Location) & "))"
            End If
            If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " and TPH.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
            End If

            If obj.Acc_Code IsNot Nothing AndAlso obj.Acc_Code.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " AND (coalesce(JED.Account_code,'') in (" & clsCommon.GetMulcallString(obj.Acc_Code) & ") "
            End If

            'If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
            '    qry = qry & " and TVIH.Document_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
            'End If

            strRunFinalQry = strRunFinalQry & Environment.NewLine & " UNION ALL " &
                        " select TBR.Document_No,convert(varchar,TBR.Reversal_Date,103) as Document_Date, 'Bank Reverse' as Trans_Type,TBR.Vendor_Code,TBR.Vendor_Name,Null AS AP_DOC_NO," &
                        " (CASE WHEN JED.Reco_Control_Account='V'  THEN JED.Account_code ELSE NULL END) AS Vendor_Account_Code, " &
                        " (CASE WHEN JED.Reco_Control_Account='V'  THEN JED.Account_Desc ELSE NULL END) AS Vendor_Account_Desc, " &
                        " (CASE WHEN JED.Reco_Control_Account='V'  THEN JED.Amount ELSE NULL END) AS Vendor_Account_Amount, " &
                        " JE.Voucher_No as GL_VoucherNo,JE.Source_Doc_No AS GL_Source_Doc_No,JED.Account_code AS GL_Account_No, " &
                        " JED.Account_Desc AS GL_Account_Desc,JED.Amount AS GL_Account_Amount,(abs(coalesce((CASE WHEN JED.Reco_Control_Account='V'  THEN JED.Amount ELSE NULL END),0))-abs(coalesce(JED.Amount,0))) as [Diff Amount] from TSPL_BANK_REVERSE TBR " &
                        " left join TSPL_PAYMENT_HEADER TPH ON TBR.Document_No=TPH.Payment_No " &
                        " left join TSPL_JOURNAL_MASTER JE on JE.Source_Doc_No=TPH.Payment_No " &
                        " LEFT JOIN TSPL_JOURNAL_DETAILS JED ON JED.Voucher_No=JE.Voucher_No " &
                        " WHERE TBR.Post='P' AND LEN(COALESCE(TBR.Vendor_Code,''))>0 and TBR.Reverse_Document='Payments' " &
                        " and  CAST(TBR.Reversal_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' "

            If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " and JE.Segment_code in  (select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (" & clsCommon.GetMulcallString(obj.Location) & "))"
            End If
            If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " and TBR.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
            End If
            If obj.Acc_Code IsNot Nothing AndAlso obj.Acc_Code.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " AND (coalesce(JED.Account_code,'') in (" & clsCommon.GetMulcallString(obj.Acc_Code) & ") "
            End If


            '' PAYMENT ADJUSTMENT
            strRunFinalQry = strRunFinalQry & Environment.NewLine & " UNION ALL " &
                       " SELECT TPAH.Adjustment_No,convert(varchar,TPAH.Adjustment_Date,103) as Document_Date,'AP Adjustment' as Trans_Type,TPAH.Vendor_No,TPAH.Vendor_Name,TPAH.Doc_No AS AP_DOC_NO," &
                       " (CASE WHEN JED.Reco_Control_Account='V'  THEN JED.Account_code ELSE NULL END) AS Vendor_Account_Code, " &
                       " (CASE WHEN JED.Reco_Control_Account='V'  THEN JED.Account_Desc ELSE NULL END) AS Vendor_Account_Desc, " &
                       " (CASE WHEN JED.Reco_Control_Account='V'  THEN JED.Amount ELSE NULL END) AS Vendor_Account_Amount, " &
                       " JE.Voucher_No as GL_VoucherNo,JE.Source_Doc_No AS GL_Source_Doc_No,JED.Account_code AS GL_Account_No, " &
                       " JED.Account_Desc AS GL_Account_Desc,JED.Amount AS GL_Account_Amount,(abs(coalesce((CASE WHEN JED.Reco_Control_Account='V'  THEN JED.Amount ELSE NULL END),0))-abs(coalesce(JED.Amount,0))) as [Diff Amount] " &
                       " FROM TSPL_Payment_Adjustment_Header TPAH inner join TSPL_VENDOR_INVOICE_HEAD TVIH ON TPAH.Doc_No=TVIH.Document_No " &
                       " left join TSPL_JOURNAL_MASTER JE on JE.Source_Doc_No=TPAH.Adjustment_No " &
                       " LEFT JOIN TSPL_JOURNAL_DETAILS JED ON JED.Voucher_No=JE.Voucher_No" &
                       " where 2=2 and  CAST(TPAH.Adjustment_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' "

            If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " and JE.Segment_code in  (select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (" & clsCommon.GetMulcallString(obj.Location) & "))"
            End If
            If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " and TPAH.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
            End If
            If obj.Acc_Code IsNot Nothing AndAlso obj.Acc_Code.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " AND (coalesce(JED.Account_code,'') in (" & clsCommon.GetMulcallString(obj.Acc_Code) & ") "
            End If
        End If

        Return strRunFinalQry
    End Function

    Public Shared Function GetVendorRecoQryOLD(ByVal obj As clsPurchaseReco) As String
        Dim strRunFinalQry As String = ""
        Dim qry As String = ReturnQryAllPurchase(obj)
        Dim APQry As String = " select COALESCE(TSPL_VENDOR_INVOICE_HEAD.Document_No,JE.Source_Doc_No) AS AP_Doc_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code," &
                " TSPL_VENDOR_INVOICE_HEAD.Posting_Date AS AP_DATE,JE.Account_code AS AP_Account_Code,JE.Account_Desc AS AP_Account_Desc, " &
                " abs(abs(abs(coalesce(TDS_Actual_Amount,0))-TSPL_VENDOR_INVOICE_HEAD.Document_Total)-(case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered=1 and TSPL_VENDOR_INVOICE_HEAD.RCM=0 then 0 else TSPL_VENDOR_INVOICE_HEAD.Total_Tax end)) AS AP_Account_Amount,JE.Voucher_No AS GL_VoucherNo,JE.SOURCE_DOC_NO,JE.Account_code AS GL_Account_No, " &
                " JE.Account_Desc AS GL_Account_Desc,JE.Amount AS GL_Account_Amount " &
                " from TSPL_VENDOR_INVOICE_HEAD " &
                " left join (select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Doc_No,max(TSPL_JOURNAL_MASTER.Segment_code) as Segment_code,max(TSPL_JOURNAL_DETAILS.Account_code) as Account_code,max(TSPL_JOURNAL_DETAILS.Account_Desc) as Account_Desc, " &
                " sum(TSPL_JOURNAL_DETAILS.Amount) as Amount from TSPL_JOURNAL_DETAILS inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No " &
                " where TSPL_JOURNAL_DETAILS.Reco_Control_Account='V'  group by TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Doc_No) as JE on TSPL_VENDOR_INVOICE_HEAD.Document_No=JE.Source_Doc_No where TSPL_VENDOR_INVOICE_HEAD.Posting_Date is not null  "

        If obj.Acc_Code IsNot Nothing AndAlso obj.Acc_Code.Count > 0 Then
            APQry = APQry & " AND (coalesce(JE.Account_code,'') in (" & clsCommon.GetMulcallString(obj.Acc_Code) & ")) "
        End If

        strRunFinalQry = "select Purchase.[Document Code],convert(varchar,Purchase.[Document Date],103) as [Document Date],Purchase.Name as Trans_Type,AP.Document_Type as [Document Type],AP.Vendor_Code,TSPL_VENDOR_MASTER.vendor_name,AP.AP_Doc_No,AP.AP_Account_Code,AP.AP_Account_Desc,AP.AP_Account_Amount,AP.GL_VoucherNo,AP.Source_Doc_No,AP.GL_Account_No,AP.GL_Account_Desc,ABS(AP.GL_Account_Amount) as GL_Account_Amount,(abs(coalesce(AP.AP_Account_Amount,0))-abs(coalesce(AP.GL_Account_Amount,0))) as [Diff Amount],TSPL_VENDOR_MASTER.Vendor_Account as [Vendor Account Set],TSPL_VENDOR_MASTER.Vendor_Group_Code as [Vendor Group] from (" & qry & ") Purchase " &
           " left join (" & APQry & ") AP on Purchase.AP_Doc_No=coalesce(AP.AP_Doc_No,AP.Source_Doc_No) " &
           " left join TSPL_VENDOR_MASTER on AP.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code where 2=2 and  len(coalesce(AP.Vendor_Code,''))>0"
        If obj.Account_Set IsNot Nothing AndAlso obj.Account_Set.Count > 0 Then
            strRunFinalQry = strRunFinalQry & " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Account,'') in (" & clsCommon.GetMulcallString(obj.Account_Set) & ") "
        End If
        If obj.Vendor_Group IsNot Nothing AndAlso obj.Vendor_Group.Count > 0 Then
            strRunFinalQry = strRunFinalQry & " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Group_Code,'') in (" & clsCommon.GetMulcallString(obj.Vendor_Group) & ") "
        End If

        If obj.IncludeAllDoc Then
            strRunFinalQry = strRunFinalQry & Environment.NewLine & " UNION ALL " &
                        " SELECT TPH.Payment_No,convert(varchar,TPH.Payment_Date,103) as Document_Date,'Payment' AS Trans_Type,TPH.Payment_Type as [Document Type],TPH.Vendor_Code,TPH.Vendor_Name,Null AS AP_DOC_NO," &
                        " JE.Account_code AS Vendor_Account_Code, " &
                        " JE.Account_Desc AS Vendor_Account_Desc, " &
                        " (TPH.Payment_Amount+coalesce(TDS_Amount,0)) AS Vendor_Account_Amount, " &
                        " JE.Voucher_No as GL_VoucherNo,JE.Source_Doc_No AS GL_Source_Doc_No,JE.Account_code AS GL_Account_No, " &
                        " JE.Account_Desc AS GL_Account_Desc,abs(JE.Amount) AS GL_Account_Amount,((abs(coalesce(TPH.Payment_Amount,0))+abs(coalesce(TDS_Amount,0)))-abs(coalesce(JE.Amount,0))) as [Diff Amount],TSPL_VENDOR_MASTER.Vendor_Account as [Vendor Account Set],TSPL_VENDOR_MASTER.Vendor_Group_Code as [Vendor Group] " &
                        " FROM TSPL_PAYMENT_HEADER TPH  " &
                        " left join (select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Doc_No,max(TSPL_JOURNAL_MASTER.Segment_code) as Segment_code,max(TSPL_JOURNAL_DETAILS.Account_code) as Account_code,max(TSPL_JOURNAL_DETAILS.Account_Desc) as Account_Desc, " &
                        " sum(TSPL_JOURNAL_DETAILS.Amount) as Amount from TSPL_JOURNAL_DETAILS inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No " &
                        " where TSPL_JOURNAL_DETAILS.Reco_Control_Account='V'  group by TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Doc_No) as JE ON TPH.Payment_No=JE.Source_Doc_No " &
                        " left join TSPL_VENDOR_MASTER on TPH.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " &
                        " WHERE TPH.Posted='1' AND LEN(COALESCE(TPH.Vendor_Code,''))>0 " &
                        " and  CAST(TPH.Payment_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'  and TPH.Payment_Type not in ('AD')"

            If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " and JE.Segment_code in  (select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (" & clsCommon.GetMulcallString(obj.Location) & "))"
            End If
            If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " and TPH.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
            End If

            If obj.Acc_Code IsNot Nothing AndAlso obj.Acc_Code.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " AND (coalesce(JE.Account_code,'') in (" & clsCommon.GetMulcallString(obj.Acc_Code) & ")) "
            End If

            If obj.Account_Set IsNot Nothing AndAlso obj.Account_Set.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Account,'') in (" & clsCommon.GetMulcallString(obj.Account_Set) & ") "
            End If
            If obj.Vendor_Group IsNot Nothing AndAlso obj.Vendor_Group.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Group_Code,'') in (" & clsCommon.GetMulcallString(obj.Vendor_Group) & ") "
            End If
            strRunFinalQry = strRunFinalQry & Environment.NewLine & " UNION ALL " &
                        " select TBR.Reverse_Code,convert(varchar,TBR.Reversal_Date,103) as Document_Date, 'Bank Reverse' as Trans_Type,TBR.Source_Type AS [Document Type],TBR.Vendor_Code,TBR.Vendor_Name,Null AS AP_DOC_NO," &
                        " JE.Account_code AS Vendor_Account_Code, " &
                        " JE.Account_Desc AS Vendor_Account_Desc, " &
                        " TBR.Amount AS Vendor_Account_Amount, " &
                        " JE.Voucher_No as GL_VoucherNo,JE.Source_Doc_No AS GL_Source_Doc_No,JE.Account_code AS GL_Account_No, " &
                        " JE.Account_Desc AS GL_Account_Desc,abs(JE.Amount) AS GL_Account_Amount,(abs(COALESCE(TBR.Amount,0))-abs(coalesce(JE.Amount,0))) as [Diff Amount],TSPL_VENDOR_MASTER.Vendor_Account as [Vendor Account Set],TSPL_VENDOR_MASTER.Vendor_Group_Code as [Vendor Group] from TSPL_BANK_REVERSE TBR " &
                        " left join TSPL_PAYMENT_HEADER TPH ON TBR.Document_No=TPH.Payment_No " &
                        " left join (select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Doc_No,max(TSPL_JOURNAL_MASTER.Segment_code) as Segment_code,max(TSPL_JOURNAL_DETAILS.Account_code) as Account_code,max(TSPL_JOURNAL_DETAILS.Account_Desc) as Account_Desc, " &
                        " sum(TSPL_JOURNAL_DETAILS.Amount) as Amount from TSPL_JOURNAL_DETAILS inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No " &
                        " where TSPL_JOURNAL_DETAILS.Reco_Control_Account='V'  group by TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Doc_No) as JE ON TBR.Reverse_Code=JE.Source_Doc_No " &
                        " left join TSPL_VENDOR_MASTER on TBR.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " &
                        " WHERE TBR.Post='P' AND LEN(COALESCE(TBR.Vendor_Code,''))>0 and TBR.Reverse_Document='Payments' " &
                        " and  CAST(TBR.Reversal_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' and TPH.Payment_Type not in ('AD')"

            If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " and JE.Segment_code in  (select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (" & clsCommon.GetMulcallString(obj.Location) & "))"
            End If
            If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " and TBR.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
            End If
            If obj.Acc_Code IsNot Nothing AndAlso obj.Acc_Code.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " AND (coalesce(JE.Account_code,'') in (" & clsCommon.GetMulcallString(obj.Acc_Code) & ")) "
            End If

            If obj.Account_Set IsNot Nothing AndAlso obj.Account_Set.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Account,'') in (" & clsCommon.GetMulcallString(obj.Account_Set) & ") "
            End If
            If obj.Vendor_Group IsNot Nothing AndAlso obj.Vendor_Group.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Group_Code,'') in (" & clsCommon.GetMulcallString(obj.Vendor_Group) & ") "
            End If
            '' PAYMENT ADJUSTMENT
            strRunFinalQry = strRunFinalQry & Environment.NewLine & " UNION ALL " &
                       " SELECT TPAH.Adjustment_No,convert(varchar,TPAH.Adjustment_Date,103) as Document_Date,'AP Adjustment' as Trans_Type,TPAH.Adjust_Type as [Document Type],TPAH.Vendor_No,TPAH.Vendor_Name,TPAH.Doc_No AS AP_DOC_NO," &
                       " JE.Account_code AS Vendor_Account_Code, " &
                       " JE.Account_Desc AS Vendor_Account_Desc, " &
                       " TPAH.Adjustment_Amount AS Vendor_Account_Amount, " &
                       " JE.Voucher_No as GL_VoucherNo,JE.Source_Doc_No AS GL_Source_Doc_No,JE.Account_code AS GL_Account_No, " &
                       " JE.Account_Desc AS GL_Account_Desc,abs(JE.Amount) AS GL_Account_Amount,(abs(COALESCE(TPAH.Adjustment_Amount,0))-abs(coalesce(JE.Amount,0))) as [Diff Amount],TSPL_VENDOR_MASTER.Vendor_Account as [Vendor Account Set],TSPL_VENDOR_MASTER.Vendor_Group_Code as [Vendor Group] " &
                       " FROM TSPL_Payment_Adjustment_Header TPAH inner join TSPL_VENDOR_INVOICE_HEAD TVIH ON TPAH.Doc_No=TVIH.Document_No " &
                       " left join (select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Doc_No,max(TSPL_JOURNAL_MASTER.Segment_code) as Segment_code,max(TSPL_JOURNAL_DETAILS.Account_code) as Account_code,max(TSPL_JOURNAL_DETAILS.Account_Desc) as Account_Desc, " &
                       " sum(TSPL_JOURNAL_DETAILS.Amount) as Amount from TSPL_JOURNAL_DETAILS inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No " &
                       " where TSPL_JOURNAL_DETAILS.Reco_Control_Account='V'  group by TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Doc_No) as JE ON TPAH.Adjustment_No=JE.Source_Doc_No " &
                       " left join TSPL_VENDOR_MASTER on TPAH.Vendor_No=TSPL_VENDOR_MASTER.Vendor_Code " &
                       " where 2=2 and  CAST(TPAH.Adjustment_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' "

            If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " and JE.Segment_code in  (select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (" & clsCommon.GetMulcallString(obj.Location) & "))"
            End If
            If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " and TPAH.Vendor_No in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
            End If
            If obj.Acc_Code IsNot Nothing AndAlso obj.Acc_Code.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " AND (coalesce(JE.Account_code,'') in (" & clsCommon.GetMulcallString(obj.Acc_Code) & ")) "
            End If
            If obj.Account_Set IsNot Nothing AndAlso obj.Account_Set.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Account,'') in (" & clsCommon.GetMulcallString(obj.Account_Set) & ") "
            End If
            If obj.Vendor_Group IsNot Nothing AndAlso obj.Vendor_Group.Count > 0 Then
                strRunFinalQry = strRunFinalQry & " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Group_Code,'') in (" & clsCommon.GetMulcallString(obj.Vendor_Group) & ") "
            End If
        End If
        If obj.ShowMismatchDoc = True Then
            strRunFinalQry = "select * from (" & strRunFinalQry & ") as t1 where abs([Diff Amount])>0"
        End If
        Return strRunFinalQry
    End Function

    Public Shared Function GetVendorRecoQry(ByVal obj As clsPurchaseReco) As String
        Dim qry As String = "select Account_code,Account_Desc,DocNo,RefDocNo,[Document Date],TransType,DocumentType,Vendor_Code,Vendor_Name,Vendor_Account,Vendor_Group_Code,Group_Desc,DRAmount,CRAmount,DRAmount-CRAmount as NetAmount,Voucher_No,Voucher_Date,GLDrAmount,GLCrAmount,(GLDrAmount-GLCrAmount) as GLNetAmount,isnull((DRAmount-CRAmount)-(GLDRAmount-GLCrAmount),0) as DiffAmount from (" + Environment.NewLine + Environment.NewLine + Environment.NewLine +
        "select GL.Account_code,gl.Account_Desc,xx.DocNo,xx.[Document Date],xx.TransType,xx.DocumentType,xx.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Vendor_Account, TSPL_VENDOR_MASTER.Vendor_Group_Code,TSPL_VENDOR_GROUP.Group_Desc,xx.RefDocNo,xx.DRAmount as DRAmount,xx.CRAmount as CRAmount, GL.Voucher_No,GL.Voucher_Date, isnull( GL.DRAmount,0) as GLDrAmount,isnull(GL.CRAmount,0) as GLCrAmount from (" + Environment.NewLine + Environment.NewLine +
        "SELECT   TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as [Document Date],case " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition,''))>0 then 'Acquisition' " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work,''))>0 then 'Asset' " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No,''))>0 then 'Bulk Milk Purchase' " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No,''))>0  then 'Milk Purchase Invoice'  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0  then 'Purchase Invoice'  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,''))>0  then 'Purchase Return'  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No,''))>0  then 'VSP-Issue/Return' " + Environment.NewLine +
          "when len(isnull(RefDocType,''))>0 then RefDocType " + Environment.NewLine +
          "else  'AP Invoice' end as TransType" + Environment.NewLine +
          ",case " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition,''))>0 then 'AQI' " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work,''))>0 then 'Asset' " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No,''))>0 then 'Bulk-PI' " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No,''))>0  then 'MCC-PI'  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0  then 'PI'  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,''))>0  then 'PR'  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No,''))>0  then 'VSP-IR' " + Environment.NewLine +
          "when len(isnull(RefDocType,''))>0 then RefDocType " + Environment.NewLine +
          "else  'AP' end As DocumentType ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code," + Environment.NewLine +
          "case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition,''))>0 then TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition" + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work,''))>0 then TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No,''))>0 then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No,''))>0  then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0  then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,''))>0  then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No" + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No,''))>0  then TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo,''))>0 then TSPL_VENDOR_INVOICE_HEAD.RefDocNo else  '' end as RefDocNo" + Environment.NewLine +
         ", cast(TSPL_VENDOR_INVOICE_HEAD.ConvRate*(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('D') then Document_Total - (case when TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 then 0 else isnull(TDS_Actual_Amount,0) end) - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end ) as decimal(18,2)) as DRAmount" + Environment.NewLine +
",cast(TSPL_VENDOR_INVOICE_HEAD.ConvRate*(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('I','C') Then document_total - (case when TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 then 0 else isnull(TDS_Actual_Amount,0) end) - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then( " + Environment.NewLine +
" case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end " + Environment.NewLine +
" +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end " + Environment.NewLine +
" +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end " + Environment.NewLine +
" +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end " + Environment.NewLine +
" +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end " + Environment.NewLine +
" +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end " + Environment.NewLine +
"  ) else 0 end)  else 0 end)as decimal(18,2)) as CRAmount,null as Account" + Environment.NewLine +
         " from  TSPL_VENDOR_INVOICE_HEAD " + Environment.NewLine +
         " where cast(TSPL_VENDOR_INVOICE_HEAD.Posting_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
            qry = qry & " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        End If
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry = qry & " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If

        If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
            qry = qry & " and TSPL_VENDOR_INVOICE_HEAD.Document_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        End If
        '--PaymentHeader
        qry += Environment.NewLine + " union all " + Environment.NewLine +
        "SELECT TSPL_PAYMENT_HEADER.Payment_No as DocNo,TSPL_PAYMENT_HEADER.Payment_Date as Document_Date,'Payment' AS TransType,TSPL_PAYMENT_HEADER.Payment_Type as DocumentType,TSPL_PAYMENT_HEADER.Vendor_Code,Null AS RefDocNo , cast((((case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 else (TSPL_PAYMENT_HEADER.Payment_Amount+coalesce(TDS_Amount,0)) end ) * TSPL_PAYMENT_HEADER.ConvRate) + TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT-TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT) as decimal(18,2)) AS DrAmount,cast(((case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then (TSPL_PAYMENT_HEADER.Payment_Amount+coalesce(TDS_Amount,0)) else 0 end)*TSPL_PAYMENT_HEADER.ConvRate) as decimal(18,2))  AS CRAmount,null as Account " + Environment.NewLine +
        "FROM  TSPL_PAYMENT_HEADER      " + Environment.NewLine +
        "left join TSPL_VENDOR_MASTER on TSPL_PAYMENT_HEADER.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code  " + Environment.NewLine +
        "WHERE TSPL_PAYMENT_HEADER.Is_Security=0 and TSPL_PAYMENT_HEADER.Payment_Type not in ('PY','AD') and  TSPL_PAYMENT_HEADER.Posted='1' AND LEN(COALESCE(TSPL_PAYMENT_HEADER.Vendor_Code,''))>0  and  CAST(TSPL_PAYMENT_HEADER.Payment_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'" + Environment.NewLine
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry += " and TSPL_PAYMENT_HEADER.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        '--PaymentDetail ''richa BHA/13/03/19-000840 use DRAmount and CrAmount as seperate column in inner query
        qry += Environment.NewLine + Environment.NewLine + " union all " + Environment.NewLine +
        "select DocNo,max(Document_Date) as Document_Date,TransType,DocumentType,Vendor_Code,Null as RefDocNo, " + Environment.NewLine +
 " sum(DrAmount) as DrAmount,sum(CrAmount) as CrAmount,Account from (" + Environment.NewLine +
"select DocNo,Document_Date,TransType,DocumentType,Vendor_Code,RefDocNo, DrAmount,CrAmount,GLFinal.Account_Code as Account from (" + Environment.NewLine +
"SELECT TSPL_PAYMENT_HEADER.Payment_No as DocNo,TSPL_PAYMENT_HEADER.Payment_Date as Document_Date,'Payment' AS TransType,TSPL_PAYMENT_HEADER.Payment_Type as DocumentType,TSPL_PAYMENT_HEADER.Vendor_Code,Null AS RefDocNo ,case when len(isnull(HeadPaymentTable.Payment_No,''))>0 then HeadPaymentTable.Location_GL_Code when len(isnull(HeadAPInvTable.Document_No,''))>0 then HeadAPInvTable.Loc_Code else null end as SegCode,TSPL_PAYMENT_HEADER.Credit_Account as AccountCode,0 as DrAmount,1*cast( case when len(isnull(HeadPaymentTable.Payment_No,''))>0 then TSPL_PAYMENT_HEADER.Payment_Amount*HeadPaymentTable.ConvRate   when len(isnull(HeadAPInvTable.Document_No,''))>0 then TSPL_PAYMENT_HEADER.Payment_Amount*HeadAPInvTable.ConvRate else 0 end as decimal(18,2)) as CrAmount" + Environment.NewLine +
        "FROM TSPL_PAYMENT_HEADER" + Environment.NewLine +
        "left join TSPL_VENDOR_MASTER on TSPL_PAYMENT_HEADER.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code  " + Environment.NewLine +
        "left outer join TSPL_VENDOR_INVOICE_HEAD as HeadAPInvTable on HeadAPInvTable.Document_No=TSPL_PAYMENT_HEADER.Applied_Payment" + Environment.NewLine +
        "left outer join TSPL_PAYMENT_HEADER as HeadPaymentTable on HeadPaymentTable.Payment_No=TSPL_PAYMENT_HEADER.Applied_Payment" + Environment.NewLine +
        "WHERE TSPL_PAYMENT_HEADER.Is_Security=0 and TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AD') and  TSPL_PAYMENT_HEADER.Posted='1' AND LEN(COALESCE(TSPL_PAYMENT_HEADER.Vendor_Code,''))>0  and  CAST(TSPL_PAYMENT_HEADER.Payment_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'" + Environment.NewLine
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry += " and TSPL_PAYMENT_HEADER.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        If Not obj.IncludeApplyDocumentPayment Then
            qry += "  and TSPL_PAYMENT_HEADER.Payment_Type<>'AD' "
        End If
        qry += Environment.NewLine + " union all " + Environment.NewLine +
         "SELECT TSPL_PAYMENT_HEADER.Payment_No as DocNo,TSPL_PAYMENT_HEADER.Payment_Date as Document_Date,'Payment' AS TransType,TSPL_PAYMENT_HEADER.Payment_Type as DocumentType,TSPL_PAYMENT_HEADER.Vendor_Code,Null AS RefDocNo,case when len(isnull(HeadPaymentTable.Payment_No,''))>0 then HeadPaymentTable.Location_GL_Code when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Document_No,''))>0 then TSPL_VENDOR_INVOICE_HEAD.Loc_Code else null end as SegCode , TSPL_PAYMENT_HEADER.Debit_Account as Account,cast((Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End * TSPL_PAYMENT_HEADER.ConvRate)as decimal(18,2))+ (case when TSPL_PAYMENT_DETAIL.Payment_Line_No=1 then TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT-TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT else 0 end) AS DrAmount,0 as CrAmount" + Environment.NewLine +
        "FROM TSPL_PAYMENT_DETAIL" + Environment.NewLine +
        "left join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No  " + Environment.NewLine +
        "left join TSPL_VENDOR_MASTER on TSPL_PAYMENT_HEADER.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code  " + Environment.NewLine +
        "left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No" + Environment.NewLine +
        "left outer join TSPL_PAYMENT_HEADER as HeadPaymentTable on HeadPaymentTable.Payment_No=TSPL_PAYMENT_DETAIL.Document_No " + Environment.NewLine +
        "WHERE TSPL_PAYMENT_HEADER.Is_Security=0 and TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AD') and  TSPL_PAYMENT_HEADER.Posted='1' AND LEN(COALESCE(TSPL_PAYMENT_HEADER.Vendor_Code,''))>0  and  CAST(TSPL_PAYMENT_HEADER.Payment_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'" + Environment.NewLine
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry += " and TSPL_PAYMENT_HEADER.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        If Not obj.IncludeApplyDocumentPayment Then
            qry += "  and TSPL_PAYMENT_HEADER.Payment_Type<>'AD' "
        End If
        qry += ")a" + Environment.NewLine +
"left outer join TSPL_GL_ACCOUNTS as GLTable on GLTable.Account_Code=a.AccountCode " + Environment.NewLine +
"left outer join TSPL_GL_ACCOUNTS as GLFinal on GLFinal.Account_Seg_Code1=GLTable.Account_Seg_Code1 and GLFinal.Account_Seg_Code7=a.segCode" + Environment.NewLine +
")x group by DocNo,TransType,DocumentType,Account,Vendor_Code"
        '--EndOfPaymentDetail


        '--Bank Reverse Head
        qry += Environment.NewLine + Environment.NewLine + "UNION ALL" + Environment.NewLine +
        "select TSPL_BANK_REVERSE.Reverse_Code as DocNo,TSPL_BANK_REVERSE.Reversal_Date as DocDate, 'Bank Reverse' as TransType,TSPL_BANK_REVERSE.Source_Type AS DocumentType,TSPL_BANK_REVERSE.Vendor_Code,Null AS RefDocNo ,cast(((case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then (TSPL_PAYMENT_HEADER.Payment_Amount+coalesce(TDS_Amount,0)) else 0 end )*TSPL_PAYMENT_HEADER.ConvRate) as decimal(18,2))   AS DrAmount, cast((case when isnull(TDS_Amount,0)>0 then  TSPL_BANK_REVERSE.Amount* TSPL_PAYMENT_HEADER.ConvRate else  ((case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 else (TSPL_PAYMENT_HEADER.Payment_Amount+coalesce(TDS_Amount,0)) end  ) * TSPL_PAYMENT_HEADER.ConvRate) + TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT-TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT end  )  as decimal(18,2)) AS CRAmount,null as Account" + Environment.NewLine +
        "from  TSPL_BANK_REVERSE  " + Environment.NewLine +
        "left join TSPL_PAYMENT_HEADER ON TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_HEADER.Payment_No    " + Environment.NewLine +
        "WHERE TSPL_PAYMENT_HEADER.Is_Security=0 and TSPL_PAYMENT_HEADER.Payment_Type not in ('PY','AD') and TSPL_BANK_REVERSE.Post='P' AND LEN(COALESCE(TSPL_BANK_REVERSE.Vendor_Code,''))>0 and TSPL_BANK_REVERSE.Reverse_Document='Payments'  and  CAST(TSPL_BANK_REVERSE.Reversal_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'  " + Environment.NewLine
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry += " and TSPL_BANK_REVERSE.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        '--End of Bank Reverse Head
        '--Bank Reverse Detail
        qry += Environment.NewLine + "UNION ALL" + Environment.NewLine +
            "select DocNo,Document_Date,TransType,DocumentType,Vendor_Code,RefDocNo,CrAmount as DrAmount,DrAmount as CrAmount,Account from (" + Environment.NewLine +
        "select DocNo,max(Document_Date) as Document_Date,TransType,DocumentType,Vendor_Code,Null as RefDocNo" + Environment.NewLine +
",case when sum(amount)<0 then 0 else sum(amount) end as DrAmount,case when sum(amount)<0 then -1*sum(amount) else 0 end as CrAmount,Account from (" + Environment.NewLine +
"select DocNo,Document_Date,TransType,DocumentType,Vendor_Code,RefDocNo, Amount,GLFinal.Account_Code as Account from (" + Environment.NewLine +
"SELECT TSPL_BANK_REVERSE.Reverse_Code as DocNo,TSPL_BANK_REVERSE.Reversal_Date as Document_Date,'Bank Reverse' AS TransType,TSPL_PAYMENT_HEADER.Payment_Type as DocumentType,TSPL_PAYMENT_HEADER.Vendor_Code,Null AS RefDocNo ,case when len(isnull(HeadPaymentTable.Payment_No,''))>0 then HeadPaymentTable.Location_GL_Code when len(isnull(HeadAPInvTable.Document_No,''))>0 then HeadAPInvTable.Loc_Code else null end as SegCode,TSPL_PAYMENT_HEADER.Credit_Account as AccountCode,-1*cast( case when len(isnull(HeadPaymentTable.Payment_No,''))>0 then TSPL_PAYMENT_HEADER.Payment_Amount*HeadPaymentTable.ConvRate   when len(isnull(HeadAPInvTable.Document_No,''))>0 then TSPL_PAYMENT_HEADER.Payment_Amount*HeadAPInvTable.ConvRate else 0 end as decimal(18,2)) as Amount" + Environment.NewLine +
        "from  TSPL_BANK_REVERSE  " + Environment.NewLine +
        "left join TSPL_PAYMENT_HEADER ON TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_HEADER.Payment_No    " + Environment.NewLine +
        "left outer join TSPL_VENDOR_INVOICE_HEAD as HeadAPInvTable on HeadAPInvTable.Document_No=TSPL_PAYMENT_HEADER.Applied_Payment" + Environment.NewLine +
        "left outer join TSPL_PAYMENT_HEADER as HeadPaymentTable on HeadPaymentTable.Payment_No=TSPL_PAYMENT_HEADER.Applied_Payment" + Environment.NewLine +
        "WHERE TSPL_PAYMENT_HEADER.Is_Security=0 and TSPL_BANK_REVERSE.Post='P' AND LEN(COALESCE(TSPL_BANK_REVERSE.Vendor_Code,''))>0 and TSPL_BANK_REVERSE.Reverse_Document='Payments'  and  CAST(TSPL_BANK_REVERSE.Reversal_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' " + Environment.NewLine
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry += " and TSPL_BANK_REVERSE.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        qry += " and TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AD')" + Environment.NewLine +
        " UNION ALL" + Environment.NewLine +
        "SELECT TSPL_BANK_REVERSE.Reverse_Code as DocNo,TSPL_BANK_REVERSE.Reversal_Date as Document_Date,'Bank Reverse' AS TransType,TSPL_PAYMENT_HEADER.Payment_Type as DocumentType,TSPL_PAYMENT_HEADER.Vendor_Code,Null AS RefDocNo,case when len(isnull(HeadPaymentTable.Payment_No,''))>0 then HeadPaymentTable.Location_GL_Code when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Document_No,''))>0 then TSPL_VENDOR_INVOICE_HEAD.Loc_Code else null end as SegCode , TSPL_PAYMENT_HEADER.Debit_Account as Account,cast((Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End * TSPL_PAYMENT_HEADER.ConvRate)as decimal(18,2))+ (case when TSPL_PAYMENT_DETAIL.Payment_Line_No=1 then TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT-TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT else 0 end) AS Amount" + Environment.NewLine +
        "from  TSPL_BANK_REVERSE  " + Environment.NewLine +
        "left join TSPL_PAYMENT_HEADER ON TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_HEADER.Payment_No    " + Environment.NewLine +
        "right join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No =TSPL_PAYMENT_HEADER.Payment_No" + Environment.NewLine +
        "left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No" + Environment.NewLine +
        "left outer join TSPL_PAYMENT_HEADER as HeadPaymentTable on HeadPaymentTable.Payment_No=TSPL_PAYMENT_DETAIL.Document_No " + Environment.NewLine +
        "WHERE TSPL_PAYMENT_HEADER.Is_Security=0 and TSPL_BANK_REVERSE.Post='P' AND LEN(COALESCE(TSPL_BANK_REVERSE.Vendor_Code,''))>0 and TSPL_BANK_REVERSE.Reverse_Document='Payments'  and  CAST(TSPL_BANK_REVERSE.Reversal_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' " + Environment.NewLine
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry += " and TSPL_BANK_REVERSE.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        qry += " and TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AD')" + Environment.NewLine +
        ")a" + Environment.NewLine +
"left outer join TSPL_GL_ACCOUNTS as GLTable on GLTable.Account_Code=a.AccountCode " + Environment.NewLine +
"left outer join TSPL_GL_ACCOUNTS as GLFinal on GLFinal.Account_Seg_Code1=GLTable.Account_Seg_Code1 and GLFinal.Account_Seg_Code7=a.segCode" + Environment.NewLine +
")x group by DocNo,TransType,DocumentType,Account,Vendor_Code" + Environment.NewLine +
        " ) xx "
        qry += Environment.NewLine + "UNION ALL" + Environment.NewLine
        '--End of Bank Reverse Detail

        'qry += "SELECT  TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo,TSPL_Payment_Adjustment_Header.Adjustment_Date as DocDate,'AP Adjustment' as TransType,TSPL_Payment_Adjustment_Header.Adjust_Type as DocumentType,TSPL_Payment_Adjustment_Header.Vendor_No,null AS RefDocNo," + Environment.NewLine + _
        '"case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') then  TSPL_Payment_Adjustment_Header.Adjustment_Amount else 0 end AS DRAmount," + Environment.NewLine + _
        '"case when TSPL_VENDOR_INVOICE_HEAD.Document_Type in('I','C') then  0 else TSPL_Payment_Adjustment_Header.Adjustment_Amount end as CRAmount,null as Account  " + Environment.NewLine + _
        '"FROM TSPL_Payment_Adjustment_Header" + Environment.NewLine + _
        '"left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_Payment_Adjustment_Header.Doc_No" + Environment.NewLine + _
        '"where CAST(TSPL_Payment_Adjustment_Header.Adjustment_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'" + Environment.NewLine + Environment.NewLine

        qry += "SELECT  TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo,TSPL_Payment_Adjustment_Header.Adjustment_Date as DocDate,'AP Adjustment' as TransType,TSPL_Payment_Adjustment_Header.Adjust_Type as DocumentType,TSPL_Payment_Adjustment_Header.Vendor_No,null AS RefDocNo," + Environment.NewLine +
        "case when TSPL_Payment_Adjustment_Header.Adjust_Type='P' then  TSPL_Payment_Adjustment_Header.Adjustment_Amount else 0 end AS DRAmount," + Environment.NewLine +
        "case when  TSPL_Payment_Adjustment_Header.Adjust_Type='R' then  TSPL_Payment_Adjustment_Header.Adjustment_Amount else 0 end as CRAmount,null as Account  " + Environment.NewLine +
        "FROM TSPL_Payment_Adjustment_Header" + Environment.NewLine +
        "left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_Payment_Adjustment_Header.Doc_No" + Environment.NewLine +
        "where isnull(tspl_payment_adjustment_header.IS_Post,'')='Y' and CAST(TSPL_Payment_Adjustment_Header.Adjustment_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'" + Environment.NewLine + Environment.NewLine


        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry += " and TSPL_Payment_Adjustment_Header.Vendor_No in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        qry += ")xx" + Environment.NewLine +
        "left outer join (" + Environment.NewLine +
        "select xxx.Voucher_No,max(xxx.Voucher_Date) as Voucher_Date,xxx.Source_Doc_No,max(xxx.Segment_code) as Segment_code, xxx.Account_code,max(xxx.Account_Desc) as Account_Desc," + Environment.NewLine +
        "sum(xxx.Amount * case when xxx.Amount>0  then 1 else 0 end   ) as DRAmount ," + Environment.NewLine +
        "sum(xxx.Amount * case when xxx.Amount<0 then -1 else 0 end  ) as CRAmount,max(xxx.Account_Seg_Code7) as Account_Seg_Code7 " + Environment.NewLine +
        "from (" + Environment.NewLine +
        "select TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Voucher_Date,TSPL_JOURNAL_MASTER.Source_Doc_No,TSPL_JOURNAL_MASTER.Segment_code, TSPL_JOURNAL_DETAILS.Account_code ,TSPL_GL_ACCOUNTS.Description as Account_Desc,TSPL_JOURNAL_DETAILS.Amount ,isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'') as Reco_Control_Account,TSPL_GL_ACCOUNTS.Account_Seg_Code7" + Environment.NewLine +
        "from TSPL_JOURNAL_DETAILS " + Environment.NewLine +
        "left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code" + Environment.NewLine +
        "inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No  " + Environment.NewLine +
        "where 2=2 and TSPL_JOURNAL_MASTER.Authorized='A' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='V' and " + Environment.NewLine +
        "CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' " + Environment.NewLine

        ''changed by richa agarwal 18 Mar,2019 BHA/13/03/19-000840 remove  having sum(xxx.Amount)<>0 from this condition(group by xxx.Voucher_No,xxx.Source_Doc_No,xxx.Account_code having sum(xxx.Amount)<>0) 
        qry += ")xxx" + Environment.NewLine +
        "group by xxx.Voucher_No,xxx.Source_Doc_No,xxx.Account_code  " + Environment.NewLine +
        ")GL on GL.Source_Doc_No= xx.DocNo and 2=(case when xx.TransType in ('Payment','Bank Reverse') and xx.DocumentType in ('PY','AD') then (case when GL.Account_code=xx.Account then 2 else 3 end) else 2 end ) " + Environment.NewLine +
        "left outer join  TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= xx.Vendor_Code" + Environment.NewLine +
        "left outer join  TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code= TSPL_VENDOR_MASTER.Vendor_Group_Code" + Environment.NewLine +
        " Where 2=2 "


        If obj.Account_Set IsNot Nothing AndAlso obj.Account_Set.Count > 0 Then
            qry += " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Account,'') in (" & clsCommon.GetMulcallString(obj.Account_Set) & ") "
        End If
        If obj.Vendor_Group IsNot Nothing AndAlso obj.Vendor_Group.Count > 0 Then
            qry += " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Group_Code,'') in (" & clsCommon.GetMulcallString(obj.Vendor_Group) & ") "
        End If
        If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
            qry += " and GL.Segment_code in  (select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (" & clsCommon.GetMulcallString(obj.Location) & "))"
        End If
        If obj.Acc_Code IsNot Nothing AndAlso obj.Acc_Code.Count > 0 Then
            qry += " AND (coalesce(GL.Account_code,'') in (" & clsCommon.GetMulcallString(obj.Acc_Code) & ")) "
        End If
        qry += ")xxx where (DRAmount>0 or CRAmount>0 or GLDRAmount>0 or GLCRAmount>0) "

        If obj.ShowMismatchDoc Then
            qry += " and  abs(isnull((DRAmount-CRAmount)-(GLDRAmount-GLCRAmount),0))>0"
        End If
        Return qry
    End Function


    Public Shared Function GetVendorRecoSytemOPQry(ByVal obj As clsPurchaseReco) As String
        Dim qry As String = "select Account_code,Account_Desc,DocNo,RefDocNo,[Document Date],TransType,DocumentType,Vendor_Code,Vendor_Name,Vendor_Account,Vendor_Group_Code,Group_Desc,DRAmount,CRAmount,DRAmount-CRAmount as NetAmount,Voucher_No,Voucher_Date,GLDrAmount,GLCrAmount,(GLDrAmount-GLCrAmount) as GLNetAmount,isnull((DRAmount-CRAmount)-(GLDRAmount-GLCrAmount),0) as DiffAmount from (" + Environment.NewLine + Environment.NewLine + Environment.NewLine +
        "select xx.Account as  Account_code,TSPL_GL_ACCOUNTS.Description as  Account_Desc,xx.DocNo,xx.[Document Date],xx.TransType,xx.DocumentType,xx.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Vendor_Account, TSPL_VENDOR_MASTER.Vendor_Group_Code,TSPL_VENDOR_GROUP.Group_Desc,'' as RefDocNo,xx.DRAmount as DRAmount,xx.CRAmount as CRAmount, '' as Voucher_No,null as Voucher_Date, 0 as GLDrAmount,0 as GLCrAmount from (" + Environment.NewLine + Environment.NewLine +
        "SELECT   TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as [Document Date],case " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition,''))>0 then 'Acquisition' " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work,''))>0 then 'Asset' " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No,''))>0 then 'Bulk Milk Purchase' " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No,''))>0  then 'Milk Purchase Invoice'  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0  then 'Purchase Invoice'  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,''))>0  then 'Purchase Return'  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No,''))>0  then 'VSP-Issue/Return' " + Environment.NewLine +
          "when len(isnull(RefDocType,''))>0 then RefDocType " + Environment.NewLine +
          "else  'AP Invoice' end as TransType" + Environment.NewLine +
          ",case " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition,''))>0 then 'AQI' " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work,''))>0 then 'Asset' " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No,''))>0 then 'Bulk-PI' " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No,''))>0  then 'MCC-PI'  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0  then 'PI'  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,''))>0  then 'PR'  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No,''))>0  then 'VSP-IR' " + Environment.NewLine +
          "when len(isnull(RefDocType,''))>0 then RefDocType " + Environment.NewLine +
          "else  'AP' end As DocumentType ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code," + Environment.NewLine +
          "case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition,''))>0 then TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition" + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work,''))>0 then TSPL_VENDOR_INVOICE_HEAD.Against_Asset_Work " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No,''))>0 then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No,''))>0  then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0  then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,''))>0  then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No" + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No,''))>0  then TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No " + Environment.NewLine +
          "when len(isnull(TSPL_VENDOR_INVOICE_HEAD.RefDocNo,''))>0 then TSPL_VENDOR_INVOICE_HEAD.RefDocNo else  '' end as RefDocNo" + Environment.NewLine +
         ", cast(TSPL_VENDOR_INVOICE_HEAD.ConvRate*(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('D') then Document_Total - (case when TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 then 0 else isnull(TDS_Actual_Amount,0) end) - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end ) as decimal(18,2)) as DRAmount" + Environment.NewLine +
",cast(TSPL_VENDOR_INVOICE_HEAD.ConvRate*(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('I','C') Then document_total - (case when TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 then 0 else isnull(TDS_Actual_Amount,0) end) - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then( " + Environment.NewLine +
" case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end " + Environment.NewLine +
" +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end " + Environment.NewLine +
" +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end " + Environment.NewLine +
" +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end " + Environment.NewLine +
" +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end " + Environment.NewLine +
" +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end " + Environment.NewLine +
"  ) else 0 end)  else 0 end)as decimal(18,2)) as CRAmount,TSPL_VENDOR_INVOICE_HEAD.Vendor_Control_AC as Account" + Environment.NewLine +
         " from  TSPL_VENDOR_INVOICE_HEAD " + Environment.NewLine +
         " where cast(TSPL_VENDOR_INVOICE_HEAD.Posting_Date AS DATE) < '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
            qry = qry & " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        End If
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry = qry & " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If

        If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
            qry = qry & " and TSPL_VENDOR_INVOICE_HEAD.Document_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        End If
        '--PaymentHeader
        qry += Environment.NewLine + " union all " + Environment.NewLine +
        "SELECT TSPL_PAYMENT_HEADER.Payment_No as DocNo,TSPL_PAYMENT_HEADER.Payment_Date as Document_Date,'Payment' AS TransType,TSPL_PAYMENT_HEADER.Payment_Type as DocumentType,TSPL_PAYMENT_HEADER.Vendor_Code,Null AS RefDocNo , cast((((case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 else (TSPL_PAYMENT_HEADER.Payment_Amount+coalesce(TDS_Amount,0)) end ) * TSPL_PAYMENT_HEADER.ConvRate) + TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT-TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT) as decimal(18,2)) AS DrAmount,cast(((case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then (TSPL_PAYMENT_HEADER.Payment_Amount+coalesce(TDS_Amount,0)) else 0 end)*TSPL_PAYMENT_HEADER.ConvRate) as decimal(18,2))  AS CRAmount,case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then Credit_Account  else  Debit_Account end as Account " + Environment.NewLine +
        "FROM  TSPL_PAYMENT_HEADER      " + Environment.NewLine +
        "left join TSPL_VENDOR_MASTER on TSPL_PAYMENT_HEADER.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code  " + Environment.NewLine +
        "WHERE TSPL_PAYMENT_HEADER.Is_Security=0 and TSPL_PAYMENT_HEADER.Payment_Type not in ('PY','AD') and  TSPL_PAYMENT_HEADER.Posted='1' AND LEN(COALESCE(TSPL_PAYMENT_HEADER.Vendor_Code,''))>0  and  CAST(TSPL_PAYMENT_HEADER.Payment_Date AS DATE) < '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'" + Environment.NewLine
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry += " and TSPL_PAYMENT_HEADER.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If
        qry += ")xx" + Environment.NewLine +
        "left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=xx.Account" + Environment.NewLine +
        "left outer join  TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= xx.Vendor_Code" + Environment.NewLine +
        "left outer join  TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code= TSPL_VENDOR_MASTER.Vendor_Group_Code" + Environment.NewLine +
        " Where 2=2 "
        If obj.Account_Set IsNot Nothing AndAlso obj.Account_Set.Count > 0 Then
            qry += " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Account,'') in (" & clsCommon.GetMulcallString(obj.Account_Set) & ") "
        End If
        If obj.Vendor_Group IsNot Nothing AndAlso obj.Vendor_Group.Count > 0 Then
            qry += " AND coalesce(TSPL_VENDOR_MASTER.Vendor_Group_Code,'') in (" & clsCommon.GetMulcallString(obj.Vendor_Group) & ") "
        End If
        If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
            qry += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in  (select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (" & clsCommon.GetMulcallString(obj.Location) & "))"
        End If
        If obj.Acc_Code IsNot Nothing AndAlso obj.Acc_Code.Count > 0 Then
            qry += " AND (coalesce(TSPL_GL_ACCOUNTS.Account_Code,'') in (" & clsCommon.GetMulcallString(obj.Acc_Code) & ")) "
        End If
        qry += ")xxx where (DRAmount>0 or CRAmount>0 or GLDRAmount>0 or GLCRAmount>0) "
        Return qry
    End Function

    Public Shared Function GetVendorQry(ByVal obj As clsPurchaseReco) As String
        Dim qry As String = " SELECT DISTINCT 'AP' As Code , 'AP Invoice' As Name,TVIH.Loc_Code AS Location_Code ,TVIH.RefDocNo as [Document Code],TVIH.Posting_Date  as [Document Date]," &
                            " TVIH.Vendor_Code as [Vendor Code],TVIH.Document_No AS AP_DOC_NO,'1' as Status " &
                            " from  TSPL_VENDOR_INVOICE_HEAD TVIH " &
                            " WHERE len(COALESCE(TVIH.Against_Acquisition,''))<=0 and len(COALESCE(TVIH.Against_Asset_Work,''))<=0 and len(COALESCE(TVIH.Against_BulkMillkPurchaseInvoice_No,''))<=0 " &
                            " and len(COALESCE(TVIH.Against_MillkPurchaseInvoice_No,''))<=0 and len(COALESCE(TVIH.Against_POInvoice_No,''))<=0 and len(COALESCE(TVIH.Against_PurchaseReturn_No,''))<=0 " &
                            " and len(COALESCE(TVIH.Against_VSPItemIssue_No,''))<=0 " &
                            " and  CAST(TVIH.Posting_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' "

        If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
            qry = qry & " and TVIH.Loc_Code in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        End If
        If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
            qry = qry & " and TVIH.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        End If

        If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
            qry = qry & " and TVIH.Document_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        End If
        'qry = qry & Environment.NewLine & " UNION ALL " & _
        '      " select 'TDS' as Code,'Remitance' as Name,TVIH.Loc_Code,TR.Document_No AS [Document Code],CONVERT(DATE,TR.Document_Date,103) AS Document_Date,tr.Vendor_Code as [Vendor Code]," & _
        '      " TVIH.Document_No AS AP_DOC_NO,TVIH.Posting_Date from TSPL_REMITTANCE TR inner join TSPL_VENDOR_INVOICE_HEAD TVIH ON TR.Document_No=TVIH.Document_No where CONVERT(DATE,TR.Document_Date,103) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"

        'If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
        '    qry = qry & " and TVIH.Loc_Code in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        'End If
        'If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
        '    qry = qry & " and TVIH.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        'End If

        'If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
        '    qry = qry & " and TR.Document_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        'End If
        'qry = qry & Environment.NewLine & " UNION ALL " & _
        '      " SELECT 'Payment' AS Code,TPH.Payment_Type as Name,TPH.Location_Code,TPH.Payment_No,TPH.Payment_Date,TPH.Vendor_Code,TPD.Document_No AS AP_DOC_NO,TPH.Payment_Post_Date " & _
        '      " FROM TSPL_PAYMENT_DETAIL TPD LEFT JOIN TSPL_PAYMENT_HEADER TPH ON TPH.Payment_No=TPD.Payment_No WHERE TPH.Posted='1' " & _
        '      " AND LEN(COALESCE(TPH.Vendor_Code,''))>0 and Cast(TPH.Payment_Date as Date) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"

        'If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
        '    qry = qry & " and TPH.Location_Code in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        'End If
        'If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
        '    qry = qry & " and TPH.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        'End If

        'If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
        '    qry = qry & " and TPH.Payment_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        'End If

        'qry = qry & Environment.NewLine & " UNION ALL " & _
        '      " select 'Bank Reverse' as Code,TBR.Reverse_Document as Name,TPH.Location_Code,TBR.Document_No,TBR.Reversal_Date,TBR.Vendor_Code,NULL AS AP_DOC_NO," & _
        '      " CONVERT(DATE,TBR.Modify_Date,103) AS Posting_Date from TSPL_BANK_REVERSE TBR left join TSPL_PAYMENT_HEADER TPH ON TBR.Document_No=TPH.Payment_No " & _
        '      " where TBR.Reverse_Document='Payments' and  cast(TBR.Reversal_Date as Date) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"

        'If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
        '    qry = qry & " and TPH.Location_Code in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        'End If
        'If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
        '    qry = qry & " and TBR.Vendor_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        'End If

        'If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
        '    qry = qry & " and TBR.Document_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        'End If

        'qry = qry & Environment.NewLine & " UNION ALL " & _
        '      " select 'VCGL' as Code,Document_Type as Name,Location_Segment,Document_No,Document_Date,VC_Code,NULL AS AP_DOC_NO,Posting_Date from TSPL_VCGL_Head cast(Document_Date as date) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"

        'If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
        '    qry = qry & " and Location_Segment in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        'End If
        'If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
        '    qry = qry & " and VC_Code in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        'End If

        'If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
        '    qry = qry & " and Document_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        'End If

        'qry = qry & Environment.NewLine & " UNION ALL " & _
        '     " SELECT 'AP Adjustment' as Code,Adjust_Type as Name,TVIH.Loc_Code,TPAH.Adjustment_No,TPAH.Adjustment_Date,TPAH.Vendor_No,TPAH.Doc_No AS AP_DOC_NO,TPAH.Post_Date " & _
        '     " FROM TSPL_Payment_Adjustment_Header TPAH inner join TSPL_VENDOR_INVOICE_HEAD TVIH ON TPAH.Doc_No=TVIH.Document_No where cast(TPAH.Adjustment_Date as Date) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"

        'If Not obj.Location Is Nothing AndAlso obj.Location.Count > 0 Then
        '    qry = qry & " and TVIH.Loc_Code in (" & clsCommon.GetMulcallString(obj.Location) & ")"
        'End If
        'If Not obj.Vendor_Code Is Nothing AndAlso obj.Vendor_Code.Count > 0 Then
        '    qry = qry & " and TPAH.Vendor_No in (" & clsCommon.GetMulcallString(obj.Vendor_Code) & ")"
        'End If

        'If Not obj.Doc_No Is Nothing AndAlso obj.Doc_No.Count > 0 Then
        '    qry = qry & " and TPAH.Adjustment_No in (" & clsCommon.GetMulcallString(obj.Doc_No) & ")"
        'End If
        Return qry
    End Function

    Public Shared Function ReturnQuery(ByVal isNew As Boolean, ByVal From_Date As Date, ByVal To_Date As Date, ByVal Unit_Code As String, ByVal StockingUom As Boolean, Optional ByVal IsAgainstJobwork As Boolean = False) As ArrayList
        If isNew Then
            Return ReturnQueryNew(From_Date, To_Date, Unit_Code, StockingUom, IsAgainstJobwork)
        Else
            Return ReturnQueryOLD(From_Date, To_Date, Unit_Code, StockingUom, IsAgainstJobwork)
        End If

    End Function
    Public Shared Function ReturnQueryOLD(ByVal From_Date As Date, ByVal To_Date As Date, ByVal Unit_Code As String, ByVal StockingUom As Boolean, Optional ByVal IsAgainstJobwork As Boolean = False) As ArrayList
        '' query change by Panch raj against Ticket No:BM00000008426
        Dim strMainQuery As String = ""
        Dim QryLst As New ArrayList
        Dim strCodeColumn As String = ""
        Dim strCodeColumn1 As String = ""
        Dim strCodeColumnMax As String = ""
        Dim strCodeDescColumn As String = ""
        Dim strCodeDescColumnMax As String = ""
        Dim strPivotForFinalOuterQuery As String = ""
        Dim strPivotForAddChargeFinalOuterQuery As String = ""
        Dim strPivotForAddChargeFinalOuterSumQuery As String = ""
        Dim strCategoryTable As String = ""
        Dim dtCategory As DataTable
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
        If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
            For ii As Integer = 0 To dtCategory.Rows.Count - 1
                If ii <> 0 Then
                    strCodeColumn += ","
                    strCodeColumn1 += ","
                    strCodeColumnMax += ","
                    strCodeDescColumn += ","
                    strCodeDescColumnMax += ","
                End If
                strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeColumn1 += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"
            Next
            strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine &
            " select * from ( " + Environment.NewLine &
            " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine &
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+' Description' as Item_Category_CodeDesc " + Environment.NewLine &
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine &
            " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine &
            " from  TSPL_ITEM_MASTER  " + Environment.NewLine &
            " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine &
            " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine &
            " where 2=2 " + Environment.NewLine &
            " )xx" + Environment.NewLine

            If clsCommon.myLen(strCodeColumn) > 0 Then
                strCategoryTable = strCategoryTable + " Pivot " + Environment.NewLine &
            " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine &
            " ) Pivt" + Environment.NewLine
            End If

            If clsCommon.myLen(strCodeDescColumn) > 0 Then
                strCategoryTable = strCategoryTable + " Pivot " + Environment.NewLine &
           " (" + Environment.NewLine &
           " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine &
           " ) Pivt1 " + Environment.NewLine
            End If

            strCategoryTable = strCategoryTable + " ) xxx group by Item_Code "
        End If
        Dim qryTaxQuery As String = ""
        Dim qryAddChargeQuery As String = ""
        Dim qryAddChargeForZeroQuery As String = ""

        Dim strPivotForOuter As String
        Dim strPivotForOuterOnlyForDocumentInfo As String
        Dim strPivotForAddChargeOuter As String
        Dim lstTables As New List(Of String)
        '========added By preeti gupta===========
        Dim lstTablesAddCharge As New List(Of String)
        '================ENd==========
        lstTables.Add("TSPL_PI_DETAIL")
        qryTaxQuery = GetTaxQuery(lstTables)
        '===============Added By preeti gupta============
        lstTablesAddCharge.Add("TSPL_PI_HEAD")
        qryAddChargeQuery = GetAddChargeQuery(lstTablesAddCharge)
        qryAddChargeForZeroQuery = GetAddChargeZeroQuery(lstTablesAddCharge)

        '==========================END=================

        'strPivotForOuter = " select distinct (select Distinct ',sum(isnull(final.'+tax1+',0)) as '+TAX1 from ( " & qryTaxQuery
        strPivotForOuter = "select distinct (select Distinct ',sum(isnull(final.['+tax1+'],0)) as ['+TAX1+']' from ( " & qryTaxQuery
        strPivotForOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuter))
        '============================
        '=====================Added by preeti Gupta========================================
        strPivotForOuterOnlyForDocumentInfo = "select distinct (select Distinct ',(isnull(final.['+tax1+'],0)) as ['+TAX1+']' from ( " & qryTaxQuery
        strPivotForOuterOnlyForDocumentInfo += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQueryOnlyForDocumentInfo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuterOnlyForDocumentInfo))
        '=====================================================================================
        '==================Added By Preeti Gupta=============================
        strPivotForAddChargeOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',sum(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeOuter += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as xp)fin"

        Dim strPivotForAddChargeOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeOuter))
        '===============================END==================================
        Dim strPivotForFinalOuter As String
        strPivotForFinalOuter = ""
        strPivotForFinalOuter = " select distinct (select Distinct ',xx.['+tax1+']' from ( " & qryTaxQuery
        strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        strPivotForFinalOuterQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))
        '=====================================Added By Preeti Gupta============================================
        Dim strPivotForAddChargeFinalOuter As String
        strPivotForAddChargeFinalOuter = ""
        strPivotForAddChargeFinalOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',xx.['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalOuter += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as xp)fin"
        strPivotForAddChargeFinalOuterQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuter))

        Dim strPivotForAddChargeFinalSum As String
        strPivotForAddChargeFinalSum = ""
        strPivotForAddChargeFinalSum = " select REPLACE(xp,'&amp;','&') from ( select distinct (select Distinct ',xx.['+'AC_'+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalSum += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        strPivotForAddChargeFinalOuterSumQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalSum))

        '=============================================END======================================================

        Dim strPivotForFinalOuterPercent As String
        strPivotForFinalOuterPercent = " select distinct (select  Distinct ',xx.['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForFinalOuterPercent += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strPivotForFinalOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuterPercent))

        Dim strPivotForTransfer_In As String
        strPivotForFinalOuter = ""
        strPivotForFinalOuter = " select distinct (select Distinct ',0 as ['+tax1+']' from ( " & qryTaxQuery
        strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        strPivotForTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))
        Dim strTransferTaxColumns As String = ""
        Dim strTransferTaxPerColumns As String = ""
        Dim dtTempDT As DataTable = clsDBFuncationality.GetDataTable("select * from (" + qryTaxQuery + ")xx where len(isnull(TAX1,''))>0  order by  TAX1")
        If dtTempDT IsNot Nothing AndAlso dtTempDT.Rows.Count > 0 Then
            For Each dr As DataRow In dtTempDT.Rows
                Dim strTax As String = clsCommon.myCstr(dr(0))
                strTransferTaxColumns += ", (case when TSPL_TRANSFER_ORDER_DETAIL.TAX1='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX2='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX3='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX4='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX5='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX6='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX7='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX8='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX9='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX10='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt else 0 end end end end end end end end end end ) as [" + strTax + "]"
                strTransferTaxPerColumns += ",(case when TSPL_TRANSFER_ORDER_DETAIL.TAX1='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX2='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX3='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX4='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX5='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX6='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX7='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX8='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX9='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX10='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate else 0 end end end end end end end end end end ) as [" + strTax + "%]"
            Next
        End If

        '=============================Added By Preeti Gupta=================================================

        Dim strPivotForAddChargeFinalOuterPercent As String
        strPivotForAddChargeFinalOuterPercent = " select REPLACE(xp,'&amp;','&') from (select distinct (select  Distinct ',xx.['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalOuterPercent += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        Dim strPivotForFinalAddChargeOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuterPercent))

        Dim strPivotForAddChargeZeroFinalOuterPercent As String
        strPivotForAddChargeZeroFinalOuterPercent = " select REPLACE(xp,'&amp;','&') from ( select distinct (select  Distinct ',xx.['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery
        strPivotForAddChargeZeroFinalOuterPercent += " )aa where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        Dim strPivotForFinalAddChargeZeroOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeZeroFinalOuterPercent))

        Dim strPivotForAddChargeTransfer_In As String
        strPivotForAddChargeFinalOuter = ""
        strPivotForAddChargeFinalOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',0 as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalOuter += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        strPivotForAddChargeTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuter))

        Dim strPivotForAddChargeForZeroTransfer_In As String
        strPivotForAddChargeFinalOuter = ""
        strPivotForAddChargeFinalOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',0 as ['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery
        strPivotForAddChargeFinalOuter += " )aa where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        strPivotForAddChargeForZeroTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuter))

        '=========================================END=======================================================

        Dim strPivotFortRANSFER_INPercent As String
        strPivotFortRANSFER_INPercent = " select distinct (select  Distinct ',0 as ['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotFortRANSFER_INPercent += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strPivotFortRANSFER_INPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotFortRANSFER_INPercent))
        '===========

        Dim strPivotForGroupOuter As String
        strPivotForGroupOuter = " select REPLACE(abc,'&amp;','&') from ( select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForGroupOuter += " select distinct (select Distinct ',max(isnull(final.['+tax1+'%'+'],0)) as ['+TAX1+'%'+']' from ( " & qryTaxQuery

        strPivotForGroupOuter += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForGroupOuter))

        ''done below code so that when variable placed in query,no need for comma(,),in case of blank variable its gives error.(06/12/2016)
        If clsCommon.myLen(strPivotFoGrouprOuterQuery) > 0 Then
            strPivotFoGrouprOuterQuery = "," + strPivotFoGrouprOuterQuery
        End If




        '======================================Added by Preeti Gupta
        '================================Added by preeti Gupta===============================
        Dim strPivotForGroupOuterOnlyForDocumnetInfo As String
        strPivotForGroupOuterOnlyForDocumnetInfo = " select REPLACE(abc,'&amp;','&') from ( select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForGroupOuterOnlyForDocumnetInfo += " select distinct (select Distinct ',(isnull(final.['+tax1+'%'+'],0)) as ['+TAX1+'%'+']' from ( " & qryTaxQuery

        strPivotForGroupOuterOnlyForDocumnetInfo += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoGrouprOuterQueryonlyForDocumnetInfo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForGroupOuterOnlyForDocumnetInfo))
        '=========================================================================================

        Dim strPivotForADDChargeGroupOuter As String
        strPivotForADDChargeGroupOuter = " select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForADDChargeGroupOuter += " select distinct (select Distinct ',max(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForADDChargeGroupOuter += " )a where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoAddChargeGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForADDChargeGroupOuter))

        Dim strPivotForADDChargeZeroGroupOuter As String
        strPivotForADDChargeZeroGroupOuter = "select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForADDChargeZeroGroupOuter += " select distinct (select Distinct ',sum(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery

        strPivotForADDChargeZeroGroupOuter += " )a where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoAddChargeZeroGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForADDChargeZeroGroupOuter))

        ''done below code so that when variable placed in query,no need for comma(,),in case of blank variable its gives error.(06/12/2016)
        If clsCommon.myLen(strPivotFoAddChargeZeroGrouprOuterQuery) > 0 Then
            strPivotFoAddChargeZeroGrouprOuterQuery = "," + strPivotFoAddChargeZeroGrouprOuterQuery
        End If



        '================================================END================================================
        '==================================Added by preeti Gupta====================================
        Dim strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo As String
        strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo = "select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo += " select distinct (select Distinct ',(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery

        strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo += " )a where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoAddChargeZeroGrouprOuterQueryonlyForDocumentInfo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo))
        '===============================================================================================


        Dim strPivotForOuterForBulk As String
        strPivotForOuterForBulk = " select distinct (select Distinct ',0 as ['+TAX1+']' from ( " & qryTaxQuery

        strPivotForOuterForBulk += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuterForBulk))

        Dim strDoublePivotForOuterForBulk As String

        strDoublePivotForOuterForBulk = " select distinct (select Distinct ',0 as ['+tax1+'%'+']' from ( " & qryTaxQuery


        strDoublePivotForOuterForBulk += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strDoublePivotForOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForOuterForBulk))

        '=====================================Added by Preeti Gupta===================================================================
        Dim strPivotForAddChargeOuterForBulk As String
        strPivotForAddChargeOuterForBulk = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',0 as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForAddChargeOuterForBulk += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"

        Dim strPivotForAddChargeOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeOuterForBulk))

        'Dim strDoublePivotForAddChrageOuterForBulk As String

        'strDoublePivotForAddChrageOuterForBulk = " select distinct (select Distinct ',0 as ['+Add_Charge_Code1+'%'+']' from ( " & qryAddChargeQuery


        'strDoublePivotForAddChrageOuterForBulk += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )"

        'Dim strDoublePivotForAddChargeOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForAddChrageOuterForBulk))

        '============================================END===============================================================================


        Dim strPivotForInner As String
        strPivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
        strPivotForInner += " select distinct (select Distinct ',['+tax1+']' from ( " & qryTaxQuery

        strPivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

        Dim strPivotForInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForInner))

        '============================================Added By Preeti Gupta ticket no [BM00000009024]===========================================
        Dim strPivotForAddChargeInner As String
        strPivotForAddChargeInner = "select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        strPivotForAddChargeInner += " select distinct (select Distinct ',['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForAddChargeInner += " )a where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as ax)Axx)XXX"

        Dim strPivotForAddChargeInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeInner))

        Dim strPivotForAddChargeInnerOuter As String
        strPivotForAddChargeInnerOuter = " select REPLACE(abc,'&amp;','&') from (select ','+SUBSTRING(ax,2,len(Ax)) as abc from ("
        strPivotForAddChargeInnerOuter += " select distinct (select Distinct ',['+Add_Charge_Code1+']  as ['+'AC_'+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForAddChargeInnerOuter += " )a where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as ax)Axx)XXX"
        Dim strPivotForAddChargeInnerQueryOuter As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeInnerOuter))

        '=======================================================END==================================================



        Dim strDoublePivotForInner As String
        strDoublePivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
        strDoublePivotForInner += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery

        strDoublePivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

        Dim strDoublePivotForInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForInner))
        Dim qryQC As String = ""
        qryQC = " select Item_Code,MAX(Fat_Per) as Fat_Per,MAX(SNF_Per) as SNF_Per from (" &
                " select Item_QCP.Item_Code,Item_QCP.Code as Parameter_Code,(case when QCP.Type='FAT' then Item_QCP.Actual_Range end) as Fat_Per," &
                " (case when QCP.Type='SNF' then Item_QCP.Actual_Range  end) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QCP " &
                " left join TSPL_PARAMETER_MASTER QCP  on Item_QCP.Code=QCP.Code) as QC group by Item_Code"

        Dim qryKG As String = ""
        qryKG = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG'"
        Dim qryStock As String = ""
        qryStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "

        '' query for transaction  UOM conversion
        Dim qryTransStock As String = ""
        If clsCommon.myLen(Unit_Code) <= 0 AndAlso StockingUom = False Then
            qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "
        Else
            If StockingUom Then
                qryTransStock = "select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code"
            Else
                qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='" & Unit_Code & "'"
            End If

            'qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='" & Unit_Code & "'"
        End If

        '' end query for transaction  UOM conversion

        '' query for structure and item group custom field
        Dim strSDCommonQuery As String = ""
        Dim strTaxColumns As String = ""
        Dim strAddChargeColumns As String = ""
        Dim strTaxNonRecoverableAmt As String = ""
        Dim strSDEndQry As String = ",TSPL_PI_DETAIL.TAX1+'%' as Tax1_Rate"
        strSDCommonQuery = " select Distinct  TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'PI' as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location], " &
                           " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],'PI' as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                           " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                           " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                           " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                           " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                           " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,0 as NonRecoverable_Tax, "
        strSDEndQry = ",TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Amount Before Tax],TSPL_PI_DETAIL.SRN_Id as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Document Total],TSPL_PI_DETAIL.Po_Id,TSPL_PI_DETAIL.MRP,TSPL_PI_HEAD.Purchase_Tax_Invoice,TSPL_PURCHASE_ORDER_HEAD.Created_By,TSPL_PURCHASE_ORDER_HEAD.Modify_By,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as Doc_No,TSPL_PURCHASE_ORDER_head.PurchaseOrder_Date as Doc_Purchase_Date " +
        ",case when TSPL_PI_Head.PI_Type='I' then 'Yes' else 'No' end as [Import Type],case when TSPL_PI_Head.PI_Type='I' then TSPL_PI_Head.Port  else null end as [Port],case when TSPL_PI_Head.PI_Type='I' then TSPL_PI_Head.Import_Entry_No else null end as [Import Bill of Entry No],case when TSPL_PI_Head.PI_Type='I' then convert(varchar, TSPL_PI_Head.Import_Entry_Date,103) else null end as [Import Bill of Entry Date]" + Environment.NewLine +
        ",'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],case when TSPL_PI_HEAD.ITC_Elibible=1 then 'Yes' else 'No' end as [ITC Eligible],case when TSPL_PI_HEAD.ITC_Elibible=1 then case when TSPL_PI_HEAD.ITC_Type=1 then 'Yes' else 'No' end else '' end as [ITC Status],case when TSPL_PI_HEAD.ITC_Elibible=1 then TSPL_PI_HEAD.ITC_Type_Category else '' end as [ITC Details] " + Environment.NewLine +
                        " from TSPL_PI_DETAIL " &
                           " left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_NO =TSPL_PI_DETAIL.PI_NO " &
                           " left join TSPL_VEHICLE_MASTER on TSPL_PI_HEAD.vehicledesc=TSPL_VEHICLE_MASTER.Vehicle_Id left join TSPL_vendor_Invoice_Head on TSPL_vendor_Invoice_Head.against_PoInvoice_No=TSPL_PI_HEAD.PI_NO and TSPL_vendor_Invoice_Head.against_PoInvoice_No is not null left join TSPL_vendor_master on   TSPL_vendor_master.Vendor_Code=tspl_Pi_Head.Vendor_Code left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code " &
                           "    "
        strMainQuery = "  select [Trans Type],[Location Code],[Location Name],[Location Address],TSPL_STATE_MASTER.GST_STATE_Code as [Location State GST], TSPL_STATE_MASTER.STATE_CODE as [Location State Code], TSPL_STATE_MASTER.STATE_NAME as [Location State Name],TSPL_LOCATION_MASTER_For_GSTIN.City_Code as VendorCityCode, TSPL_LOCATION_MASTER_For_GSTIN.City_Code as VendorCityName,TSPL_LOCATION_MASTER_For_GSTIN.GSTNO as Location_GSTIN,[Invoice Type],[Document No],[Document Date],[Way Bill No],[GR No],[LR No],Vendor_Invoice_no as [Vendor Invoice No],case when Coalesce(Vendor_Invoice_no,'')<>'' then convert(varchar,Vendor_Invoice_Date,103) else null end as [Vendor Invoice Date],vehicledesc as [Vehicle Code],Vehicle_No as [Vehicle No],cast(Additional_Charge as numeric(18,3)) as [Additional Amount],[Vendor Code],[Vendor Name]," & If(IsAgainstJobwork = True, "[Sub Location],TSPL_LOCATION_MASTER_SubLoc.Location_Desc as [Sub Location Name],", "") & " [Vendor Address],[State Code] as [Vendor State Code],[State Name] as [Vendor State Desc.],case when cust.GST_Composition_scheme=1 then 'Yes' else 'No' end as GST_Composition_scheme,case when [Trans Type] in ('MCC Transfer','Transfer') then case when TSPL_LOCATION_MASTER_AS_Transfer.Registered=1 then 'Yes' else 'No' end else case when cust.GSTRegistered=1 then 'Yes' else 'No' end end GSTRegistered,case when [Trans Type] in ('MCC Transfer','Transfer') then TSPL_LOCATION_MASTER_AS_Transfer.GSTNO else cust.GSTFinalNo end GSTFinalNo ,case when [Trans Type] in ('MCC Transfer','Transfer') then TSPL_LOCATION_MASTER_AS_Transfer.GST_STATE_Code else cust.Vendor_GST_STATE_Code end as Vendor_GST_STATE_Code,case when [Trans Type] in ('MCC Transfer','Transfer') then TSPL_LOCATION_MASTER_AS_Transfer.STATE_NAME else cust.Veindor_STATE_Name end as Veindor_STATE_Name,[Vendor TIN No],xx.[Transporter],[Transporter Name],Cust.Vendor_Group_Code as [Vendor Group Code],Cust_Group.Group_Desc as [Vendor Group Description], [Parent Vendor No],[Parent Vendor Code], [Parent Vendor Name]"
        If clsCommon.myLen(strCategoryTable) > 0 Then
            strMainQuery += "," + strCodeColumn1 + "," + strCodeDescColumn
        End If
        strMainQuery += " , [Item Code],[Item Name],cast(([Quantity]*Stock_SU.Conversion_Factor)/(coalesce(TransStock.Conversion_Factor,1)) as Numeric(18,3)) as [Quantity]," & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " as [UOM],[Item Cost] as [Item Rate],[Fat Per] as [FAT %],[SNF Per] as [SNF %],cast(([Quantity]*[Fat Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast(([Quantity]*[SNF Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],Amount,[Discount Per] as [Discount %], [Discount Amount],cast(Round(XX.[EMP],2) as decimal(18,2)) as [EMP],Round([Incentive],2) as [Incentive],Round([IncentiveEMP],0) as [Incentive EMP] ,Round([Amount Less Discount],2) as [Amount Less Discount] " + strPivotForFinalOuterQuery + " " + strPivotForFinalOuterPercentQuery + "" + strPivotForFinalAddChargeZeroOuterPercentQuery + ",[Tax Type] as [Form Type],round(([Total Amount]+cast(Additional_Charge as numeric(18,3))-[Total Tax Amount]),2) as [Purchase Amount],Round([Total Tax Amount],2) as [Total Tax Amount], Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2) as [Total Amount],(SUBSTRING(tps.Inv_Control_Account,0,10) + [Location Code]) as [Inventory Account Code],TSPL_GL_ACCOUNTS.Description as [Inventory Account Name],[AP Document No] ,coalesce(against_PoInvoice_No, coalesce(Against_PurchaseREturn_No,coalesce(Against_MillkpurchaseInvoice_No,Against_BulkMillkpurchaseInvoice_No))) as [Against Invoice No],[AP Total Tax],[AP Total Add Charge],[AP Landed Amt],[AP Document Total],MRP, Item.HSN_Code,Purchase_Tax_Invoice " +
        " ,case when cust.GST_Composition_scheme=1 then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [Composition Amount] ,case when [Total Tax Amount]=0 then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [NILL Rate Amount],case when TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted=1 then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [Exempted Amount] " + Environment.NewLine + Environment.NewLine +
        " ,case when item.Skip_GST=1 then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [Non-GST Amount],[Import Type],[Import Bill of Entry No],[Import Bill of Entry Date],case when [Import Type]='Yes' then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [Import Bill of Entry Amount],[Original Invoice No],case when len(isnull([Original Invoice No],''))>0 then convert(varchar, [Original Invoice Date],103) else null end as [Original Invoice Date],[Reason For Revision],[ITC Eligible],[ITC Status],[ITC Details] " + Environment.NewLine

        strMainQuery += " from ( "
        strMainQuery += Environment.NewLine + Environment.NewLine + "  "
        strMainQuery += "  select max(Head_Tax_Group) as Head_Tax_Group,max(Head_Tax_Group_Type) as Head_Tax_Group_Type, case when Trans_Type ='PI' then 'Purchase Invoice' when Trans_Type ='Transfer' then 'Transfer' when Trans_Type='MCC' then 'Milk Receipt' when Trans_Type='Bulk' then 'Bulk Purchase'  when Trans_Type='Bulk Purchase Return' then 'Bulk Purchase Return' when Trans_Type ='MT' then 'Merchant Trade' end  as [Trans Type],max(final.Line_No) as Line_No,max(final.ConvRate) as ConvRate,max(TSPL_LOCATION_MASTER .location_Code) as [Location Code],max(TSPL_LOCATION_MASTER.Add1) + ' ' + max(TSPL_LOCATION_MASTER.Add2) + ' ' + max(TSPL_LOCATION_MASTER.Add3) As [Location Address],max(TSPL_LOCATION_MASTER.State) as [Location State],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.PI_NO as [Document No],final.SRN_Id as [SRN No],final.PI_Date as [Document Date],final.Way_BillNo as [Way Bill No],Final.GRNO as [GR No],final.LR_NO as [LR No] ,max(VENDOR_INVOICE_no)as VENDOR_INVOICE_no,max(VENDOR_INVOICE_Date)as VENDOR_INVOICE_Date,vehicledesc,Vehicle_No,final.Additional_Charge+Case when coalesce(final.Additional_Charge,0)>0 then coalesce(max(PACKING),0) else 0 end as Additional_Charge,final.Customer_Code as [Vendor Code] ,max(TSPL_vendor_MASTER .vendor_Name) as [Vendor Name],max([Sub Location]) as [Sub Location],max(TSPL_VENDOR_MASTER.Add1) + ' ' + max(TSPL_VENDOR_MASTER.Add2) + ' ' + max(TSPL_VENDOR_MASTER.Add3) As [Vendor Address],Max([State Code]) as [State Code],Max([State Name]) as [State Name],max(TSPL_vendor_MASTER .Tin_No) as [Vendor TIN No] ,max(TSPL_vendor_MASTER .Parent_vendor_Code) as [Parent Vendor No] ,max(Parent_Master.Vendor_Code) as [Parent Vendor Code],max(Parent_Master.Vendor_Name) as [Parent Vendor Name],Max(final.[Transporter]) as [Transporter],Max([Transporter Name]) as [Transporter Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.Amt_Less_Discount  as [Amount Less Discount],0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]  " + strPivotForOuterQuery + " " + strPivotFoGrouprOuterQuery + " " + strPivotFoAddChargeZeroGrouprOuterQuery + ",max(_Type) as [Tax Type] ,(final.Total_Tax_Amt-coalesce(sum(final.NonRecoverable_Tax),0)) as [Total Tax Amount],final.Total_Amt as [Total Amount],Max([AP Document No]) as [AP Document No],Max(coalesce([AP Document Amt],0)) as [AP Document Amt],Max(coalesce([AP Document Discount Amt],0)) as [AP Document Discount Amt],Max(coalesce([AP Amount Before Tax],0)) as [AP Amount Before Tax],Max(against_PoInvoice_No) as against_PoInvoice_No,Max(Against_PurchaseREturn_No) as Against_PurchaseREturn_No,Max(coalesce([AP Total Tax],0)) as [AP Total Tax],max(coalesce([AP Total Add Charge],0)) as [AP Total Add Charge],(Max(coalesce([AP Landed Amt],0))-coalesce(sum(final.NonRecoverable_Tax),0)) as [AP Landed Amt],max(Against_MillkpurchaseInvoice_No) as Against_MillkpurchaseInvoice_No,Max(Against_BulkMillkpurchaseInvoice_No) as Against_BulkMillkpurchaseInvoice_No,Max(coalesce([AP Document Total],0)) as [AP Document Total],max(final.MRP) as MRP,coalesce(sum(final.NonRecoverable_Tax),0) as NonRecoverable_Tax,max(final.Purchase_Tax_Invoice) as Purchase_Tax_Invoice " + Environment.NewLine +
        ",max([Import Type]) as [Import Type],max([Port]) as [Port], max([Import Bill of Entry No]) as [Import Bill of Entry No],max([Import Bill of Entry Date]) as [Import Bill of Entry Date]" + Environment.NewLine +
        ",max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason For Revision]) as [Reason For Revision],max([ITC Eligible]) as [ITC Eligible],max([ITC Status]) as [ITC Status],max([ITC Details]) as [ITC Details]"
        strMainQuery += " from (" + Environment.NewLine
        strTaxColumns = " TSPL_PI_DETAIL.TAX1 ,TSPL_PI_DETAIL.TAX1_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX1_Amt, TSPL_PI_DETAIL.TAX1_Rate,TSPL_PI_DETAIL.TAX1+'%' as Tax1Rate,'' as _Type,'N' as Tax_Recoverable "
        strAddChargeColumns = " ,TSPL_PI_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt1*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt1 "
        'strTaxNonRecoverableAmt = " tm.Tax_Recoverable "
        '' query for no tax applied
        strMainQuery += " select Head_Tax_Group,Head_Tax_Group_Type,Trans_Type,Line_No,ConvRate ,SRN_Id ,Status ,Bill_To_Location,Customer_Code,[Sub Location] ,[State Code] ,[State Name] ,Invoice_Type ,PI_No ,PI_Date ,Way_BillNo ,GRNo ,LR_No ,Vendor_Invoice_No ,Vendor_Invoice_Date ,Transporter ,[Transporter Name] ,Item_Code ,Qty ,Unit_code ,Item_Cost ,Amount ,Disc_Per,Disc_Amt ,Amt_Less_Discount ,Total_Tax_Amt ,Total_Amt ,Vehicle_No ,VehicleDesc ,Additional_Charge ,NonRecoverable_Tax ,_Type ,Tax_Recoverable ,[AP Document No] ,[AP Document Amt],[AP Document Discount Amt] ,[AP Amount Before Tax],Against_POInvoice_No,Against_PurchaseReturn_No,[AP Total Tax],[AP Total Add Charge],[AP Landed Amt],Against_MillkPurchaseInvoice_No,Against_BulkMillkPurchaseInvoice_No,[AP Document Total],PO_ID,MRP,final1.Purchase_Tax_invoice,[Import Type],[Port], [Import Bill of Entry No],[Import Bill of Entry Date] " &
            ",[Original Invoice No],[Original Invoice Date],[Reason For Revision],[ITC Eligible],[ITC Status],[ITC Details] " + Environment.NewLine +
            " " + IIf(clsCommon.myLen(strPivotForInnerQuery) > 0, "," + strPivotForInnerQuery, "") + " " + IIf(clsCommon.myLen(strDoublePivotForInnerQuery) > 0, "," + strDoublePivotForInnerQuery, "") + " " + strPivotForAddChargeInnerQueryOuter + "  " &
            " from (select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2 and (coalesce(TSPL_PI_DETAIL.tax1,'')='' and coalesce(TSPL_PI_DETAIL.tax2,'')='' " &
                          " and coalesce(TSPL_PI_DETAIL.tax3,'')='' and coalesce(TSPL_PI_DETAIL.tax4,'')='' and " &
                          " coalesce(TSPL_PI_DETAIL.tax5,'')='' and coalesce(TSPL_PI_DETAIL.tax6,'')='' and " &
                          " coalesce(TSPL_PI_DETAIL.tax7,'')='' and coalesce(TSPL_PI_DETAIL.tax8,'')='' and " &
                          " coalesce(TSPL_PI_DETAIL.tax9,'')='' and coalesce(TSPL_PI_DETAIL.tax10,'')='') and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type, case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                          " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                          " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                          " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                          " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                          " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                          " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX1_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX1 ,TSPL_PI_DETAIL.TAX1_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX1_Amt, TSPL_PI_DETAIL.TAX1_Rate,TSPL_PI_DETAIL.TAX1+'%' as Tax1Rate,ttr._Type as _Type,tm.Tax_Recoverable "
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt1*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt1 "

        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine
        '' query for tax1 applied============BM00000008364 ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_PI_DETAIL.tax1 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX1_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax1=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code1 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2    and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103)  and coalesce(TSPL_PI_HEAD.tax1,'')<>'' "

        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                          " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                          " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                          " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                          " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                          " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                          " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX2_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX2 ,TSPL_PI_DETAIL.TAX2_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX2_Amt,TSPL_PI_DETAIL.TAX2_Rate, TSPL_PI_DETAIL.TAX2+'%' as Tax2Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code2 as Add_Charge_Code2 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt2*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt2 "

        ''add date filter richa
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax2 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX2_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax2=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code2 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax2,'')<>'' "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt2) for Add_Charge_Code2 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                         " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX3_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX3 ,TSPL_PI_DETAIL.TAX3_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)  as TAX3_Amt, TSPL_PI_DETAIL.TAX3_Rate, TSPL_PI_DETAIL.TAX3+'%' as Tax3Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code3 as Add_Charge_Code3 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt3*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt3 "
        ''add date filter richa
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax3 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX3_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax3=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code3 =AdCh .Code  left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax3,'')<>'' "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt3) for Add_Charge_Code3 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                         " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX4_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX4 ,TSPL_PI_DETAIL.TAX4_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX4_Amt,TSPL_PI_DETAIL.TAX4_Rate, TSPL_PI_DETAIL.TAX4+'%' as Tax4Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code4 as Add_Charge_Code4 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt4*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt4 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax4 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX4_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax4=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code4 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax4,'')<>'' "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt4) for Add_Charge_Code4 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine
        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                         " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX5_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX5 ,TSPL_PI_DETAIL.TAX5_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX5_Amt,TSPL_PI_DETAIL.TAX5_Rate, TSPL_PI_DETAIL.TAX5+'%' as Tax5Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = ", TSPL_PI_Detail.ItemAdd_Charge_Code5 as Add_Charge_Code5 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt5*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt5 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax5 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX5_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax5=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code5 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2 and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax5,'')<>''  "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt5) for Add_Charge_Code5 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                         " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX6_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX6 ,TSPL_PI_DETAIL.TAX6_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX6_Amt,TSPL_PI_DETAIL.TAX6_Rate, TSPL_PI_DETAIL.TAX6+'%' as Tax6Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code6 as Add_Charge_Code6 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt6*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt6 "
        ''richa add date filter 
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax6 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX6_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax6=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code6 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax6,'')<>''  "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt6) for Add_Charge_Code6 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                         " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX7_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX7 ,TSPL_PI_DETAIL.TAX7_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX7_Amt,TSPL_PI_DETAIL.TAX7_Rate, TSPL_PI_DETAIL.TAX7+'%' as Tax7Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code7 as Add_Charge_Code7 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt7*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt7 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax7 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX7_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax7=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code7 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax7,'')<>'' "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt7) for Add_Charge_Code7 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If



        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                         " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX8_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX8 ,TSPL_PI_DETAIL.TAX8_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX8_Amt,TSPL_PI_DETAIL.TAX8_Rate, TSPL_PI_DETAIL.TAX8+'%' as Tax8Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code8 as Add_Charge_Code8 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt8*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt8 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax8 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX8_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax8=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code8 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax8,'')<>'' "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt8) for Add_Charge_Code8 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If



        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                         " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX9_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX9 ,TSPL_PI_DETAIL.TAX9_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX9_Amt,TSPL_PI_DETAIL.TAX9_Rate, TSPL_PI_DETAIL.TAX9+'%' as Tax9Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code9 as Add_Charge_Code9 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt9*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt9 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax9 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX9_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax9=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code9 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax9,'')<>'' "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t  "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "  pivot (sum(Add_Charge_Amt9) for Add_Charge_Code9 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                         " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX10_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX10 ,TSPL_PI_DETAIL.TAX10_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX10_Amt,TSPL_PI_DETAIL.TAX10_Rate,TSPL_PI_DETAIL.TAX10+'%' as Tax10Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code10 as Add_Charge_Code10 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt10*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt10 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & "  left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax10 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX10_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax10=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code10 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax10,'')<>'' "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt10) for Add_Charge_Code10 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += " )final1)final"
        strMainQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
        strMainQuery += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
        strMainQuery += " left outer join TSPL_vendor_MASTER on TSPL_vendor_MASTER .Vendor_Code =final.Customer_Code "
        strMainQuery += " LEFT OUTER JOIN TSPL_vendor_MASTER as Parent_Master ON Parent_Master.Vendor_Code=TSPL_vendor_MASTER.Parent_Vendor_Code "
        strMainQuery += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code  " &
                        " Left join (select * from (select PI_No,Item_Code,Item_Net_AMt from " &
                        " tspl_Pi_Detail where  item_Code='PACKING') Pivoting pivot(SUm(Item_Net_AMt) for item_Code in ([PACKING]) ) pivoted) as pvt on pvt.PI_No=final.PI_No"
        strMainQuery += " group by  final.Trans_Type,final .Status  ,final.PI_NO,final.SRN_Id ,final.Item_Code ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.PI_Date,final.Way_BillNo,Final.GRNO,final.LR_NO ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt ,final.Amt_Less_Discount ,final.Total_Amt,QC.FAT_Per,QC.SNF_Per,vehicledesc,Vehicle_No,final.Additional_Charge ,final.Line_No " ', " + strPivotFoGrouprOuterQuery + "

        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine


        'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,0 as TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
        strMainQuery += " select * from (Select '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'MCC Transfer' as Trans_Type,0 as Line_No,0 as ConvRate,recv.location_Code as  Bill_To_Location,recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_MILK_TRANSFER_IN.isPosted as Status," _
            & " recv.location_desc,'MCC Transfer' as Invoice_Type,TSPL_MILK_TRANSFER_IN.Receipt_Challan_No as PI_NO,'' as SRN_Id  ,  convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No as Vendor_Invoice_No,convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Vendor_Invoice_Date," _
            & " '' as vehicledesc,tm.tanker_NO as Vehicle_No,0  as Additional_Charge ,  TSPL_MCC_Dispatch_Challan.mcc_code as Customer_Code,  TSPL_LOCATION_MASTER.Location_Desc  as Customer_Name,TSPL_MCC_Dispatch_Challan.Sublocation_Code as [Sub Location],Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,tspl_State_Master.state_COde as [State Code],tspl_State_Master.state_Name as [State Name]" _
            & ",tspl_LocaTION_mASTER.tin_No as [TIN No] ,'' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name],Tanker_Transporter_Code,tm.description, TSPL_MCC_Dispatch_Challan.Item_Code," _
            & " TSPL_MCC_Dispatch_Challan.Item_Desc , t_Qty_Recd.Net_Weight  as Qty ,t_Qty_Recd.UOM as  Unit_code ,round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))/coalesce(t_qty_recd.net_Weight,1) ,2) as  Item_Cost ,  t_FAT_Recd.Param_Field_Value as [FAT Per],  t_SNF_Recd.Param_Field_Value as [SNF Per],(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100) as [FAT KG],(coalesce(cast(t_Snf_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100) as [SNF KG],Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2) as  Amount ,0 as Disc_Per " _
            & " ,0 as Disc_Amt ,  Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2) as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type],  0 as Total_Tax_Amt ,Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2) as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice" + Environment.NewLine +
            ",null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine +
            ",'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details]" + Environment.NewLine +
            " from TSPL_MCC_Dispatch_Challan  left outer " _
            & " join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO  LEFT JOIN tspl_Mcc_Master ON tspl_Mcc_Master.MCC_Code=TSPL_MCC_Dispatch_Challan.MCC_CODE  left join tspl_Location_master on tspl_Location_master.location_code=TSPL_MCC_Dispatch_Challan.mcc_Code left join tspl_Location_master recv on recv.location_code=TSPL_MILK_TRANSFER_IN.Location_Code left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.vendor_Invoice_No=TSPL_MILK_TRANSFER_IN.Receipt_Challan_No and len(coalesce(TSPL_vendor_Invoice_Head.vendor_Invoice_No,''))>0  left join tspl_tanker_Master tm on tm.tanker_no=TSPL_MCC_Dispatch_Challan.tanker_No Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.QC_No  where TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF_Recd On t_SNF_Recd.QC_No   = TSPL_MILK_TRANSFER_IN.QC_No " _
            & " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.QC_No  where TSPL_QC_Parameter_Detail.Param_Type = 'FAT' ) t_FAT_Recd On t_FAT_Recd.QC_No   = TSPL_MILK_TRANSFER_IN.QC_No " _
            & " Left Outer Join (Select TSPL_Weighment_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Weighment_No  = TSPL_MILK_TRANSFER_IN.Weighment_No ) t_Qty_Recd On t_Qty_Recd.Weighment_No   = TSPL_MILK_TRANSFER_IN.Weighment_No  left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Mcc_Master.State_Code  where  convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date ,103) <= convert(date,('" + To_Date + "'),103) "

        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_MILK_TRANSFER_IN.IsAgainstJobWork,0)=1 "
        End If
        strMainQuery += " )t "

        If IsAgainstJobwork = False Then
            strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine

            strMainQuery += " select * from (Select TSPL_TRANSFER_ORDER_HEAD.Tax_Group as Head_Tax_Group,'T' as Head_Tax_Group_Type,'Transfer' as Trans_Type,0 as Line_No,0 as ConvRate ,recv.location_Code as  Bill_To_Location,recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_TRANSFER_ORDER_HEAD.Status as Status, recv.location_desc " _
            & "  ,'Transfer' as Invoice_Type,TSPL_TRANSFER_ORDER_HEAD.Document_No as PI_NO,'' as SRN_Id ,  convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103 ) as PI_Date,TSPL_TRANSFER_ORDER_HEAD.waybill_No as Way_BillNo,TSPL_TRANSFER_ORDER_HEAD.GR_No as [GRNO],'' as LR_NO," _
            & " TSPL_TRANSFER_ORDER_HEAD.transferoutno  as Vendor_Invoice_No,convert(varchar,Out.DOcument_Date,103) as Vendor_Invoice_Date, '' as vehicledesc,TSPL_TRANSFER_ORDER_HEAD.Vehicle_No as Vehicle_No,0  as Additional_Charge ,  Tspl_Location_Master.Location_Code as Customer_Code," _
            & " Tspl_Location_Master.Location_Desc  as Customer_Name,'' as [Sub Location],Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,Tspl_State_Master.state_Code as [State Code],Tspl_State_Master.state_name as [State Name],Tspl_Location_Master.tin_No as [TIN No],'' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name], " _
            & " tspl_transport_Master.Transport_id as [Transport Code],Tspl_Transport_Master.Transporter_Name as [Transporter Name],TSPL_TRANSFER_ORDER_DETAIL.Item_Code, TSPL_TRANSFER_ORDER_DETAIL.Item_Desc ,  TSPL_TRANSFER_ORDER_DETAIL.In_Qty  as Qty ,TSPL_TRANSFER_ORDER_DETAIL.Unit_code " _
            & " as  Unit_code ,coalesce(TSPL_TRANSFER_ORDER_DETAIL.Price,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost)  as  Item_Cost ,   QC.FAT_Per as [FAT Per],   QC.SNF_Per as [SNF Per],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],TSPL_TRANSFER_ORDER_DETAIL.Amount " _
            & " ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per as Disc_Per  ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt as Disc_Amt ,  TSPL_TRANSFER_ORDER_DETAIL.Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " _
            & " " & strTransferTaxColumns & "" & strTransferTaxPerColumns & "  " & strPivotForAddChargeForZeroTransfer_In & ",case when coalesce(out.is_AgainstformF,0)=1 then 'F' else '' end as [Tax Type],  TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt ," _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Net_Amt as Total_Amt,'' as [AP Document No],0 as  [AP Document Amt],0 as [AP Document Discount Amt],0 as [AP Amount Before Tax]," _
            & " TSPL_TRANSFER_ORDER_HEAD.transferoutno as against_PoInvoice_No,'' as Against_PurchaseREturn_No,0 as [AP Total Tax],0 as [AP Total Add Charge],0 as [AP Landed Amt]," _
            & " '' as Against_MillkpurchaseInvoice_No,'' as Against_BulkMillkpurchaseInvoice_No,0 as  [AP Document Total],TSPL_TRANSFER_ORDER_DETAIL.MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice " + Environment.NewLine +
            " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine +
            ",'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details]" + Environment.NewLine +
            " from TSPL_TRANSFER_ORDER_HEAD " _
            & " left outer  join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_NO  " _
            & "  left join TSPL_TRANSFER_ORDER_HEAD out on out.Document_No=TSPL_TRANSFER_ORDER_HEAD.TransferOutNo  left join tspl_Location_master on tspl_Location_master.LOcation_Code=out.From_Location " _
            & " left join tspl_Location_master recv on recv.location_code=TSPL_TRANSFER_ORDER_Head.To_Location left join TSPL_TRANSPORT_MASTER on " _
            & " TSPL_TRANSPORT_MASTER.transport_Id=tspl_Transfer_Order_Head.Transport_Id  left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =" _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on " _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=Stock_SU.Item_Code and TSPL_TRANSFER_ORDER_DETAIL.Unit_code=Stock_SU.UOM_Code  " _
            & " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on " _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=StockKG.Item_Code  " _
            & " left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Location_Master.State where TSPL_TRANSFER_ORDER_Head.Transfer_Type='I' and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103) <= convert(date,('" + To_Date + "'),103) )t"
        End If

        ''richa agarwal
        If IsAgainstJobwork = False Then
            strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine

            strMainQuery += " select * from (Select TSPL_TRANSFER_ORDER_HEAD.Tax_Group as Head_Tax_Group,'T' as Head_Tax_Group_Type,'Transfer Return' as Trans_Type,0 as Line_No,0 as ConvRate ,recv.location_Code as  Bill_To_Location, recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_TRANSFER_ORDER_HEAD.Status as Status, recv.location_desc , " _
            & "  'Transfer Return' as Invoice_Type,TSPL_TRANSFER_RETURN.Document_No as PI_NO,'' as SRN_Id ,  convert(varchar,TSPL_TRANSFER_RETURN.Document_Date,103 ) as PI_Date,TSPL_TRANSFER_ORDER_HEAD.waybill_No as Way_BillNo,TSPL_TRANSFER_ORDER_HEAD.GR_No as [GRNO],'' as LR_NO," _
            & " TSPL_TRANSFER_ORDER_HEAD.Document_No  as Vendor_Invoice_No,convert(varchar,TSPL_TRANSFER_ORDER_HEAD.DOcument_Date,103) as Vendor_Invoice_Date, '' as vehicledesc,TSPL_TRANSFER_ORDER_HEAD.Vehicle_No as Vehicle_No,0  as Additional_Charge , Tspl_Location_Master.Location_Code as Customer_Code, Tspl_Location_Master.Location_Desc  as Customer_Name,'' as [Sub Location],Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,Tspl_State_Master.state_Code as [State Code],Tspl_State_Master.state_name as [State Name],Tspl_Location_Master.TIN_No as [TIN No]," _
            & " '' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name], " _
            & " tspl_transport_Master.Transport_id as [Transport Code],Tspl_Transport_Master.Transporter_Name as [Transporter Name],TSPL_TRANSFER_ORDER_DETAIL.Item_Code, TSPL_TRANSFER_ORDER_DETAIL.Item_Desc ,  -TSPL_TRANSFER_ORDER_DETAIL.In_Qty  as Qty ,TSPL_TRANSFER_ORDER_DETAIL.Unit_code " _
            & " as  Unit_code ,-coalesce(TSPL_TRANSFER_ORDER_DETAIL.Price,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost)  as  Item_Cost ,   QC.FAT_Per as [FAT Per],   QC.SNF_Per as [SNF Per],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],-TSPL_TRANSFER_ORDER_DETAIL.Amount AS Amount " _
            & " ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per as Disc_Per  ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt as Disc_Amt , - TSPL_TRANSFER_ORDER_DETAIL.Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " _
            & " " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & "  " & strPivotForAddChargeForZeroTransfer_In & ",case when coalesce(out.is_AgainstformF,0)=1 then 'F' else '' end as [Tax Type],  0 as Total_Tax_Amt ," _
            & " -TSPL_TRANSFER_ORDER_DETAIL.Amount as   Total_Amt,'' as [AP Document No],0 as  [AP Document Amt],0 as [AP Document Discount Amt],0 as [AP Amount Before Tax]," _
            & " TSPL_TRANSFER_ORDER_HEAD.Document_No as against_PoInvoice_No,'' as Against_PurchaseREturn_No,0 as [AP Total Tax],0 as [AP Total Add Charge],0 as [AP Landed Amt]," _
            & " '' as Against_MillkpurchaseInvoice_No,'' as Against_BulkMillkpurchaseInvoice_No,0 as  [AP Document Total],TSPL_TRANSFER_ORDER_DETAIL.MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice " + Environment.NewLine +
            " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine +
            ",TSPL_TRANSFER_RETURN.Transfer_No as [Original Invoice No],convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details]" + Environment.NewLine +
            " from TSPL_TRANSFER_ORDER_HEAD " _
            & " left outer  join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_NO  " _
            & "  left join TSPL_TRANSFER_ORDER_HEAD out on out.Document_No=TSPL_TRANSFER_ORDER_HEAD.TransferOutNo " _
            & "  left join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_RETURN.Transfer_No " _
            & " left join tspl_Location_master on tspl_Location_master.LOcation_Code=out.From_Location " _
            & " left join tspl_Location_master recv on recv.location_code=TSPL_TRANSFER_ORDER_Head.To_Location left join TSPL_TRANSPORT_MASTER on " _
            & " TSPL_TRANSPORT_MASTER.transport_Id=tspl_Transfer_Order_Head.Transport_Id  left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =" _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on " _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=Stock_SU.Item_Code and TSPL_TRANSFER_ORDER_DETAIL.Unit_code=Stock_SU.UOM_Code  " _
            & " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on " _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=StockKG.Item_Code  " _
            & " left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Location_Master.State where TSPL_TRANSFER_ORDER_Head.Transfer_Type='I'  and isnull(TSPL_TRANSFER_RETURN.Document_No,'')<>'' and convert(date,TSPL_TRANSFER_RETURN.Document_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_TRANSFER_RETURN.Document_Date ,103) <= convert(date,('" + To_Date + "'),103) )t"

            ''------------



            strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine


            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            "  case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then Tspl_PR_detail.Total_Tax_Amt else -1 * Tspl_PR_detail.Total_Tax_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,0 as NonRecoverable_Tax, "
            strSDEndQry = ",TSPL_vendor_Invoice_Head.Document_No as [AP Document No], (case when Document_Type='C' then TSPL_vendor_Invoice_Head.Document_Total else -1 * TSPL_vendor_Invoice_Head.Document_Total end) *(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as  [AP Document Amt],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.Discount_Amount else -1 * TSPL_vendor_Invoice_Head.Discount_Amount end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Document Discount Amt],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.amount_less_Discount else -1 * TSPL_vendor_Invoice_Head.amount_less_Discount end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Amount Before Tax],TSPL_PR_DETAIL.PI_Id as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,(case when Document_Type='C' then TSPL_vendor_Invoice_Head.total_tax else -1 * TSPL_vendor_Invoice_Head.total_tax end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Total Tax],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.total_Add_Charge else -1 * TSPL_vendor_Invoice_Head.total_Add_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Total Add Charge],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.Total_landed_Amt else -1 * TSPL_vendor_Invoice_Head.Total_landed_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,(case when Document_Type='C' then TSPL_vendor_Invoice_Head.Document_total else -1 * TSPL_vendor_Invoice_Head.Document_total end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Document Total] ,PI_Id,Tspl_PR_detail.MRP,TSPL_PR_DETAIL.PI_Id as [Original Invoice No],PIHeadTable.PI_Date as [Original Invoice Date],isnull(TSPL_PR_HEAD.Description,'')+' '+isnull(TSPL_PR_HEAD.Remarks,'')+' '+isnull(TSPL_PR_HEAD.Comments,'') as [Reason For Revision] from Tspl_PR_detail " &
                               " left outer join Tspl_PR_Head on Tspl_PR_Head.PR_NO =Tspl_PR_detail.PR_NO " & Environment.NewLine +
                               " left outer join (select PI_Date,PI_No,Description,Remarks,Comments from TSPL_PI_HEAD) as PIHeadTable on PIHeadTable.PI_No =Tspl_PR_detail.PI_Id " & Environment.NewLine +
                               " left join TSPL_VEHICLE_MASTER on Vehicle_No=TSPL_VEHICLE_MASTER.Vehicle_Id left join TSPL_vendor_Invoice_Head on TSPL_vendor_Invoice_Head.Against_PurchaseReturn_No=Tspl_PR_Head.PR_NO and TSPL_vendor_Invoice_Head.Against_PurchaseReturn_No is not null  left join TSPL_vendor_master on   TSPL_vendor_master.Vendor_Code=tspl_PR_Head.Vendor_Code left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code " &
                               "    "
            strMainQuery += " select  max(Head_Tax_Group) as Head_Tax_Group,max(Head_Tax_Group_Type) as Head_Tax_Group_Type,'Purchase Return'  as [Trans Type],max(Line_No)as Line_No ,max(ConvRate) as ConvRate,max(TSPL_LOCATION_MASTER .location_Code) as [Location Code],	 max(TSPL_LOCATION_MASTER.Add1) + ' ' + max(TSPL_LOCATION_MASTER.Add2) + ' ' + max(TSPL_LOCATION_MASTER.Add3) As [Location Address] ,max(TSPL_LOCATION_MASTER.State) as [Location State],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.PR_NO as [Document No],'' as SRN_Id,final.PR_Date as [Document Date],final.Way_BillNo as [Way Bill No],Final.GRNO as [GR No],final.LR_NO as [LR No] ,max(VENDOR_INVOICE_no)as VENDOR_INVOICE_no,max(VENDOR_INVOICE_Date)as VENDOR_INVOICE_Date,vehicledesc,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Vendor Code] ,max(TSPL_vendor_MASTER .vendor_Name) as [Vendor Name],max([Sub Location]) as [Sub Location], max(TSPL_VENDOR_MASTER.Add1) + ' ' + max(TSPL_VENDOR_MASTER.Add2) + ' ' + max(TSPL_VENDOR_MASTER.Add3) As [Vendor Address],Max([State Code]) as [State Code],Max([State Name]) as [State Name],max(TSPL_vendor_MASTER .Tin_No) as [Vendor TIN No] ,max(TSPL_vendor_MASTER .Parent_vendor_Code) as [Parent Vendor No] ,max(Parent_Master.Vendor_Code) as [Parent Vendor Code],max(Parent_Master.Vendor_Name) as [Parent Vendor Name],Max(final.[Transporter]) as [Transporter],Max([Transporter Name]) as [Transporter Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.Amt_Less_Discount  as [Amount Less Discount],0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]  " + strPivotForOuterQuery + " " + strPivotFoGrouprOuterQuery + " " + strPivotFoAddChargeZeroGrouprOuterQuery + ",max(_Type) as [Tax Type] ,(final.Total_Tax_Amt-coalesce(sum(final.NonRecoverable_Tax),0)) as [Total Tax Amount],final.Total_Amt as [Total Amount],Max([AP Document No]) as [AP Document No],Max(coalesce([AP Document Amt],0)) as [AP Document Amt],Max(coalesce([AP Document Discount Amt],0)) as [AP Document Discount Amt],Max(coalesce([AP Amount Before Tax],0)) as [AP Amount Before Tax],Max(against_PoInvoice_No) as against_PoInvoice_No,Max(Against_PurchaseREturn_No) as Against_PurchaseREturn_No,(Max(coalesce([AP Total Tax],0))-coalesce(sum(final.NonRecoverable_Tax),0)) as [AP Total Tax],max(coalesce([AP Total Add Charge],0)) as [AP Total Add Charge],(Max(coalesce([AP Landed Amt],0))-coalesce(-sum(final.NonRecoverable_Tax),0)) as [AP Landed Amt],Against_MillkpurchaseInvoice_No, Against_BulkMillkpurchaseInvoice_No,Max(coalesce([AP Document Total],0)) as [AP Document Total],max(MRP) as MRP,coalesce(sum(final.NonRecoverable_Tax),0) as NonRecoverable_Tax ,'' as Purchase_Tax_Invoice   " + Environment.NewLine +
            "  ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine +
            " ,max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason For Revision]) as [Reason For Revision],null as [ITC Eligible],null as [ITC Status],null as [ITC Details]" + Environment.NewLine
            strMainQuery += " from ("
            strTaxColumns = " TSPL_PR_DETAIL.TAX1 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX1_Amt else -1 * TSPL_PR_DETAIL.TAX1_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX1_Amt, TSPL_PR_DETAIL.TAX1_Rate,TSPL_PR_DETAIL.TAX1+'%' as Tax1Rate,'' as _Type,'N' as Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt1"
            '' query for no tax applied
            strMainQuery += "  select Head_Tax_Group,Head_Tax_Group_Type,Trans_Type, Line_No,ConvRate ,Status  ,Bill_To_Location ,Customer_Code,[Sub Location] ,[State Code] ,[State Name] ,Invoice_Type ,PR_No  ,SRN_Id ,PR_Date ,Way_BillNo ,GRNo ,LR_NO ,Vendor_Invoice_No ,Vendor_Invoice_Date ,Transporter ,[Transporter Name] ,Item_Code ,Qty ,Unit_code ,Item_Cost ,Amount ,Disc_Per ,Disc_Amt ,Amt_Less_Discount ,Total_Tax_Amt ,Total_Amt ,vehicledesc ,Vehicle_No ,Additional_Charge ,NonRecoverable_Tax ,_Type,Tax_Recoverable,[AP Document No],[AP Document Amt],[AP Document Discount Amt],[AP Amount Before Tax],Against_POInvoice_No,Against_PurchaseReturn_No ,[AP Total Tax],[AP Total Add Charge],[AP Landed Amt],Against_MillkPurchaseInvoice_No,Against_BulkMillkPurchaseInvoice_No,[AP Document Total],PI_Id,MRP,[Original Invoice No],[Original Invoice Date],[Reason For Revision] " &
                " " + IIf(clsCommon.myLen(strPivotForInnerQuery) > 0, "," + strPivotForInnerQuery, "") + " " + IIf(clsCommon.myLen(strDoublePivotForInnerQuery) > 0, "," + strDoublePivotForInnerQuery, "") + " " + strPivotForAddChargeInnerQueryOuter + " " &
                " from( select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " where 2=2 and (coalesce(TSPL_PR_DETAIL.tax1,'')='' and coalesce(TSPL_PR_DETAIL.tax2,'')='' " &
                              " and coalesce(TSPL_PR_DETAIL.tax3,'')='' and coalesce(TSPL_PR_DETAIL.tax4,'')='' and " &
                              " coalesce(TSPL_PR_DETAIL.tax5,'')='' and coalesce(TSPL_PR_DETAIL.tax6,'')='' and " &
                              " coalesce(TSPL_PR_DETAIL.tax7,'')='' and coalesce(TSPL_PR_DETAIL.tax8,'')='' and " &
                              " coalesce(TSPL_PR_DETAIL.tax9,'')='' and coalesce(TSPL_PR_DETAIL.tax10,'')='') and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s  "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t "
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine
            '' query for tax1 applied
            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX1_Amt else 0 end) as NonRecoverable_Tax, "
            strTaxColumns = " TSPL_PR_DETAIL.TAX1 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX1_Amt else -1 * TSPL_PR_DETAIL.TAX1_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX1_Amt,TSPL_PR_DETAIL.TAX1_Rate, TSPL_PR_DETAIL.TAX1+'%' as Tax1Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt1"
            ''richa add filter date
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax1 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX1_Rate and ttr._type<>'OH'  left join tspl_tax_master tm on TSPL_PR_DETAIL.tax1=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code1 =AdCh .Code where 2=2  and (TSPL_PR_DETAIL.tax1<>'' or TSPL_PR_HEAD.Add_Charge_Code1<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX1,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t"
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t "
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX2_Amt else 0 end) as NonRecoverable_Tax, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX2 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX2_Amt else -1 * TSPL_PR_DETAIL.TAX2_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX2_Amt,TSPL_PR_DETAIL.TAX2_Rate, TSPL_PR_DETAIL.TAX2+'%' as Tax2Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code2 as Add_Charge_Code2 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt2 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt2 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt2"
            '' add filter date richa
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax2 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX2_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax2=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code2 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax2<>'' or TSPL_PR_HEAD.Add_Charge_Code2<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX2,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt2) for Add_Charge_Code2 in (" + strPivotForAddChargeInnerQuery + "))t "
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX3_Amt else 0 end) as NonRecoverable_Tax, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX3 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX3_Amt else -1 * TSPL_PR_DETAIL.TAX3_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX3_Amt, TSPL_PR_DETAIL.TAX3_Rate, TSPL_PR_DETAIL.TAX3+'%' as Tax3Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code3 as Add_Charge_Code3 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt3 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt3 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt3"
            ''add filter date richa
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax3 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX3_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax3=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code3 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax3<>'' or TSPL_PR_HEAD.Add_Charge_Code3<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX3,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt3) for Add_Charge_Code3 in (" + strPivotForAddChargeInnerQuery + "))t "
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX4_Amt else 0 end) as NonRecoverable_Tax, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX4 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX4_Amt else -1 * TSPL_PR_DETAIL.TAX4_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX4_Amt,TSPL_PR_DETAIL.TAX4_Rate, TSPL_PR_DETAIL.TAX4+'%' as Tax4Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_PR_Detail.ItemAdd_Charge_Code4 as Add_Charge_Code4 ,(case when Document_Type='C' then TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt4 else -1 * TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt4 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt4"
            ''add filter date richa
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax4 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX4_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax4=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code4 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax4<>''  or TSPL_PR_HEAD.Add_Charge_Code4 <>'') and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX4,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt4) for Add_Charge_Code4 in (" + strPivotForAddChargeInnerQuery + "))t "
            End If



            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX5_Amt else 0 end) as NonRecoverable_Tax, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX5 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX5_Amt else -1 * TSPL_PR_DETAIL.TAX5_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX5_Amt,TSPL_PR_DETAIL.TAX5_Rate, TSPL_PR_DETAIL.TAX5+'%' as Tax5Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code5 as Add_Charge_Code5 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt5 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt5 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt5"
            ''richa add date filter
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax5 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX5_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax5=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code5 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax5<>'' or TSPL_PR_HEAD.Add_Charge_Code5<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX5,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt5) for Add_Charge_Code5 in (" + strPivotForAddChargeInnerQuery + "))t "
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX6_Amt else 0 end) as NonRecoverable_Tax, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX6 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX6_Amt else -1 * TSPL_PR_DETAIL.TAX6_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX6_Amt,TSPL_PR_DETAIL.TAX6_Rate, TSPL_PR_DETAIL.TAX6+'%' as Tax6Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code6 as Add_Charge_Code6 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt6 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt6 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt6"
            ''richa add date filter
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax6 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX6_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax6=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code6 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax6<>'' or TSPL_PR_HEAD.Add_Charge_Code6<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX6,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt6) for Add_Charge_Code6 in (" + strPivotForAddChargeInnerQuery + "))t"
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX7_Amt else 0 end) as NonRecoverable_Tax, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX7 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX7_Amt else -1 * TSPL_PR_DETAIL.TAX7_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX7_Amt,TSPL_PR_DETAIL.TAX7_Rate, TSPL_PR_DETAIL.TAX7+'%' as Tax7Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code7 as Add_Charge_Code7 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt7 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt7 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt7"
            ''richa add date filter
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax7 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX7_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax7=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code7 =AdCh .Code  where 2=2 and (TSPL_PR_DETAIL.tax7<>''or TSPL_PR_HEAD.Add_Charge_Code7 <>'') and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX7,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt7) for Add_Charge_Code7 in (" + strPivotForAddChargeInnerQuery + "))t"
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX8_Amt else 0 end) as NonRecoverable_Tax, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX8 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX8_Amt else -1 * TSPL_PR_DETAIL.TAX8_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX8_Amt,TSPL_PR_DETAIL.TAX8_Rate, TSPL_PR_DETAIL.TAX8+'%' as Tax8Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_PR_Detail.ItemAdd_Charge_Code8 as Add_Charge_Code8 ,(case when Document_Type='C' then TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt8 else -1 * TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt8 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt8"
            ''richa add date filter
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax8 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX8_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax8=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code8 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax8<>''or TSPL_PR_HEAD.Add_Charge_Code8<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX8,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt8) for Add_Charge_Code8 in (" + strPivotForAddChargeInnerQuery + "))t"
            End If



            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX9_Amt else 0 end) as NonRecoverable_Tax, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX9 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX9_Amt else -1 * TSPL_PR_DETAIL.TAX9_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX9_Amt,TSPL_PR_DETAIL.TAX9_Rate, TSPL_PR_DETAIL.TAX9+'%' as Tax9Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code9 as Add_Charge_Code9 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt9 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt9 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt9"
            ''richa add date filter
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax9 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX9_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax9=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code9 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax9<>'' or TSPL_PR_HEAD.Add_Charge_Code9 <>'')  and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX9,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt9) for Add_Charge_Code9 in (" + strPivotForAddChargeInnerQuery + "))t"
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX10_Amt else 0 end) as NonRecoverable_Tax, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX10 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX10_Amt else -1 * TSPL_PR_DETAIL.TAX10_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX10_Amt,TSPL_PR_DETAIL.TAX10_Rate,TSPL_PR_DETAIL.TAX10+'%' as Tax10Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code10 as Add_Charge_Code10 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt10 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt10 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt10"
            ''richa add date filter
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & "  left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax10 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX10_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax10=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code10 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax10<>'' or TSPL_PR_HEAD.Add_Charge_Code10<>'' )  and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX10,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += "pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt10) for Add_Charge_Code10 in (" + strPivotForAddChargeInnerQuery + "))t"
            End If


            strMainQuery += " )final1)final"
            strMainQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
            strMainQuery += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
            strMainQuery += " left outer join TSPL_vendor_MASTER on TSPL_vendor_MASTER .Vendor_Code =final.Customer_Code "
            strMainQuery += " LEFT OUTER JOIN TSPL_vendor_MASTER as Parent_Master ON Parent_Master.Vendor_Code=TSPL_vendor_MASTER.Parent_Vendor_Code "
            strMainQuery += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
            strMainQuery += " group by  final.Trans_Type,final .Status  ,final.PR_NO ,final.Item_Code ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.PR_Date,final.Way_BillNo,Final.GRNO,final.LR_NO ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt ,final.Amt_Less_Discount ,final.Total_Amt,QC.FAT_Per,QC.SNF_Per,vehicledesc,Vehicle_No,final.Additional_Charge ,final.Against_BulkMillkPurchaseInvoice_No ,final.Against_MillkPurchaseInvoice_No ,final.PI_Id  " ', " + strPivotFoGrouprOuterQuery + "


            strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine

            'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,0 as TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
            strMainQuery += " select * from (Select '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'Milk Receipt' as Trans_Type,0 as Line_No,0 as ConvRate ,TSPL_LOCATION_MASTER .location_Code  as  Bill_To_Location, " _
             & " (TSPL_LOCATION_MASTER.Add1) + ' ' + (TSPL_LOCATION_MASTER.Add2) + ' ' + (TSPL_LOCATION_MASTER.Add3) As [Location Address],TSPL_LOCATION_MASTER.State as [Location State],TSPL_MILK_PURCHASE_INVOICE_Head.Posted as Status, " _
                & " tspl_mcc_Master.mcc_name,'Milk Receipt' as Invoice_Type,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_CODE as PI_NO,'' as SRN_Id ,  convert(varchar,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,'' as Vendor_Invoice_No,'' as Vendor_Invoice_Date," _
                & " TSPL_Primary_Vehicle_Master.Vehicle_Code as Vehicle_No, TSPL_Primary_Vehicle_Master.Description as vehicledesc,0  as Additional_Charge ,  " _
                & " TSPL_MILK_RECEIPT_DETAIL.Vsp_CODE as Customer_Code,  tspl_vendor_Master.Vendor_Name as Customer_Name,'' as [Sub Location],(TSPL_VENDOR_MASTER.Add1) + ' ' + (TSPL_VENDOR_MASTER.Add2) + ' ' + (TSPL_VENDOR_MASTER.Add3) As [Vendor Address],tspl_state_Master.state_Code as [State Code],tspl_State_Master.state_Name as [State Name],tspl_vendor_Master.Tin_No as [TIN No],Parent_V.vendor_Code as [Parent Vendor No],Parent_V.vendor_Code as [Parent Vendor Code],Parent_V.vendor_Name as [Parent Vendor Name],pm.vendor_Code as [Transporter],pm.Vendor_Name as [Transporter Name]," _
                & " TSPL_MILK_RECEIPT_DETAIL.Item_Code, tspl_Item_Master.Item_Desc ,  MILK_WEIGHT  as Qty ,TSPL_MILK_RECEIPT_DETAIL.UOM_Code as  Unit_code ,TSPL_MILK_SRN_DETAIL.RATE as  Item_Cost " _
                & " ,  TSPL_MILK_SRN_DETAIL.FAT_Per as [FAT Per],  TSPL_MILK_SRN_DETAIL.SNF_PER as [SNF Per],TSPL_MILK_SRN_DETAIL.FAT_KG as [FAT KG],TSPL_MILK_SRN_DETAIL.SNF_KG " _
                & " as [SNF KG],TSPL_MILK_SRN_DETAIL.Amount ,0 as Disc_Per  ,0 as Disc_Amt ,  TSPL_MILK_PURCHASE_INVOICE_DETAIL.NET_AMOUNT as  Amt_Less_Discount ," _
                & " round(coalesce(TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT*TSPL_MILK_PURCHASE_INVOICE_DETAIL.PAYMENT_COMMISSION/100,0),2) as EMP," _
                & " round(coalesce(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Incentive,0),2) as Incentive_Head,round(coalesce(IncentiveEMP,0),2) as IncentiveEMP " _
                & " " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type],  0 as Total_Tax_Amt ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.NET_AMOUNT as   Total_Amt," _
                & " TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount " _
                & " as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],stuff((select ',' + isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE  ,'')  FROM TSPL_MILK_PURCHASE_INVOICE_DETAIL WHERE TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE  for xml path ('')),1,1,'' )as against_PoInvoice_No," _
                & " TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as " _
                & " [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No," _
                & " TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax, TSPL_MILK_PURCHASE_INVOICE_HEAD.Purchase_Tax_Invoice " _
                & " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " _
                & " ,'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details] " _
                & " from TSPL_MILK_RECEIPT_DETAIL Left Outer Join TSPL_MILK_RECEIPT_HEAD        On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE  " _
                & " Left Outer Join TSPL_MILK_SAMPLE_HEAD        On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE =        TSPL_MILK_RECEIPT_HEAD.DOC_CODE Left Outer Join" _
                & " TSPL_MILK_SAMPLE_DETAIL        On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO        And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE =" _
                & " TSPL_MILK_SAMPLE_HEAD.DOC_CODE      Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE =        TSPL_MILK_SAMPLE_HEAD.DOC_CODE And " _
                & " TSPL_MILK_SRN_HEAD.SAMPLE_NO =        TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL        On TSPL_MILK_SRN_HEAD.DOC_CODE = " _
                & " TSPL_MILK_SRN_DETAIL.DOC_CODE      Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL        On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE =   " _
                & " TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join      TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE        = " _
                & " TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE Left Outer Join      TSPL_MCC_MASTER        On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE   " _
                & " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code =        TSPL_MILK_RECEIPT_DETAIL.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER  " _
                & " On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE      Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code =  " _
                & " TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE Left Outer Join      TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code =    " _
                & " TSPL_MCC_ROUTE_MASTER.Vehicle_Code left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code=TSPL_MILK_RECEIPT_DETAIL.Item_Code " _
                & " Left join tspl_Vendor_Master Parent_v on Parent_v.vendor_Code=tspl_Vendor_Master.Parent_Vendor_Code" _
                & " Left join tspl_Vendor_Master Pm on pm.vendor_Code=TSPL_Primary_Vehicle_Master.Vendor_Code" _
                & " left outer join TSPL_FAT_SNF_UPLOADER_MASTER on TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_MILK_SRN_DETAIL.Price_Code and TSPL_FAT_SNF_UPLOADER_MASTER.FAT=TSPL_MILK_SRN_DETAIL.FAT_PER and TSPL_FAT_SNF_UPLOADER_MASTER.SNF=TSPL_MILK_SRN_DETAIL.SNF_PER  " _
                & " left outer join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code " _
                & " left join tspl_Location_master on tspl_Location_master.location_code=tspl_Milk_receipt_Head.mcc_Code left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No=TSPL_MILK_PURCHASE_INVOICE_Head.DOC_CODE and len(coalesce(TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,''))>0  left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_Mcc_MASTER.State_Code where coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.doc_Code,'')<>''  AND TSPL_vendor_Invoice_Head.DOCUMENT_TYPE='I' and convert(date,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE ,103) <= convert(date,('" + To_Date + "'),103) and convert(date,TSPL_vendor_Invoice_Head.Invoice_Entry_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_vendor_Invoice_Head.Invoice_Entry_Date,103) <= convert(date,('" + To_Date + "'),103) )t"
        End If
        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine


        strMainQuery += "  select * from (Select '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'Bulk Purchase' as Trans_Type,0 as Line_No,0 as ConvRate ,TSPL_LOCATION_MASTER .location_Code  as  Bill_To_Location, (TSPL_LOCATION_MASTER.Add1) + ' ' + (TSPL_LOCATION_MASTER.Add2) + ' ' + (TSPL_LOCATION_MASTER.Add3) As [Location Address],TSPL_LOCATION_MASTER.State as [Location State],tspl_Bulk_milk_purchase_Invoice_head.isPosted " _
            & " as Status, TSPL_LOCATION_MASTER.Location_Desc as [Location Desc],'Bulk Purchase' as Invoice_Type,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO as PI_NO,'' as SRN_Id , " _
            & " convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No as Vendor_Invoice_No,convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.Doc_Date,103) as Vendor_Invoice_Date,Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end as Vehicle_No, Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end  " _
            & " as vehicledesc,Case when row_Number()over(partition by tspl_Bulk_milk_purchase_Invoice_head.DOC_NO Order by tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code)=1 then tspl_Bulk_milk_purchase_Invoice_head.RoundoffAMount else 0 end  as Additional_Charge ,  tspl_Bulk_milk_purchase_Invoice_head.vendor_code as Customer_Code, .tspl_Vendor_Master.Vendor_Name as Customer_Name,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Joblocation_Code as [Sub Location],(TSPL_VENDOR_MASTER.Add1) + ' ' + (TSPL_VENDOR_MASTER.Add2) + ' ' + (TSPL_VENDOR_MASTER.Add3) As [Vendor Address],tspl_State_Master.state_COde as [State Code],Tspl_State_Master.state_Name as [State Name], tspl_vendor_Master.Tin_No as [TIN No],Parent_v.vendor_Code as " _
            & " [Parent Vendor No],Parent_v.vendor_Code as [Parent Vendor Code],Parent_v.vendor_Name as [Parent Vendor Name],'' as Tanker_Transporter_Code,'' as [Transporter Name], tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code, " _
            & " tspl_Bulk_milk_purchase_Invoice_Detail.Item_Desc ,  tspl_Bulk_milk_purchase_Invoice_Detail.Net_Weight  as Qty ,tspl_Bulk_milk_purchase_Invoice_Detail.UOM " _
            & " as  Unit_code ,Case when   TSPL_Bulk_MILK_SRN.Approved_Rate<=0 then   tspl_Bulk_milk_purchase_Invoice_Detail.NetRate else   TSPL_Bulk_MILK_SRN.Approved_Rate end as  Item_Cost ,  tspl_Bulk_milk_purchase_Invoice_Detail.FAT_Per as [FAT Per],  tspl_Bulk_milk_purchase_Invoice_Detail.SNF_PER as [SNF Per]," _
            & " tspl_Bulk_milk_purchase_Invoice_Detail.FAT_KG as [FAT KG],tspl_Bulk_milk_purchase_Invoice_Detail.SNF_KG as [SNF KG],tspl_Bulk_milk_purchase_Invoice_Detail.Amount ,0 as Disc_Per  ,0 as Disc_Amt , " _
            & " tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type], " _
            & " 0 as Total_Tax_Amt ,tspl_Bulk_milk_purchase_Invoice_DETAIL.actual_amount as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],stuff((select ',' + isnull(tspl_Bulk_milk_purchase_Invoice_DETAIL.SRN_NO ,'')  FROM tspl_Bulk_milk_purchase_Invoice_DETAIL WHERE tspl_Bulk_milk_purchase_Invoice_DETAIL.DOC_NO =tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  for xml path ('')),1,1,'' )as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax, tspl_Bulk_milk_purchase_Invoice_head.Purchase_Tax_Invoice " _
            & " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " _
            & " ,'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details] " _
            & " from tspl_Bulk_milk_purchase_Invoice_head inner join" _
            & " tspl_Bulk_milk_purchase_Invoice_Detail on tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Head.DOC_NO left join " _
            & " TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_Bulk_milk_purchase_Invoice_head.Loc_Code left join TSPL_VENDOR_MASTER on " _
            & " TSPL_VENDOR_MASTER.Vendor_Code=tspl_Bulk_milk_purchase_Invoice_head.VENDOR_CODE left join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=" _
            & " tspl_Bulk_milk_purchase_Invoice_DETAIL.SRN_NO   Left join tspl_Vendor_Master Parent_v on Parent_v.vendor_Code=tspl_Vendor_Master.Parent_Vendor_Code  left join TSPL_Dispatch_BulkSale_Trade on TSPL_Dispatch_BulkSale_Trade.Against_SRN_No=TSPL_Bulk_MILK_SRN.SRN_NO  left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.Against_bulkmillkPurchaseInvoice_No=tspl_Bulk_milk_purchase_Invoice_head.DOC_NO and len(coalesce(TSPL_vendor_Invoice_Head.Against_bulkmillkPurchaseInvoice_No,''))>0 left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code where convert(date,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) <= convert(date,('" + To_Date + "'),103) and convert(date,TSPL_vendor_Invoice_Head.Invoice_Entry_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_vendor_Invoice_Head.Invoice_Entry_Date,103) <= convert(date,('" + To_Date + "'),103)  "

        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.IsAgainstJobWork,0)=1 "
        End If
        strMainQuery += " ) t "
        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine

        strMainQuery += " select * from (Select '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'Bulk Purchase Return' as Trans_Type,0 as Line_No,0 as ConvRate,TSPL_LOCATION_MASTER .location_Code  as  Bill_To_Location,(TSPL_LOCATION_MASTER.Add1) + ' ' + (TSPL_LOCATION_MASTER.Add2) + ' ' + (TSPL_LOCATION_MASTER.Add3) As [Location Address], " &
                            " TSPL_LOCATION_MASTER.State as [Location State],TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.isPosted  as Status,TSPL_LOCATION_MASTER.Location_Desc as [Location Desc],'Bulk Purchase Return' as Invoice_Type,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No as PI_NO,'' as SRN_Id , " &
                            " convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Vendor_Invoice_No as Vendor_Invoice_No,convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) as Vendor_Invoice_Date, " &
                            " Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end as Vehicle_No, Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end   as vehicledesc," &
                            " Case when row_Number()over(partition by TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No Order by TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Code)=1 then TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.RoundoffAMount else 0 end  as Additional_Charge ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.vendor_code as Customer_Code " &
                            " , .tspl_Vendor_Master.Vendor_Name as Customer_Name,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Joblocation_Code as [Sub Location],(TSPL_VENDOR_MASTER.Add1) + ' ' + (TSPL_VENDOR_MASTER.Add2) + ' ' + (TSPL_VENDOR_MASTER.Add3) As [Vendor Address],tspl_State_Master.state_COde as [State Code],Tspl_State_Master.state_Name as [State Name], tspl_vendor_Master.Tin_No as [TIN No],Parent_v.vendor_Code as  [Parent Vendor No], " &
                            " Parent_v.vendor_Code as [Parent Vendor Code],Parent_v.vendor_Name as [Parent Vendor Name],'' as Tanker_Transporter_Code,'' as [Transporter Name], TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Code,  TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Desc ,  -1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Net_Weight  as Qty ,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.UOM  as  Unit_code " &
                            " ,Case when   TSPL_Bulk_MILK_SRN.Approved_Rate<=0 then   TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.NetRate else   TSPL_Bulk_MILK_SRN.Approved_Rate end as  Item_Cost ,  TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.FAT_Per as [FAT Per],  TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SNF_PER as [SNF Per], -1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.FAT_KG as [FAT KG],-1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SNF_KG as [SNF KG],-1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Amount as Amount " &
                            " ,0 as Disc_Per  ,0 as Disc_Amt ,  -1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Actual_Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]  " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & " ,'' as [Tax Type],  0 as Total_Tax_Amt ,-1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.actual_amount as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],-1*TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],-1*TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt], " &
                            " TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],stuff((select ',' + isnull(TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SRN_NO ,'')  FROM TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL WHERE TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No =TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No  for xml path ('')),1,1,'' )as against_PoInvoice_No, " &
                            " TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No, " &
                            " TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice " &
                            " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " &
                            " ,'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details]" &
                            " from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD inner join TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL " &
                            " on TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No left join  TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Loc_Code left join TSPL_VENDOR_MASTER on  TSPL_VENDOR_MASTER.Vendor_Code=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.VENDOR_CODE " &
                            " left join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO= TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SRN_NO   Left join tspl_Vendor_Master Parent_v on Parent_v.vendor_Code=tspl_Vendor_Master.Parent_Vendor_Code  left join TSPL_Dispatch_BulkSale_Trade on TSPL_Dispatch_BulkSale_Trade.Against_SRN_No=TSPL_Bulk_MILK_SRN.SRN_NO  left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.Against_bulkmillkPurchaseInvoice_No=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No and TSPL_vendor_Invoice_Head.Against_bulkmillkPurchaseInvoice_No is not null  " &
                            " left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code where convert(date,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) <= convert(date,('" + To_Date + "'),103) "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.IsAgainstJobWork,0)=1 "
        End If
        strMainQuery += " ) t "

        strMainQuery += ") xx"
        strMainQuery += Environment.NewLine + Environment.NewLine
        strMainQuery += " left outer join tspl_item_master Item on Item.Item_Code =xx.[Item Code] " + Environment.NewLine
        strMainQuery += " left join (" & qryTransStock & ") as  TransStock on xx.[Item Code]=TransStock.Item_Code and TransStock.UOM_Code=" & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " " + Environment.NewLine
        strMainQuery += " left join (" & qryStock & ") as Stock_SU on xx.[Item Code]=Stock_SU.Item_Code and xx.[UOM]=Stock_SU.UOM_Code " + Environment.NewLine
        strMainQuery += " left join (" & qryKG & ") as StockKG on xx.[Item Code]=StockKG.Item_Code  " + Environment.NewLine
        strMainQuery += " left join (select Vendor_Code,Vendor_Group_Code,'' as Zone_Code,'' as Struct_Code,GST_Composition_scheme,GSTRegistered,GSTFinalNo,TSPL_VENDOR_MASTER.City_Code as VendorCityCode,TSPL_CITY_MASTER.City_Name as VendorCityName ,tspl_state_master.GST_STATE_Code as Vendor_GST_STATE_Code ,tspl_state_master.STATE_NAME as Veindor_STATE_Name from TSPL_vendor_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_VENDOR_MASTER.City_Code left outer join tspl_state_master on tspl_state_master.state_code =TSPL_vendor_MASTER.state_code ) as Cust on xx.[vENDOR Code]=Cust.Vendor_Code " + Environment.NewLine
        strMainQuery += " left outer join (select TSPL_LOCATION_MASTER.Registered,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.GSTNO,TSPL_LOCATION_MASTER.City_Code,TSPL_LOCATION_MASTER.City_Code as City_Name,tspl_state_master.GST_STATE_Code,tspl_state_master.STATE_NAME from TSPL_LOCATION_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_LOCATION_MASTER.City_Code     left outer join tspl_state_master on tspl_state_master.state_code =TSPL_LOCATION_MASTER.State  ) as TSPL_LOCATION_MASTER_AS_Transfer on TSPL_LOCATION_MASTER_AS_Transfer.Location_Code = xx.[vENDOR Code] and xx.[Trans Type] in ('MCC Transfer','Transfer')" + Environment.NewLine
        strMainQuery += " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_For_GSTIN on TSPL_LOCATION_MASTER_For_GSTIN.Location_Code = xx.[Location Code] " + Environment.NewLine +
        " left outer join TSPL_STATE_MASTER on  TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER_For_GSTIN.State " + Environment.NewLine +
        " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_SubLoc  on TSPL_LOCATION_MASTER_SubLoc.Location_Code = xx.[Sub Location] " + Environment.NewLine
        strMainQuery += " left join (select Ven_Group_Code,Group_Desc from TSPL_Vendor_GROUP) as Cust_Group on Cust.Vendor_Group_Code=Cust_Group.ven_Group_Code " + Environment.NewLine
        strMainQuery += " left join (select Zone_Code,Description from TSPL_ZONE_MASTER) as Zone on Cust.Zone_Code=Zone.Zone_Code " + Environment.NewLine
        strMainQuery += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=xx.Head_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type=xx.Head_Tax_Group_Type" + Environment.NewLine
        If clsCommon.myLen(strCategoryTable) > 0 Then
            strMainQuery += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=xx.[Item Code]" + Environment.NewLine
        End If
        strMainQuery += " left join tspl_item_master itmp on itmp.Item_Code=xx.[Item Code] " + Environment.NewLine + " left join TSPL_PURCHASE_ACCOUNTS tps on tps.Purchase_Class_Code=itmp.Purchase_Class_Code " + Environment.NewLine + " left join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code= " _
                   & " concat(SUBSTRING(tps.Inv_Control_Account,0,10), [Location Code]) " + Environment.NewLine + " where 2 = 2  and  convert(date,xx.[Document Date],103) >= convert(date,('" + From_Date + "'),103) and convert(date,xx.[Document Date],103) <= convert(date,('" + To_Date + "'),103) " ' + clsCommon.myCstr(IIf(clsCommon.myLen(txtUOM.Value) > 0, "and xx.[UOM]='" + txtUOM.Value + "' ", ""))
        QryLst.Add(strMainQuery)
        QryLst.Add(strPivotForFinalOuterQuery)
        QryLst.Add(strPivotForAddChargeFinalOuterSumQuery)
        Return QryLst
    End Function

    Public Shared Function ReturnQueryNew(ByVal From_Date As Date, ByVal To_Date As Date, ByVal Unit_Code As String, ByVal StockingUom As Boolean, Optional ByVal IsAgainstJobwork As Boolean = False) As ArrayList
        Dim strMainQuery As String = ""
        Dim QryLst As New ArrayList
        Dim strCodeColumn As String = ""
        Dim strCodeColumn1 As String = ""
        Dim strCodeColumnMax As String = ""
        Dim strCodeDescColumn As String = ""
        Dim strCodeDescColumnMax As String = ""
        Dim strPivotForFinalOuterQuery As String = ""
        Dim strPivotForAddChargeFinalOuterQuery As String = ""
        Dim strPivotForAddChargeFinalOuterSumQuery As String = ""
        Dim strCategoryTable As String = ""
        Dim dtCategory As DataTable
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
        If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
            For ii As Integer = 0 To dtCategory.Rows.Count - 1
                If ii <> 0 Then
                    strCodeColumn += ","
                    strCodeColumn1 += ","
                    strCodeColumnMax += ","
                    strCodeDescColumn += ","
                    strCodeDescColumnMax += ","
                End If
                strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeColumn1 += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"
            Next
            strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine &
            " select * from ( " + Environment.NewLine &
            " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine &
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+' Description' as Item_Category_CodeDesc " + Environment.NewLine &
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine &
            " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine &
            " from  TSPL_ITEM_MASTER  " + Environment.NewLine &
            " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine &
            " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine &
            " where 2=2 " + Environment.NewLine &
            " )xx" + Environment.NewLine

            If clsCommon.myLen(strCodeColumn) > 0 Then
                strCategoryTable = strCategoryTable + " Pivot " + Environment.NewLine &
            " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine &
            " ) Pivt" + Environment.NewLine
            End If

            If clsCommon.myLen(strCodeDescColumn) > 0 Then
                strCategoryTable = strCategoryTable + " Pivot " + Environment.NewLine &
           " (" + Environment.NewLine &
           " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine &
           " ) Pivt1 " + Environment.NewLine
            End If

            strCategoryTable = strCategoryTable + " ) xxx group by Item_Code "
        End If
        Dim qryTaxQuery As String = ""
        Dim qryAddChargeQuery As String = ""
        Dim qryAddChargeForZeroQuery As String = ""

        Dim strPivotForOuter As String
        Dim strPivotForOuterOnlyForDocumentInfo As String
        Dim strPivotForAddChargeOuter As String
        Dim lstTables As New List(Of String)
        Dim lstTablesAddCharge As New List(Of String)
        lstTables.Add("TSPL_PI_DETAIL")
        qryTaxQuery = GetTaxQuery(lstTables)
        lstTablesAddCharge.Add("TSPL_PI_HEAD")
        qryAddChargeQuery = GetAddChargeQuery(lstTablesAddCharge)
        qryAddChargeForZeroQuery = GetAddChargeZeroQuery(lstTablesAddCharge)
        strPivotForOuter = "select distinct (select Distinct ',sum(isnull(final.['+tax1+'],0)) as ['+TAX1+']' from ( " & qryTaxQuery
        strPivotForOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strPivotForOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuter))
        strPivotForOuterOnlyForDocumentInfo = "select distinct (select Distinct ',(isnull(final.['+tax1+'],0)) as ['+TAX1+']' from ( " & qryTaxQuery
        strPivotForOuterOnlyForDocumentInfo += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strPivotForOuterQueryOnlyForDocumentInfo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuterOnlyForDocumentInfo))
        strPivotForAddChargeOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',sum(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeOuter += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as xp)fin"

        Dim strPivotForAddChargeOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeOuter))
        Dim strPivotForFinalOuter As String
        strPivotForFinalOuter = ""
        strPivotForFinalOuter = " select distinct (select Distinct ',xx.['+tax1+']' from ( " & qryTaxQuery
        strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        strPivotForFinalOuterQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))
        Dim strPivotForAddChargeFinalOuter As String
        strPivotForAddChargeFinalOuter = ""
        strPivotForAddChargeFinalOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',xx.['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalOuter += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as xp)fin"
        strPivotForAddChargeFinalOuterQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuter))

        Dim strPivotForAddChargeFinalSum As String
        strPivotForAddChargeFinalSum = ""
        strPivotForAddChargeFinalSum = " select REPLACE(xp,'&amp;','&') from ( select distinct (select Distinct ',xx.['+'AC_'+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalSum += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        strPivotForAddChargeFinalOuterSumQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalSum))


        Dim strPivotForFinalOuterPercent As String
        strPivotForFinalOuterPercent = " select distinct (select  Distinct ',xx.['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForFinalOuterPercent += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strPivotForFinalOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuterPercent))

        Dim strPivotForTransfer_In As String
        strPivotForFinalOuter = ""
        strPivotForFinalOuter = " select distinct (select Distinct ',0 as ['+tax1+']' from ( " & qryTaxQuery
        strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        strPivotForTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))
        Dim strTransferTaxColumns As String = ""
        Dim strTransferTaxPerColumns As String = ""
        Dim strBulkTaxColumns As String = ""
        Dim strBulkTaxPerColumns As String = ""
        Dim dtTempDT As DataTable = clsDBFuncationality.GetDataTable("select * from (" + qryTaxQuery + ")xx where len(isnull(TAX1,''))>0  order by  TAX1")
        If dtTempDT IsNot Nothing AndAlso dtTempDT.Rows.Count > 0 Then
            For Each dr As DataRow In dtTempDT.Rows
                Dim strTax As String = clsCommon.myCstr(dr(0))
                strTransferTaxColumns += ", (case when TSPL_TRANSFER_ORDER_DETAIL.TAX1='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX2='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX3='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX4='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX5='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX6='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX7='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX8='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX9='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX10='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt else 0 end end end end end end end end end end ) as [" + strTax + "]"
                strTransferTaxPerColumns += ",(case when TSPL_TRANSFER_ORDER_DETAIL.TAX1='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX2='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX3='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX4='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX5='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX6='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX7='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX8='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX9='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX10='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate else 0 end end end end end end end end end end ) as [" + strTax + "%]"
                strBulkTaxColumns += ", (case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX1='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX1_Amt else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX2='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX2_Amt else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX3='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX3_Amt else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX4='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX4_Amt else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX5='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX5_Amt else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX6='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX6_Amt else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX7='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX7_Amt else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX8='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX8_Amt else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX9='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX9_Amt else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX10='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX10_Amt else 0 end end end end end end end end end end ) as [" + strTax + "]"
                strBulkTaxPerColumns += ",(case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX1='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX1_Rate else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX2='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX2_Rate else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX3='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX3_Rate else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX4='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX4_Rate else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX5='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX5_Rate else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX6='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX6_Rate else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX7='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX7_Rate else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX8='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX8_Rate else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX9='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX9_Rate else case when tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX10='" + strTax + "' then tspl_Bulk_milk_purchase_Invoice_DETAIL.TAX10_Rate else 0 end end end end end end end end end end ) as [" + strTax + "%]"
            Next
        End If
        ''richa for transfer return richa KDI/20/09/18-000432
        Dim strTransferReturnTaxColumns As String = ""
        If dtTempDT IsNot Nothing AndAlso dtTempDT.Rows.Count > 0 Then
            For Each dr As DataRow In dtTempDT.Rows
                Dim strTax As String = clsCommon.myCstr(dr(0))
                strTransferReturnTaxColumns += ", (case when TSPL_TRANSFER_ORDER_DETAIL.TAX1='" + strTax + "' then (-1 * TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt)  else case when TSPL_TRANSFER_ORDER_DETAIL.TAX2='" + strTax + "' then (-1 * TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt)  else case when TSPL_TRANSFER_ORDER_DETAIL.TAX3='" + strTax + "' then (-1 * TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt) else case when TSPL_TRANSFER_ORDER_DETAIL.TAX4='" + strTax + "' then (-1 * TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt) else case when TSPL_TRANSFER_ORDER_DETAIL.TAX5='" + strTax + "' then (-1 * TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt) else case when TSPL_TRANSFER_ORDER_DETAIL.TAX6='" + strTax + "' then (-1 * TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt) else case when TSPL_TRANSFER_ORDER_DETAIL.TAX7='" + strTax + "' then (-1 * TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt) else case when TSPL_TRANSFER_ORDER_DETAIL.TAX8='" + strTax + "' then (-1 * TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt) else case when TSPL_TRANSFER_ORDER_DETAIL.TAX9='" + strTax + "' then (-1 * TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt) else case when TSPL_TRANSFER_ORDER_DETAIL.TAX10='" + strTax + "' then (-1 * TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt) else 0 end end end end end end end end end end ) as [" + strTax + "]"
            Next
        End If
        ''------

        Dim strPivotForAddChargeFinalOuterPercent As String
        strPivotForAddChargeFinalOuterPercent = " select REPLACE(xp,'&amp;','&') from (select distinct (select  Distinct ',xx.['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalOuterPercent += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        Dim strPivotForFinalAddChargeOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuterPercent))

        Dim strPivotForAddChargeZeroFinalOuterPercent As String
        strPivotForAddChargeZeroFinalOuterPercent = " select REPLACE(xp,'&amp;','&') from ( select distinct (select  Distinct ',xx.['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery
        strPivotForAddChargeZeroFinalOuterPercent += " )aa where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        Dim strPivotForFinalAddChargeZeroOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeZeroFinalOuterPercent))

        Dim strPivotForAddChargeTransfer_In As String
        strPivotForAddChargeFinalOuter = ""
        strPivotForAddChargeFinalOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',0 as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalOuter += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        strPivotForAddChargeTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuter))

        Dim strPivotForAddChargeForZeroTransfer_In As String
        strPivotForAddChargeFinalOuter = ""
        strPivotForAddChargeFinalOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',0 as ['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery
        strPivotForAddChargeFinalOuter += " )aa where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        strPivotForAddChargeForZeroTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuter))


        Dim strPivotFortRANSFER_INPercent As String
        strPivotFortRANSFER_INPercent = " select distinct (select  Distinct ',0 as ['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotFortRANSFER_INPercent += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strPivotFortRANSFER_INPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotFortRANSFER_INPercent))

        Dim strPivotForGroupOuter As String
        strPivotForGroupOuter = " select REPLACE(abc,'&amp;','&') from ( select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForGroupOuter += " select distinct (select Distinct ',max(isnull(final.['+tax1+'%'+'],0)) as ['+TAX1+'%'+']' from ( " & qryTaxQuery

        strPivotForGroupOuter += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForGroupOuter))

        ''done below code so that when variable placed in query,no need for comma(,),in case of blank variable its gives error.(06/12/2016)
        If clsCommon.myLen(strPivotFoGrouprOuterQuery) > 0 Then
            strPivotFoGrouprOuterQuery = "," + strPivotFoGrouprOuterQuery
        End If





        Dim strPivotForGroupOuterOnlyForDocumnetInfo As String
        strPivotForGroupOuterOnlyForDocumnetInfo = " select REPLACE(abc,'&amp;','&') from ( select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForGroupOuterOnlyForDocumnetInfo += " select distinct (select Distinct ',(isnull(final.['+tax1+'%'+'],0)) as ['+TAX1+'%'+']' from ( " & qryTaxQuery

        strPivotForGroupOuterOnlyForDocumnetInfo += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoGrouprOuterQueryonlyForDocumnetInfo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForGroupOuterOnlyForDocumnetInfo))


        Dim strPivotForADDChargeGroupOuter As String
        strPivotForADDChargeGroupOuter = " select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        strPivotForADDChargeGroupOuter += " select distinct (select Distinct ',max(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForADDChargeGroupOuter += " )a where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoAddChargeGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForADDChargeGroupOuter))

        Dim strPivotForADDChargeZeroGroupOuter As String
        strPivotForADDChargeZeroGroupOuter = "select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        strPivotForADDChargeZeroGroupOuter += " select distinct (select Distinct ',sum(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery

        strPivotForADDChargeZeroGroupOuter += " )a where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoAddChargeZeroGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForADDChargeZeroGroupOuter))

        ''done below code so that when variable placed in query,no need for comma(,),in case of blank variable its gives error.(06/12/2016)
        If clsCommon.myLen(strPivotFoAddChargeZeroGrouprOuterQuery) > 0 Then
            strPivotFoAddChargeZeroGrouprOuterQuery = "," + strPivotFoAddChargeZeroGrouprOuterQuery
        End If



        Dim strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo As String
        strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo = "select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo += " select distinct (select Distinct ',(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery

        strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo += " )a where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoAddChargeZeroGrouprOuterQueryonlyForDocumentInfo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo))


        Dim strPivotForOuterForBulk As String
        strPivotForOuterForBulk = " select distinct (select Distinct ',0 as ['+TAX1+']' from ( " & qryTaxQuery
        strPivotForOuterForBulk += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuterForBulk))
        Dim strDoublePivotForOuterForBulk As String
        strDoublePivotForOuterForBulk = " select distinct (select Distinct ',0 as ['+tax1+'%'+']' from ( " & qryTaxQuery
        strDoublePivotForOuterForBulk += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strDoublePivotForOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForOuterForBulk))

        Dim strPivotForAddChargeOuterForBulk As String
        strPivotForAddChargeOuterForBulk = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',0 as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForAddChargeOuterForBulk += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"

        Dim strPivotForAddChargeOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeOuterForBulk))

        Dim strPivotForInner As String
        strPivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
        strPivotForInner += " select distinct (select Distinct ',['+tax1+']' from ( " & qryTaxQuery

        strPivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

        Dim strPivotForInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForInner))

        Dim strPivotForAddChargeInner As String
        strPivotForAddChargeInner = "select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        strPivotForAddChargeInner += " select distinct (select Distinct ',['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForAddChargeInner += " )a where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as ax)Axx)XXX"

        Dim strPivotForAddChargeInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeInner))

        Dim strPivotForAddChargeInnerOuter As String
        strPivotForAddChargeInnerOuter = " select REPLACE(abc,'&amp;','&') from (select ','+SUBSTRING(ax,2,len(Ax)) as abc from ("
        strPivotForAddChargeInnerOuter += " select distinct (select Distinct ',['+Add_Charge_Code1+']  as ['+'AC_'+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForAddChargeInnerOuter += " )a where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as ax)Axx)XXX"
        Dim strPivotForAddChargeInnerQueryOuter As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeInnerOuter))




        Dim strDoublePivotForInner As String
        strDoublePivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
        strDoublePivotForInner += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery

        strDoublePivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

        Dim strDoublePivotForInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForInner))
        Dim qryQC As String = ""
        qryQC = " select Item_Code,MAX(Fat_Per) as Fat_Per,MAX(SNF_Per) as SNF_Per from (" &
                " select Item_QCP.Item_Code,Item_QCP.Code as Parameter_Code,(case when QCP.Type='FAT' then Item_QCP.Actual_Range end) as Fat_Per," &
                " (case when QCP.Type='SNF' then Item_QCP.Actual_Range  end) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QCP " &
                " left join TSPL_PARAMETER_MASTER QCP  on Item_QCP.Code=QCP.Code) as QC group by Item_Code"

        Dim qryKG As String = ""
        qryKG = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG'"
        Dim qryStock As String = ""
        qryStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "

        '' query for transaction  UOM conversion
        Dim qryTransStock As String = ""
        If clsCommon.myLen(Unit_Code) <= 0 AndAlso StockingUom = False Then
            qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "
        Else
            If StockingUom Then
                qryTransStock = "select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code"
            Else
                qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='" & Unit_Code & "'"
            End If
        End If

        '' query for structure and item group custom field
        Dim strSDCommonQuery As String = ""
        Dim strTaxColumns As String = ""
        Dim strAddChargeColumns As String = ""
        Dim strTaxNonRecoverableAmt As String = ""
        Dim strSDEndQry As String = ",TSPL_PI_DETAIL.TAX1+'%' as Tax1_Rate"
        strSDCommonQuery = " select Distinct  TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'PI' as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location], " &
                           " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],'PI' as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                           " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                           " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                           " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                           " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                           " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,0 as NonRecoverable_Tax,TSPL_PI_HEAD.isJobWorkOutward,(TSPL_PI_DETAIL.Landed_Cost_Amount+(case when TSPL_PI_HEAD.Is_Shortage_Include_In_Landed_Cost=0 then TSPL_PI_DETAIL.Shortage_Amount else 0 end))*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Landed_Cost_Amount, "
        strSDEndQry = ",TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Amount Before Tax],TSPL_PI_DETAIL.SRN_Id as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Document Total],TSPL_PI_DETAIL.Po_Id,TSPL_PI_DETAIL.MRP,TSPL_PI_HEAD.Purchase_Tax_Invoice,TSPL_PURCHASE_ORDER_HEAD.Created_By,TSPL_PURCHASE_ORDER_HEAD.Modify_By,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as Doc_No,TSPL_PURCHASE_ORDER_head.PurchaseOrder_Date as Doc_Purchase_Date " +
        ",case when TSPL_PI_Head.PI_Type='I' then 'Yes' else 'No' end as [Import Type],case when TSPL_PI_Head.PI_Type='I' then TSPL_PI_Head.Port  else null end as [Port],case when TSPL_PI_Head.PI_Type='I' then TSPL_PI_Head.Import_Entry_No else null end as [Import Bill of Entry No],case when TSPL_PI_Head.PI_Type='I' then convert(varchar, TSPL_PI_Head.Import_Entry_Date,103) else null end as [Import Bill of Entry Date]" + Environment.NewLine +
        ",'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],case when TSPL_PI_HEAD.ITC_Elibible=1 then 'Yes' else 'No' end as [ITC Eligible],case when TSPL_PI_HEAD.ITC_Elibible=1 then case when TSPL_PI_HEAD.ITC_Type=1 then 'Yes' else 'No' end else '' end as [ITC Status],case when TSPL_PI_HEAD.ITC_Elibible=1 then TSPL_PI_HEAD.ITC_Type_Category else '' end as [ITC Details],TSPL_PI_HEAD.PI_Date as DocDateView " + Environment.NewLine +
                        " from TSPL_PI_DETAIL " &
                           " left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_NO =TSPL_PI_DETAIL.PI_NO " &
                           " left join TSPL_VEHICLE_MASTER on TSPL_PI_HEAD.vehicledesc=TSPL_VEHICLE_MASTER.Vehicle_Id " &
                           " left join TSPL_vendor_Invoice_Head on TSPL_vendor_Invoice_Head.against_PoInvoice_No=TSPL_PI_HEAD.PI_NO  and TSPL_vendor_Invoice_Head.against_PoInvoice_No is not null and TSPL_vendor_Invoice_Head.Document_Type='I'"  ''By Balwinder Against Ticket No BHA/07/06/19-000902 on 14/06/2019
        strSDEndQry += " left join TSPL_vendor_master on   TSPL_vendor_master.Vendor_Code=tspl_Pi_Head.Vendor_Code left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code " &
                           "    "
        strMainQuery = "  select [Trans Type],[Location Code],[Location Name],[Location Address],TSPL_STATE_MASTER.GST_STATE_Code as [Location State GST], TSPL_STATE_MASTER.STATE_CODE as [Location State Code], TSPL_STATE_MASTER.STATE_NAME as [Location State Name],TSPL_LOCATION_MASTER_For_GSTIN.City_Code as VendorCityCode, TSPL_LOCATION_MASTER_For_GSTIN.City_Code as VendorCityName,TSPL_LOCATION_MASTER_For_GSTIN.GSTNO as Location_GSTIN,[Invoice Type],[Document No],[Document Date],[Way Bill No],[GR No],[LR No],Vendor_Invoice_no as [Vendor Invoice No],case when Coalesce(Vendor_Invoice_no,'')<>'' then convert(varchar,Vendor_Invoice_Date,103) else null end as [Vendor Invoice Date],vehicledesc as [Vehicle Code],Vehicle_No as [Vehicle No],cast(Additional_Charge as numeric(18,3)) as [Additional Amount],[Vendor Code],[Vendor Name]," & If(IsAgainstJobwork = True, "[Sub Location],TSPL_LOCATION_MASTER_SubLoc.Location_Desc as [Sub Location Name],", "") & " [Vendor Address],[State Code] as [Vendor State Code],[State Name] as [Vendor State Desc.],case when cust.GST_Composition_scheme=1 then 'Yes' else 'No' end as GST_Composition_scheme,case when [Trans Type] in ('MCC Transfer','Transfer','Transfer Return') then case when TSPL_LOCATION_MASTER_AS_Transfer.Registered=1 then 'Yes' else 'No' end else case when cust.GSTRegistered=1 then 'Yes' else 'No' end end GSTRegistered,case when [Trans Type] in ('MCC Transfer','Transfer','Transfer Return') then TSPL_LOCATION_MASTER_AS_Transfer.GSTNO else cust.GSTFinalNo end GSTFinalNo ,case when [Trans Type] in ('MCC Transfer','Transfer','Transfer Return') then TSPL_LOCATION_MASTER_AS_Transfer.GST_STATE_Code else cust.Vendor_GST_STATE_Code end as Vendor_GST_STATE_Code,case when [Trans Type] in ('MCC Transfer','Transfer','Transfer Return') then TSPL_LOCATION_MASTER_AS_Transfer.STATE_NAME else cust.Veindor_STATE_Name end as Veindor_STATE_Name,[Vendor TIN No],xx.[Transporter],[Transporter Name],Cust.Vendor_Group_Code as [Vendor Group Code],Cust_Group.Group_Desc as [Vendor Group Description], [Parent Vendor No],[Parent Vendor Code], [Parent Vendor Name]"
        If clsCommon.myLen(strCategoryTable) > 0 Then
            strMainQuery += "," + strCodeColumn1 + "," + strCodeDescColumn
        End If
        ''richa richa KDI/20/09/18-000432 richa again changes on 15 Nov,2018
        'strMainQuery += " , [Item Code],[Item Name],cast(([Quantity]*Stock_SU.Conversion_Factor)/(coalesce(TransStock.Conversion_Factor,1)) as Numeric(18,3)) as [Quantity]," & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " as [UOM],[Item Cost] as [Item Rate],[Fat Per] as [FAT %],[SNF Per] as [SNF %],cast(([Quantity]*[Fat Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast(([Quantity]*[SNF Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],Amount,[Discount Per] as [Discount %], [Discount Amount],cast(Round(XX.[EMP],2) as decimal(18,2)) as [EMP],Round([Incentive],2) as [Incentive],Round([IncentiveEMP],0) as [Incentive EMP] ,Round([Amount Less Discount],2) as [Amount Less Discount] " + strPivotForFinalOuterQuery + " " + strPivotForFinalOuterPercentQuery + "" + strPivotForFinalAddChargeZeroOuterPercentQuery + ",[Tax Type] as [Form Type],round(([Total Amount]+cast(Additional_Charge as numeric(18,3))-[Total Tax Amount]),2) as [Purchase Amount],Round([Total Tax Amount],2) as [Total Tax Amount], Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2) as [Total Amount], " & _
        ''strMainQuery += " , [Item Code],[Item Name],cast(([Quantity]*Stock_SU.Conversion_Factor)/(coalesce(TransStock.Conversion_Factor,1)) as Numeric(18,3)) as [Quantity]," & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " as [UOM],[Item Cost] as [Item Rate],[Fat Per] as [FAT %],[SNF Per] as [SNF %],cast(([Quantity]*[Fat Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast(([Quantity]*[SNF Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],Amount,[Discount Per] as [Discount %], [Discount Amount],cast(Round(XX.[EMP],2) as decimal(18,2)) as [EMP],Round([Incentive],2) as [Incentive],Round([IncentiveEMP],0) as [Incentive EMP] ,Round([Amount Less Discount],2) as [Amount Less Discount] " + strPivotForFinalOuterQuery + " " + strPivotForFinalOuterPercentQuery + "" + strPivotForFinalAddChargeZeroOuterPercentQuery + ",[Tax Type] as [Form Type],case when [Trans Type]  in ('Transfer Return','Transfer')  then round(([Total Amount]+cast(Additional_Charge as numeric(18,3))),2) else round(([Total Amount]+cast(Additional_Charge as numeric(18,3))-[Total Tax Amount]),2) end as [Purchase Amount],Round([Total Tax Amount],2) as [Total Tax Amount], Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2) as [Total Amount], " & _
        strMainQuery += " , [Item Code],[Item Name],cast(([Quantity]*Stock_SU.Conversion_Factor)/(coalesce(TransStock.Conversion_Factor,1)) as Numeric(18,3)) as [Quantity]," & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " as [UOM],[Item Cost] as [Item Rate],[Fat Per] as [FAT %],[SNF Per] as [SNF %],cast(([Quantity]*[Fat Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast(([Quantity]*[SNF Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],Amount,[Discount Per] as [Discount %], [Discount Amount],cast(Round(XX.[EMP],2) as decimal(18,2)) as [EMP],Round([Incentive],2) as [Incentive],Round([IncentiveEMP],0) as [Incentive EMP] ,Round([Amount Less Discount],2) as [Amount Less Discount] " + strPivotForFinalOuterQuery + " " + strPivotForFinalOuterPercentQuery + "" + strPivotForFinalAddChargeZeroOuterPercentQuery + ",[Tax Type] as [Form Type],case when [Trans Type]  in ('Transfer Return','Transfer','Bulk Purchase')  then round(([Total Amount]+cast(Additional_Charge as numeric(18,3))),2) else round(([Total Amount]+cast(Additional_Charge as numeric(18,3))-[Total Tax Amount]),2) end as [Purchase Amount],Round([Total Tax Amount],2) as [Total Tax Amount],  " &
            " case when [Trans Type]  in ('Transfer Return','Transfer','Bulk Purchase')  then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]+[Total Tax Amount]),2) else  Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2) end as [Total Amount], " &
         "  TabPurchaseGLCode.Account_Code as [Inventory Account Code],TabPurchaseGLCode.Description as [Inventory Account Name],[AP Document No] ,coalesce(against_PoInvoice_No, coalesce(Against_PurchaseREturn_No,coalesce(Against_MillkpurchaseInvoice_No,Against_BulkMillkpurchaseInvoice_No))) as [Against Invoice No],[AP Total Tax],[AP Total Add Charge],[AP Landed Amt],[AP Document Total],MRP, Item.HSN_Code,Purchase_Tax_Invoice " +
                " ,case when cust.GST_Composition_scheme=1 then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [Composition Amount] ,case when [Total Tax Amount]=0 then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [NILL Rate Amount],case when TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted=1 then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [Exempted Amount] " + Environment.NewLine + Environment.NewLine +
                " ,case when item.Skip_GST=1 then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [Non-GST Amount],[Import Type],[Import Bill of Entry No],[Import Bill of Entry Date],case when [Import Type]='Yes' then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [Import Bill of Entry Amount],[Original Invoice No],case when len(isnull([Original Invoice No],''))>0 then convert(varchar, [Original Invoice Date],103) else null end as [Original Invoice Date],[Reason For Revision],[ITC Eligible],[ITC Status],[ITC Details],DocDateView,isJobWorkOutward,cast(case when  [Invoice Type] in ('PI','Purchase Return','MT') then Landed_Cost_Amount else Round([Amount Less Discount],2) end  as decimal(18,2)) * (case when [Invoice Type] in ('Transfer Return','Purchase Return','Bulk Purchase Return') then -1 else 1 end)    as Landed_Cost_Amount " + Environment.NewLine
        strMainQuery += " from ( "
        strMainQuery += Environment.NewLine + Environment.NewLine + "  "
        strMainQuery += "  select max(Head_Tax_Group) as Head_Tax_Group,max(Head_Tax_Group_Type) as Head_Tax_Group_Type, case when Trans_Type ='PI' then 'Purchase Invoice' when Trans_Type ='Transfer' then 'Transfer' when Trans_Type='MCC' then 'Milk Receipt' when Trans_Type='Bulk' then 'Bulk Purchase'  when Trans_Type='Bulk Purchase Return' then 'Bulk Purchase Return' when Trans_Type ='MT' then 'Merchant Trade' end  as [Trans Type],max(final.Line_No) as Line_No,max(final.ConvRate) as ConvRate,max(TSPL_LOCATION_MASTER .location_Code) as [Location Code],max(TSPL_LOCATION_MASTER.Add1) + ' ' + max(TSPL_LOCATION_MASTER.Add2) + ' ' + max(TSPL_LOCATION_MASTER.Add3) As [Location Address],max(TSPL_LOCATION_MASTER.State) as [Location State],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.PI_NO as [Document No],final.SRN_Id as [SRN No],final.PI_Date as [Document Date],final.Way_BillNo as [Way Bill No],Final.GRNO as [GR No],final.LR_NO as [LR No] ,max(VENDOR_INVOICE_no)as VENDOR_INVOICE_no,max(VENDOR_INVOICE_Date)as VENDOR_INVOICE_Date,vehicledesc,Vehicle_No,final.Additional_Charge+Case when coalesce(final.Additional_Charge,0)>0 then coalesce(max(PACKING),0) else 0 end as Additional_Charge,final.Customer_Code as [Vendor Code] ,max(TSPL_vendor_MASTER .vendor_Name) as [Vendor Name],max([Sub Location]) as [Sub Location],max(TSPL_VENDOR_MASTER.Add1) + ' ' + max(TSPL_VENDOR_MASTER.Add2) + ' ' + max(TSPL_VENDOR_MASTER.Add3) As [Vendor Address],Max([State Code]) as [State Code],Max([State Name]) as [State Name],max(TSPL_vendor_MASTER .Tin_No) as [Vendor TIN No] ,max(TSPL_vendor_MASTER .Parent_vendor_Code) as [Parent Vendor No] ,max(Parent_Master.Vendor_Code) as [Parent Vendor Code],max(Parent_Master.Vendor_Name) as [Parent Vendor Name],Max(final.[Transporter]) as [Transporter],Max([Transporter Name]) as [Transporter Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.Amt_Less_Discount  as [Amount Less Discount],0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]  " + strPivotForOuterQuery + " " + strPivotFoGrouprOuterQuery + " " + strPivotFoAddChargeZeroGrouprOuterQuery + ",max(_Type) as [Tax Type] ,(final.Total_Tax_Amt-coalesce(sum(final.NonRecoverable_Tax),0)) as [Total Tax Amount],final.Total_Amt as [Total Amount],Max([AP Document No]) as [AP Document No],Max(coalesce([AP Document Amt],0)) as [AP Document Amt],Max(coalesce([AP Document Discount Amt],0)) as [AP Document Discount Amt],Max(coalesce([AP Amount Before Tax],0)) as [AP Amount Before Tax],Max(against_PoInvoice_No) as against_PoInvoice_No,Max(Against_PurchaseREturn_No) as Against_PurchaseREturn_No,Max(coalesce([AP Total Tax],0)) as [AP Total Tax],max(coalesce([AP Total Add Charge],0)) as [AP Total Add Charge],(Max(coalesce([AP Landed Amt],0))-coalesce(sum(final.NonRecoverable_Tax),0)) as [AP Landed Amt],max(Against_MillkpurchaseInvoice_No) as Against_MillkpurchaseInvoice_No,Max(Against_BulkMillkpurchaseInvoice_No) as Against_BulkMillkpurchaseInvoice_No,Max(coalesce([AP Document Total],0)) as [AP Document Total],max(final.MRP) as MRP,coalesce(sum(final.NonRecoverable_Tax),0) as NonRecoverable_Tax,max(final.Purchase_Tax_Invoice) as Purchase_Tax_Invoice " + Environment.NewLine +
        ",max([Import Type]) as [Import Type],max([Port]) as [Port], max([Import Bill of Entry No]) as [Import Bill of Entry No],max([Import Bill of Entry Date]) as [Import Bill of Entry Date]" + Environment.NewLine +
        ",max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason For Revision]) as [Reason For Revision],max([ITC Eligible]) as [ITC Eligible],max([ITC Status]) as [ITC Status],max([ITC Details]) as [ITC Details],max(DocDateView) as DocDateView,max(isJobWorkOutward) as isJobWorkOutward,Max(Landed_Cost_Amount) as Landed_Cost_Amount "
        strMainQuery += " from (" + Environment.NewLine
        strTaxColumns = " TSPL_PI_DETAIL.TAX1 ,TSPL_PI_DETAIL.TAX1_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX1_Amt, TSPL_PI_DETAIL.TAX1_Rate,TSPL_PI_DETAIL.TAX1+'%' as Tax1Rate,'' as _Type,'N' as Tax_Recoverable "
        strAddChargeColumns = " ,TSPL_PI_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt1*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt1 "
        '' query for no tax applied
        strMainQuery += " select Head_Tax_Group,Head_Tax_Group_Type,Trans_Type,Line_No,ConvRate ,SRN_Id ,Status ,Bill_To_Location,Customer_Code,[Sub Location] ,[State Code] ,[State Name] ,Invoice_Type ,PI_No ,PI_Date ,Way_BillNo ,GRNo ,LR_No ,Vendor_Invoice_No ,Vendor_Invoice_Date ,Transporter ,[Transporter Name] ,Item_Code ,Qty ,Unit_code ,Item_Cost ,Amount ,Disc_Per,Disc_Amt ,Amt_Less_Discount ,Total_Tax_Amt ,Total_Amt ,Vehicle_No ,VehicleDesc ,Additional_Charge ,NonRecoverable_Tax ,_Type ,Tax_Recoverable ,[AP Document No] ,[AP Document Amt],[AP Document Discount Amt] ,[AP Amount Before Tax],Against_POInvoice_No,Against_PurchaseReturn_No,[AP Total Tax],[AP Total Add Charge],[AP Landed Amt],Against_MillkPurchaseInvoice_No,Against_BulkMillkPurchaseInvoice_No,[AP Document Total],PO_ID,MRP,final1.Purchase_Tax_invoice,[Import Type],[Port], [Import Bill of Entry No],[Import Bill of Entry Date] " &
            ",[Original Invoice No],[Original Invoice Date],[Reason For Revision],[ITC Eligible],[ITC Status],[ITC Details],DocDateView,isJobWorkOutward,Landed_Cost_Amount  " + Environment.NewLine +
            " " + IIf(clsCommon.myLen(strPivotForInnerQuery) > 0, "," + strPivotForInnerQuery, "") + " " + IIf(clsCommon.myLen(strDoublePivotForInnerQuery) > 0, "," + strDoublePivotForInnerQuery, "") + " " + strPivotForAddChargeInnerQueryOuter + "  " &
            " from (select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2 and (coalesce(TSPL_PI_DETAIL.tax1,'')='' and coalesce(TSPL_PI_DETAIL.tax2,'')='' " &
                          " and coalesce(TSPL_PI_DETAIL.tax3,'')='' and coalesce(TSPL_PI_DETAIL.tax4,'')='' and " &
                          " coalesce(TSPL_PI_DETAIL.tax5,'')='' and coalesce(TSPL_PI_DETAIL.tax6,'')='' and " &
                          " coalesce(TSPL_PI_DETAIL.tax7,'')='' and coalesce(TSPL_PI_DETAIL.tax8,'')='' and " &
                          " coalesce(TSPL_PI_DETAIL.tax9,'')='' and coalesce(TSPL_PI_DETAIL.tax10,'')='') and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then
            strMainQuery += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then
            strMainQuery += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then
            strMainQuery += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type, case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                          " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                          " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                          " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                          " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                          " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                          " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX1_Amt else 0 end) as NonRecoverable_Tax,TSPL_PI_HEAD.isJobWorkOutward,(TSPL_PI_DETAIL.Landed_Cost_Amount+(case when TSPL_PI_HEAD.Is_Shortage_Include_In_Landed_Cost=0 then TSPL_PI_DETAIL.Shortage_Amount else 0 end))*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Landed_Cost_Amount, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX1 ,TSPL_PI_DETAIL.TAX1_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX1_Amt, TSPL_PI_DETAIL.TAX1_Rate,TSPL_PI_DETAIL.TAX1+'%' as Tax1Rate,ttr._Type as _Type,tm.Tax_Recoverable "
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt1*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt1 "

        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_PI_DETAIL.tax1 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX1_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax1=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code1 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2    and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103)  and coalesce(TSPL_PI_HEAD.tax1,'')<>'' "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then
            strMainQuery += "pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then
            strMainQuery += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then
            strMainQuery += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If
        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine
        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                          " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                          " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                          " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                          " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                          " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                          " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX2_Amt else 0 end) as NonRecoverable_Tax,TSPL_PI_HEAD.isJobWorkOutward,(TSPL_PI_DETAIL.Landed_Cost_Amount+(case when TSPL_PI_HEAD.Is_Shortage_Include_In_Landed_Cost=0 then TSPL_PI_DETAIL.Shortage_Amount else 0 end))*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Landed_Cost_Amount, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX2 ,TSPL_PI_DETAIL.TAX2_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX2_Amt,TSPL_PI_DETAIL.TAX2_Rate, TSPL_PI_DETAIL.TAX2+'%' as Tax2Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code2 as Add_Charge_Code2 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt2*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt2 "
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax2 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX2_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax2=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code2 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax2,'')<>'' "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt2) for Add_Charge_Code2 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If
        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine
        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                         " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX3_Amt else 0 end) as NonRecoverable_Tax,TSPL_PI_HEAD.isJobWorkOutward,(TSPL_PI_DETAIL.Landed_Cost_Amount+(case when TSPL_PI_HEAD.Is_Shortage_Include_In_Landed_Cost=0 then TSPL_PI_DETAIL.Shortage_Amount else 0 end))*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Landed_Cost_Amount, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX3 ,TSPL_PI_DETAIL.TAX3_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)  as TAX3_Amt, TSPL_PI_DETAIL.TAX3_Rate, TSPL_PI_DETAIL.TAX3+'%' as Tax3Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code3 as Add_Charge_Code3 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt3*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt3 "
        ''add date filter richa
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax3 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX3_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax3=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code3 =AdCh .Code  left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax3,'')<>'' "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then
            strMainQuery += "pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then
            strMainQuery += " pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then
            strMainQuery += " pivot (sum(Add_Charge_Amt3) for Add_Charge_Code3 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If
        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                         " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX4_Amt else 0 end) as NonRecoverable_Tax,TSPL_PI_HEAD.isJobWorkOutward,(TSPL_PI_DETAIL.Landed_Cost_Amount+(case when TSPL_PI_HEAD.Is_Shortage_Include_In_Landed_Cost=0 then TSPL_PI_DETAIL.Shortage_Amount else 0 end))*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Landed_Cost_Amount, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX4 ,TSPL_PI_DETAIL.TAX4_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX4_Amt,TSPL_PI_DETAIL.TAX4_Rate, TSPL_PI_DETAIL.TAX4+'%' as Tax4Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code4 as Add_Charge_Code4 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt4*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt4 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax4 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX4_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax4=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code4 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax4,'')<>'' "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then
            strMainQuery += "pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then
            strMainQuery += " pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then
            strMainQuery += " pivot (sum(Add_Charge_Amt4) for Add_Charge_Code4 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine
        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                         " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX5_Amt else 0 end) as NonRecoverable_Tax,TSPL_PI_HEAD.isJobWorkOutward,(TSPL_PI_DETAIL.Landed_Cost_Amount+(case when TSPL_PI_HEAD.Is_Shortage_Include_In_Landed_Cost=0 then TSPL_PI_DETAIL.Shortage_Amount else 0 end))*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Landed_Cost_Amount, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX5 ,TSPL_PI_DETAIL.TAX5_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX5_Amt,TSPL_PI_DETAIL.TAX5_Rate, TSPL_PI_DETAIL.TAX5+'%' as Tax5Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = ", TSPL_PI_Detail.ItemAdd_Charge_Code5 as Add_Charge_Code5 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt5*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt5 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax5 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX5_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax5=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code5 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2 and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax5,'')<>''  "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then
            strMainQuery += "pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then
            strMainQuery += " pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then
            strMainQuery += " pivot (sum(Add_Charge_Amt5) for Add_Charge_Code5 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine
        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_Head.Sublocation_Code as [Sub Location] , " &
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " &
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " &
                         " (isnull(TSPL_PI_DETAIL.PI_Qty,0)+isnull(TSPL_PI_DETAIL.Reject_Qty,0)+isnull(TSPL_PI_DETAIL.Short_Qty,0)+isnull(TSPL_PI_DETAIL.Leak_Qty,0)+isnull(TSPL_PI_DETAIL.Burst_Qty,0)) as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " &
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " &
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " &
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX6_Amt else 0 end) as NonRecoverable_Tax,TSPL_PI_HEAD.isJobWorkOutward,(TSPL_PI_DETAIL.Landed_Cost_Amount+(case when TSPL_PI_HEAD.Is_Shortage_Include_In_Landed_Cost=0 then TSPL_PI_DETAIL.Shortage_Amount else 0 end))*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Landed_Cost_Amount, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX6 ,TSPL_PI_DETAIL.TAX6_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX6_Amt,TSPL_PI_DETAIL.TAX6_Rate, TSPL_PI_DETAIL.TAX6+'%' as Tax6Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code6 as Add_Charge_Code6 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt6*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt6 "
        ''richa add date filter 
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax6 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX6_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax6=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code6 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(TSPL_PI_HEAD.tax6,'')<>''  "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_PI_HEAD.isJobWorkOutward,0)=1 "
        End If
        strMainQuery += " )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then
            strMainQuery += "pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then
            strMainQuery += " pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then
            strMainQuery += " pivot (sum(Add_Charge_Amt6) for Add_Charge_Code6 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If

        strMainQuery += " )final1)final"
        strMainQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
        strMainQuery += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
        strMainQuery += " left outer join TSPL_vendor_MASTER on TSPL_vendor_MASTER .Vendor_Code =final.Customer_Code "
        strMainQuery += " LEFT OUTER JOIN TSPL_vendor_MASTER as Parent_Master ON Parent_Master.Vendor_Code=TSPL_vendor_MASTER.Parent_Vendor_Code "
        strMainQuery += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code  Left join (select * from (select PI_No,Item_Code,Item_Net_AMt from " _
            & " tspl_Pi_Detail where  item_Code='PACKING') Pivoting pivot(SUm(Item_Net_AMt) for item_Code in ([PACKING]) ) pivoted) as pvt on pvt.PI_No=final.PI_No"
        strMainQuery += " group by  final.Trans_Type,final .Status  ,final.PI_NO,final.SRN_Id ,final.Item_Code ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.PI_Date,final.Way_BillNo,Final.GRNO,final.LR_NO ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt ,final.Amt_Less_Discount ,final.Total_Amt,QC.FAT_Per,QC.SNF_Per,vehicledesc,Vehicle_No,final.Additional_Charge ,final.Line_No " ', " + strPivotFoGrouprOuterQuery + "

        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine


        strMainQuery += " select * from (Select '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'MCC Transfer' as Trans_Type,0 as Line_No,0 as ConvRate,recv.location_Code as  Bill_To_Location,recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_MILK_TRANSFER_IN.isPosted as Status," _
            & " recv.location_desc,'MCC Transfer' as Invoice_Type,TSPL_MILK_TRANSFER_IN.Receipt_Challan_No as PI_NO,'' as SRN_Id  ,  convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No as Vendor_Invoice_No,convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Vendor_Invoice_Date," _
            & " '' as vehicledesc,tm.tanker_NO as Vehicle_No,0  as Additional_Charge ,  TSPL_MCC_Dispatch_Challan.mcc_code as Customer_Code,  TSPL_LOCATION_MASTER.Location_Desc  as Customer_Name,TSPL_MCC_Dispatch_Challan.Sublocation_Code as [Sub Location],Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,tspl_State_Master.state_COde as [State Code],tspl_State_Master.state_Name as [State Name]" _
            & ",tspl_LocaTION_mASTER.tin_No as [TIN No] ,'' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name],Tanker_Transporter_Code,tm.description, TSPL_MCC_Dispatch_Challan.Item_Code," _
            & " TSPL_MCC_Dispatch_Challan.Item_Desc , t_Qty_Recd.Net_Weight  as Qty ,t_Qty_Recd.UOM as  Unit_code ,round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))/coalesce(t_qty_recd.net_Weight,1) ,2) as  Item_Cost ,  t_FAT_Recd.Param_Field_Value as [FAT Per],  t_SNF_Recd.Param_Field_Value as [SNF Per],(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100) as [FAT KG],(coalesce(cast(t_Snf_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100) as [SNF KG],Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2) as  Amount ,0 as Disc_Per " _
            & " ,0 as Disc_Amt ,  Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2) as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type],  0 as Total_Tax_Amt ,Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2) as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice" + Environment.NewLine +
            ",null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine +
            ",'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details],TSPL_MCC_Dispatch_Challan.Dispatch_Date as DocDateView,0 as isJobWorkOutward,0 as Landed_Cost_Amount " + Environment.NewLine +
            " from TSPL_MCC_Dispatch_Challan  left outer " _
            & " join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO  LEFT JOIN tspl_Mcc_Master ON tspl_Mcc_Master.MCC_Code=TSPL_MCC_Dispatch_Challan.MCC_CODE  left join tspl_Location_master on tspl_Location_master.location_code=TSPL_MCC_Dispatch_Challan.mcc_Code left join tspl_Location_master recv on recv.location_code=TSPL_MILK_TRANSFER_IN.Location_Code left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.vendor_Invoice_No=TSPL_MILK_TRANSFER_IN.Receipt_Challan_No and len(coalesce(TSPL_vendor_Invoice_Head.vendor_Invoice_No,''))>0  left join tspl_tanker_Master tm on tm.tanker_no=TSPL_MCC_Dispatch_Challan.tanker_No Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.QC_No  where TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF_Recd On t_SNF_Recd.QC_No   = TSPL_MILK_TRANSFER_IN.QC_No " _
            & " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.QC_No  where TSPL_QC_Parameter_Detail.Param_Type = 'FAT' ) t_FAT_Recd On t_FAT_Recd.QC_No   = TSPL_MILK_TRANSFER_IN.QC_No " _
            & " Left Outer Join (Select TSPL_Weighment_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Weighment_No  = TSPL_MILK_TRANSFER_IN.Weighment_No ) t_Qty_Recd On t_Qty_Recd.Weighment_No   = TSPL_MILK_TRANSFER_IN.Weighment_No  left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Mcc_Master.State_Code  where  convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date ,103) <= convert(date,('" + To_Date + "'),103) "

        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_MILK_TRANSFER_IN.IsAgainstJobWork,0)=1 "
        End If
        strMainQuery += " )t "

        '=========================================preeti gupta [MILK Transfer In Return][KDI/04/07/18-000386]====================
        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine


        strMainQuery += " select * from (Select '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'MCC Transfer' as Trans_Type,0 as Line_No,0 as ConvRate,recv.location_Code as  Bill_To_Location,recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_MILK_TRANSFER_IN_RETURN.isPosted as Status," _
            & " recv.location_desc,'MCC Transfer' as Invoice_Type,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No as PI_NO,'' as SRN_Id  ,  convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,TSPL_MILK_TRANSFER_IN_RETURN.Dispatch_Challan_No as Vendor_Invoice_No,convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Vendor_Invoice_Date," _
            & " '' as vehicledesc,tm.tanker_NO as Vehicle_No,0  as Additional_Charge ,  TSPL_MCC_Dispatch_Challan.mcc_code as Customer_Code,  TSPL_LOCATION_MASTER.Location_Desc  as Customer_Name,TSPL_MCC_Dispatch_Challan.Sublocation_Code as [Sub Location],Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,tspl_State_Master.state_COde as [State Code],tspl_State_Master.state_Name as [State Name]" _
            & ",tspl_LocaTION_mASTER.tin_No as [TIN No] ,'' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name],Tanker_Transporter_Code,tm.description, TSPL_MCC_Dispatch_Challan.Item_Code," _
            & " TSPL_MCC_Dispatch_Challan.Item_Desc , t_Qty_Recd.Net_Weight*(-1)  as Qty ,t_Qty_Recd.UOM as  Unit_code ,(round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))/coalesce(t_qty_recd.net_Weight,1) ,2))*(-1) as  Item_Cost ,  t_FAT_Recd.Param_Field_Value as [FAT Per],  t_SNF_Recd.Param_Field_Value as [SNF Per],(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)*(-1) as [FAT KG],(coalesce(cast(t_Snf_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)*(-1) as [SNF KG],(Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2))*(-1) as  Amount ,0 as Disc_Per " _
            & " ,0 as Disc_Amt ,  (Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2))*(-1) as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type],  0 as Total_Tax_Amt ,(Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2))*(-1) as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total*(-1) [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount*(-1) as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount*(-1) as [AP Amount Before Tax],TSPL_MILK_TRANSFER_IN_RETURN.Dispatch_Challan_No as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax*(-1) as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge*(-1) as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt*(-1) as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total*(-1) as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice" + Environment.NewLine +
            ",null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine +
            ",'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details],TSPL_MCC_Dispatch_Challan.Dispatch_Date as DocDateView,0 as isJobWorkOutward,0 as Landed_Cost_Amount " + Environment.NewLine +
            " from TSPL_MILK_TRANSFER_IN_RETURN  left outer " _
            & " join TSPL_MCC_Dispatch_Challan on TSPL_MILK_TRANSFER_IN_RETURN.Dispatch_Challan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO  LEFT JOIN tspl_Mcc_Master ON tspl_Mcc_Master.MCC_Code=TSPL_MCC_Dispatch_Challan.MCC_CODE  left join tspl_Location_master on tspl_Location_master.location_code=TSPL_MCC_Dispatch_Challan.mcc_Code left join tspl_Location_master recv on recv.location_code=TSPL_MILK_TRANSFER_IN_RETURN.Location_Code left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.vendor_Invoice_No=TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No and len(coalesce(TSPL_vendor_Invoice_Head.vendor_Invoice_No,''))>0  left join tspl_tanker_Master tm on tm.tanker_no=TSPL_MCC_Dispatch_Challan.tanker_No Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN_RETURN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN_RETURN.QC_No  where TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF_Recd On t_SNF_Recd.QC_No   = TSPL_MILK_TRANSFER_IN_RETURN.QC_No " _
            & " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN_RETURN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN_RETURN.QC_No  where TSPL_QC_Parameter_Detail.Param_Type = 'FAT' ) t_FAT_Recd On t_FAT_Recd.QC_No   = TSPL_MILK_TRANSFER_IN_RETURN.QC_No " _
            & " Left Outer Join (Select TSPL_Weighment_Detail.* From TSPL_MILK_TRANSFER_IN_RETURN Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Weighment_No  = TSPL_MILK_TRANSFER_IN_RETURN.Weighment_No ) t_Qty_Recd On t_Qty_Recd.Weighment_No   = TSPL_MILK_TRANSFER_IN_RETURN.Weighment_No  left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Mcc_Master.State_Code  where  convert(date,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date ,103) <= convert(date,('" + To_Date + "'),103) "

        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_MILK_TRANSFER_IN_RETURN.IsAgainstJobWork,0)=1 "
        End If
        strMainQuery += " )t "
        '====================================================================================================
        ''richa KDI/22/10/18-000439
        If IsAgainstJobwork = False Then
            strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine
            ''richa show data for transfer return from transfer order table because partial return done from trasfer return instead of transfer return UDL/06/12/19-001012
            strMainQuery += " select * from (Select TSPL_TRANSFER_ORDER_HEAD.Tax_Group as Head_Tax_Group,'T' as Head_Tax_Group_Type,'Transfer' as Trans_Type,0 as Line_No,0 as ConvRate ,recv.location_Code as  Bill_To_Location,recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_TRANSFER_ORDER_HEAD.Status as Status, recv.location_desc " _
            & "  ,'Transfer' as Invoice_Type,TSPL_TRANSFER_ORDER_HEAD.Document_No as PI_NO,'' as SRN_Id ,  convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103 ) as PI_Date,TSPL_TRANSFER_ORDER_HEAD.waybill_No as Way_BillNo,TSPL_TRANSFER_ORDER_HEAD.GR_No as [GRNO],'' as LR_NO," _
            & " TSPL_TRANSFER_ORDER_HEAD.transferoutno  as Vendor_Invoice_No,convert(varchar,Out.DOcument_Date,103) as Vendor_Invoice_Date, '' as vehicledesc,TSPL_TRANSFER_ORDER_HEAD.Vehicle_No as Vehicle_No,0  as Additional_Charge ,case when TSPL_TRANSFER_ORDER_Head.Transfer_Type='T' then RETURN_FROM_LOCATION.Location_Code  else  Tspl_Location_Master.Location_Code end as Customer_Code," _
            & "  case when TSPL_TRANSFER_ORDER_Head.Transfer_Type='T' then RETURN_FROM_LOCATION.Location_Desc  else Tspl_Location_Master.Location_Desc end  as Customer_Name,'' as [Sub Location],case when TSPL_TRANSFER_ORDER_Head.Transfer_Type='T' then RETURN_FROM_LOCATION.Add1 + ' ' + RETURN_FROM_LOCATION.Add2 + ' ' + RETURN_FROM_LOCATION.Add3  else Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 end As Vendor_ADD,Tspl_State_Master.state_Code as [State Code],Tspl_State_Master.state_name as [State Name],case when TSPL_TRANSFER_ORDER_Head.Transfer_Type='T' then RETURN_FROM_LOCATION.TIN_No else Tspl_Location_Master.tin_No end as [TIN No],'' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name], " _
            & " tspl_transport_Master.Transport_id as [Transport Code],Tspl_Transport_Master.Transporter_Name as [Transporter Name],TSPL_TRANSFER_ORDER_DETAIL.Item_Code, TSPL_TRANSFER_ORDER_DETAIL.Item_Desc ,  TSPL_TRANSFER_ORDER_DETAIL.In_Qty  as Qty ,TSPL_TRANSFER_ORDER_DETAIL.Unit_code " _
            & " as  Unit_code ,coalesce(TSPL_TRANSFER_ORDER_DETAIL.Price,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost)  as  Item_Cost ,   QC.FAT_Per as [FAT Per],   QC.SNF_Per as [SNF Per],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],TSPL_TRANSFER_ORDER_DETAIL.Amount " _
            & " ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per as Disc_Per  ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt as Disc_Amt ,  TSPL_TRANSFER_ORDER_DETAIL.Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " _
            & " " & strTransferTaxColumns & "" & strTransferTaxPerColumns & "  " & strPivotForAddChargeForZeroTransfer_In & ",case when coalesce(out.is_AgainstformF,0)=1 then 'F' else '' end as [Tax Type],  TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt ," _
            & " TSPL_TRANSFER_ORDER_DETAIL.Amount as Total_Amt,'' as [AP Document No],0 as  [AP Document Amt],0 as [AP Document Discount Amt],0 as [AP Amount Before Tax]," _
            & " TSPL_TRANSFER_ORDER_HEAD.transferoutno as against_PoInvoice_No,'' as Against_PurchaseREturn_No,0 as [AP Total Tax],0 as [AP Total Add Charge],0 as [AP Landed Amt]," _
            & " '' as Against_MillkpurchaseInvoice_No,'' as Against_BulkMillkpurchaseInvoice_No,0 as  [AP Document Total],TSPL_TRANSFER_ORDER_DETAIL.MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice " + Environment.NewLine +
            " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine +
            ",'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details],TSPL_TRANSFER_ORDER_HEAD.Document_Date as DocDateView,0 as isJobWorkOutward,0 as Landed_Cost_Amount " + Environment.NewLine +
            " from TSPL_TRANSFER_ORDER_HEAD " _
            & " left outer  join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_NO  " _
            & "  left join TSPL_TRANSFER_ORDER_HEAD out on out.Document_No=TSPL_TRANSFER_ORDER_HEAD.TransferOutNo  left join tspl_Location_master on tspl_Location_master.LOcation_Code=out.From_Location " _
            & " left join tspl_Location_master recv on recv.location_code=TSPL_TRANSFER_ORDER_Head.To_Location " _
            & "  left join tspl_Location_master RETURN_FROM_LOCATION on RETURN_FROM_LOCATION.location_code=TSPL_TRANSFER_ORDER_Head.From_Location " _
            & " left join TSPL_TRANSPORT_MASTER on " _
            & " TSPL_TRANSPORT_MASTER.transport_Id=tspl_Transfer_Order_Head.Transport_Id  left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =" _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on " _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=Stock_SU.Item_Code and TSPL_TRANSFER_ORDER_DETAIL.Unit_code=Stock_SU.UOM_Code  " _
            & " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on " _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=StockKG.Item_Code  " _
            & " left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Location_Master.State where TSPL_TRANSFER_ORDER_Head.Transfer_Type in ('I','T') and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103) <= convert(date,('" + To_Date + "'),103) )t"
        End If

        ''richa agarwal
        If IsAgainstJobwork = False Then
            strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine

            'strMainQuery += " select * from (Select TSPL_TRANSFER_ORDER_HEAD.Tax_Group as Head_Tax_Group,'T' as Head_Tax_Group_Type,'Transfer Return' as Trans_Type,0 as Line_No,0 as ConvRate ,recv.location_Code as  Bill_To_Location, recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_TRANSFER_ORDER_HEAD.Status as Status, recv.location_desc , " _
            '& "  'Transfer Return' as Invoice_Type,TSPL_TRANSFER_RETURN.Document_No as PI_NO,'' as SRN_Id ,  convert(varchar,TSPL_TRANSFER_RETURN.Document_Date,103 ) as PI_Date,TSPL_TRANSFER_ORDER_HEAD.waybill_No as Way_BillNo,TSPL_TRANSFER_ORDER_HEAD.GR_No as [GRNO],'' as LR_NO," _
            '& " TSPL_TRANSFER_ORDER_HEAD.Document_No  as Vendor_Invoice_No,convert(varchar,TSPL_TRANSFER_ORDER_HEAD.DOcument_Date,103) as Vendor_Invoice_Date, '' as vehicledesc,TSPL_TRANSFER_ORDER_HEAD.Vehicle_No as Vehicle_No,0  as Additional_Charge , Tspl_Location_Master.Location_Code as Customer_Code, Tspl_Location_Master.Location_Desc  as Customer_Name,'' as [Sub Location],Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,Tspl_State_Master.state_Code as [State Code],Tspl_State_Master.state_name as [State Name],Tspl_Location_Master.TIN_No as [TIN No]," _
            '& " '' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name], " _
            '& " tspl_transport_Master.Transport_id as [Transport Code],Tspl_Transport_Master.Transporter_Name as [Transporter Name],TSPL_TRANSFER_ORDER_DETAIL.Item_Code, TSPL_TRANSFER_ORDER_DETAIL.Item_Desc ,  -TSPL_TRANSFER_ORDER_DETAIL.In_Qty  as Qty ,TSPL_TRANSFER_ORDER_DETAIL.Unit_code " _
            '& " as  Unit_code ,-coalesce(TSPL_TRANSFER_ORDER_DETAIL.Price,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost)  as  Item_Cost ,   QC.FAT_Per as [FAT Per],   QC.SNF_Per as [SNF Per],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],-TSPL_TRANSFER_ORDER_DETAIL.Amount AS Amount " _
            '& " ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per as Disc_Per  ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt as Disc_Amt , - TSPL_TRANSFER_ORDER_DETAIL.Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " _
            '& " " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & "  " & strPivotForAddChargeForZeroTransfer_In & ",case when coalesce(out.is_AgainstformF,0)=1 then 'F' else '' end as [Tax Type],  0 as Total_Tax_Amt ," _
            '& " -TSPL_TRANSFER_ORDER_DETAIL.Amount as   Total_Amt,'' as [AP Document No],0 as  [AP Document Amt],0 as [AP Document Discount Amt],0 as [AP Amount Before Tax]," _
            '& " TSPL_TRANSFER_ORDER_HEAD.Document_No as against_PoInvoice_No,'' as Against_PurchaseREturn_No,0 as [AP Total Tax],0 as [AP Total Add Charge],0 as [AP Landed Amt]," _
            '& " '' as Against_MillkpurchaseInvoice_No,'' as Against_BulkMillkpurchaseInvoice_No,0 as  [AP Document Total],TSPL_TRANSFER_ORDER_DETAIL.MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice " + Environment.NewLine + _
            '" ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine + _
            '",TSPL_TRANSFER_RETURN.Transfer_No as [Original Invoice No],convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details],TSPL_TRANSFER_ORDER_HEAD.Document_Date as DocDateView,0 as isJobWorkOutward,0 as Landed_Cost_Amount" + Environment.NewLine + _
            '" from TSPL_TRANSFER_ORDER_HEAD " _
            '& " left outer  join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_NO  " _
            '& "  left join TSPL_TRANSFER_ORDER_HEAD out on out.Document_No=TSPL_TRANSFER_ORDER_HEAD.TransferOutNo " _
            '& "  left join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_RETURN.Transfer_No " _
            '& " left join tspl_Location_master on tspl_Location_master.LOcation_Code=out.From_Location " _
            '& " left join tspl_Location_master recv on recv.location_code=TSPL_TRANSFER_ORDER_Head.To_Location left join TSPL_TRANSPORT_MASTER on " _
            '& " TSPL_TRANSPORT_MASTER.transport_Id=tspl_Transfer_Order_Head.Transport_Id  left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =" _
            '& " TSPL_TRANSFER_ORDER_DETAIL.Item_Code  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on " _
            '& " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=Stock_SU.Item_Code and TSPL_TRANSFER_ORDER_DETAIL.Unit_code=Stock_SU.UOM_Code  " _
            '& " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on " _
            '& " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=StockKG.Item_Code  " _
            '& " left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Location_Master.State where TSPL_TRANSFER_ORDER_Head.Transfer_Type='I'  and isnull(TSPL_TRANSFER_RETURN.Document_No,'')<>'' and convert(date,TSPL_TRANSFER_RETURN.Document_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_TRANSFER_RETURN.Document_Date ,103) <= convert(date,('" + To_Date + "'),103) )t"

            ''richa KDI/20/09/18-000432
            strMainQuery += " select * from (Select TSPL_TRANSFER_ORDER_HEAD.Tax_Group as Head_Tax_Group,'T' as Head_Tax_Group_Type,'Transfer Return' as Trans_Type,0 as Line_No,0 as ConvRate ,recv.location_Code as  Bill_To_Location,recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_TRANSFER_ORDER_HEAD.Status as Status, recv.location_desc " _
            & "  ,'Transfer Return' as Invoice_Type,TSPL_TRANSFER_RETURN.Document_No as PI_NO,'' as SRN_Id ,  convert(varchar,TSPL_TRANSFER_RETURN.Document_Date,103 ) as PI_Date,TSPL_TRANSFER_ORDER_HEAD.waybill_No as Way_BillNo,TSPL_TRANSFER_ORDER_HEAD.GR_No as [GRNO],'' as LR_NO," _
            & " TSPL_TRANSFER_ORDER_HEAD.Document_No  as Vendor_Invoice_No,convert(varchar,TSPL_TRANSFER_ORDER_HEAD.DOcument_Date,103) as Vendor_Invoice_Date, '' as vehicledesc,TSPL_TRANSFER_ORDER_HEAD.Vehicle_No as Vehicle_No,0  as Additional_Charge ,  Tspl_Location_Master.Location_Code as Customer_Code," _
            & " Tspl_Location_Master.Location_Desc  as Customer_Name,'' as [Sub Location],Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,Tspl_State_Master.state_Code as [State Code],Tspl_State_Master.state_name as [State Name],Tspl_Location_Master.tin_No as [TIN No],'' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name], " _
            & " tspl_transport_Master.Transport_id as [Transport Code],Tspl_Transport_Master.Transporter_Name as [Transporter Name],TSPL_TRANSFER_ORDER_DETAIL.Item_Code, TSPL_TRANSFER_ORDER_DETAIL.Item_Desc ,  -1* TSPL_TRANSFER_ORDER_DETAIL.In_Qty  as Qty ,TSPL_TRANSFER_ORDER_DETAIL.Unit_code " _
            & " as  Unit_code , -1* coalesce(TSPL_TRANSFER_ORDER_DETAIL.Price,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost)  as  Item_Cost ,   QC.FAT_Per as [FAT Per],   QC.SNF_Per as [SNF Per],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],-1 * TSPL_TRANSFER_ORDER_DETAIL.Amount as Amount " _
            & " ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per as Disc_Per  , -1* TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt as Disc_Amt ,  -1* TSPL_TRANSFER_ORDER_DETAIL.Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " _
            & " " & strTransferReturnTaxColumns & "" & strTransferTaxPerColumns & "  " & strPivotForAddChargeForZeroTransfer_In & ",case when coalesce(out.is_AgainstformF,0)=1 then 'F' else '' end as [Tax Type], TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt * -1 as Total_Tax_Amt," _
            & "  -1* TSPL_TRANSFER_ORDER_DETAIL.Amount as Total_Amt,'' as [AP Document No],0 as  [AP Document Amt],0 as [AP Document Discount Amt],0 as [AP Amount Before Tax]," _
            & "  TSPL_TRANSFER_ORDER_HEAD.Document_No as against_PoInvoice_No,'' as Against_PurchaseREturn_No,0 as [AP Total Tax],0 as [AP Total Add Charge],0 as [AP Landed Amt]," _
            & " '' as Against_MillkpurchaseInvoice_No,'' as Against_BulkMillkpurchaseInvoice_No,0 as  [AP Document Total],TSPL_TRANSFER_ORDER_DETAIL.MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice " + Environment.NewLine +
            " , null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine +
            ", TSPL_TRANSFER_RETURN.Transfer_No as [Original Invoice No],convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)  as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details],TSPL_TRANSFER_ORDER_HEAD.Document_Date as DocDateView,0 as isJobWorkOutward,0 as Landed_Cost_Amount " + Environment.NewLine +
            " from TSPL_TRANSFER_ORDER_HEAD " _
            & " left outer  join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_NO  " _
            & "  left join TSPL_TRANSFER_ORDER_HEAD out on out.Document_No=TSPL_TRANSFER_ORDER_HEAD.TransferOutNo " &
              " left join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_RETURN.Transfer_No " &
              " left join tspl_Location_master on tspl_Location_master.LOcation_Code=out.From_Location " _
            & " left join tspl_Location_master recv on recv.location_code=TSPL_TRANSFER_ORDER_Head.To_Location left join TSPL_TRANSPORT_MASTER on " _
            & " TSPL_TRANSPORT_MASTER.transport_Id=tspl_Transfer_Order_Head.Transport_Id  left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =" _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on " _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=Stock_SU.Item_Code and TSPL_TRANSFER_ORDER_DETAIL.Unit_code=Stock_SU.UOM_Code  " _
            & " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on " _
            & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=StockKG.Item_Code  " _
            & " left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Location_Master.State where TSPL_TRANSFER_ORDER_Head.Transfer_Type='I' and isnull(TSPL_TRANSFER_RETURN.Document_No,'')<>'' and convert(date,TSPL_TRANSFER_RETURN.Document_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_TRANSFER_RETURN.Document_Date ,103) <= convert(date,('" + To_Date + "'),103) )t"

            ''------------



            strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine


            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when tspl_pr_head.transaction_type='R' then case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end else 0 end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then Tspl_PR_detail.Total_Tax_Amt else -1 * Tspl_PR_detail.Total_Tax_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,0 as NonRecoverable_Tax,TSPL_PR_HEAD.isJobWorkOutward,TSPL_PR_DETAIL.Landed_Cost_Amount*(case when coalesce(TSPL_PR_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_PR_HEAD.convrate,0) end) as Landed_Cost_Amount, "
            strSDEndQry = ",TSPL_vendor_Invoice_Head.Document_No as [AP Document No], (case when Document_Type='C' then TSPL_vendor_Invoice_Head.Document_Total else -1 * TSPL_vendor_Invoice_Head.Document_Total end) *(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as  [AP Document Amt],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.Discount_Amount else -1 * TSPL_vendor_Invoice_Head.Discount_Amount end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Document Discount Amt],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.amount_less_Discount else -1 * TSPL_vendor_Invoice_Head.amount_less_Discount end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Amount Before Tax],TSPL_PR_DETAIL.PI_Id as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,(case when Document_Type='C' then TSPL_vendor_Invoice_Head.total_tax else -1 * TSPL_vendor_Invoice_Head.total_tax end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Total Tax],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.total_Add_Charge else -1 * TSPL_vendor_Invoice_Head.total_Add_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Total Add Charge],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.Total_landed_Amt else -1 * TSPL_vendor_Invoice_Head.Total_landed_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,(case when Document_Type='C' then TSPL_vendor_Invoice_Head.Document_total else -1 * TSPL_vendor_Invoice_Head.Document_total end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Document Total] ,PI_Id,Tspl_PR_detail.MRP,TSPL_PR_DETAIL.PI_Id as [Original Invoice No],PIHeadTable.PI_Date as [Original Invoice Date],isnull(TSPL_PR_HEAD.Description,'')+' '+isnull(TSPL_PR_HEAD.Remarks,'')+' '+isnull(TSPL_PR_HEAD.Comments,'') as [Reason For Revision],Tspl_PR_Head.PR_Date as DocDateView from Tspl_PR_detail " &
                               " left outer join Tspl_PR_Head on Tspl_PR_Head.PR_NO =Tspl_PR_detail.PR_NO " & Environment.NewLine +
                               " left outer join (select PI_Date,PI_No,Description,Remarks,Comments from TSPL_PI_HEAD) as PIHeadTable on PIHeadTable.PI_No =Tspl_PR_detail.PI_Id " & Environment.NewLine +
                               " left join TSPL_VEHICLE_MASTER on Vehicle_No=TSPL_VEHICLE_MASTER.Vehicle_Id left join TSPL_vendor_Invoice_Head on TSPL_vendor_Invoice_Head.Against_PurchaseReturn_No=Tspl_PR_Head.PR_NO and TSPL_vendor_Invoice_Head.Against_PurchaseReturn_No is not null  left join TSPL_vendor_master on   TSPL_vendor_master.Vendor_Code=tspl_PR_Head.Vendor_Code left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code " &
                               "    "
            strMainQuery += " select  max(Head_Tax_Group) as Head_Tax_Group,max(Head_Tax_Group_Type) as Head_Tax_Group_Type,'Purchase Return'  as [Trans Type],max(Line_No)as Line_No ,max(ConvRate) as ConvRate,max(TSPL_LOCATION_MASTER .location_Code) as [Location Code],	 max(TSPL_LOCATION_MASTER.Add1) + ' ' + max(TSPL_LOCATION_MASTER.Add2) + ' ' + max(TSPL_LOCATION_MASTER.Add3) As [Location Address] ,max(TSPL_LOCATION_MASTER.State) as [Location State],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.PR_NO as [Document No],'' as SRN_Id,final.PR_Date as [Document Date],final.Way_BillNo as [Way Bill No],Final.GRNO as [GR No],final.LR_NO as [LR No] ,max(VENDOR_INVOICE_no)as VENDOR_INVOICE_no,max(VENDOR_INVOICE_Date)as VENDOR_INVOICE_Date,vehicledesc,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Vendor Code] ,max(TSPL_vendor_MASTER .vendor_Name) as [Vendor Name],max([Sub Location]) as [Sub Location], max(TSPL_VENDOR_MASTER.Add1) + ' ' + max(TSPL_VENDOR_MASTER.Add2) + ' ' + max(TSPL_VENDOR_MASTER.Add3) As [Vendor Address],Max([State Code]) as [State Code],Max([State Name]) as [State Name],max(TSPL_vendor_MASTER .Tin_No) as [Vendor TIN No] ,max(TSPL_vendor_MASTER .Parent_vendor_Code) as [Parent Vendor No] ,max(Parent_Master.Vendor_Code) as [Parent Vendor Code],max(Parent_Master.Vendor_Name) as [Parent Vendor Name],Max(final.[Transporter]) as [Transporter],Max([Transporter Name]) as [Transporter Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.Amt_Less_Discount  as [Amount Less Discount],0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]  " + strPivotForOuterQuery + " " + strPivotFoGrouprOuterQuery + " " + strPivotFoAddChargeZeroGrouprOuterQuery + ",max(_Type) as [Tax Type] ,(final.Total_Tax_Amt-coalesce(sum(final.NonRecoverable_Tax),0)) as [Total Tax Amount],final.Total_Amt as [Total Amount],Max([AP Document No]) as [AP Document No],Max(coalesce([AP Document Amt],0)) as [AP Document Amt],Max(coalesce([AP Document Discount Amt],0)) as [AP Document Discount Amt],Max(coalesce([AP Amount Before Tax],0)) as [AP Amount Before Tax],Max(against_PoInvoice_No) as against_PoInvoice_No,Max(Against_PurchaseREturn_No) as Against_PurchaseREturn_No,(Max(coalesce([AP Total Tax],0))-coalesce(sum(final.NonRecoverable_Tax),0)) as [AP Total Tax],max(coalesce([AP Total Add Charge],0)) as [AP Total Add Charge],(Max(coalesce([AP Landed Amt],0))-coalesce(-sum(final.NonRecoverable_Tax),0)) as [AP Landed Amt],Against_MillkpurchaseInvoice_No, Against_BulkMillkpurchaseInvoice_No,Max(coalesce([AP Document Total],0)) as [AP Document Total],max(MRP) as MRP,coalesce(sum(final.NonRecoverable_Tax),0) as NonRecoverable_Tax ,'' as Purchase_Tax_Invoice   " + Environment.NewLine +
            "  ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine +
            " ,max([Original Invoice No]) as [Original Invoice No],convert(varchar, max([Original Invoice Date]),103) as [Original Invoice Date],max([Reason For Revision]) as [Reason For Revision],null as [ITC Eligible],null as [ITC Status],null as [ITC Details],max(DocDateView) as DocDateView,max(isJobWorkOutward) as isJobWorkOutward,Max(Landed_Cost_Amount) as Landed_Cost_Amount" + Environment.NewLine
            strMainQuery += " from ("
            strTaxColumns = " TSPL_PR_DETAIL.TAX1 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX1_Amt else -1 * TSPL_PR_DETAIL.TAX1_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX1_Amt, TSPL_PR_DETAIL.TAX1_Rate,TSPL_PR_DETAIL.TAX1+'%' as Tax1Rate,'' as _Type,'N' as Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt1"
            '' query for no tax applied
            strMainQuery += "  select Head_Tax_Group,Head_Tax_Group_Type,Trans_Type, Line_No,ConvRate ,Status  ,Bill_To_Location ,Customer_Code,[Sub Location] ,[State Code] ,[State Name] ,Invoice_Type ,PR_No  ,SRN_Id ,PR_Date ,Way_BillNo ,GRNo ,LR_NO ,Vendor_Invoice_No ,Vendor_Invoice_Date ,Transporter ,[Transporter Name] ,Item_Code ,Qty ,Unit_code ,Item_Cost ,Amount ,Disc_Per ,Disc_Amt ,Amt_Less_Discount ,Total_Tax_Amt ,Total_Amt ,vehicledesc ,Vehicle_No ,Additional_Charge ,NonRecoverable_Tax ,_Type,Tax_Recoverable,[AP Document No],[AP Document Amt],[AP Document Discount Amt],[AP Amount Before Tax],Against_POInvoice_No,Against_PurchaseReturn_No ,[AP Total Tax],[AP Total Add Charge],[AP Landed Amt],Against_MillkPurchaseInvoice_No,Against_BulkMillkPurchaseInvoice_No,[AP Document Total],PI_Id,MRP,[Original Invoice No],[Original Invoice Date],[Reason For Revision],isJobWorkOutward,Landed_Cost_Amount,DocDateView " &
                " " + IIf(clsCommon.myLen(strPivotForInnerQuery) > 0, "," + strPivotForInnerQuery, "") + " " + IIf(clsCommon.myLen(strDoublePivotForInnerQuery) > 0, "," + strDoublePivotForInnerQuery, "") + " " + strPivotForAddChargeInnerQueryOuter + " " &
                " from( select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " where 2=2 and (coalesce(TSPL_PR_DETAIL.tax1,'')='' and coalesce(TSPL_PR_DETAIL.tax2,'')='' " &
                              " and coalesce(TSPL_PR_DETAIL.tax3,'')='' and coalesce(TSPL_PR_DETAIL.tax4,'')='' and " &
                              " coalesce(TSPL_PR_DETAIL.tax5,'')='' and coalesce(TSPL_PR_DETAIL.tax6,'')='' and " &
                              " coalesce(TSPL_PR_DETAIL.tax7,'')='' and coalesce(TSPL_PR_DETAIL.tax8,'')='' and " &
                              " coalesce(TSPL_PR_DETAIL.tax9,'')='' and coalesce(TSPL_PR_DETAIL.tax10,'')='') and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s  "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t "
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine
            '' query for tax1 applied
            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when tspl_pr_head.transaction_type='R' then case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end else 0 end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX1_Amt else 0 end) as NonRecoverable_Tax,TSPL_PR_HEAD.isJobWorkOutward,TSPL_PR_DETAIL.Landed_Cost_Amount*(case when coalesce(TSPL_PR_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_PR_HEAD.convrate,0) end) as Landed_Cost_Amount, "
            strTaxColumns = " TSPL_PR_DETAIL.TAX1 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX1_Amt else -1 * TSPL_PR_DETAIL.TAX1_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX1_Amt,TSPL_PR_DETAIL.TAX1_Rate, TSPL_PR_DETAIL.TAX1+'%' as Tax1Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt1"
            ''richa add filter date
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax1 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX1_Rate and ttr._type<>'OH'  left join tspl_tax_master tm on TSPL_PR_DETAIL.tax1=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code1 =AdCh .Code where 2=2  and (TSPL_PR_DETAIL.tax1<>'' or TSPL_PR_HEAD.Add_Charge_Code1<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX1,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t"
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t "
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when tspl_pr_head.transaction_type='R' then case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end else 0 end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX2_Amt else 0 end) as NonRecoverable_Tax,TSPL_PR_HEAD.isJobWorkOutward,TSPL_PR_DETAIL.Landed_Cost_Amount*(case when coalesce(TSPL_PR_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_PR_HEAD.convrate,0) end) as Landed_Cost_Amount, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX2 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX2_Amt else -1 * TSPL_PR_DETAIL.TAX2_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX2_Amt,TSPL_PR_DETAIL.TAX2_Rate, TSPL_PR_DETAIL.TAX2+'%' as Tax2Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code2 as Add_Charge_Code2 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt2 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt2 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt2"
            '' add filter date richa
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax2 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX2_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax2=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code2 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax2<>'' or TSPL_PR_HEAD.Add_Charge_Code2<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX2,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt2) for Add_Charge_Code2 in (" + strPivotForAddChargeInnerQuery + "))t "
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when tspl_pr_head.transaction_type='R' then case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end else 0 end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX3_Amt else 0 end) as NonRecoverable_Tax,TSPL_PR_HEAD.isJobWorkOutward,TSPL_PR_DETAIL.Landed_Cost_Amount*(case when coalesce(TSPL_PR_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_PR_HEAD.convrate,0) end) as Landed_Cost_Amount, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX3 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX3_Amt else -1 * TSPL_PR_DETAIL.TAX3_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX3_Amt, TSPL_PR_DETAIL.TAX3_Rate, TSPL_PR_DETAIL.TAX3+'%' as Tax3Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code3 as Add_Charge_Code3 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt3 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt3 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt3"
            ''add filter date richa
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax3 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX3_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax3=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code3 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax3<>'' or TSPL_PR_HEAD.Add_Charge_Code3<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX3,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt3) for Add_Charge_Code3 in (" + strPivotForAddChargeInnerQuery + "))t "
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when tspl_pr_head.transaction_type='R' then case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end else 0 end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX4_Amt else 0 end) as NonRecoverable_Tax,TSPL_PR_HEAD.isJobWorkOutward,TSPL_PR_DETAIL.Landed_Cost_Amount*(case when coalesce(TSPL_PR_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_PR_HEAD.convrate,0) end) as Landed_Cost_Amount, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX4 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX4_Amt else -1 * TSPL_PR_DETAIL.TAX4_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX4_Amt,TSPL_PR_DETAIL.TAX4_Rate, TSPL_PR_DETAIL.TAX4+'%' as Tax4Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_PR_Detail.ItemAdd_Charge_Code4 as Add_Charge_Code4 ,(case when Document_Type='C' then TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt4 else -1 * TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt4 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt4"
            ''add filter date richa
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax4 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX4_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax4=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code4 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax4<>''  or TSPL_PR_HEAD.Add_Charge_Code4 <>'') and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX4,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt4) for Add_Charge_Code4 in (" + strPivotForAddChargeInnerQuery + "))t "
            End If



            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when tspl_pr_head.transaction_type='R' then case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end else 0 end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX5_Amt else 0 end) as NonRecoverable_Tax,TSPL_PR_HEAD.isJobWorkOutward,TSPL_PR_DETAIL.Landed_Cost_Amount*(case when coalesce(TSPL_PR_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_PR_HEAD.convrate,0) end) as Landed_Cost_Amount, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX5 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX5_Amt else -1 * TSPL_PR_DETAIL.TAX5_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX5_Amt,TSPL_PR_DETAIL.TAX5_Rate, TSPL_PR_DETAIL.TAX5+'%' as Tax5Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code5 as Add_Charge_Code5 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt5 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt5 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt5"
            ''richa add date filter
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax5 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX5_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax5=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code5 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax5<>'' or TSPL_PR_HEAD.Add_Charge_Code5<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX5,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt5) for Add_Charge_Code5 in (" + strPivotForAddChargeInnerQuery + "))t "
            End If


            strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

            strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location,'' as [Sub Location], " &
                            " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " &
                            " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " &
                            " case when tspl_pr_head.transaction_type='R' then case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end else 0 end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " &
                            " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " &
                            " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX6_Amt else 0 end) as NonRecoverable_Tax,TSPL_PR_HEAD.isJobWorkOutward,TSPL_PR_DETAIL.Landed_Cost_Amount*(case when coalesce(TSPL_PR_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_PR_HEAD.convrate,0) end) as Landed_Cost_Amount, "

            strTaxColumns = " TSPL_PR_DETAIL.TAX6 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX6_Amt else -1 * TSPL_PR_DETAIL.TAX6_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX6_Amt,TSPL_PR_DETAIL.TAX6_Rate, TSPL_PR_DETAIL.TAX6+'%' as Tax6Rate,ttr._Type,tm.Tax_Recoverable"
            strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code6 as Add_Charge_Code6 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt6 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt6 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt6"
            ''richa add date filter
            strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax6 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX6_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax6=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code6 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax6<>'' or TSPL_PR_HEAD.Add_Charge_Code6<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) and coalesce(Tspl_PR_Head.TAX6,'')<>'')s "

            If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t "
            End If
            If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
                strMainQuery += " pivot (sum(Add_Charge_Amt6) for Add_Charge_Code6 in (" + strPivotForAddChargeInnerQuery + "))t"
            End If

            strMainQuery += " )final1)final"
            strMainQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
            strMainQuery += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
            strMainQuery += " left outer join TSPL_vendor_MASTER on TSPL_vendor_MASTER .Vendor_Code =final.Customer_Code "
            strMainQuery += " LEFT OUTER JOIN TSPL_vendor_MASTER as Parent_Master ON Parent_Master.Vendor_Code=TSPL_vendor_MASTER.Parent_Vendor_Code "
            strMainQuery += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
            strMainQuery += " group by  final.Trans_Type,final .Status  ,final.PR_NO ,final.Item_Code ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.PR_Date,final.Way_BillNo,Final.GRNO,final.LR_NO ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt ,final.Amt_Less_Discount ,final.Total_Amt,QC.FAT_Per,QC.SNF_Per,vehicledesc,Vehicle_No,final.Additional_Charge ,final.Against_BulkMillkPurchaseInvoice_No ,final.Against_MillkPurchaseInvoice_No ,final.PI_Id  " ', " + strPivotFoGrouprOuterQuery + "


            strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine


            strMainQuery += " select * from ( Select    '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'Milk Receipt' as Trans_Type,0 as Line_No,0 as ConvRate ,TSPL_LOCATION_MASTER .location_Code  as  Bill_To_Location,  (TSPL_LOCATION_MASTER.Add1) + ' ' + (TSPL_LOCATION_MASTER.Add2) + ' ' + (TSPL_LOCATION_MASTER.Add3) As [Location Address],TSPL_LOCATION_MASTER.State as [Location State],TSPL_MILK_PURCHASE_INVOICE_Head.Posted as Status,  tspl_mcc_Master.mcc_name,'Milk Receipt' as Invoice_Type,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_CODE as PI_NO,'' as SRN_Id ,  convert(varchar,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,'' as Vendor_Invoice_No,'' as Vendor_Invoice_Date, TSPL_Primary_Vehicle_Master.Vehicle_Code as Vehicle_No, TSPL_Primary_Vehicle_Master.Description as vehicledesc,0  as Additional_Charge ,   TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE as Customer_Code,  tspl_vendor_Master.Vendor_Name as Customer_Name,'' as [Sub Location],(TSPL_VENDOR_MASTER.Add1) + ' ' + (TSPL_VENDOR_MASTER.Add2) + ' ' + (TSPL_VENDOR_MASTER.Add3) As [Vendor Address],tspl_state_Master.state_Code as [State Code],tspl_State_Master.state_Name as [State Name],tspl_vendor_Master.Tin_No as [TIN No],Parent_V.vendor_Code as [Parent Vendor No],Parent_V.vendor_Code as [Parent Vendor Code],Parent_V.vendor_Name as [Parent Vendor Name],pm.vendor_Code as [Transporter],pm.Vendor_Name as [Transporter Name], TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code, tspl_Item_Master.Item_Desc , " +
            " TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty as Qty ,TSPL_MILK_SRN_DETAIL.UOM_Code as  Unit_code ,TSPL_MILK_SRN_DETAIL.RATE as  Item_Cost  ,  TSPL_MILK_SRN_DETAIL.FAT_PER as [FAT Per],  TSPL_MILK_SRN_DETAIL.SNF_PER as [SNF Per],TSPL_MILK_SRN_DETAIL.FAT_KG as [FAT KG],TSPL_MILK_SRN_DETAIL.SNF_KG  as [SNF KG],TSPL_MILK_SRN_DETAIL.Amount ,0 as Disc_Per  ,0 as Disc_Amt ,  TSPL_MILK_PURCHASE_INVOICE_DETAIL.NET_AMOUNT as  Amt_Less_Discount , round(coalesce(TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT*TSPL_MILK_PURCHASE_INVOICE_DETAIL.PAYMENT_COMMISSION/100,0),2) as EMP, round(coalesce(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Incentive,0),2) as Incentive_Head ,round(coalesce(IncentiveEMP,0),2) as IncentiveEMP " _
            & " " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ", '' as [Tax Type],  0 as Total_Tax_Amt ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.NET_AMOUNT as   Total_Amt, TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount  as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],stuff((select ',' + isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE  ,'')  FROM TSPL_MILK_PURCHASE_INVOICE_DETAIL WHERE TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE  for xml path ('')),1,1,'' )as against_PoInvoice_No, TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as  [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No, TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax, TSPL_MILK_PURCHASE_INVOICE_HEAD.Purchase_Tax_Invoice  ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date]  ,'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details],TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE as DocDateView,0 as isJobWorkOutward,0 as Landed_Cost_Amount" +
            " from TSPL_MILK_PURCHASE_INVOICE_DETAIL " + Environment.NewLine +
            "Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_DETAIL.DOC_CODE =  TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE   " + Environment.NewLine +
            "Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " + Environment.NewLine +
            "Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE    " + Environment.NewLine +
            "Left Outer Join TSPL_VENDOR_MASTER   On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE " + Environment.NewLine +
            "Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code =   TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE " + Environment.NewLine +
            "Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code =  TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
            "left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code=TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code  " + Environment.NewLine +
            "Left join tspl_Vendor_Master Parent_v on Parent_v.vendor_Code=tspl_Vendor_Master.Parent_Vendor_Code " + Environment.NewLine +
            "Left join tspl_Vendor_Master Pm on pm.vendor_Code=TSPL_Primary_Vehicle_Master.Vendor_Code " + Environment.NewLine +
            "left join tspl_Location_master on tspl_Location_master.location_code=TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE " + Environment.NewLine +
            "left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No=TSPL_MILK_PURCHASE_INVOICE_Head.DOC_CODE and len(coalesce(TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,''))>0  " + Environment.NewLine +
            "left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_Mcc_MASTER.State_Code " + Environment.NewLine +
            "where coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.doc_Code,'')<>''  AND TSPL_vendor_Invoice_Head.DOCUMENT_TYPE='I' and convert(date,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE ,103) <= convert(date,('" + To_Date + "'),103) and convert(date,TSPL_vendor_Invoice_Head.Invoice_Entry_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_vendor_Invoice_Head.Invoice_Entry_Date,103) <= convert(date,('" + To_Date + "'),103) )t"
        End If
        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine


        strMainQuery += "  select * from (Select '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'Bulk Purchase' as Trans_Type,0 as Line_No,0 as ConvRate ,TSPL_LOCATION_MASTER .location_Code  as  Bill_To_Location, (TSPL_LOCATION_MASTER.Add1) + ' ' + (TSPL_LOCATION_MASTER.Add2) + ' ' + (TSPL_LOCATION_MASTER.Add3) As [Location Address],TSPL_LOCATION_MASTER.State as [Location State],tspl_Bulk_milk_purchase_Invoice_head.isPosted " _
            & " as Status, TSPL_LOCATION_MASTER.Location_Desc as [Location Desc],'Bulk Purchase' as Invoice_Type,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO as PI_NO,'' as SRN_Id , " _
            & " convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No as Vendor_Invoice_No,convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.Doc_Date,103) as Vendor_Invoice_Date,Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end as Vehicle_No, Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end  " _
            & " as vehicledesc,Case when row_Number()over(partition by tspl_Bulk_milk_purchase_Invoice_head.DOC_NO Order by tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code)=1 then tspl_Bulk_milk_purchase_Invoice_head.RoundoffAMount else 0 end  as Additional_Charge ,  tspl_Bulk_milk_purchase_Invoice_head.vendor_code as Customer_Code, .tspl_Vendor_Master.Vendor_Name as Customer_Name,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Joblocation_Code as [Sub Location],(TSPL_VENDOR_MASTER.Add1) + ' ' + (TSPL_VENDOR_MASTER.Add2) + ' ' + (TSPL_VENDOR_MASTER.Add3) As [Vendor Address],tspl_State_Master.state_COde as [State Code],Tspl_State_Master.state_Name as [State Name], tspl_vendor_Master.Tin_No as [TIN No],Parent_v.vendor_Code as " _
            & " [Parent Vendor No],Parent_v.vendor_Code as [Parent Vendor Code],Parent_v.vendor_Name as [Parent Vendor Name],'' as Tanker_Transporter_Code,'' as [Transporter Name], tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code, " _
            & " tspl_Bulk_milk_purchase_Invoice_Detail.Item_Desc ,  tspl_Bulk_milk_purchase_Invoice_Detail.Net_Weight  as Qty ,tspl_Bulk_milk_purchase_Invoice_Detail.UOM " _
            & " as  Unit_code ,Case when   TSPL_Bulk_MILK_SRN.Approved_Rate<=0 then   tspl_Bulk_milk_purchase_Invoice_Detail.NetRate else   TSPL_Bulk_MILK_SRN.Approved_Rate end as  Item_Cost ,  tspl_Bulk_milk_purchase_Invoice_Detail.FAT_Per as [FAT Per],  tspl_Bulk_milk_purchase_Invoice_Detail.SNF_PER as [SNF Per]," _
            & " tspl_Bulk_milk_purchase_Invoice_Detail.FAT_KG as [FAT KG],tspl_Bulk_milk_purchase_Invoice_Detail.SNF_KG as [SNF KG],tspl_Bulk_milk_purchase_Invoice_Detail.Amount ,0 as Disc_Per  ,0 as Disc_Amt , " _
            & " tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " & strBulkTaxColumns & "" & strBulkTaxPerColumns & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type], " _
            & " tspl_Bulk_milk_purchase_Invoice_Detail.Total_Tax_Amt ,tspl_Bulk_milk_purchase_Invoice_DETAIL.actual_amount as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],stuff((select ',' + isnull(tspl_Bulk_milk_purchase_Invoice_DETAIL.SRN_NO ,'')  FROM tspl_Bulk_milk_purchase_Invoice_DETAIL WHERE tspl_Bulk_milk_purchase_Invoice_DETAIL.DOC_NO =tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  for xml path ('')),1,1,'' )as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax, tspl_Bulk_milk_purchase_Invoice_head.Purchase_Tax_Invoice " _
            & " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " _
            & " ,'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details],tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE as DocDateView,ISNULL(tspl_Bulk_milk_purchase_Invoice_head.IsAgainstJobWork,0) as isJobWorkOutward,0 as Landed_Cost_Amount " _
            & " from tspl_Bulk_milk_purchase_Invoice_head inner join" _
            & " tspl_Bulk_milk_purchase_Invoice_Detail on tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Head.DOC_NO left join " _
            & " TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_Bulk_milk_purchase_Invoice_head.Loc_Code left join TSPL_VENDOR_MASTER on " _
            & " TSPL_VENDOR_MASTER.Vendor_Code=tspl_Bulk_milk_purchase_Invoice_head.VENDOR_CODE left join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=" _
            & " tspl_Bulk_milk_purchase_Invoice_DETAIL.SRN_NO   Left join tspl_Vendor_Master Parent_v on Parent_v.vendor_Code=tspl_Vendor_Master.Parent_Vendor_Code  left join TSPL_Dispatch_BulkSale_Trade on TSPL_Dispatch_BulkSale_Trade.Against_SRN_No=TSPL_Bulk_MILK_SRN.SRN_NO  left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.Against_bulkmillkPurchaseInvoice_No=tspl_Bulk_milk_purchase_Invoice_head.DOC_NO and len(coalesce(TSPL_vendor_Invoice_Head.Against_bulkmillkPurchaseInvoice_No,''))>0 left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code where convert(date,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) <= convert(date,('" + To_Date + "'),103) and convert(date,TSPL_vendor_Invoice_Head.Invoice_Entry_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_vendor_Invoice_Head.Invoice_Entry_Date,103) <= convert(date,('" + To_Date + "'),103)  "

        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.IsAgainstJobWork,0)=1 "
        End If
        strMainQuery += " ) t "
        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine

        strMainQuery += " select * from (Select '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'Bulk Purchase Return' as Trans_Type,0 as Line_No,0 as ConvRate,TSPL_LOCATION_MASTER .location_Code  as  Bill_To_Location,(TSPL_LOCATION_MASTER.Add1) + ' ' + (TSPL_LOCATION_MASTER.Add2) + ' ' + (TSPL_LOCATION_MASTER.Add3) As [Location Address], " &
                            " TSPL_LOCATION_MASTER.State as [Location State],TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.isPosted  as Status,TSPL_LOCATION_MASTER.Location_Desc as [Location Desc],'Bulk Purchase Return' as Invoice_Type,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No as PI_NO,'' as SRN_Id , " &
                            " convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Vendor_Invoice_No as Vendor_Invoice_No,convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) as Vendor_Invoice_Date, " &
                            " Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end as Vehicle_No, Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end   as vehicledesc," &
                            " Case when row_Number()over(partition by TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No Order by TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Code)=1 then TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.RoundoffAMount else 0 end  as Additional_Charge ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.vendor_code as Customer_Code " &
                            " , .tspl_Vendor_Master.Vendor_Name as Customer_Name,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Joblocation_Code as [Sub Location],(TSPL_VENDOR_MASTER.Add1) + ' ' + (TSPL_VENDOR_MASTER.Add2) + ' ' + (TSPL_VENDOR_MASTER.Add3) As [Vendor Address],tspl_State_Master.state_COde as [State Code],Tspl_State_Master.state_Name as [State Name], tspl_vendor_Master.Tin_No as [TIN No],Parent_v.vendor_Code as  [Parent Vendor No], " &
                            " Parent_v.vendor_Code as [Parent Vendor Code],Parent_v.vendor_Name as [Parent Vendor Name],'' as Tanker_Transporter_Code,'' as [Transporter Name], TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Code,  TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Desc ,  -1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Net_Weight  as Qty ,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.UOM  as  Unit_code " &
                            " ,Case when   TSPL_Bulk_MILK_SRN.Approved_Rate<=0 then   TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.NetRate else   TSPL_Bulk_MILK_SRN.Approved_Rate end as  Item_Cost ,  TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.FAT_Per as [FAT Per],  TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SNF_PER as [SNF Per], -1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.FAT_KG as [FAT KG],-1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SNF_KG as [SNF KG],-1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Amount as Amount " &
                            " ,0 as Disc_Per  ,0 as Disc_Amt ,  -1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Actual_Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]  " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & " ,'' as [Tax Type],  0 as Total_Tax_Amt ,-1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.actual_amount as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],-1*TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],-1*TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt], " &
                            " TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],stuff((select ',' + isnull(TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SRN_NO ,'')  FROM TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL WHERE TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No =TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No  for xml path ('')),1,1,'' )as against_PoInvoice_No, " &
                            " TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No, " &
                            " TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice " &
                            " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " &
                            " ,'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details],TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date as DocDateView, isnull(TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.IsAgainstJobWork,0) as isJobWorkOutward,0 as Landed_Cost_Amount" &
                            " from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD inner join TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL " &
                            " on TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No left join  TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Loc_Code left join TSPL_VENDOR_MASTER on  TSPL_VENDOR_MASTER.Vendor_Code=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.VENDOR_CODE " &
                            " left join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO= TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SRN_NO   Left join tspl_Vendor_Master Parent_v on Parent_v.vendor_Code=tspl_Vendor_Master.Parent_Vendor_Code  left join TSPL_Dispatch_BulkSale_Trade on TSPL_Dispatch_BulkSale_Trade.Against_SRN_No=TSPL_Bulk_MILK_SRN.SRN_NO  left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.Against_bulkmillkPurchaseInvoice_No=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No and TSPL_vendor_Invoice_Head.Against_bulkmillkPurchaseInvoice_No is not null  " &
                            " left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code where convert(date,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) <= convert(date,('" + To_Date + "'),103) "
        If IsAgainstJobwork Then
            strMainQuery += " and isnull(TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.IsAgainstJobWork,0)=1 "
        End If
        strMainQuery += " ) t "

        strMainQuery += ") xx"
        strMainQuery += Environment.NewLine + Environment.NewLine
        strMainQuery += " left outer join tspl_item_master Item on Item.Item_Code =xx.[Item Code] " + Environment.NewLine
        strMainQuery += " left join (" & qryTransStock & ") as  TransStock on xx.[Item Code]=TransStock.Item_Code and TransStock.UOM_Code=" & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " " + Environment.NewLine
        strMainQuery += " left join (" & qryStock & ") as Stock_SU on xx.[Item Code]=Stock_SU.Item_Code and xx.[UOM]=Stock_SU.UOM_Code " + Environment.NewLine
        strMainQuery += " left join (" & qryKG & ") as StockKG on xx.[Item Code]=StockKG.Item_Code  " + Environment.NewLine
        strMainQuery += " left join (select Vendor_Code,Vendor_Group_Code,'' as Zone_Code,'' as Struct_Code,GST_Composition_scheme,GSTRegistered,GSTFinalNo,TSPL_VENDOR_MASTER.City_Code as VendorCityCode,TSPL_CITY_MASTER.City_Name as VendorCityName ,tspl_state_master.GST_STATE_Code as Vendor_GST_STATE_Code ,tspl_state_master.STATE_NAME as Veindor_STATE_Name from TSPL_vendor_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_VENDOR_MASTER.City_Code left outer join tspl_state_master on tspl_state_master.state_code =TSPL_vendor_MASTER.state_code ) as Cust on xx.[vENDOR Code]=Cust.Vendor_Code " + Environment.NewLine
        strMainQuery += " left outer join (select TSPL_LOCATION_MASTER.Registered,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.GSTNO,TSPL_LOCATION_MASTER.City_Code,TSPL_LOCATION_MASTER.City_Code as City_Name,tspl_state_master.GST_STATE_Code,tspl_state_master.STATE_NAME from TSPL_LOCATION_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_LOCATION_MASTER.City_Code     left outer join tspl_state_master on tspl_state_master.state_code =TSPL_LOCATION_MASTER.State  ) as TSPL_LOCATION_MASTER_AS_Transfer on TSPL_LOCATION_MASTER_AS_Transfer.Location_Code = xx.[vENDOR Code] and xx.[Trans Type] in ('MCC Transfer','Transfer','Transfer Return')" + Environment.NewLine
        strMainQuery += " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_For_GSTIN on TSPL_LOCATION_MASTER_For_GSTIN.Location_Code = xx.[Location Code] " + Environment.NewLine +
        " left outer join TSPL_STATE_MASTER on  TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER_For_GSTIN.State " + Environment.NewLine +
        " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_SubLoc  on TSPL_LOCATION_MASTER_SubLoc.Location_Code = xx.[Sub Location] " + Environment.NewLine
        strMainQuery += " left join (select Ven_Group_Code,Group_Desc from TSPL_Vendor_GROUP) as Cust_Group on Cust.Vendor_Group_Code=Cust_Group.ven_Group_Code " + Environment.NewLine
        strMainQuery += " left join (select Zone_Code,Description from TSPL_ZONE_MASTER) as Zone on Cust.Zone_Code=Zone.Zone_Code " + Environment.NewLine
        strMainQuery += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=xx.Head_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type=xx.Head_Tax_Group_Type" + Environment.NewLine
        If clsCommon.myLen(strCategoryTable) > 0 Then
            strMainQuery += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=xx.[Item Code]" + Environment.NewLine
        End If
        strMainQuery += " left join tspl_item_master itmp on itmp.Item_Code=xx.[Item Code] " + Environment.NewLine + " left join TSPL_PURCHASE_ACCOUNTS tps on tps.Purchase_Class_Code=itmp.Purchase_Class_Code " + Environment.NewLine +
        " left join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code= (case when [Invoice Type] in ('MCC Transfer','Transfer','Transfer Return') then tps.Stock_Transfer_In else case when [Invoice Type] in ('PI','Purchase Return','Bulk Purchase','Bulk Purchase Return') and isJobWorkOutward=1 then tps.Purchase_JobWork else  tps.Inv_Control_Account end end)  " + Environment.NewLine +
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.[Location Code] " + Environment.NewLine +
        " left outer join TSPL_GL_ACCOUNTS as TabPurchaseGLCode on TabPurchaseGLCode.Account_Seg_Code1=TSPL_GL_ACCOUNTS.Account_Seg_Code1  and TabPurchaseGLCode.Account_Seg_Code7=TSPL_LOCATION_MASTER.Loc_Segment_Code" + Environment.NewLine +
        "  where 2 = 2 " + Environment.NewLine + " "
        QryLst.Add(strMainQuery)
        QryLst.Add(strPivotForFinalOuterQuery)
        QryLst.Add(strPivotForAddChargeFinalOuterSumQuery)
        Return QryLst
    End Function

    Public Shared Function GetTaxQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select TAX" & intloop & " from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select TAX" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function

    Public Shared Function GetAddChargeQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select Add_Charge_Code" & intloop & "  from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select Add_Charge_Code" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function

    Public Shared Function GetAddChargeZeroQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select 'AC_'+Add_Charge_Code" & intloop & " as Add_Charge_Code" & intloop & "  from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select 'AC_'+Add_Charge_Code" & intloop & " as Add_Charge_Code" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        If clsCommon.myLen(qry) > 0 Then
            qry = "select * from( " & qry & ") as t1 where Add_Charge_Code1 not in ('AC_')"
        End If

        Return qry
    End Function

    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean

        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Doc_Type As String = Nothing

            Dim obj As clsPurchaseInvoiceHead = clsPurchaseInvoiceHead.GetData(Doc_No, NavigatorType.Current, trans, "")
            If obj Is Nothing OrElse clsCommon.myLen(obj.PI_No) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If
            clsItemLocationDetails.CheckCancelInventoryBalance(Form_Id, Doc_No, trans)
            '' transfer data into cancel table

            Dim Str_PJVNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_PJV_HEAD.PJV_No from TSPL_PJV_HEAD where TSPL_PJV_HEAD.Invoice_No='" + Doc_No + "'", trans))


            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PI_HEAD", "PI_No", "TSPL_PI_DETAIL", "PI_No", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Str_PJVNo, "TSPL_PJV_HEAD", "PJV_No", "TSPL_PJV_Detail", "PJV_No", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PO_ADVANCE_ADJUSTMENT_KNOCKOFF", "PI_No", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PI_REMITTANCE", "Document_No", trans)

            clsPurchaseInvoiceHead.ReverseAndUnpost(Doc_No, trans)
            clsPurchaseInvoiceHead.DeleteData(Doc_No, trans)

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

End Class

Public Class clsPurchaseInvoiceDetail
#Region "Variables"
    Public PI_No As String = Nothing
    Public Row_Type As String = Nothing
    Public Line_No As Integer = 0
    Public Status As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public PI_Qty As Double = 0
    Public SRN_Id As String = Nothing
    Public PO_ID As String = Nothing
    Public GRN_ID As String = Nothing
    Public MRN_ID As String = Nothing
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
    Public Header_Discount_Per As Decimal = 0
    Public Header_Discount_Amount As Decimal = 0
    Public Disc_Per As Double = 0
    Public Detail_Discount_Amount As Decimal = 0
    Public Disc_Amt As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public Taxable_Amount_Per As Decimal = 0
    Public Taxable_Amount As Decimal = 0
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

    Public Accepted_Amount As Double = 0
    Public Rejected_Amount As Double = 0
    Public Shortage_Amount As Double = 0
    Public Leak_Amount As Double = 0
    Public Burst_Amount As Double = 0
    Public Amt_Less_Discount_Without_Shortage As Double = 0

    ''====================19/10/2016====================================
    Public ItemAdd_Charge_Code1 As String = Nothing
    Public ItemAdd_Org_Charge_Amt1 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt1 As Double = Nothing
    Public ItemAdd_Charge_Code9 As String = Nothing
    Public ItemAdd_Org_Charge_Amt9 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt9 As Double = Nothing
    Public ItemAdd_Charge_Code8 As String = Nothing
    Public ItemAdd_Org_Charge_Amt8 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt8 As Double = Nothing
    Public ItemAdd_Charge_Code7 As String = Nothing
    Public ItemAdd_Org_Charge_Amt7 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt7 As Double = Nothing
    Public ItemAdd_Charge_Code6 As String = Nothing
    Public ItemAdd_Org_Charge_Amt6 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt6 As Double = Nothing
    Public ItemAdd_Charge_Code5 As String = Nothing
    Public ItemAdd_Org_Charge_Amt5 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt5 As Double = Nothing
    Public ItemAdd_Charge_Code4 As String = Nothing
    Public ItemAdd_Org_Charge_Amt4 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt4 As Double = Nothing
    Public ItemAdd_Charge_Code3 As String = Nothing
    Public ItemAdd_Org_Charge_Amt3 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt3 As Double = Nothing
    Public ItemAdd_Charge_Code2 As String = Nothing
    Public ItemAdd_Org_Charge_Amt2 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt2 As Double = Nothing
    Public ItemAdd_Charge_Code10 As String = Nothing
    Public ItemAdd_Org_Charge_Amt10 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt10 As Double = Nothing
    Public Total_ItemAdd_Charge As Double = Nothing

    Public Capex_SubCode As String = Nothing
    Public Capex_Code As String = Nothing
    Public Emergency As Boolean = Nothing
    Public Category As String = Nothing
    ''==================================================================
    Public Insurance_Base_Amt As Decimal
    Public Insurance_Per As Decimal

    Public Against_Item_Wise_Tax_Rate As String = Nothing

    Public Item_Insurance_Base_Amt As Decimal = 0
    Public Item_Insurance_Apply_On As String = Nothing
    Public Item_Insurance_Rate As Decimal = 0
    Public Item_Insurance_Amt As Decimal = 0
    Public Item_Amt_After_Insurance As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strLocation As String, ByVal Arr As List(Of clsPurchaseInvoiceDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsPurchaseInvoiceDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PI_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "PI_Qty", obj.PI_Qty)
                clsCommon.AddColumnsForChange(coll, "SRN_Id", obj.SRN_Id, True)
                clsCommon.AddColumnsForChange(coll, "PO_ID", obj.PO_ID, True)
                clsCommon.AddColumnsForChange(coll, "GRN_ID", obj.GRN_ID, True)
                clsCommon.AddColumnsForChange(coll, "MRN_ID", obj.MRN_ID, True)

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
                clsCommon.AddColumnsForChange(coll, "Header_Discount_Per", obj.Header_Discount_Per)
                clsCommon.AddColumnsForChange(coll, "Header_Discount_Amount", obj.Header_Discount_Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Per", obj.Disc_Per)
                clsCommon.AddColumnsForChange(coll, "Detail_Discount_Amount", obj.Detail_Discount_Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Amt", obj.Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount", obj.Amt_Less_Discount)

                clsCommon.AddColumnsForChange(coll, "Taxable_Amount_Per", obj.Taxable_Amount_Per)
                clsCommon.AddColumnsForChange(coll, "Taxable_Amount", obj.Taxable_Amount)

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

                clsCommon.AddColumnsForChange(coll, "Accepted_Amount", obj.Accepted_Amount)
                clsCommon.AddColumnsForChange(coll, "Rejected_Amount", obj.Rejected_Amount)
                clsCommon.AddColumnsForChange(coll, "Shortage_Amount", obj.Shortage_Amount)
                clsCommon.AddColumnsForChange(coll, "Leak_Amount", obj.Leak_Amount)
                clsCommon.AddColumnsForChange(coll, "Burst_Amount", obj.Leak_Amount)
                clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount_Without_Shortage", obj.Amt_Less_Discount_Without_Shortage)

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


                ''==========================19/10/2016======================
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code1", obj.ItemAdd_Charge_Code1)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt1", obj.ItemAdd_Org_Charge_Amt1)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt1", obj.ItemAdd_Calc_Charge_Amt1)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code9", obj.ItemAdd_Charge_Code9)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt9", obj.ItemAdd_Org_Charge_Amt9)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt9", obj.ItemAdd_Calc_Charge_Amt9)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code8", obj.ItemAdd_Charge_Code8)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt8", obj.ItemAdd_Org_Charge_Amt8)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt8", obj.ItemAdd_Calc_Charge_Amt8)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code7", obj.ItemAdd_Charge_Code7)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt7", obj.ItemAdd_Org_Charge_Amt7)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt7", obj.ItemAdd_Calc_Charge_Amt7)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code6", obj.ItemAdd_Charge_Code6)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt6", obj.ItemAdd_Org_Charge_Amt6)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt6", obj.ItemAdd_Calc_Charge_Amt6)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code5", obj.ItemAdd_Charge_Code5)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt5", obj.ItemAdd_Org_Charge_Amt5)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt5", obj.ItemAdd_Calc_Charge_Amt5)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code4", obj.ItemAdd_Charge_Code4)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt4", obj.ItemAdd_Org_Charge_Amt4)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt4", obj.ItemAdd_Calc_Charge_Amt4)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code3", obj.ItemAdd_Charge_Code3)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt3", obj.ItemAdd_Org_Charge_Amt3)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt3", obj.ItemAdd_Calc_Charge_Amt3)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code2", obj.ItemAdd_Charge_Code2)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt2", obj.ItemAdd_Org_Charge_Amt2)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt2", obj.ItemAdd_Calc_Charge_Amt2)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code10", obj.ItemAdd_Charge_Code10)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt10", obj.ItemAdd_Org_Charge_Amt10)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt10", obj.ItemAdd_Calc_Charge_Amt10)
                clsCommon.AddColumnsForChange(coll, "Total_ItemAdd_Charge", obj.Total_ItemAdd_Charge)
                ''=================================================================
                clsCommon.AddColumnsForChange(coll, "Capex_Code", obj.Capex_Code, True)
                clsCommon.AddColumnsForChange(coll, "Capex_SubCode", obj.Capex_SubCode, True)
                clsCommon.AddColumnsForChange(coll, "Category", obj.Category, True)
                clsCommon.AddColumnsForChange(coll, "Emergency", IIf(obj.Emergency, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Against_Item_Wise_Tax_Rate", obj.Against_Item_Wise_Tax_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Insurance_Per", obj.Insurance_Per)
                clsCommon.AddColumnsForChange(coll, "Insurance_Base_Amt", obj.Insurance_Base_Amt)

                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Base_Amt", obj.Item_Insurance_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Apply_On", obj.Item_Insurance_Apply_On)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Rate", obj.Item_Insurance_Rate)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Amt", obj.Item_Insurance_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Amt_After_Insurance", obj.Item_Amt_After_Insurance)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PI_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function CompletePI(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_PI_DETAIL set Status=1 where PI_No='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function

    Public Shared Function GetBalancePIQty(ByVal strPICode As String, ByVal strICode As String, ByVal strCurrPRNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double, ByVal isForReject As Boolean, Optional ByVal strCurrIssueReturnNo As String = Nothing, Optional ByVal trans As SqlTransaction = Nothing) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(   " &
            " select TSPL_PI_DETAIL.Item_Code as ICode,"
        If isForReject Then
            qry += " isnull( TSPL_PI_DETAIL.Reject_Qty,0) as Qty,"
        Else
            qry += " TSPL_PI_DETAIL.PI_Qty as Qty,"
        End If
        qry += " 1 as RI  from TSPL_PI_DETAIL " &
            " left outer join TSPL_Pi_HEAD on TSPL_Pi_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_DETAIL.Status=0 and TSPL_PI_HEAD.Status=1 and TSPL_PI_DETAIL.PI_No ='" + strPICode + "' and TSPL_PI_DETAIL.Item_Code='" + strICode + "' and  TSPL_PI_DETAIL.Unit_code='" + strUOM + "' "
        If clsCommon.myLen(strCurrIssueReturnNo) <= 0 Then ''when not coming from issue/return screen then mrp cond, not checked
            qry += " and isnull(TSPL_PI_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_PI_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' "
        End If
        If isForReject Then
            qry += " and isnull( TSPL_PI_DETAIL.Reject_Qty,0)>0"
        End If
        qry += " union all  select TSPL_PR_DETAIL.Item_Code as ICode,TSPL_PR_DETAIL.PR_Qty as Qty,-1 as RI from TSPL_PR_DETAIL left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No where TSPL_PR_DETAIL.PI_Id='" + strPICode + "'   and TSPL_PR_DETAIL.Item_Code='" + strICode + "' and  TSPL_PR_DETAIL.Unit_code='" + strUOM + "' and TSPL_PR_DETAIL.PR_No not in ('" + strCurrPRNo + "')"
        If clsCommon.myLen(strCurrIssueReturnNo) <= 0 Then
            qry += " and isnull(TSPL_PR_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_PR_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' "
        End If
        If isForReject Then
            qry += " and TSPL_PR_HEAD.is_Reject_Item=1 "
        End If
        If isForReject Then
            qry += " Union all select TSPL_Transfer_ORDER_DETAIL.Item_Code,TSPL_Transfer_ORDER_DETAIL.Out_Qty,-1 as RI from TSPL_Transfer_ORDER_DETAIL left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_Transfer_ORDER_DETAIL.Document_No where LEN( ISNULL( TSPL_TRANSFER_ORDER_HEAD.RMDA_Code,''))>0  and TSPL_Transfer_ORDER_DETAIL.Item_Code='" + strICode + "' and TSPL_Transfer_ORDER_DETAIL.Unit_code='" + strUOM + "' and TSPL_TRANSFER_ORDER_HEAD.RMDA_Code in (select MAX(TSPL_SRN_HEAD.RMDA_No) from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No left outer join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_SRN_DETAIL.SRN_No where TSPL_PI_DETAIL.PI_No='" + strPICode + "')  and TSPL_Transfer_ORDER_DETAIL.Document_No not in ('" + strCurrPRNo + "')"
            If clsCommon.myLen(strCurrIssueReturnNo) <= 0 Then
                qry += " and isnull(TSPL_Transfer_ORDER_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' "
            End If
        End If
        ''=================add unposted issue/return screen when purchase invoice is made auto,after post it considered from PR screen======15/11/2016=============
        qry += " union all  select TSPL_IssueReturn_DETAIL.Item_Code as ICode,TSPL_IssueReturn_DETAIL.issued_qty as Qty,-1 as RI from TSPL_IssueReturn_DETAIL left outer join TSPL_IssueReturn_Head on TSPL_IssueReturn_DETAIL.doc_no=TSPL_IssueReturn_Head.doc_no where isnull(TSPL_IssueReturn_Head.[status],0)=0 and TSPL_IssueReturn_DETAIL.purchaseinvoice_no='" + strPICode + "'   and TSPL_IssueReturn_DETAIL.Item_Code='" + strICode + "' and  TSPL_IssueReturn_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_IssueReturn_DETAIL.amount,0)='" + clsCommon.myCstr(dblAssessable) + "' and TSPL_IssueReturn_DETAIL.doc_no not in ('" + strCurrIssueReturnNo + "') "
        If isForReject Then
            qry += " and TSPL_IssueReturn_Head.Is_Reject=1 "
        End If
        ''========================================================================================================
        qry += " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

End Class

Public Class clsPurchaseReco

#Region "Variables"
    Public Transaction As ArrayList
    Public Location As ArrayList
    Public Item_Code As ArrayList
    Public Vendor_Code As ArrayList
    Public Acc_Code As ArrayList
    Public Doc_No As ArrayList
    Public From_Date As Date
    Public To_Date As Date
    Public IncludeAllDoc As Boolean = False
    Public Account_Set As ArrayList
    Public Vendor_Group As ArrayList
    Public ShowMismatchDoc As Boolean = False
    Public IncludeApplyDocumentPayment As Boolean = False
#End Region

End Class

<Serializable()>
Public Class clsPIAdditionChargeInsurance
#Region "Variables"
    Public TR_Code As String = Nothing
    Public PI_No As String = Nothing
    Public AC_Code As String = Nothing
    Public AC_Name As String = Nothing ''Not a table Field
    Public Amount As Decimal
#End Region
    Public Shared Function SaveData(ByVal DocNo As String, ByVal DocDate As DateTime, ByVal arr As List(Of clsPIAdditionChargeInsurance), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPIAdditionChargeInsurance In arr
                    coll = New Hashtable()
                    objtr.TR_Code = clsERPFuncationality.GetNextCode(trans, DocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_Code", objtr.TR_Code)
                    clsCommon.AddColumnsForChange(coll, "PI_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "AC_Code", objtr.AC_Code)
                    clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PI_ADDITION_CHARGE_INSURANCE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal DocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "delete from TSPL_PI_ADDITION_CHARGE_INSURANCE where PI_No='" + DocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function
    Public Shared Function GetData(ByVal DocNo As String, ByVal trans As SqlTransaction) As List(Of clsPIAdditionChargeInsurance)
        Dim qry As String = "select TSPL_PI_ADDITION_CHARGE_INSURANCE.*,TSPL_Additional_Charges.Description as AC_Name  from TSPL_PI_ADDITION_CHARGE_INSURANCE left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code=TSPL_PI_ADDITION_CHARGE_INSURANCE.AC_Code where TSPL_PI_ADDITION_CHARGE_INSURANCE.PI_No='" + DocNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim Arr_ACInsurance As List(Of clsPIAdditionChargeInsurance) = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Arr_ACInsurance = New List(Of clsPIAdditionChargeInsurance)
            For Each dr As DataRow In dt.Rows
                Dim objtr As New clsPIAdditionChargeInsurance()
                objtr.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                objtr.PI_No = clsCommon.myCstr(dr("PI_No"))
                objtr.AC_Code = clsCommon.myCstr(dr("AC_Code"))
                objtr.AC_Name = clsCommon.myCstr(dr("AC_Name"))
                objtr.Amount = clsCommon.myCstr(dr("Amount"))
                Arr_ACInsurance.Add(objtr)
            Next
        End If
        Return Arr_ACInsurance
    End Function
End Class
