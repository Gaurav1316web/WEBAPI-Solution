'--27/08/2012--Updation By--Pankaj Kumar--Applied GL Security While Navigating Document Finder  --Fwd By--Ranjana Mam
Imports common
Imports System.Data.SqlClient
Public Class clsGRNHead

#Region "Variables"
    Public IsSkipPurchaseQC As Integer = 0
    Public isJobWorkOutward As Integer = 0
    Public Amendment_No As Double = 0
    Public IsCancel As Integer = Nothing
    Public RGP_Type As String = Nothing
    Public PurchaseOrder_Type As String = Nothing
    Public GRN_No As String = Nothing
    Public GRN_Date As DateTime = Nothing
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
    Public Total_Taxable_Amount As Double = 0
    Public GRN_Total_Amt As Double = 0
    Public Comments As String = Nothing
    Public Comp_Code As String = Nothing
    Public Terms_Code As String = Nothing
    Public TermsName As String = Nothing
    Public Due_Date As String = Nothing
    Public Posting_Date As DateTime?

    Public Carrier As String = Nothing
    Public VehicleNo As String = Nothing
    Public GRNo As String = Nothing
    Public GENo As String = Nothing
    Public GEDate As Date? = Nothing
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public Item_Type As String = Nothing

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
    Public Against_Requisition As String = Nothing
    Public Against_PO As String = Nothing
    Public Arr As List(Of clsGRNDetail) = Nothing
    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    '' Anubhooti 03-Sep-2014 BM00000003657
    Public LR_No As String = Nothing
    Public LR_Date As Date? = Nothing
    Public RoadPermit_No As String = Nothing
    Public RoadPermit_Date As Date? = Nothing
    Public Invoiceno As String = Nothing
    Public InvoiceDate As Date? = Nothing
    Public TransporterDocumentBility As String = Nothing
    Public Against_RGP_No As String = Nothing
    Public Against_Schedule_Code As String = Nothing
    Public RGP_Non_Inventory_Item As String = Nothing
    Public Sublocation_Code As String
    Public SubLocationName As String
    Public Tax_Calculation_Type As EnumTaxCalucationType

    Public Total_Item_Insurance_Amt As Decimal = 0
    Public Total_Add_Charge_Insurance As Decimal = 0
    Public Arr_ACInsurance As List(Of clsGRNAdditionChargeInsurance) = Nothing
    Public VisualQCStatus As Integer = 0
    Public VisualQCRemarks As String = Nothing
    Public VisualQCUpdatedDate As Date? = Nothing
    Public VisualQCUpdatedDateSecond As Date? = Nothing
    Public VisualQCStatusSecond As Integer = 0
    Public VisualQCRemarksSecond As String = Nothing
    Public GRN_Qty As Decimal = 0
    Public Item_Desc As String = Nothing
    Public Item_Code As String = Nothing
    Public WeighmentNo As String = Nothing
    Public WeighmentDate As Date? = Nothing
    Public MRNNo As String = Nothing
    Public MRNDate As Date? = Nothing
    Public PINo As String = Nothing
    Public SRNNo As String = Nothing
    Public SRNDate As Date? = Nothing
    Public penalty As String = Nothing
    Public Retention As Decimal = 0
#End Region
    Public Function SaveData(ByVal obj As clsGRNHead, ByVal isNewEntry As Boolean, Optional ByVal isamendment As Boolean = False) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans, isamendment)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsGRNHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal isamendment As Boolean = False) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Gate Received Note", obj.Bill_To_Location, obj.GRN_Date, trans)
            clsGRNAdditionChargeInsurance.DeleteData(obj.GRN_No, trans)
            Dim qry As String = "delete from TSPL_GRN_DETAIL where GRN_No='" + obj.GRN_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isamendment Then
                obj.Amendment_No = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT ISNULL(Amendment_No,0) from TSPL_GRN_HEAD WHERE GRN_No='" + clsCommon.myCstr(obj.GRN_No) + "'", trans))
                isSaved = isSaved AndAlso SaveDataForHistory(obj.GRN_No, clsCommon.myCdbl(obj.Amendment_No + 1), trans)
            End If
            Dim strDocNo As String = ""
            If isNewEntry Then
                If obj.isJobWorkOutward = 1 Then
                    'Dim strSegCode As String = clsDBFuncationality.getSingleValue("  select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code = '" + obj.Sublocation_Code + "'", trans)
                    obj.GRN_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.GRN_Date), clsDocType.GRN, clsDocTransactionType.POJobWorkOutward, obj.Sublocation_Code)
                Else
                    Dim isPODocumentTypeWise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseCounterOnTransactionType, clsFixedParameterCode.PurchaseCounterOnTransactionType, trans)) = 0, False, True) ''Make Setting Balwinder
                    If isPODocumentTypeWise Then
                        ''------------ Added by Parteek 28/03/2017 client UDL
                        Dim UDLRGPWiseDocument As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UDLRGPWiseDocument, clsFixedParameterCode.UDLRGPWiseDocument, trans)) = "1", True, False))
                        If UDLRGPWiseDocument = True Then
                            If clsCommon.myLen(obj.Against_RGP_No) > 0 Then
                                obj.GRN_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.GRN_Date), clsDocType.GRN, clsDocTransactionType.RGPWise, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                            Else
                                If clsCommon.CompairString(obj.PurchaseOrder_Type, "J") = CompairStringResult.Equal Then
                                    obj.GRN_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.GRN_Date), clsDocType.GRN, clsDocTransactionType.POJobWork, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                                ElseIf clsCommon.CompairString(obj.PurchaseOrder_Type, "I") = CompairStringResult.Equal Then
                                    obj.GRN_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.GRN_Date), clsDocType.GRN, clsDocTransactionType.POImport, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                                ElseIf clsCommon.CompairString(obj.PurchaseOrder_Type, "L") = CompairStringResult.Equal Then
                                    obj.GRN_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.GRN_Date), clsDocType.GRN, clsDocTransactionType.PODomestic, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                                Else
                                    Throw New Exception("Type is Not Correct To Generate the Transaction Code")
                                End If
                            End If


                        Else
                            If clsCommon.CompairString(obj.PurchaseOrder_Type, "J") = CompairStringResult.Equal Then
                                obj.GRN_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.GRN_Date), clsDocType.GRN, clsDocTransactionType.POJobWork, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                            ElseIf clsCommon.CompairString(obj.PurchaseOrder_Type, "I") = CompairStringResult.Equal Then
                                obj.GRN_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.GRN_Date), clsDocType.GRN, clsDocTransactionType.POImport, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                            ElseIf clsCommon.CompairString(obj.PurchaseOrder_Type, "L") = CompairStringResult.Equal Then
                                obj.GRN_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.GRN_Date), clsDocType.GRN, clsDocTransactionType.PODomestic, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                            Else
                                Throw New Exception("Type is Not Correct To Generate the Transaction Code")
                            End If
                        End If

                    Else
                        Dim TransType = clsDBFuncationality.getSingleValue("SELECT PREFIX_CODE FROM TSPL_ITEM_TYPE_MASTER WHERE ITEM_TYPE_CODE='" + obj.Item_Type + "'", trans)
                        obj.GRN_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.GRN_Date), clsDocType.GRN, TransType, obj.Bill_To_Location)
                        If clsCommon.CompairString(obj.GRN_No, String.Empty) = CompairStringResult.Equal Then
                            Throw New Exception("Item Type is Not Correct To Generate the Transaction Code")
                        End If
                    End If
                End If

            End If
            'If Not isNewEntry Then
            '    CancleUpdate(obj.GRNo, trans)
            'End If
            If (clsCommon.myLen(obj.GRN_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "GRN_Date", clsCommon.GetPrintDate(obj.GRN_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Type", obj.PurchaseOrder_Type)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
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
            clsCommon.AddColumnsForChange(coll, "GRN_Total_Amt", obj.GRN_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "IsCancel", obj.IsCancel)
            clsCommon.AddColumnsForChange(coll, "Carrier", obj.Carrier)
            clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
            clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo)
            clsCommon.AddColumnsForChange(coll, "GENo", obj.GENo)
            clsCommon.AddColumnsForChange(coll, "isJobWorkOutward", obj.isJobWorkOutward)
            clsCommon.AddColumnsForChange(coll, "Against_Requisition", obj.Against_Requisition, True)
            clsCommon.AddColumnsForChange(coll, "Against_PO", obj.Against_PO, True)
            clsCommon.AddColumnsForChange(coll, "Against_RGP_No", obj.Against_RGP_No, True)
            clsCommon.AddColumnsForChange(coll, "Against_Schedule_Code", obj.Against_Schedule_Code, True)
            clsCommon.AddColumnsForChange(coll, "RGP_Non_Inventory_Item", obj.RGP_Non_Inventory_Item)
            clsCommon.AddColumnsForChange(coll, "IsSkipPurchaseQC", obj.IsSkipPurchaseQC)
            If obj.GEDate.HasValue Then
                clsCommon.AddColumnsForChange(coll, "GEDate", clsCommon.GetPrintDate(obj.GEDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "GEDate", Nothing, True)
            End If

            'stuti
            If obj.RoadPermit_Date IsNot Nothing AndAlso clsCommon.myLen(obj.RoadPermit_Date) > 0 AndAlso IsDate(obj.RoadPermit_Date) Then
                clsCommon.AddColumnsForChange(coll, "RoadPermit_Date", clsCommon.GetPrintDate(obj.RoadPermit_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "RoadPermit_No", obj.RoadPermit_No)
            clsCommon.AddColumnsForChange(coll, "[Invoice/Challan_No]", obj.Invoiceno)
            If obj.InvoiceDate IsNot Nothing AndAlso clsCommon.myLen(obj.InvoiceDate) > 0 AndAlso IsDate(obj.InvoiceDate) Then
                clsCommon.AddColumnsForChange(coll, "Invoice_Date", clsCommon.GetPrintDate(obj.InvoiceDate, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "TransporterDocument_Bility", obj.TransporterDocumentBility)
            '=======end here===========
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
            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)

            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)


            clsCommon.AddColumnsForChange(coll, "Total_Add_Charge_Insurance", obj.Total_Add_Charge_Insurance)
            clsCommon.AddColumnsForChange(coll, "Total_Item_Insurance_Amt", obj.Total_Item_Insurance_Amt)
            clsCommon.AddColumnsForChange(coll, "Retention", obj.Retention)
            If isamendment Then
                Dim AmendmentCode As String = Nothing
                AmendmentCode = obj.GRN_No + "$" + clsCommon.myCstr(obj.Amendment_No + 1)
                clsCommon.AddColumnsForChange(coll, "Amendment_No", obj.Amendment_No + 1)
                clsCommon.AddColumnsForChange(coll, "Amendment_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Amendment_Code", AmendmentCode)
                clsCommon.AddColumnsForChange(coll, "Amendment_By", objCommonVar.CurrentUserCode)
            Else
                clsCommon.AddColumnsForChange(coll, "Amendment_No", obj.Amendment_No)
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            End If

            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            If clsCommon.myLen(obj.ApplicableFrom) > 0 Then
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", Nothing, True)
            End If
            '' End currencyconversion

            '' Anubhooti 03-Sep-2014 BM00000003657 (Add LR_No and LR_Date)
            clsCommon.AddColumnsForChange(coll, "LR_No", obj.LR_No)

            If clsCommon.myLen(obj.LR_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "LR_Date", clsCommon.GetPrintDate(obj.LR_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "LR_Date", Nothing, True)
            End If
            ''

            clsCommon.AddColumnsForChange(coll, "RGP_Type", obj.RGP_Type)
            clsCommon.AddColumnsForChange(coll, "VisualQCStatus", obj.VisualQCStatus)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "GRN_No", obj.GRN_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GRN_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GRN_HEAD", OMInsertOrUpdate.Update, "TSPL_GRN_HEAD.GRN_No='" + obj.GRN_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsGRNDetail.SaveData(obj.GRN_No, obj.Arr, trans)
            isSaved = isSaved AndAlso clsGRNAdditionChargeInsurance.SaveData(obj.GRN_No, obj.GRN_Date, obj.Arr_ACInsurance, trans)
            If isamendment Then
                qry = "update TSPL_PO_WEIGHTMENT_DETAIL set GRN_Qty=xx.GRN_Qty from (" + Environment.NewLine +
                " select Item_Code,sum(GRN_Qty) as GRN_Qty from TSPL_GRN_DETAIL where GRN_No='" + obj.GRN_No + "' group by Item_Code" + Environment.NewLine +
                " )xx inner join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Item_Code=TSPL_PO_WEIGHTMENT_DETAIL.Item_Code" + Environment.NewLine +
                " where Weighment_Code in (select Weighment_Code from TSPL_PO_WEIGHTMENT_HEAD where Against_GRN_No='" + obj.GRN_No + "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            qry = "select case when WeightItem>0 and NormalItem>0 then 'FAIL' else 'PASS' end as ISCorrect,WeightItem,NormalItem  from (" + Environment.NewLine +
              " select  sum(case when TSPL_ITEM_MASTER.Is_Auto_Weighment=1 then 1 else 0 end ) as WeightItem,sum(case when TSPL_ITEM_MASTER.Is_Auto_Weighment=0 then 1 else 0 end ) as NormalItem  from TSPL_GRN_DETAIL" + Environment.NewLine +
              " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code " + Environment.NewLine +
              "  where TSPL_GRN_DETAIL.GRN_No='" + obj.GRN_No + "'" + Environment.NewLine +
              " )x"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("ISCorrect")), "FAIL") = CompairStringResult.Equal Then
                    Throw New Exception("All items should be auto Weighment item or Not")
                End If
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Is_Auto_Weighment_Type", IIf(clsCommon.myCdbl(dt.Rows(0)("WeightItem")) > 0, 1, 0))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GRN_HEAD", OMInsertOrUpdate.Update, "TSPL_GRN_HEAD.GRN_No='" + obj.GRN_No + "'", trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.GRN_No), "TSPL_GRN_HEAD", "GRN_No", "TSPL_GRN_DETAIL", "GRN_No", "TSPL_GRN_RGP_CONVERSION_DETAIL", "GRN_No", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CancleUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_GRN_HEAD", "GRN_No", "TSPL_GRN_DETAIL", "GRN_No", "TSPL_PI_REMITTANCE", "Document_No", trans)
        Return True
    End Function

    Public Shared Function UpdateData(ByVal obj As clsGRNHead, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        trans = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "GRN_Date", clsCommon.GetPrintDate(obj.GRN_Date, "dd/MMM/yyyy hh:mm tt"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GRN_HEAD", OMInsertOrUpdate.Update, "TSPL_GRN_HEAD.GRN_No='" + obj.GRN_No + "'", trans)
            clsCommon.AddColumnsForChange(coll, "LR_No", obj.LR_No)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GRN_HEAD", OMInsertOrUpdate.Update, "TSPL_GRN_HEAD.GRN_No='" + obj.GRN_No + "'", trans)

            Dim coll1 As New Hashtable()
            clsCommon.AddColumnsForChange(coll1, "VehicleNo", obj.VehicleNo)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_GRN_HEAD", OMInsertOrUpdate.Update, "TSPL_GRN_HEAD.GRN_No='" + obj.GRN_No + "'", trans)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_MRN_HEAD", OMInsertOrUpdate.Update, "TSPL_MRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_SRN_HEAD", OMInsertOrUpdate.Update, "TSPL_SRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)

            Dim coll6 As New Hashtable()
            clsCommon.AddColumnsForChange(coll6, "[Invoice/Challan_No]", obj.Invoiceno)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll6, "TSPL_GRN_HEAD", OMInsertOrUpdate.Update, "TSPL_GRN_HEAD.GRN_No='" + obj.GRN_No + "'", trans)

            Dim coll8 As New Hashtable()
            clsCommon.AddColumnsForChange(coll8, "Invoice_No", obj.Invoiceno)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll8, "TSPL_MRN_HEAD", OMInsertOrUpdate.Update, "TSPL_MRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)

            Dim coll9 As New Hashtable()
            clsCommon.AddColumnsForChange(coll9, "Inv_No", obj.Invoiceno)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll9, "TSPL_SRN_HEAD", OMInsertOrUpdate.Update, "TSPL_SRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)

            Dim coll5 As New Hashtable()
            clsCommon.AddColumnsForChange(coll5, "Invoice_Date", clsCommon.GetPrintDate(obj.InvoiceDate, "dd/MMM/yyyy"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll5, "TSPL_GRN_HEAD", OMInsertOrUpdate.Update, "TSPL_GRN_HEAD.GRN_No='" + obj.GRN_No + "'", trans)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll5, "TSPL_MRN_HEAD", OMInsertOrUpdate.Update, "TSPL_MRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)

            Dim INVD As New Hashtable()
            clsCommon.AddColumnsForChange(INVD, "Inv_Date", clsCommon.GetPrintDate(obj.InvoiceDate, "dd/MMM/yyyy"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(INVD, "TSPL_SRN_HEAD", OMInsertOrUpdate.Update, "TSPL_SRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)

            Dim SRND As New Hashtable()
            clsCommon.AddColumnsForChange(SRND, "SRNDATE", clsCommon.GetPrintDate(obj.SRNDate, "dd/MMM/yyyy"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(INVD, "TSPL_SRN_HEAD", OMInsertOrUpdate.Update, "TSPL_SRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)

            Dim coll4 As New Hashtable()
            clsCommon.AddColumnsForChange(coll4, "GRNo", obj.GRNo)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll4, "TSPL_GRN_HEAD", OMInsertOrUpdate.Update, "TSPL_GRN_HEAD.GRN_No='" + obj.GRN_No + "'", trans)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll4, "TSPL_MRN_HEAD", OMInsertOrUpdate.Update, "TSPL_MRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll4, "TSPL_SRN_HEAD", OMInsertOrUpdate.Update, "TSPL_SRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)

            Dim coll3 As New Hashtable()
            clsCommon.AddColumnsForChange(coll3, "GENo", obj.GENo)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll3, "TSPL_GRN_HEAD", OMInsertOrUpdate.Update, "TSPL_GRN_HEAD.GRN_No='" + obj.GRN_No + "'", trans)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll3, "TSPL_MRN_HEAD", OMInsertOrUpdate.Update, "TSPL_MRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll3, "TSPL_SRN_HEAD", OMInsertOrUpdate.Update, "TSPL_SRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)

            Dim coll2 As New Hashtable()
            If obj.GEDate IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll2, "GEDate", clsCommon.GetPrintDate(obj.GEDate, "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll2, "TSPL_GRN_HEAD", OMInsertOrUpdate.Update, "TSPL_GRN_HEAD.GRN_No='" + obj.GRN_No + "'", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll2, "TSPL_MRN_HEAD", OMInsertOrUpdate.Update, "TSPL_MRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll2, "TSPL_SRN_HEAD", OMInsertOrUpdate.Update, "TSPL_SRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)
            End If

            Dim coll7 As New Hashtable()
            clsCommon.AddColumnsForChange(coll7, "Weighment_Date", clsCommon.GetPrintDate(obj.WeighmentDate, "dd/MMM/yyyy hh:mm tt"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll7, "TSPL_PO_WEIGHTMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No='" + obj.GRN_No + "'", trans)

            If clsCommon.myLen(obj.WeighmentNo) > 0 Then
                Dim MD As New Hashtable()
                clsCommon.AddColumnsForChange(MD, "MRN_Date", clsCommon.GetPrintDate(obj.WeighmentDate, "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(MD, "TSPL_MRN_HEAD", OMInsertOrUpdate.Update, "TSPL_MRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)
            Else
                Dim MD1 As New Hashtable()
                clsCommon.AddColumnsForChange(MD1, "MRN_Date", clsCommon.GetPrintDate(obj.MRNDate, "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(MD1, "TSPL_MRN_HEAD", OMInsertOrUpdate.Update, "TSPL_MRN_HEAD.Against_GRN='" + obj.GRN_No + "'", trans)
            End If

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveDataForHistory(ByVal strCode As String, ByVal intAmbandentNo As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_GRN_HEAD", trans)
            strInvColumns = "[" + strInvColumns.Replace(",", "],[") + "]"
            Dim qry As String = "INSERT INTO TSPL_GRN_HEAD_HISTORY (" + strInvColumns + ") SELECT " + strInvColumns.Replace("[Amendment_No]", "" + clsCommon.myCstr(intAmbandentNo) + "") + " FROM TSPL_GRN_HEAD WHERE GRN_NO='" + clsCommon.myCstr(strCode) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            strInvColumns = clsERPFuncationality.GetTableColumnNameForQry("TSPL_GRN_DETAIL", trans)
            strInvColumns = "[" + strInvColumns.Replace(",", "],[") + "]"
            qry = "INSERT INTO TSPL_GRN_DETAIL_HISTORY (" + strInvColumns + ") SELECT " + strInvColumns + " FROM TSPL_GRN_DETAIL WHERE GRN_NO='" + clsCommon.myCstr(strCode) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function UpdateVisualQCStatus(ByVal strCode As String, ByVal VisualQCStatus As Integer, ByVal VisualQCRemarks As String, ByVal VisualQCUpdatedDate As Date) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            UpdateVisualQCStatus(strCode, VisualQCStatus, VisualQCRemarks, VisualQCUpdatedDate, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function UpdateVisualQCStatus(ByVal strCode As String, ByVal VisualQCStatus As Integer, ByVal VisualQCRemarks As String, ByVal VisualQCUpdatedDate As Date, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If clsCommon.myLen(strCode) > 0 Then
                Dim qry As String = "Update tspl_grn_head set VisualQCStatus='" + clsCommon.myCstr(VisualQCStatus) + "',VisualQCRemarks='" + clsCommon.myCstr(VisualQCRemarks) + "',VisualQCUpdatedBy='" + objCommonVar.CurrentUserCode + "',VisualQCUpdatedDate='" + clsCommon.GetPrintDate(VisualQCUpdatedDate, "dd/MMM/yyyy") + "' where GRN_NO='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function UpdateVisualQCStatusSecond(ByVal strCode As String, ByVal VisualQCStatus As Integer, ByVal VisualQCRemarks As String, ByVal VisualQCUpdatedDate As Date) As Boolean
        Dim isSaved As Boolean = True
        Try
            If clsCommon.myLen(strCode) > 0 Then
                Dim qry As String = "Update tspl_grn_head set VisualQCStatusSecond='" + clsCommon.myCstr(VisualQCStatus) + "',VisualQCRemarksSecond='" + clsCommon.myCstr(VisualQCRemarks) + "',VisualQCUpdatedBySecond='" + objCommonVar.CurrentUserCode + "',VisualQCUpdatedDateSecond='" + clsCommon.GetPrintDate(VisualQCUpdatedDate, "dd/MMM/yyyy") + "' where GRN_NO='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsGRNHead
        Try
            Return GetData(strDocumentNo, NavType, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsGRNHead
        Try
            Dim obj As clsGRNHead = Nothing
            Dim qry As String = "SELECT TSPL_GRN_HEAD.*,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName, " &
            " TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_LOCATION_MASTER_SubLocation.Location_Desc as SubLocationName,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo FROM TSPL_GRN_HEAD " &
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GRN_HEAD.Bill_To_Location " &
            " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_GRN_HEAD.Ship_To_Location " &
            " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_SubLocation  on TSPL_LOCATION_MASTER_SubLocation.Location_Code=TSPL_GRN_HEAD.Sublocation_Code " &
            " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_GRN_HEAD.Tax_Group " &
            " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_GRN_HEAD.Terms_Code left outer join TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_GRN_HEAD.Against_PO where 2=2"
            Dim WhrCls As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_GRN_HEAD.GRN_No = (select MIN(GRN_No) from TSPL_GRN_HEAD Where 1=1 " + WhrCls + ")"
                Case NavigatorType.Last
                    qry += " and TSPL_GRN_HEAD.GRN_No = (select Max(GRN_No) from TSPL_GRN_HEAD Where 1=1 " + WhrCls + ")"
                Case NavigatorType.Next
                    qry += " and TSPL_GRN_HEAD.GRN_No = (select Min(GRN_No) from TSPL_GRN_HEAD where GRN_No>'" + strPONo + "' " + WhrCls + ")"
                Case NavigatorType.Previous
                    qry += " and TSPL_GRN_HEAD.GRN_No = (select Max(GRN_No) from TSPL_GRN_HEAD where GRN_No<'" + strPONo + "' " + WhrCls + ")"
                Case NavigatorType.Current
                    qry += " and TSPL_GRN_HEAD.GRN_No = '" + strPONo + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsGRNHead
                obj.VisualQCStatusSecond = CInt(dt.Rows(0)("VisualQCStatusSecond"))
                obj.VisualQCRemarksSecond = clsCommon.myCstr(dt.Rows(0)("VisualQCRemarksSecond"))
                If dt.Rows(0)("VisualQCUpdatedDateSecond") IsNot DBNull.Value Then
                    obj.VisualQCUpdatedDateSecond = clsCommon.myCDate(dt.Rows(0)("VisualQCUpdatedDateSecond"))
                End If
                If dt.Rows(0)("VisualQCUpdatedDate") IsNot DBNull.Value Then
                    obj.VisualQCUpdatedDate = clsCommon.myCDate(dt.Rows(0)("VisualQCUpdatedDate"))
                End If
                obj.IsSkipPurchaseQC = CInt(dt.Rows(0)("IsSkipPurchaseQC"))
                obj.VisualQCStatus = CInt(dt.Rows(0)("VisualQCStatus"))
                obj.VisualQCRemarks = clsCommon.myCstr(dt.Rows(0)("VisualQCRemarks"))
                obj.isJobWorkOutward = clsCommon.myCstr(dt.Rows(0)("isJobWorkOutward"))
                obj.GRN_No = clsCommon.myCstr(dt.Rows(0)("GRN_No"))
                obj.GRN_Date = clsCommon.myCstr(dt.Rows(0)("GRN_Date"))
                obj.PurchaseOrder_Type = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1 AndAlso clsCommon.myCdbl(dt.Rows(0)("iscancel")) <> 1, ERPTransactionStatus.Approved, IIf(clsCommon.myCdbl(dt.Rows(0)("iscancel")) = 1, ERPTransactionStatus.Cancel, ERPTransactionStatus.Pending))
                obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
                'obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
                obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("RefTendorNo"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
                obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
                obj.Sublocation_Code = clsCommon.myCstr(dt.Rows(0)("Sublocation_Code"))
                obj.SubLocationName = clsCommon.myCstr(dt.Rows(0)("SubLocationName"))
                obj.RGP_Type = clsCommon.myCstr(dt.Rows(0)("rgp_type"))
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
                obj.Total_Taxable_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Taxable_Amount"))
                obj.GRN_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("GRN_Total_Amt"))
                obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
                obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
                obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
                obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
                obj.ShipToLocationName = clsCommon.myCstr(dt.Rows(0)("ShipToLocationName"))
                obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
                obj.TermsName = clsCommon.myCstr(dt.Rows(0)("TermsName"))
                If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                    obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
                End If
                obj.Carrier = clsCommon.myCstr(dt.Rows(0)("Carrier"))
                obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
                obj.GRNo = clsCommon.myCstr(dt.Rows(0)("GRNo"))
                obj.GENo = clsCommon.myCstr(dt.Rows(0)("GENo"))

                If dt.Rows(0)("GEDate") IsNot DBNull.Value Then
                    obj.GEDate = clsCommon.myCDate(dt.Rows(0)("GEDate"))
                End If

                'stuti
                obj.IsCancel = CInt(dt.Rows(0)("IsCancel"))
                obj.RoadPermit_No = clsCommon.myCstr(dt.Rows(0)("RoadPermit_No"))
                If dt.Rows(0)("RoadPermit_Date") IsNot DBNull.Value Then
                    obj.RoadPermit_Date = clsCommon.myCDate(dt.Rows(0)("RoadPermit_Date"))
                End If
                obj.Invoiceno = clsCommon.myCstr(dt.Rows(0)("Invoice/Challan_No"))
                If dt.Rows(0)("Invoice_Date") IsNot DBNull.Value Then
                    obj.InvoiceDate = clsCommon.myCDate(dt.Rows(0)("Invoice_Date"))
                End If
                obj.TransporterDocumentBility = clsCommon.myCstr(dt.Rows(0)("TransporterDocument_Bility"))
                '====end here===

                obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
                obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
                obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))

                obj.Against_Requisition = clsCommon.myCstr(dt.Rows(0)("Against_Requisition"))
                obj.Against_PO = clsCommon.myCstr(dt.Rows(0)("Against_PO"))
                obj.Against_RGP_No = clsCommon.myCstr(dt.Rows(0)("Against_RGP_No"))
                obj.Against_Schedule_Code = clsCommon.myCstr(dt.Rows(0)("Against_Schedule_Code"))
                obj.RGP_Non_Inventory_Item = clsCommon.myCstr(dt.Rows(0)("RGP_Non_Inventory_Item"))


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
                '' CURRENCYCONVERSION 
                obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
                obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
                If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                    obj.ApplicableFrom = Nothing
                Else
                    obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
                End If
                '' END CURRENCYCONVERSION 

                '' Anubhooti 03-Sep-2014 BM00000003657
                obj.LR_No = clsCommon.myCstr(dt.Rows(0)("LR_No"))
                '' richa only condition to check whether lr date is in table or not
                If clsCommon.myLen(dt.Rows(0)("LR_Date")) > 0 Then
                    obj.LR_Date = clsCommon.myCstr(dt.Rows(0)("LR_Date"))
                End If

                ''
                obj.Total_Add_Charge_Insurance = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge_Insurance"))
                obj.Total_Item_Insurance_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Item_Insurance_Amt"))
                obj.Retention = clsCommon.myCdbl(dt.Rows(0)("Retention"))
                obj.Arr_ACInsurance = clsGRNAdditionChargeInsurance.GetData(obj.GRN_No, trans)
                qry = "SELECT TSPL_GRN_DETAIL.*,TSPL_LOCATION_MASTER.Location_Desc as LocationName,(case when len(isnull(TSPL_GRN_DETAIL.PO_Id,''))>0 then (select MAX(PurchaseOrder_Qty) from TSPL_PURCHASE_ORDER_DETAIL where TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_GRN_DETAIL.PO_Id and TSPL_PURCHASE_ORDER_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code and TSPL_PURCHASE_ORDER_DETAIL.Unit_code=TSPL_GRN_DETAIL.Unit_code and ISNULL(TSPL_PURCHASE_ORDER_DETAIL.MRP,0)=isnull(TSPL_GRN_DETAIL.MRP,0) and isnull(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0)=isnull(TSPL_GRN_DETAIL.Assessable,0))  else 0 end) as OriginalROQty FROM TSPL_GRN_DETAIL left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GRN_DETAIL.Location where TSPL_GRN_DETAIL.GRN_No='" + obj.GRN_No + "' ORDER BY TSPL_GRN_DETAIL.Line_No"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Arr = New List(Of clsGRNDetail)
                    Dim objTr As clsGRNDetail
                    For Each dr As DataRow In dt.Rows
                        objTr = New clsGRNDetail
                        'DONE BY STUTI ON 20/10/2016 AGAINST PURCHASE POINTS
                        objTr.Category = clsCommon.myCstr(dr("Category"))
                        objTr.Emergency = CInt(dr("Emergency"))
                        objTr.Capex_Code = clsCommon.myCstr(dr("Capex_Code"))
                        objTr.Capex_SubCode = clsCommon.myCstr(dr("Capex_SubCode"))

                        objTr.Against_Schedule_Code = clsCommon.myCstr(dr("Against_Schedule_Code"))
                        objTr.Against_RGP_No = clsCommon.myCstr(dr("Against_RGP_No"))
                        objTr.RGP_Item_Code = clsCommon.myCstr(dr("RGP_Item_Code"))
                        objTr.Requisition_Id = clsCommon.myCstr(dr("Requisition_Id"))
                        objTr.GRN_No = clsCommon.myCstr(dr("GRN_No"))
                        objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                        objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                        objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                        objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objTr.GRN_Qty = clsCommon.myCdbl(dr("GRN_Qty"))
                        objTr.Tolerence_Qty = clsCommon.myCdbl(dr("Tolerence_Qty"))
                        objTr.OriginalROQty = clsCommon.myCdbl(dr("OriginalROQty"))
                        objTr.PO_Id = clsCommon.myCstr(dr("PO_Id"))
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
                        objTr.Header_Discount_Per = clsCommon.myCdbl(dr("Header_Discount_Per"))
                        objTr.Header_Discount_Amount = clsCommon.myCdbl(dr("Header_Discount_Amount"))
                        objTr.Disc_Per = clsCommon.myCdbl(dr("Disc_Per"))
                        objTr.Detail_Discount_Amount = clsCommon.myCdbl(dr("Detail_Discount_Amount"))

                        objTr.Disc_Per_Unit = clsCommon.myCdbl(dr("Disc_Per_Unit"))
                        objTr.Disc_Amt_Per_Unit = clsCommon.myCdbl(dr("Disc_Amt_Per_Unit"))

                        objTr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                        objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amt_Less_Discount"))

                        objTr.Taxable_Amount_Per = clsCommon.myCdbl(dr("Taxable_Amount_Per"))
                        objTr.Taxable_Amount = clsCommon.myCdbl(dr("Taxable_Amount"))

                        objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                        objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                        objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                        objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                        If dr("MFG_Date") IsNot DBNull.Value Then
                            objTr.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                        End If
                        If dr("Expiry_Date") IsNot DBNull.Value Then
                            objTr.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                        End If
                        objTr.Specification = clsCommon.myCstr(dr("Specification"))
                        objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                        objTr.Assessable = clsCommon.myCdbl(dr("Assessable"))
                        objTr.AssessableAmt = clsCommon.myCdbl(dr("AssessableAmt"))
                        objTr.Leak_Qty = clsCommon.myCdbl(dr("Leak_Qty"))
                        objTr.Burst_Qty = clsCommon.myCdbl(dr("Burst_Qty"))
                        objTr.Short_Qty = clsCommon.myCdbl(dr("Short_Qty"))


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
                        objTr.Against_Item_Wise_Tax_Rate = clsCommon.myCstr(dr("Against_Item_Wise_Tax_Rate"))
                        objTr.Insurance_Base_Amt = clsCommon.myCdbl(dr("Insurance_Base_Amt"))
                        objTr.Insurance_Per = clsCommon.myCdbl(dr("Insurance_Per"))

                        objTr.Item_Insurance_Base_Amt = clsCommon.myCdbl(dr("Item_Insurance_Base_Amt"))
                        objTr.Item_Insurance_Apply_On = clsCommon.myCstr(dr("Item_Insurance_Apply_On"))
                        objTr.Item_Insurance_Rate = clsCommon.myCdbl(dr("Item_Insurance_Rate"))
                        objTr.Item_Insurance_Amt = clsCommon.myCdbl(dr("Item_Insurance_Amt"))
                        objTr.Item_Amt_After_Insurance = clsCommon.myCdbl(dr("Item_Amt_After_Insurance"))
                        obj.Arr.Add(objTr)
                    Next
                End If

            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Return PostData(strDocNo, True, trans)
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Is_Auto_Generate_MRN As Boolean = False
            Is_Auto_Generate_MRN = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGenerateMRN, clsFixedParameterCode.AutoGenerateMRN, trans)) = "1", True, False))

            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("GRN No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy  hh:mm tt")

            Dim obj As clsGRNHead = clsGRNHead.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.GRN_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (isCheckForPosted AndAlso obj.On_Hold) Then
                Throw New Exception("GRN No " + obj.GRN_No + " Is On Hold.Can't Post it")
            End If

            Dim qry As String = ""
            For Each objTr As clsGRNDetail In obj.Arr
                If clsCommon.myLen(objTr.PO_Id) > 0 Then
                    Dim qry1 As String = "update TSPL_PURCHASE_ORDER_DETAIL set Balance_Qty=Balance_Qty - " + clsCommon.myCstr(clsCommon.myCdbl(objTr.GRN_Qty + objTr.Leak_Qty + +objTr.Burst_Qty + objTr.Short_Qty)) + " where PurchaseOrder_No='" + objTr.PO_Id + "' and Item_Code='" + objTr.Item_Code + "' and  TSPL_PURCHASE_ORDER_DETAIL.Unit_code='" + objTr.Unit_code + "' and isnull(TSPL_PURCHASE_ORDER_DETAIL.MRP,0)='" + clsCommon.myCstr(objTr.MRP) + "' and isnull(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0)='" + clsCommon.myCstr(objTr.Assessable) + "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry1, trans)
                End If
            Next

            '===========if against job-work and aginst bom then in entry of raw-material at vendor stock==============
            If clsCommon.CompairString(obj.PurchaseOrder_Type, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.RGP_Type, "AB") = CompairStringResult.Equal Then
                SaveRGPBOMDetail(obj, trans)
            End If
            '================================================================================

            If Is_Auto_Generate_MRN Then
                GenerateMRN(obj, trans)
            End If

            qry = "Update TSPL_GRN_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where GRN_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If objCommonVar.InternalSMSEmailinPurchaseModule = True Then
                CreateInternalEmailSMS(obj, trans)
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GenerateMRN(ByVal obj As clsGRNHead, ByVal trans As SqlTransaction)
        Try
            Dim objMRNHead As New clsMRNHead()
            objMRNHead.isJobWorkOutward = IIf(obj.isJobWorkOutward = True, 1, 0)
            objMRNHead.MRN_Date = obj.GRN_Date
            objMRNHead.Vendor_Code = obj.Vendor_Code
            objMRNHead.Vendor_Name = obj.Vendor_Name
            objMRNHead.Ref_No = obj.Ref_No
            objMRNHead.Total_Tax_Amt = obj.Total_Tax_Amt
            objMRNHead.Remarks = obj.Remarks
            objMRNHead.Bill_To_Location = obj.Bill_To_Location
            objMRNHead.Ship_To_Location = obj.Ship_To_Location
            objMRNHead.Sublocation_Code = obj.Sublocation_Code
            objMRNHead.Comments = obj.Comments
            objMRNHead.On_Hold = obj.On_Hold
            objMRNHead.Description = obj.Description
            objMRNHead.Tax_Group = obj.Tax_Group
            objMRNHead.PurchaseOrder_Type = obj.PurchaseOrder_Type
            objMRNHead.RGP_Type = obj.RGP_Type
            objMRNHead.Retention = obj.Retention
            If objMRNHead.RoadPermit_Date IsNot Nothing AndAlso clsCommon.myLen(obj.RoadPermit_Date) > 0 AndAlso IsDate(obj.RoadPermit_Date) Then
                objMRNHead.RoadPermit_Date = clsCommon.myCDate(obj.RoadPermit_Date)
            Else
                objMRNHead.RoadPermit_Date = clsCommon.GETSERVERDATE(trans)
            End If
            objMRNHead.RoadPermit_No = obj.RoadPermit_No
            objMRNHead.InvoiceNo = obj.Invoiceno
            objMRNHead.InvoiceDate = obj.InvoiceDate
            objMRNHead.Item_Type = obj.Item_Type
            objMRNHead.Dept = obj.Dept
            objMRNHead.Dept_Desc = obj.Dept_Desc
            objMRNHead.IsCancel = 0
            objMRNHead.TAX1 = obj.TAX1
            objMRNHead.TAX1_Rate = obj.TAX1_Rate
            objMRNHead.TAX1_Base_Amt = obj.TAX1_Base_Amt
            objMRNHead.TAX1_Amt = obj.TAX1_Amt
            objMRNHead.TAX2 = obj.TAX2
            objMRNHead.TAX2_Rate = obj.TAX2_Rate
            objMRNHead.TAX2_Base_Amt = obj.TAX2_Base_Amt
            objMRNHead.TAX2_Amt = obj.TAX2_Amt
            objMRNHead.TAX3 = obj.TAX3
            objMRNHead.TAX3_Rate = obj.TAX3_Rate
            objMRNHead.TAX3_Base_Amt = obj.TAX3_Base_Amt
            objMRNHead.TAX3_Amt = obj.TAX3_Amt
            objMRNHead.TAX4 = obj.TAX4
            objMRNHead.TAX4_Rate = obj.TAX4_Rate
            objMRNHead.TAX4_Base_Amt = obj.TAX4_Base_Amt
            objMRNHead.TAX4_Amt = obj.TAX4_Amt
            objMRNHead.TAX5 = obj.TAX5
            objMRNHead.TAX5_Rate = obj.TAX5_Rate
            objMRNHead.TAX5_Base_Amt = obj.TAX5_Base_Amt
            objMRNHead.TAX5_Amt = obj.TAX5_Amt
            objMRNHead.TAX6 = obj.TAX6
            objMRNHead.TAX6_Rate = obj.TAX6_Rate
            objMRNHead.TAX6_Base_Amt = obj.TAX6_Base_Amt
            objMRNHead.TAX6_Amt = obj.TAX6_Amt
            objMRNHead.TAX7 = obj.TAX7
            objMRNHead.TAX7_Rate = obj.TAX7_Rate
            objMRNHead.TAX7_Base_Amt = obj.TAX7_Base_Amt
            objMRNHead.TAX7_Amt = obj.TAX7_Amt
            objMRNHead.TAX8 = obj.TAX8
            objMRNHead.TAX8_Rate = obj.TAX8_Rate
            objMRNHead.TAX8_Base_Amt = obj.TAX8_Base_Amt
            objMRNHead.TAX8_Amt = obj.TAX8_Amt
            objMRNHead.TAX9 = obj.TAX9
            objMRNHead.TAX9_Rate = obj.TAX9_Rate
            objMRNHead.TAX9_Base_Amt = obj.TAX9_Base_Amt
            objMRNHead.TAX9_Amt = obj.TAX9_Amt
            objMRNHead.TAX10 = obj.TAX10
            objMRNHead.TAX10_Rate = obj.TAX10_Rate
            objMRNHead.TAX10_Base_Amt = obj.TAX10_Base_Amt
            objMRNHead.TAX10_Amt = obj.TAX10_Amt
            objMRNHead.Total_Add_Charge_Insurance = obj.Total_Add_Charge_Insurance
            objMRNHead.Total_Item_Insurance_Amt = obj.Total_Item_Insurance_Amt
            objMRNHead.Tax_Calculation_Type = IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1)
            objMRNHead.Terms_Code = obj.Terms_Code
            objMRNHead.Due_Date = obj.Due_Date
            objMRNHead.Discount_Base = obj.Discount_Base
            objMRNHead.Discount_Amt = obj.Discount_Amt
            objMRNHead.Amount_Less_Discount = obj.Amount_Less_Discount
            objMRNHead.Total_Taxable_Amount = obj.Total_Taxable_Amount
            objMRNHead.MRN_Total_Amt = obj.GRN_Total_Amt
            objMRNHead.Carrier = obj.Carrier
            objMRNHead.VehicleNo = obj.VehicleNo
            objMRNHead.GRNo = obj.GRNo
            objMRNHead.GENo = obj.GENo
            If objMRNHead.GEDate.HasValue Then
                objMRNHead.GEDate = clsCommon.GetPrintDate(obj.GEDate, "dd/MMM/yyyy")
            End If
            objMRNHead.Against_PO = obj.Against_PO
            objMRNHead.Against_GRN = obj.GRN_No
            objMRNHead.Against_Schedule_Code = obj.Against_Schedule_Code
            objMRNHead.Against_RGP_No = obj.Against_RGP_No
            If clsCommon.myLen(objMRNHead.Against_RGP_No) > 0 AndAlso clsCommon.myLen(objMRNHead.Against_Schedule_Code) <= 0 Then
                objMRNHead.Against_Schedule_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_Schedule_Code from TSPL_RGP_HEAD where RGP_NO='" + objMRNHead.Against_PO + "'", trans))
            End If
            If clsCommon.myLen(objMRNHead.Against_Schedule_Code) > 0 AndAlso clsCommon.myLen(objMRNHead.Against_PO) <= 0 Then
                objMRNHead.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select po_code from TSPL_PO_SCH_HEAD where document_code='" + objMRNHead.Against_PO + "'", trans))
            End If

            If clsCommon.myLen(objMRNHead.Against_RGP_No) > 0 AndAlso clsCommon.myLen(objMRNHead.Against_Schedule_Code) <= 0 AndAlso clsCommon.myLen(objMRNHead.Against_PO) <= 0 Then
                objMRNHead.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select po_id from TSPL_RGP_HEAD where RGP_NO='" + objMRNHead.Against_PO + "'", trans))
            End If
            If clsCommon.myLen(objMRNHead.Against_PO) > 0 AndAlso clsCommon.myLen(objMRNHead.Against_Requisition) <= 0 Then
                objMRNHead.Against_Requisition = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + objMRNHead.Against_PO + "' and isnull(TSPL_PURCHASE_ORDER_HEAD.ISCANCEL,0)=0", trans))
            End If
            objMRNHead.Against_Schedule_Code = obj.Against_Schedule_Code
            objMRNHead.Against_RGP_No = obj.Against_RGP_No

            objMRNHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            objMRNHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            objMRNHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            objMRNHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            objMRNHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            objMRNHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            objMRNHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            objMRNHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            objMRNHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            objMRNHead.Add_Charge_Code4 = obj.Add_Charge_Amt3
            objMRNHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            objMRNHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            objMRNHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            objMRNHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            objMRNHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            objMRNHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            objMRNHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            objMRNHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            objMRNHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            objMRNHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            objMRNHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            objMRNHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            objMRNHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            objMRNHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            objMRNHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            objMRNHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            objMRNHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            objMRNHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            objMRNHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            objMRNHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10

            objMRNHead.Total_Add_Charge = obj.Total_Add_Charge

            objMRNHead.Arr = New List(Of clsMRNDetail)
            For Each objGRNDetail As clsGRNDetail In obj.Arr
                Dim objMRNDetail As New clsMRNDetail()
                objMRNDetail.Category = objGRNDetail.Category
                objMRNDetail.Emergency = objGRNDetail.Emergency
                objMRNDetail.Capex_Code = objGRNDetail.Capex_Code
                objMRNDetail.Capex_SubCode = objGRNDetail.Capex_SubCode

                objMRNDetail.Line_No = objGRNDetail.Line_No
                objMRNDetail.Row_Type = objGRNDetail.Row_Type
                objMRNDetail.Item_Code = objGRNDetail.Item_Code
                objMRNDetail.Item_Desc = objGRNDetail.Item_Desc
                objMRNDetail.MRN_Qty = objGRNDetail.GRN_Qty
                objMRNDetail.GRN_Id = objGRNDetail.GRN_No
                objMRNDetail.Unit_code = objGRNDetail.Unit_code
                objMRNDetail.PO_ID = objGRNDetail.PO_Id
                objMRNDetail.Requisition_Id = objGRNDetail.Requisition_Id
                objMRNDetail.RGP_No = objGRNDetail.Against_RGP_No

                objMRNDetail.Location = objGRNDetail.Location
                objMRNDetail.Item_Cost = objGRNDetail.Item_Cost
                objMRNDetail.Amount = objGRNDetail.Amount
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.Against_Requisition) > 0 Then
                    objMRNDetail.Disc_Per = objGRNDetail.Disc_Per
                Else
                    objMRNDetail.Disc_Per = objGRNDetail.Disc_Per
                End If

                objMRNDetail.Header_Discount_Per = objGRNDetail.Header_Discount_Per
                objMRNDetail.Header_Discount_Amount = objGRNDetail.Header_Discount_Amount
                objMRNDetail.Detail_Discount_Amount = objGRNDetail.Detail_Discount_Amount

                objMRNDetail.Disc_Amt = objGRNDetail.Disc_Amt
                objMRNDetail.Amt_Less_Discount = objGRNDetail.Amt_Less_Discount

                objMRNDetail.Item_Insurance_Base_Amt = objGRNDetail.Item_Insurance_Base_Amt
                objMRNDetail.Item_Insurance_Apply_On = objGRNDetail.Item_Insurance_Apply_On
                objMRNDetail.Item_Insurance_Rate = objGRNDetail.Item_Insurance_Rate
                objMRNDetail.Item_Insurance_Amt = objGRNDetail.Item_Insurance_Amt
                objMRNDetail.Item_Amt_After_Insurance = objGRNDetail.Item_Amt_After_Insurance


                objMRNDetail.Taxable_Amount = objGRNDetail.Taxable_Amount
                objMRNDetail.Taxable_Amount_Per = objGRNDetail.Taxable_Amount_Per
                objMRNDetail.TAX1 = objGRNDetail.TAX1
                objMRNDetail.TAX1_Base_Amt = objGRNDetail.TAX1_Base_Amt
                objMRNDetail.TAX1_Rate = objGRNDetail.TAX1_Rate
                objMRNDetail.TAX1_Amt = objGRNDetail.TAX1_Amt
                objMRNDetail.TAX2 = objGRNDetail.TAX2
                objMRNDetail.TAX2_Base_Amt = objGRNDetail.TAX2_Base_Amt
                objMRNDetail.TAX2_Rate = objGRNDetail.TAX2_Rate
                objMRNDetail.TAX2_Amt = objGRNDetail.TAX2_Amt
                objMRNDetail.TAX3 = objGRNDetail.TAX3
                objMRNDetail.TAX3_Base_Amt = objGRNDetail.TAX3_Base_Amt
                objMRNDetail.TAX3_Rate = objGRNDetail.TAX3_Rate
                objMRNDetail.TAX3_Amt = objGRNDetail.TAX3_Amt
                objMRNDetail.TAX4 = objGRNDetail.TAX4
                objMRNDetail.TAX4_Base_Amt = objGRNDetail.TAX4_Base_Amt
                objMRNDetail.TAX4_Rate = objGRNDetail.TAX4_Rate
                objMRNDetail.TAX4_Amt = objGRNDetail.TAX4_Amt
                objMRNDetail.TAX5 = objGRNDetail.TAX5
                objMRNDetail.TAX5_Base_Amt = objGRNDetail.TAX5_Base_Amt
                objMRNDetail.TAX5_Rate = objGRNDetail.TAX5_Rate
                objMRNDetail.TAX5_Amt = objGRNDetail.TAX5_Amt
                objMRNDetail.TAX6 = objGRNDetail.TAX6
                objMRNDetail.TAX6_Base_Amt = objGRNDetail.TAX6_Base_Amt
                objMRNDetail.TAX6_Rate = objGRNDetail.TAX6_Rate
                objMRNDetail.TAX6_Amt = objGRNDetail.TAX6_Amt
                objMRNDetail.TAX7 = objGRNDetail.TAX7
                objMRNDetail.TAX7_Base_Amt = objGRNDetail.TAX7_Base_Amt
                objMRNDetail.TAX7_Rate = objGRNDetail.TAX7_Rate
                objMRNDetail.TAX7_Amt = objGRNDetail.TAX7_Amt
                objMRNDetail.TAX8 = objGRNDetail.TAX8
                objMRNDetail.TAX8_Base_Amt = objGRNDetail.TAX8_Base_Amt
                objMRNDetail.TAX8_Rate = objGRNDetail.TAX8_Rate
                objMRNDetail.TAX8_Amt = objGRNDetail.TAX8_Amt
                objMRNDetail.TAX9 = objGRNDetail.TAX9
                objMRNDetail.TAX9_Base_Amt = objGRNDetail.TAX9_Base_Amt
                objMRNDetail.TAX9_Rate = objGRNDetail.TAX9_Rate
                objMRNDetail.TAX9_Amt = objGRNDetail.TAX9_Amt
                objMRNDetail.TAX10 = objGRNDetail.TAX10
                objMRNDetail.TAX10_Base_Amt = objGRNDetail.TAX10_Base_Amt
                objMRNDetail.TAX10_Rate = objGRNDetail.TAX10_Rate
                objMRNDetail.TAX10_Amt = objGRNDetail.TAX10_Amt
                objMRNDetail.Total_Tax_Amt = objGRNDetail.Total_Tax_Amt
                objMRNDetail.Item_Net_Amt = objGRNDetail.Item_Net_Amt
                objMRNDetail.Location = objGRNDetail.Location

                objMRNDetail.MRP = objGRNDetail.MRP
                objMRNDetail.Batch_No = objGRNDetail.Batch_No

                objMRNDetail.Specification = objGRNDetail.Specification
                objMRNDetail.Remarks = objGRNDetail.Remarks

                If clsCommon.myLen(objGRNDetail.Expiry_Date) > 0 Then
                    objMRNDetail.Expiry_Date = objGRNDetail.Expiry_Date
                End If
                If clsCommon.myLen(objGRNDetail.MFG_Date) > 0 Then
                    objMRNDetail.MFG_Date = objGRNDetail.MFG_Date
                End If
                objMRNDetail.Leak_Qty = objGRNDetail.Leak_Qty
                objMRNDetail.Burst_Qty = objGRNDetail.Burst_Qty
                objMRNDetail.Short_Qty = objGRNDetail.Short_Qty
                objMRNDetail.Balance_Qty = objGRNDetail.GRN_Qty + objGRNDetail.Leak_Qty + objGRNDetail.Burst_Qty + objGRNDetail.Short_Qty


                ''-----------------19/10/2016---------additional charge itemwise------------------------------------------
                objMRNDetail.ItemAdd_Charge_Code1 = objGRNDetail.ItemAdd_Charge_Code1
                objMRNDetail.ItemAdd_Charge_Code2 = objGRNDetail.ItemAdd_Charge_Code2
                objMRNDetail.ItemAdd_Charge_Code3 = objGRNDetail.ItemAdd_Charge_Code3
                objMRNDetail.ItemAdd_Charge_Code4 = objGRNDetail.ItemAdd_Charge_Code4
                objMRNDetail.ItemAdd_Charge_Code5 = objGRNDetail.ItemAdd_Charge_Code5
                objMRNDetail.ItemAdd_Charge_Code6 = objGRNDetail.ItemAdd_Charge_Code6
                objMRNDetail.ItemAdd_Charge_Code7 = objGRNDetail.ItemAdd_Charge_Code7
                objMRNDetail.ItemAdd_Charge_Code8 = objGRNDetail.ItemAdd_Charge_Code8
                objMRNDetail.ItemAdd_Charge_Code9 = objGRNDetail.ItemAdd_Charge_Code9
                objMRNDetail.ItemAdd_Charge_Code10 = objGRNDetail.ItemAdd_Charge_Code10
                objMRNDetail.ItemAdd_Calc_Charge_Amt1 = objGRNDetail.ItemAdd_Calc_Charge_Amt1
                objMRNDetail.ItemAdd_Calc_Charge_Amt2 = objGRNDetail.ItemAdd_Calc_Charge_Amt2
                objMRNDetail.ItemAdd_Calc_Charge_Amt3 = objGRNDetail.ItemAdd_Calc_Charge_Amt3
                objMRNDetail.ItemAdd_Calc_Charge_Amt4 = objGRNDetail.ItemAdd_Calc_Charge_Amt4
                objMRNDetail.ItemAdd_Calc_Charge_Amt5 = objGRNDetail.ItemAdd_Calc_Charge_Amt5
                objMRNDetail.ItemAdd_Calc_Charge_Amt6 = objGRNDetail.ItemAdd_Calc_Charge_Amt6
                objMRNDetail.ItemAdd_Calc_Charge_Amt7 = objGRNDetail.ItemAdd_Calc_Charge_Amt7
                objMRNDetail.ItemAdd_Calc_Charge_Amt8 = objGRNDetail.ItemAdd_Calc_Charge_Amt8
                objMRNDetail.ItemAdd_Calc_Charge_Amt9 = objGRNDetail.ItemAdd_Calc_Charge_Amt9
                objMRNDetail.ItemAdd_Calc_Charge_Amt10 = objGRNDetail.ItemAdd_Calc_Charge_Amt10
                objMRNDetail.ItemAdd_Org_Charge_Amt1 = objGRNDetail.ItemAdd_Org_Charge_Amt1
                objMRNDetail.ItemAdd_Org_Charge_Amt2 = objGRNDetail.ItemAdd_Org_Charge_Amt2
                objMRNDetail.ItemAdd_Org_Charge_Amt3 = objGRNDetail.ItemAdd_Org_Charge_Amt3
                objMRNDetail.ItemAdd_Org_Charge_Amt4 = objGRNDetail.ItemAdd_Org_Charge_Amt4
                objMRNDetail.ItemAdd_Org_Charge_Amt5 = objGRNDetail.ItemAdd_Org_Charge_Amt5
                objMRNDetail.ItemAdd_Org_Charge_Amt6 = objGRNDetail.ItemAdd_Org_Charge_Amt6
                objMRNDetail.ItemAdd_Org_Charge_Amt7 = objGRNDetail.ItemAdd_Org_Charge_Amt7
                objMRNDetail.ItemAdd_Org_Charge_Amt8 = objGRNDetail.ItemAdd_Org_Charge_Amt8
                objMRNDetail.ItemAdd_Org_Charge_Amt9 = objGRNDetail.ItemAdd_Org_Charge_Amt9
                objMRNDetail.ItemAdd_Org_Charge_Amt10 = objGRNDetail.ItemAdd_Org_Charge_Amt10
                objMRNDetail.Total_ItemAdd_Charge = objGRNDetail.Total_ItemAdd_Charge
                ''=======================================================================================
                objMRNDetail.Against_Item_Wise_Tax_Rate = objGRNDetail.Against_Item_Wise_Tax_Rate
                If clsCommon.myLen(obj.GRNo) > 0 Then
                    obj.Against_RGP_No = objGRNDetail.Against_RGP_No
                End If
                objMRNDetail.Insurance_Base_Amt = objGRNDetail.Insurance_Base_Amt
                objMRNDetail.Insurance_Per = objGRNDetail.Insurance_Per

                If (clsCommon.myLen(objMRNDetail.Item_Code) > 0) Then
                    objMRNHead.Arr.Add(objMRNDetail)
                End If

            Next

            '' CurrencConversion
            ''  If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
            objMRNHead.CURRENCY_CODE = obj.CURRENCY_CODE
            objMRNHead.ConvRate = obj.ConvRate
            If clsCommon.myLen(obj.ApplicableFrom) > 0 Then
                objMRNHead.ApplicableFrom = obj.ApplicableFrom
            Else
                objMRNHead.ApplicableFrom = Nothing
            End If

            'Else
            '    objMRNHead.CURRENCY_CODE = Nothing
            '    objMRNHead.ConvRate = 1
            '    objMRNHead.ApplicableFrom = Nothing
            'End If
            objMRNHead.Arr_ACInsurance = New List(Of clsMRNAdditionChargeInsurance)
            If objMRNHead.Arr_ACInsurance Is Nothing Then
                For Each objtr As clsGRNAdditionChargeInsurance In obj.Arr_ACInsurance
                    Dim objMRNAddInsuance As New clsMRNAdditionChargeInsurance()
                    objMRNAddInsuance.AC_Code = objtr.AC_Code
                    objMRNAddInsuance.Amount = objtr.Amount
                    If clsCommon.myLen(objtr.AC_Code) > 0 Then
                        obj.Arr_ACInsurance.Add(objtr)
                    End If
                Next
            End If

            Dim isamendment As Boolean = False
            Dim isNewEntry As Boolean = True
            If (objMRNHead.SaveData(objMRNHead, isNewEntry, trans, isamendment)) Then
                clsMRNHead.PostData(objMRNHead.MRN_No, trans)
            End If
        Catch ex As Exception

        End Try
        Return True
    End Function
    Public Shared Function DeleteMRN(ByVal strCode As String, ByVal trans As SqlTransaction)
        Try
            Dim DocNo As String = clsDBFuncationality.getSingleValue("select MRN_No from TSPL_MRN_HEAD where Against_GRN='" & strCode & "'", trans)
            If clsCommon.myLen(DocNo) > 0 Then
                clsMRNHead.ReverseAndUnpost(DocNo, trans)
                clsMRNHead.DeleteData(DocNo, trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveRGPBOMDetail(ByVal obj As clsGRNHead, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Dim dt As New DataTable()
        Try
            If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objtr As clsGRNDetail In obj.Arr
                    coll = New Hashtable()
                    dt = New DataTable()

                    dt = clsRGPHead.GetRGPTypeBOMItemsDetail(obj.RGP_Type, objtr.GRN_Qty, obj.Vendor_Code, obj.GRN_No, objtr.Item_Code, objtr.Unit_code, trans, Nothing, False)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            coll = New Hashtable()

                            clsCommon.AddColumnsForChange(coll, "RGP_No", Nothing)
                            clsCommon.AddColumnsForChange(coll, "Item_Code", clsCommon.myCstr(dr("item_code")))
                            clsCommon.AddColumnsForChange(coll, "Unit_Code", clsCommon.myCstr(dr("unit_code")))
                            clsCommon.AddColumnsForChange(coll, "Qty", clsCommon.myCstr(dr("qty")))
                            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                            clsCommon.AddColumnsForChange(coll, "InOut", "I")
                            clsCommon.AddColumnsForChange(coll, "GRN_No", obj.GRN_No)
                            clsCommon.AddColumnsForChange(coll, "SRN_No", Nothing)

                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RGP_BOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                        Next
                    End If ''dt cond.
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
            dt = Nothing
        End Try

        Return True
    End Function

    Private Shared Sub CreateInternalEmailSMS(ByVal obj As clsGRNHead, ByVal trans As SqlTransaction)
        Dim qry As String = ""
        Dim itemName As String = ""
        Dim UOM As String = ""
        Dim qty As String = ""
        Dim ItemDetail As String = ""
        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnGRN + "2" + "'", trans)

        qry = "select TSPL_USER_MASTER.User_Code from TSPL_USER_MASTER "
        If clsCommon.myLen(obj.Against_Requisition) > 0 Then
            qry += " left join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Created_By=TSPL_USER_MASTER.User_Code left join TSPL_GRN_HEAD on TSPL_GRN_HEAD.Against_Requisition=TSPL_REQUISITION_HEAD.Requisition_Id "
        Else
            qry += " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.Created_By=TSPL_USER_MASTER.User_Code left join TSPL_GRN_HEAD on TSPL_GRN_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PURCHASEORDER_no "
        End If
        qry += " where TSPL_GRN_HEAD.GRN_No='" + obj.GRN_No + "'"
        Dim StrUserCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Dim arrMobileNo As New List(Of String)
        Dim arrMailID As List(Of String) = clsERPFuncationality.ReportingMailIdandPhone(StrUserCode, arrMobileNo, trans)

        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 AndAlso ((arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Or (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0)) Then

            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                For Each objdetail As clsGRNDetail In obj.Arr
                    itemName = clsCommon.myCstr(objdetail.Item_Desc)
                    UOM = clsCommon.myCstr(objdetail.Unit_code)
                    qty = clsCommon.myCstr(objdetail.GRN_Qty)
                    ItemDetail += itemName + " " + UOM + "-" + qty + Environment.NewLine
                Next
            End If

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso (arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Then
                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.arrEMail = arrMailID

                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.GRN_No)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.GRN_Date, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.Doc_Amount, clsCommon.myCstr(obj.GRN_Total_Amt))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)
                objEmailH.SaveData(clsUserMgtCode.mbtnGRN, objEmailH, trans)
                objEmailH = Nothing

            End If

            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 AndAlso (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0) Then
                Dim objSMSH As New clsSMSHead()
                objSMSH.arrMobilNo = New List(Of String)()
                objSMSH.arrMobilNo = arrMobileNo

                objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.GRN_No)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.GRN_Date, "dd/MMM/yyyy"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.Doc_Amount, clsCommon.myCstr(obj.GRN_Total_Amt))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)
                objSMSH.SaveData(clsUserMgtCode.mbtnGRN, objSMSH, trans)
                objSMSH = Nothing
            End If
        End If
    End Sub

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsGRNHead = clsGRNHead.GetData(strCode, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GRN_No) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsGRNAdditionChargeInsurance.DeleteData(strCode, trans)
                Dim qry As String = "delete from TSPL_GRN_DETAIL where GRN_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_GRN_HEAD where GRN_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Shared Function IsValidVendorForGRN(ByVal Arr As List(Of String), ByVal strVendorCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select GRN_No,Vendor_Code,Vendor_Name from TSPL_GRN_HEAD where GRN_No in (" + clsCommon.GetMulcallString(Arr) + ") and Vendor_Code not in ('" + strVendorCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "GRN No:" + clsCommon.myCstr(dr("GRN_No")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Vendor_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Vendor_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function IsValidRGPForGRN(ByVal Arr As List(Of String), ByVal strRGPType As String) As Boolean
        If clsCommon.myLen(strRGPType) <= 0 Then
            Return True
        End If
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select GRN_No,Vendor_Code,Vendor_Name from TSPL_GRN_HEAD where GRN_No in (" + clsCommon.GetMulcallString(Arr) + ") and rgp_type not in ('" + strRGPType + "') "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "GRN No:" + clsCommon.myCstr(dr("GRN_No")) + " Is not For RGP Type: " + strRGPType
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function IsValidTaxGroupForGRN(ByVal ArrPONo As List(Of String), ByVal strTaxGroupCode As String) As Boolean
        If ArrPONo IsNot Nothing AndAlso ArrPONo.Count > 0 Then
            Dim qry As String = "select GRN_No,Tax_Group from TSPL_GRN_HEAD where GRN_No  in (" + clsCommon.GetMulcallString(ArrPONo) + ") and Tax_Group not in ('" + strTaxGroupCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "GRN No:" + clsCommon.myCstr(dr("GRN_No")) + " .Tax Group is: " + clsCommon.myCstr(dr("Tax_Group"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function IsPOQtyRecv(ByVal PONo As String, ByVal trans As SqlTransaction) As Boolean
        Dim check As Boolean
        check = True
        Try
            Dim qry As String = Nothing
            qry = "select sum(case when (TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty)<=(b.qty) then 0 else 1 end) as [check]" &
                  " from (select sum(TSPL_GRN_DETAIL.GRN_Qty) as qty,TSPL_GRN_DETAIL.Item_Code,TSPL_GRN_DETAIL.PO_Id ,max(ISNULL(TSPL_ITEM_MASTER.tolerence ,0)) AS TOLERENCE" &
                  " from TSPL_GRN_DETAIL left join tspl_item_master on tspl_item_master.Item_Code = TSPL_GRN_DETAIL.Item_Code" &
                  " where TSPL_GRN_DETAIL.PO_Id ='" + clsCommon.myCstr(PONo) + "'   group by TSPL_GRN_DETAIL.Item_Code,TSPL_GRN_DETAIL.PO_Id union all " &
                    " select 0 as qty,TSPL_PURCHASE_ORDER_DETAIL.Item_Code,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No ,max(ISNULL(TSPL_ITEM_MASTER.tolerence ,0)) AS TOLERENCE from TSPL_PURCHASE_ORDER_DETAIL left join tspl_item_master on tspl_item_master.Item_Code = TSPL_PURCHASE_ORDER_DETAIL.Item_Code " &
                    " left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.Item_Code = TSPL_PURCHASE_ORDER_DETAIL.Item_Code and TSPL_GRN_DETAIL.po_id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No ='" + clsCommon.myCstr(PONo) + "'" &
                    " and isnull(TSPL_GRN_DETAIL.Item_Code,'')='' group by TSPL_PURCHASE_ORDER_DETAIL.Item_Code,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No) as b" &
                  " left outer join 
                  (SELECT TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No
				   ,TSPL_PURCHASE_ORDER_DETAIL.Item_Code
				   ,SUM(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty) AS PurchaseOrder_Qty
				   FROM TSPL_PURCHASE_ORDER_DETAIL
				  WHERE TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No ='" + clsCommon.myCstr(PONo) + "'
				  GROUP BY TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No
				   ,TSPL_PURCHASE_ORDER_DETAIL.Item_Code) TSPL_PURCHASE_ORDER_DETAIL on b.PO_Id = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No " &
                  " and b.Item_Code = TSPL_PURCHASE_ORDER_DETAIL.Item_Code " &
                  " where TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No='" + clsCommon.myCstr(PONo) + "' group by b.Item_Code"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myCdbl(dr("check")) > 0 Then
                        check = check AndAlso False
                    Else
                        check = check AndAlso True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return check
    End Function

    Public Shared Function ToleranceQty(ByVal ItemCode As String, ByVal trans As SqlTransaction) As Double
        Dim tolerence As Double
        Try
            Dim qry As String = "select isnull(TSPL_ITEM_MASTER.Tolerence,0) as tolerence from TSPL_ITEM_MASTER WHERE TSPL_ITEM_MASTER.ITEM_CODE='" + ItemCode + "'"

            tolerence = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return tolerence
    End Function
    Public Shared Function CheckItemWeighment(ByVal ItemCode As String, ByVal trans As SqlTransaction) As Double
        Dim qry As String = " "
        Dim count As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'stuti--------------
    Public Shared Function CheckGRNUsedInSRNorMRN(ByVal strGRNNo As String, ByVal trans As SqlTransaction) As Boolean

        'Dim qry As String = " select sum(fin.[cnt])from (Select 1 as [cnt] from tspl_mrn_detail where where Grn_id='" + clsCommon.myCstr(strGRNNo) + "')fin"
        'Dim qry As String = "select sum(fin.[cnt]) from (SELECT 1 as [cnt] from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.GRN_Id ='" + clsCommon.myCstr(strGRNNo) + "' union all SELECT 1 as [cnt] from TSPL_MRN_DETAIL where TSPL_MRN_DETAIL.GRN_Id ='" + clsCommon.myCstr(strGRNNo) + "')fin"
        Dim qry As String = "  select isnull(sum(fin.[cnt]),0) from (" &
        "  Select 1 as [cnt] from TSPL_SRN_HEAD left outer join TSPL_SRN_DETAIL ON TSPL_SRN_HEAD.SRN_No =TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_DETAIL.GRN_Id ='" + clsCommon.myCstr(strGRNNo) + "'" &
        " AND TSPL_SRN_HEAD .SRN_No not  IN ( sELECT SRN_No FROM TSPL_SRN_RETURN WHERE SRN_No IN ( SELECT SRN_No FROM TSPL_SRN_DETAIL WHERE TSPL_SRN_DETAIL.GRN_Id ='" + clsCommon.myCstr(strGRNNo) + "' ) ) " &
        " union all SELECT 1 as [cnt] from TSPL_MRN_DETAIL left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No =TSPL_MRN_DETAIL.MRN_No  where TSPL_MRN_DETAIL.GRN_Id ='" + clsCommon.myCstr(strGRNNo) + "' and isnull(TSPL_MRN_HEAD.IsCancel,0) <>1 )fin "
        Dim count As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function CheckGRNUsedInPOWEIGMNETS(ByVal strGRNNo As String, ByVal trans As SqlTransaction) As Boolean


        'Dim qry As String = "select sum(fin.[cnt]) from (SELECT 1 as [cnt] from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.GRN_Id ='" + clsCommon.myCstr(strGRNNo) + "' union all SELECT 1 as [cnt] from TSPL_MRN_DETAIL where TSPL_MRN_DETAIL.GRN_Id ='" + clsCommon.myCstr(strGRNNo) + "')fin"
        Dim qry As String = "select isnull(sum(fin.[cnt]),0) from 
	(Select 1 as [cnt] from TSPL_PO_WEIGHTMENT_HEAD where TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No ='" + clsCommon.myCstr(strGRNNo) + "' )fin  "
        Dim count As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GRNCancel(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean
        'Dim iscancel As Boolean = True
        'Dim qry As String = ""

        'qry = "update TSPL_GRN_HEAD SET TSPL_GRN_HEAD.IsCancel=1 where TSPL_GRN_HEAD.GRN_No='" + strGRNNo + "'"
        'iscancel = iscancel AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        'If iscancel Then
        '    Return True
        'Else
        '    Return False
        'End If
        '''''''''''''''''''''''''''
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsGRNHead = clsGRNHead.GetData(Doc_No, NavigatorType.Current, trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.GRN_No) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            'clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_GRN_HEAD", "GRN_NO", "TSPL_GRN_DETAIL", "GRN_NO", trans)

            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)
            '' delete data from original table
            
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.GRN_No), "TSPL_GRN_HEAD", "GRN_No", "TSPL_GRN_DETAIL", "GRN_No", "TSPL_PI_REMITTANCE", "Document_No", trans)
            qry = "   delete from tspl_mrn_detail where Grn_id = '" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = " delete from TSPL_MRN_HEAD where Against_grn = '" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_GRN_DETAIL where GRN_NO='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code='" & Doc_No & "' and Program_Code='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_GRN_HEAD where GRN_NO='" & Doc_No & "'"
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

    '======end here=======

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

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Is_Auto_Generate_MRN As Boolean = False
            Is_Auto_Generate_MRN = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGenerateMRN, clsFixedParameterCode.AutoGenerateMRN, trans)) = "1", True, False))

            Dim qry As String = "select 1 from TSPL_GRN_HEAD where GRN_No='" + strCode + "' and Status=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Transaction status should be posted.")
            End If
            If Not Is_Auto_Generate_MRN Then
                qry = "select distinct MRN_No from TSPL_MRN_DETAIL where GRN_Id ='" + strCode + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry = "GRN is used in following MRN"
                    For Each dr As DataRow In dt.Rows
                        qry += Environment.NewLine + clsCommon.myCstr(dr("MRN_No"))
                    Next
                    qry += Environment.NewLine + "Can't unpost it"
                    Throw New Exception(qry)
                End If

                'PO Weighment check
                qry = "select distinct Weighment_Code from TSPL_PO_WEIGHTMENT_HEAD where TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No ='" + strCode + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry = "GRN is used in following Weighment"
                    For Each dr As DataRow In dt.Rows
                        qry += Environment.NewLine + clsCommon.myCstr(dr("Weighment_Code"))
                    Next
                    qry += Environment.NewLine + "Can't update it"
                    Throw New Exception(qry)
                End If
            End If


            'Ticket No :UDL/16/12/19-001013 By Prabhakar
            Dim AutoClosePO As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoClosePO, clsFixedParameterCode.AutoClosePO, trans)) = 1)
            If AutoClosePO Then
                qry = "select distinct PO_ID  from TSPL_MRN_DETAIL where mrn_no='" + strCode + "' and PO_ID is not null"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If clsCommon.myLen(dr("PO_ID")) > 0 Then
                            If clsGRNHead.IsPOQtyRecv(clsCommon.myCstr(dr("PO_ID")), trans) Then
                                clsPurchaseOrderHead.closepodata(trans, clsCommon.myCstr(dr("PO_ID")), True, "N")
                            End If
                        End If
                    Next
                End If
            End If
            qry = "delete from TSPL_RGP_BOM_DETAIL where grn_no='" + strCode + "' and isnull(srn_no,'')=''"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If Is_Auto_Generate_MRN Then
                DeleteMRN(strCode,trans)
            End If

            qry = "update TSPL_GRN_HEAD set Status=0,Posting_Date=null where GRN_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsGRNDetail
#Region "Variables"
    Public Requisition_Id As String = Nothing
    Public RGP_Item_Code As String = Nothing
    Public GRN_No As String = Nothing
    Public Line_No As Integer = 0
    Public Status As Integer = 0
    Public Row_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public GRN_Qty As Double = 0
    Public Tolerence_Qty As Double = 0
    Public Leak_Qty As Double = 0
    Public Burst_Qty As Double = 0
    Public Short_Qty As Double = 0
    Public OriginalROQty As Double = 0
    Public PO_Id As String = Nothing
    Public Balance_Qty As Double = 0 '
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
    Public Header_Discount_Per As Decimal = 0
    Public Header_Discount_Amount As Decimal = 0
    Public Disc_Per As Double = 0
    Public Disc_Per_Unit As Decimal = 0
    Public Disc_Amt_Per_Unit As Decimal = 0
    Public Detail_Discount_Amount As Decimal = 0
    Public Disc_Amt As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public Taxable_Amount_Per As Double = 0
    Public Taxable_Amount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public GRNTax_Group As String = Nothing 'Not a table field
    Public MRP As Double = 0
    Public MFG_Date As Date? = Nothing
    Public Batch_No As String = Nothing
    Public Expiry_Date As Date? = Nothing
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
    Public Assessable As Double = 0
    Public AssessableAmt As Double = 0

    Public Against_RGP_No As String = Nothing
    Public Against_Schedule_Code As String = Nothing

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

    Public Capex_Code As String = Nothing
    Public Capex_SubCode As String = Nothing
    Public Category As String = Nothing
    Public Emergency As Integer = Nothing
    ''==================================================================
    Public Against_Item_Wise_Tax_Rate As String = Nothing
    Public Insurance_Base_Amt As Decimal
    Public Insurance_Per As Decimal

    Public Item_Insurance_Base_Amt As Decimal = 0
    Public Item_Insurance_Apply_On As String = Nothing
    Public Item_Insurance_Rate As Decimal = 0
    Public Item_Insurance_Amt As Decimal = 0
    Public Item_Amt_After_Insurance As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsGRNDetail), ByVal trans As SqlTransaction) As Boolean
        Dim arrPO As New ArrayList
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsGRNDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "GRN_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "GRN_Qty", obj.GRN_Qty)
                clsCommon.AddColumnsForChange(coll, "Tolerence_Qty", obj.Tolerence_Qty)
                clsCommon.AddColumnsForChange(coll, "Requisition_Id", obj.Requisition_Id, True)
                clsCommon.AddColumnsForChange(coll, "Leak_Qty", obj.Leak_Qty)
                clsCommon.AddColumnsForChange(coll, "Burst_Qty", obj.Burst_Qty)
                clsCommon.AddColumnsForChange(coll, "Short_Qty", obj.Short_Qty)
                clsCommon.AddColumnsForChange(coll, "PO_Id", obj.PO_Id, True)
                If clsCommon.myLen(obj.PO_Id) > 0 Then
                    If Not arrPO.Contains(obj.PO_Id) Then
                        arrPO.Add(obj.PO_Id)
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Header_Discount_Per", obj.Header_Discount_Per)
                clsCommon.AddColumnsForChange(coll, "Header_Discount_Amount", obj.Header_Discount_Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Per", obj.Disc_Per)
                clsCommon.AddColumnsForChange(coll, "Detail_Discount_Amount", obj.Detail_Discount_Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Per_Unit", obj.Disc_Per_Unit)
                clsCommon.AddColumnsForChange(coll, "Disc_Amt_Per_Unit", obj.Disc_Amt_Per_Unit)
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
                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                If obj.MFG_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(obj.MFG_Date, "dd-MMM-yyyy"))
                End If
                If obj.Expiry_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd-MMM-yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Assessable", obj.Assessable)
                clsCommon.AddColumnsForChange(coll, "AssessableAmt", obj.AssessableAmt)
                clsCommon.AddColumnsForChange(coll, "Against_RGP_No", obj.Against_RGP_No, True)
                clsCommon.AddColumnsForChange(coll, "Against_Schedule_Code", obj.Against_Schedule_Code, True)
                'clsCommon.AddColumnsForChange(coll, "RGP_Item_Code", obj.RGP_Item_Code, True)


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
                ''done by stuti on 20/10/2016 against purchase points
                clsCommon.AddColumnsForChange(coll, "Category", obj.Category)
                clsCommon.AddColumnsForChange(coll, "Emergency", obj.Emergency)
                clsCommon.AddColumnsForChange(coll, "Capex_Code", obj.Capex_Code, True)
                clsCommon.AddColumnsForChange(coll, "Capex_SubCode", obj.Capex_SubCode, True)
                clsCommon.AddColumnsForChange(coll, "Against_Item_Wise_Tax_Rate", obj.Against_Item_Wise_Tax_Rate, True)

                clsCommon.AddColumnsForChange(coll, "Insurance_Base_Amt", obj.Insurance_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "Insurance_Per", obj.Insurance_Per)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Base_Amt", obj.Item_Insurance_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Apply_On", obj.Item_Insurance_Apply_On)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Rate", obj.Item_Insurance_Rate)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Amt", obj.Item_Insurance_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Amt_After_Insurance", obj.Item_Amt_After_Insurance)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GRN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        If arrPO IsNot Nothing AndAlso arrPO.Count > 0 Then
            Dim qry As String = "select po_id from (" + Environment.NewLine +
        "select TSPL_GRN_DETAIL.PO_id,TSPL_GRN_DETAIL.Item_Net_Amt, TSPL_PURCHASE_ORDER_HEAD.PO_AMOUNT" + Environment.NewLine +
        "from TSPL_GRN_DETAIL " + Environment.NewLine +
        "left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_DETAIL.PO_Id" + Environment.NewLine +
        "where TSPL_GRN_DETAIL.PO_Id in  (" + clsCommon.GetMulcallString(arrPO) + ") and  isnull( TSPL_PURCHASE_ORDER_HEAD.isBlanket,0)>0" + Environment.NewLine +
        ")xx group by PO_id having max(PO_AMOUNT)- sum(item_net_amt)<0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Limit exceed of Blanket Purchase Order - " + clsCommon.myCstr(dt.Rows(0)("PO_id")) + "")
            End If

            qry = " select PO_Id,Expiry_Date,GRN_Date from (" + Environment.NewLine +
            " select PO_Id,convert(date,TSPL_PURCHASE_ORDER_HEAD.Expiry_Date,103) as Expiry_Date,convert(date,TSPL_GRN_head.GRN_Date,103) as GRN_Date from TSPL_GRN_DETAIL " + Environment.NewLine +
            " left outer join TSPL_GRN_head on TSPL_GRN_head.GRN_No=TSPL_GRN_DETAIL.GRN_No" + Environment.NewLine +
            " left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_DETAIL.PO_Id" + Environment.NewLine +
            " where TSPL_GRN_DETAIL.PO_Id in  (" + clsCommon.GetMulcallString(arrPO) + ") and  isnull( TSPL_PURCHASE_ORDER_HEAD.isBlanket,0)>0" + Environment.NewLine +
            " )xx where GRN_Date >Expiry_Date"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Expired Blanket Purchase Order - " + clsCommon.myCstr(dt.Rows(0)("PO_id")) + " used.Expired On - " + clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Expiry_Date")), "dd/MMM/yyyy"))
            End If
            dt = Nothing
        End If
        arrPO = Nothing
        Return True
    End Function

    Public Shared Function GetBalanceGRNQty(ByVal strGRNCode As String, ByVal strICode As String, ByVal strCurrMRNNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal StrPOCode As String = Nothing) As Double
        Dim grncond As String = ""
        Dim mrncond As String = ""
        If StrPOCode IsNot Nothing AndAlso clsCommon.myLen(StrPOCode) > 0 Then
            grncond = " and TSPL_GRN_DETAIL.po_id='" + StrPOCode + "' "
            mrncond = " and TSPL_MRN_DETAIL.po_id='" + StrPOCode + "' "
        End If
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " &
            " select TSPL_GRN_DETAIL.Item_Code as ICode,(TSPL_GRN_DETAIL.GRN_Qty +isnull(Leak_Qty,0)+isnull(Burst_Qty,0)+ isnull(Short_Qty,0)) as Qty,1 as RI from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where TSPL_GRN_DETAIL.Status=0 and TSPL_GRN_HEAD.Status=1 and TSPL_GRN_DETAIL.GRN_No ='" + strGRNCode + "' and TSPL_GRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_GRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_GRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_GRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' " + grncond + "" &
            " union all " &
            " select TSPL_MRN_DETAIL.Item_Code as ICode,(TSPL_MRN_DETAIL.MRN_Qty +isnull(Leak_Qty,0)+isnull(Burst_Qty,0)+ isnull(Short_Qty,0) )as Qty,-1 as RI from TSPL_MRN_DETAIL left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No where TSPL_MRN_DETAIL.GRN_Id='" + strGRNCode + "'   and TSPL_MRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_MRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_MRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_MRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' and TSPL_MRN_DETAIL.MRN_No not in ('" + strCurrMRNNo + "') " + mrncond + " " &
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function CompleteGRN(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_GRN_DETAIL set Status=1 where GRN_No='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
End Class

Public Class clsGRNRGPDetail
#Region "variables"
    Public GRN_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Unit_Code As String = Nothing
    Public Item_Cost As Double = Nothing
    Public Qty As Double = Nothing
    Public Specification As String = Nothing
    Public RGP_No As String = Nothing
    Public Po_Id As String = Nothing
    Public Against_Schedule_Code As String = Nothing
#End Region

    'Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsGRNRGPDetail), ByVal trans As SqlTransaction) As Boolean
    '    Try
    '        'Dim isSaved As Boolean = True

    '        'Dim qry As String = "delete from TSPL_GRN_RGP_CONVERSION_DETAIL where GRN_No='" + strCode + "'"
    '        'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '        'If arr IsNot Nothing AndAlso arr.Count > 0 Then
    '        '    For Each obj As clsGRNRGPDetail In arr
    '        '        Dim coll As New Hashtable()

    '        '        clsCommon.AddColumnsForChange(coll, "GRN_No", obj.GRN_No, True)
    '        '        clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
    '        '        clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code, True)
    '        '        clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
    '        '        clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
    '        '        clsCommon.AddColumnsForChange(coll, "RGP_No", obj.RGP_No, True)
    '        '        clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
    '        '        clsCommon.AddColumnsForChange(coll, "Against_Schedule_Code", obj.Against_Schedule_Code)
    '        '        clsCommon.AddColumnsForChange(coll, "Po_Id", obj.Po_Id)


    '        '        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GRN_RGP_CONVERSION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
    '        '    Next
    '        'End If

    '        'Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
End Class

<Serializable()>
Public Class clsGRNAdditionChargeInsurance
#Region "Variables"
    Public TR_Code As String = Nothing
    Public GRN_No As String = Nothing
    Public AC_Code As String = Nothing
    Public AC_Name As String = Nothing ''Not a table Field
    Public Amount As Decimal
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal DocDate As DateTime, ByVal arr As List(Of clsGRNAdditionChargeInsurance), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsGRNAdditionChargeInsurance In arr
                    coll = New Hashtable()
                    objtr.TR_Code = clsERPFuncationality.GetNextCode(trans, DocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_Code", objtr.TR_Code)
                    clsCommon.AddColumnsForChange(coll, "GRN_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "AC_Code", objtr.AC_Code)
                    clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GRN_ADDITION_CHARGE_INSURANCE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal DocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "delete from TSPL_GRN_ADDITION_CHARGE_INSURANCE where GRN_No='" + DocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function

    Public Shared Function GetData(ByVal DocNo As String, ByVal trans As SqlTransaction) As List(Of clsGRNAdditionChargeInsurance)
        Dim qry As String = "select TSPL_GRN_ADDITION_CHARGE_INSURANCE.*,TSPL_Additional_Charges.Description as AC_Name  from TSPL_GRN_ADDITION_CHARGE_INSURANCE left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code=TSPL_GRN_ADDITION_CHARGE_INSURANCE.AC_Code where TSPL_GRN_ADDITION_CHARGE_INSURANCE.GRN_No='" + DocNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim Arr_ACInsurance As List(Of clsGRNAdditionChargeInsurance) = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Arr_ACInsurance = New List(Of clsGRNAdditionChargeInsurance)
            For Each dr As DataRow In dt.Rows
                Dim objtr As New clsGRNAdditionChargeInsurance()
                objtr.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                objtr.GRN_No = clsCommon.myCstr(dr("GRN_No"))
                objtr.AC_Code = clsCommon.myCstr(dr("AC_Code"))
                objtr.AC_Name = clsCommon.myCstr(dr("AC_Name"))
                objtr.Amount = clsCommon.myCstr(dr("Amount"))
                Arr_ACInsurance.Add(objtr)
            Next
        End If
        Return Arr_ACInsurance
    End Function

End Class