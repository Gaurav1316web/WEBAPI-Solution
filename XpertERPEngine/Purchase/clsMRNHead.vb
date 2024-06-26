Imports common
Imports System.Data.SqlClient
Public Class clsMRNHead

#Region "Variables"
    Public isJobWorkOutward As Integer = 0
    Public Amendment_No As Double = 0
    Public IsCancel As Integer = Nothing
    Public RGP_Type As String = Nothing
    Public Against_RGP_No As String = Nothing
    Public Against_Schedule_Code As String = Nothing
    Public PurchaseOrder_Type As String = Nothing
    Public MRN_No As String = Nothing
    Public MRN_Date As DateTime
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
    'Public RAL_Tender_No As String = Nothing
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
    Public Total_Taxable_Amount As Decimal = 0
    Public MRN_Total_Amt As Double = 0
    Public Comments As String = Nothing
    Public Comp_Code As String = Nothing
    Public Terms_Code As String = Nothing
    Public TermsName As String = Nothing
    Public Due_Date As String = Nothing
    Public Posting_Date As String = Nothing
    Public Retention As Decimal = 0
    Public Carrier As String = Nothing
    Public VehicleNo As String = Nothing
    Public GRNo As String = Nothing
    Public GENo As String = Nothing
    Public GEDate As Date? = Nothing
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public Item_Type As String = Nothing

    Public Against_Requisition As String = Nothing
    Public Against_PO As String = Nothing
    Public Against_GRN As String = Nothing
    Public Tax_Calculation_Type As EnumTaxCalucationType

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

    Public Arr As List(Of clsMRNDetail) = Nothing

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing

    'stuti
    Public RoadPermit_No As String = Nothing
    Public RoadPermit_Date As Date? = Nothing
    Public InvoiceNo As String = Nothing
    Public InvoiceDate As Date? = Nothing
    Public Sublocation_Code As String = String.Empty
    Public SubLocationName As String = String.Empty
    '===end here===

    Public Total_Item_Insurance_Amt As Decimal = 0
    Public Total_Add_Charge_Insurance As Decimal = 0
    Public Nir_QC As Integer = Nothing
    Public Arr_ACInsurance As List(Of clsMRNAdditionChargeInsurance) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsMRNHead, ByVal isNewEntry As Boolean, Optional ByVal isamendment As Boolean = False) As Boolean
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

    Public Function SaveData(ByVal obj As clsMRNHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal isamendment As Boolean = False) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Material Received Note", obj.Bill_To_Location, obj.MRN_Date, trans)
            clsMRNAdditionChargeInsurance.DeleteData(obj.MRN_No, trans)
            Dim qry As String = "delete from TSPL_MRN_DETAIL where MRN_No='" + obj.MRN_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isamendment Then
                obj.Amendment_No = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT ISNULL(Amendment_No,0) from TSPL_MRN_HEAD WHERE MRN_No='" + clsCommon.myCstr(obj.MRN_No) + "'", trans))
                isSaved = isSaved AndAlso SaveDataForHistory(obj.MRN_No, clsCommon.myCdbl(obj.Amendment_No + 1), trans)
            End If
            Dim strDocNo As String = ""
            If isNewEntry Then
                If obj.isJobWorkOutward = 1 Then
                    obj.MRN_No = clsERPFuncationality.GetNextCode(trans, obj.MRN_Date, clsDocType.MRN, clsDocTransactionType.POJobWorkOutward, obj.Sublocation_Code)
                Else
                    Dim isPODocumentTypeWise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseCounterOnTransactionType, clsFixedParameterCode.PurchaseCounterOnTransactionType, trans)) = 0, False, True) ''Make Setting Balwinder
                    If isPODocumentTypeWise Then
                        ''------------ Added by Parteek 28/03/2017 client UDL
                        Dim UDLRGPWiseDocument As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UDLRGPWiseDocument, clsFixedParameterCode.UDLRGPWiseDocument, trans)) = "1", True, False))
                        If UDLRGPWiseDocument = True Then
                            If clsCommon.myLen(obj.Against_RGP_No) > 0 Then
                                obj.MRN_No = clsERPFuncationality.GetNextCode(trans, obj.MRN_Date, clsDocType.MRN, clsDocTransactionType.RGPWise, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                            Else

                                If clsCommon.CompairString(obj.PurchaseOrder_Type, "J") = CompairStringResult.Equal Then
                                    obj.MRN_No = clsERPFuncationality.GetNextCode(trans, obj.MRN_Date, clsDocType.MRN, clsDocTransactionType.POJobWork, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                                ElseIf clsCommon.CompairString(obj.PurchaseOrder_Type, "I") = CompairStringResult.Equal Then
                                    obj.MRN_No = clsERPFuncationality.GetNextCode(trans, obj.MRN_Date, clsDocType.MRN, clsDocTransactionType.POImport, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                                ElseIf clsCommon.CompairString(obj.PurchaseOrder_Type, "L") = CompairStringResult.Equal Then
                                    obj.MRN_No = clsERPFuncationality.GetNextCode(trans, obj.MRN_Date, clsDocType.MRN, clsDocTransactionType.PODomestic, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                                Else
                                    Throw New Exception("Type is Not Correct To Generate the Transaction Code")
                                End If
                            End If

                        Else
                            If clsCommon.CompairString(obj.PurchaseOrder_Type, "J") = CompairStringResult.Equal Then
                                obj.MRN_No = clsERPFuncationality.GetNextCode(trans, obj.MRN_Date, clsDocType.MRN, clsDocTransactionType.POJobWork, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                            ElseIf clsCommon.CompairString(obj.PurchaseOrder_Type, "I") = CompairStringResult.Equal Then
                                obj.MRN_No = clsERPFuncationality.GetNextCode(trans, obj.MRN_Date, clsDocType.MRN, clsDocTransactionType.POImport, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                            ElseIf clsCommon.CompairString(obj.PurchaseOrder_Type, "L") = CompairStringResult.Equal Then
                                obj.MRN_No = clsERPFuncationality.GetNextCode(trans, obj.MRN_Date, clsDocType.MRN, clsDocTransactionType.PODomestic, IIf(clsCommon.myLen(obj.Ship_To_Location) <= 0, obj.Bill_To_Location, obj.Ship_To_Location))
                            Else
                                Throw New Exception("Type is Not Correct To Generate the Transaction Code")
                            End If
                        End If

                    Else

                        Dim TransType = clsDBFuncationality.getSingleValue("SELECT PREFIX_CODE FROM TSPL_ITEM_TYPE_MASTER WHERE ITEM_TYPE_CODE='" + obj.Item_Type + "'", trans)
                        obj.MRN_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.MRN_Date), clsDocType.MRN, TransType, obj.Bill_To_Location)
                        If clsCommon.CompairString(obj.MRN_No, String.Empty) = CompairStringResult.Equal Then
                            Throw New Exception("Item Type is Not Correct To Generate the Transaction Code")
                        End If
                    End If
                End If


            End If
            If (clsCommon.myLen(obj.MRN_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "isJobWorkOutward", obj.isJobWorkOutward)
            clsCommon.AddColumnsForChange(coll, "MRN_Date", clsCommon.GetPrintDate(obj.MRN_Date, "dd/MMM/yyyy hh:mm tt"))
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
            clsCommon.AddColumnsForChange(coll, "MRN_Total_Amt", obj.MRN_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "IsCancel", obj.IsCancel)
            clsCommon.AddColumnsForChange(coll, "Carrier", obj.Carrier)
            clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
            clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo)
            clsCommon.AddColumnsForChange(coll, "GENo", obj.GENo)
            If obj.GEDate.HasValue Then
                clsCommon.AddColumnsForChange(coll, "GEDate", clsCommon.GetPrintDate(obj.GEDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "GEDate", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
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




            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)

            clsCommon.AddColumnsForChange(coll, "Against_Requisition", obj.Against_Requisition, True)
            clsCommon.AddColumnsForChange(coll, "Against_PO", obj.Against_PO, True)
            clsCommon.AddColumnsForChange(coll, "Against_GRN", obj.Against_GRN, True)
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Type", obj.PurchaseOrder_Type)
            clsCommon.AddColumnsForChange(coll, "Against_Schedule_Code", obj.Against_Schedule_Code, True)
            clsCommon.AddColumnsForChange(coll, "Against_RGP_No", obj.Against_RGP_No, True)

            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isamendment Then
                Dim AmendmentCode As String = Nothing
                AmendmentCode = obj.MRN_No + "$" + clsCommon.myCstr(obj.Amendment_No + 1)
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

            clsCommon.AddColumnsForChange(coll, "RGP_Type", obj.RGP_Type)
            'stuti
            If obj.RoadPermit_Date IsNot Nothing AndAlso clsCommon.myLen(obj.RoadPermit_Date) > 0 AndAlso IsDate(obj.RoadPermit_Date) Then
                clsCommon.AddColumnsForChange(coll, "RoadPermit_Date", clsCommon.GetPrintDate(obj.RoadPermit_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "RoadPermit_No", obj.RoadPermit_No)
            clsCommon.AddColumnsForChange(coll, "Invoice_No", obj.InvoiceNo)
            If obj.InvoiceDate IsNot Nothing AndAlso clsCommon.myLen(obj.InvoiceDate) > 0 AndAlso IsDate(obj.InvoiceDate) Then
                clsCommon.AddColumnsForChange(coll, "Invoice_Date", clsCommon.GetPrintDate(obj.InvoiceDate, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Total_Add_Charge_Insurance", obj.Total_Add_Charge_Insurance)
            clsCommon.AddColumnsForChange(coll, "Total_Item_Insurance_Amt", obj.Total_Item_Insurance_Amt)
            clsCommon.AddColumnsForChange(coll, "Retention", obj.Retention)
            '=======end here===========
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "MRN_No", obj.MRN_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRN_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRN_HEAD", OMInsertOrUpdate.Update, "TSPL_MRN_HEAD.MRN_No='" + obj.MRN_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsMRNDetail.SaveData(obj.MRN_No, Arr, trans)
            isSaved = isSaved AndAlso clsMRNAdditionChargeInsurance.SaveData(obj.MRN_No, obj.MRN_Date, obj.Arr_ACInsurance, trans)


            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.MRN_No), "TSPL_MRN_HEAD", "MRN_No", "TSPL_MRN_DETAIL", "MRN_No", "TSPL_MRN_HEAD_History", "MRN_No", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveDataForHistory(ByVal strCode As String, ByVal intAmbandentNo As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_MRN_HEAD", trans)
            strInvColumns = "[" + strInvColumns.Replace(",", "],[") + "]"
            Dim qry As String = "INSERT INTO TSPL_MRN_HEAD_HISTORY (" + strInvColumns + ") SELECT " + strInvColumns.Replace("[Amendment_No]", "" + clsCommon.myCstr(intAmbandentNo) + "") + " FROM TSPL_MRN_HEAD WHERE MRN_NO='" + clsCommon.myCstr(strCode) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            strInvColumns = clsERPFuncationality.GetTableColumnNameForQry("TSPL_MRN_DETAIL", trans)
            strInvColumns = "[" + strInvColumns.Replace(",", "],[") + "]"
            qry = "INSERT INTO TSPL_MRN_DETAIL_HISTORY (" + strInvColumns + ") SELECT " + strInvColumns + " FROM TSPL_MRN_DETAIL WHERE MRN_NO='" + clsCommon.myCstr(strCode) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsMRNHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMRNHead
        Dim obj As clsMRNHead = Nothing
        Dim qry As String = "SELECT TSPL_MRN_HEAD.* ,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName, " &
        " TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName, " &
        " TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_LOCATION_MASTER_SubLocation.Location_Desc as SubLocationName  " &
        " FROM TSPL_MRN_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MRN_HEAD.Bill_To_Location " &
        " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_SubLocation  on TSPL_LOCATION_MASTER_SubLocation.Location_Code=TSPL_MRN_HEAD.Sublocation_Code " &
        " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_MRN_HEAD.Ship_To_Location left outer join " &
        " TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_MRN_HEAD.Tax_Group left outer join TSPL_TERMS_MASTER " &
        " on TSPL_TERMS_MASTER.Terms_Code=TSPL_MRN_HEAD.Terms_Code where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MRN_HEAD.MRN_No = (select MIN(MRN_No) from TSPL_MRN_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_MRN_HEAD.MRN_No = (select Max(MRN_No) from TSPL_MRN_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Next
                qry += " and TSPL_MRN_HEAD.MRN_No = (select Min(MRN_No) from TSPL_MRN_HEAD where MRN_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MRN_HEAD.MRN_No = (select Max(MRN_No) from TSPL_MRN_HEAD where MRN_No<'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_MRN_HEAD.MRN_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMRNHead()
            obj.MRN_No = clsCommon.myCstr(dt.Rows(0)("MRN_No"))
            obj.MRN_Date = clsCommon.myCDate(dt.Rows(0)("MRN_Date"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1 AndAlso clsCommon.myCdbl(dt.Rows(0)("iscancel")) <> 1, ERPTransactionStatus.Approved, IIf(clsCommon.myCdbl(dt.Rows(0)("iscancel")) = 1, ERPTransactionStatus.Cancel, ERPTransactionStatus.Pending))
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
            obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
            obj.Sublocation_Code = clsCommon.myCstr(dt.Rows(0)("Sublocation_Code"))
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
            obj.MRN_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("MRN_Total_Amt"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
            obj.ShipToLocationName = clsCommon.myCstr(dt.Rows(0)("ShipToLocationName"))
            obj.SubLocationName = clsCommon.myCstr(dt.Rows(0)("SubLocationName"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.TermsName = clsCommon.myCstr(dt.Rows(0)("TermsName"))
            obj.Retention = clsCommon.myCdbl(dt.Rows(0)("Retention"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            End If
            obj.Carrier = clsCommon.myCstr(dt.Rows(0)("Carrier"))
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            obj.GRNo = clsCommon.myCstr(dt.Rows(0)("GRNo"))
            obj.GENo = clsCommon.myCstr(dt.Rows(0)("GENo"))
            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
            obj.RGP_Type = clsCommon.myCstr(dt.Rows(0)("rgp_type"))
            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Against_Requisition = clsCommon.myCstr(dt.Rows(0)("Against_Requisition"))
            obj.Against_PO = clsCommon.myCstr(dt.Rows(0)("Against_PO"))
            obj.Against_GRN = clsCommon.myCstr(dt.Rows(0)("Against_GRN"))
            obj.Against_RGP_No = clsCommon.myCstr(dt.Rows(0)("Against_RGP_No"))
            obj.Against_Schedule_Code = clsCommon.myCstr(dt.Rows(0)("Against_Schedule_Code"))
            obj.PurchaseOrder_Type = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type"))
            obj.isJobWorkOutward = clsCommon.myCstr(dt.Rows(0)("isJobWorkOutward"))
            If dt.Rows(0)("GEDate") IsNot DBNull.Value Then
                obj.GEDate = clsCommon.myCDate(dt.Rows(0)("GEDate"))
            End If


            'stuti
            obj.IsCancel = CInt(dt.Rows(0)("IsCancel"))
            obj.RoadPermit_No = clsCommon.myCstr(dt.Rows(0)("RoadPermit_No"))
            If dt.Rows(0)("RoadPermit_Date") IsNot DBNull.Value Then
                obj.RoadPermit_Date = clsCommon.myCDate(dt.Rows(0)("RoadPermit_Date"))
            End If
            obj.InvoiceNo = clsCommon.myCstr(dt.Rows(0)("Invoice_No"))
            If dt.Rows(0)("Invoice_Date") IsNot DBNull.Value Then
                obj.InvoiceDate = clsCommon.myCDate(dt.Rows(0)("Invoice_Date"))
            End If
            '====end here===

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

            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            '' END CURRENCYCONVERSION 
            obj.Total_Add_Charge_Insurance = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge_Insurance"))
            obj.Total_Item_Insurance_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Item_Insurance_Amt"))
            obj.Nir_QC = clsCommon.myCdbl(dt.Rows(0)("NIR_QC"))
            Dim Sum_Max As String = String.Empty
            If clsFixedParameter.GetData(clsFixedParameterType.ShowQtySum_in_GRN_MRN_SRN, clsFixedParameterCode.ShowQtySum_in_GRN_MRN_SRN, trans) = "1" Then
                Sum_Max = "Sum"
            Else
                Sum_Max = "Max"
            End If
            qry = "SELECT TSPL_MRN_DETAIL.*,TSPL_LOCATION_MASTER.Location_Desc as LocationName,(case when len(isnull(TSPL_MRN_DETAIL.GRN_Id,''))>0 then (select " & Sum_Max & "(GRN_Qty +ISNULL(Leak_Qty,0)+isnull(Burst_Qty,0)+isnull(Short_Qty,0)) from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.GRN_No=TSPL_MRN_DETAIL.GRN_Id and isnull(tspl_grn_detail.po_id,'')=isnull(tspl_mrn_detail.po_id,'')  and TSPL_GRN_DETAIL.Item_Code=TSPL_MRN_DETAIL.Item_Code and TSPL_GRN_DETAIL.Unit_code=TSPL_MRN_DETAIL.Unit_code and TSPL_GRN_DETAIL.Item_Cost=TSPL_MRN_DETAIL.Item_Cost and isnull(TSPL_GRN_DETAIL.MRP,0) = isnull(TSPL_MRN_DETAIL.MRP,0) and isnull(TSPL_GRN_DETAIL.Assessable,0)=isnull(TSPL_MRN_DETAIL.Assessable,0))  else 0 end) as OrgGRNQty FROM TSPL_MRN_DETAIL left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MRN_DETAIL.Location where TSPL_MRN_DETAIL.MRN_No='" + obj.MRN_No + "' ORDER BY TSPL_MRN_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsMRNDetail)
                Dim objTr As clsMRNDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsMRNDetail
                    'DONE BY STUTI ON 20/10/2016 AGAINST PURCHASE POINTS
                    objTr.Category = clsCommon.myCstr(dr("Category"))
                    objTr.Emergency = CInt(dr("Emergency"))
                    objTr.Capex_Code = clsCommon.myCstr(dr("Capex_Code"))
                    objTr.Capex_SubCode = clsCommon.myCstr(dr("Capex_SubCode"))
                    objTr.Requisition_Id = clsCommon.myCstr(dr("Requisition_Id"))
                    objTr.Accept_Qty = clsCommon.myCdbl(dr("Accept_Qty"))
                    objTr.Reject_Qty = clsCommon.myCdbl(dr("Reject_Qty"))
                    objTr.MRN_No = clsCommon.myCstr(dr("MRN_No"))
                    objTr.QC_Check = IIf(clsCommon.myCdbl(dr("QC_Check")) = 1, True, False)
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.MRN_Qty = clsCommon.myCdbl(dr("MRN_Qty"))
                    objTr.OrgGRNQty = clsCommon.myCdbl(dr("OrgGRNQty"))
                    objTr.Leak_Qty = clsCommon.myCdbl(dr("Leak_Qty"))
                    objTr.Burst_Qty = clsCommon.myCdbl(dr("Burst_Qty"))
                    objTr.Short_Qty = clsCommon.myCdbl(dr("Short_Qty"))
                    objTr.Excess_Qty = clsCommon.myCdbl(dr("Excess_Qty"))
                    objTr.GRN_Id = clsCommon.myCstr(dr("GRN_Id"))
                    objTr.PO_ID = clsCommon.myCstr(dr("PO_ID"))
                    objTr.RGP_No = clsCommon.myCstr(dr("RGP_NO"))
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
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                    If dr("MFG_Date") IsNot DBNull.Value Then
                        objTr.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                    End If
                    If dr("Expiry_Date") IsNot DBNull.Value Then
                        objTr.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                    End If
                    objTr.Assessable = clsCommon.myCdbl(dr("Assessable"))
                    objTr.AssessableAmt = clsCommon.myCdbl(dr("AssessableAmt"))

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
            obj.Arr_ACInsurance = clsMRNAdditionChargeInsurance.GetData(obj.MRN_No, trans)
        End If

        Return obj
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
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("MRN No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsMRNHead = clsMRNHead.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.MRN_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (isCheckForPosted AndAlso obj.On_Hold) Then
                Throw New Exception("MRN No " + obj.MRN_No + " Is On Hold.Can't Post it")
            End If
            Dim qry As String = ""
            For Each objTr As clsMRNDetail In obj.Arr
                If clsCommon.myLen(objTr.GRN_Id) > 0 Then
                    Dim qry1 As String = "update TSPL_GRN_DETAIL set Balance_Qty=Balance_Qty - " + clsCommon.myCstr(clsCommon.myCdbl(objTr.MRN_Qty - objTr.Excess_Qty + objTr.Leak_Qty + objTr.Burst_Qty + objTr.Short_Qty)) + " where TSPL_GRN_DETAIL.GRN_No='" + objTr.GRN_Id + "' and TSPL_GRN_DETAIL.Item_Code='" + objTr.Item_Code + "' and  TSPL_GRN_DETAIL.Unit_code='" + objTr.Unit_code + "' and isnull(TSPL_GRN_DETAIL.MRP,0)='" + clsCommon.myCstr(objTr.MRP) + "' and isnull(TSPL_GRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(objTr.Assessable) + "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry1, trans)
                End If
            Next

            Dim intNIR_QC As Integer = 0
            qry = "select 1 from TSPL_MRN_DETAIL
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MRN_DETAIL.Item_Code
where TSPL_MRN_DETAIL.MRN_No='" + strDocNo + "' and ISNULL( TSPL_ITEM_MASTER.NIR_QC,0)=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                intNIR_QC = 1
            End If
            qry = "Update TSPL_MRN_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "',NIR_QC=" + clsCommon.myCstr(intNIR_QC) + " where MRN_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If objCommonVar.InternalSMSEmailinPurchaseModule = True Then
                CreateInternalEmailSMS(obj, trans)
            End If

            ''Throw New Exception("asdfasdf")
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Sub CreateInternalEmailSMS(ByVal obj As clsMRNHead, ByVal trans As SqlTransaction)
        Dim itemName As String = ""
        Dim UOM As String = ""
        Dim qty As String = ""
        Dim ItemDetail As String = ""
        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject,Notification_Text,Notification_Caption,Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnMRN + "2" + "'", trans)

        Dim qry As String = "select TSPL_USER_MASTER.User_Code from TSPL_USER_MASTER "
        If clsCommon.myLen(obj.Against_Requisition) > 0 Then
            qry += " left join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Created_By=TSPL_USER_MASTER.User_Code left join TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.Against_Requisition=TSPL_REQUISITION_HEAD.Requisition_Id "
        Else
            qry += " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.Created_By=TSPL_USER_MASTER.User_Code "
        End If
        qry += " left join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PURCHASEORDER_no "
        qry += " where TSPL_MRN_HEAD.MRN_No='" + obj.MRN_No + "'"
        Dim StrUserCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Dim arrMobileNo As New List(Of String)
        Dim arrMailID As List(Of String) = clsERPFuncationality.ReportingMailIdandPhone(StrUserCode, arrMobileNo, trans)

        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 AndAlso ((arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Or (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0) Or (clsCommon.myLen(dtContent.Rows(0)("Notification_Text")) > 0)) Then

            'Dim qry1 As String = "select TSPL_MRN_DETAIL.Unit_code as Unit_code,CAST(TSPL_MRN_DETAIL.MRN_Qty AS decimal(18,3)) as Qty,isnull(TSPL_MRN_DETAIL.item_desc,'') as item_desc "
            'qry1 += "  from TSPL_MRN_DETAIL  "
            'qry1 += "  where TSPL_MRN_DETAIL.MRN_NO='" & obj.MRN_No & "' ORDER BY TSPL_MRN_DETAIL.line_no"
            'Dim dtDocWise As DataTable = clsDBFuncationality.GetDataTable(qry1, trans)

            'For ii As Integer = 0 To dtDocWise.Rows.Count - 1
            '    itemName = clsCommon.myCstr(dtDocWise.Rows(ii)("item_desc"))
            '    UOM = clsCommon.myCstr(dtDocWise.Rows(ii)("Unit_Code"))
            '    qty = clsCommon.myCstr(dtDocWise.Rows(ii)("Qty"))

            '    ItemDetail += itemName + " " + UOM + "-" + qty + ","
            'Next

            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                For Each objdetail As clsMRNDetail In obj.Arr
                    itemName = clsCommon.myCstr(objdetail.Item_Desc)
                    UOM = clsCommon.myCstr(objdetail.Unit_code)
                    qty = clsCommon.myCstr(objdetail.MRN_Qty)
                    ItemDetail += itemName + " " + UOM + "-" + qty + Environment.NewLine
                Next
            End If

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso (arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Then
                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.arrEMail = arrMailID

                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))

                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.MRN_No)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.MRN_Date, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.Doc_Amount, clsCommon.myCstr(obj.MRN_Total_Amt))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VendorNo, clsCommon.myCstr(obj.Vendor_Code))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VendorName, clsCommon.myCstr(obj.Vendor_Name))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

                objEmailH.SaveData(clsUserMgtCode.mbtnMRN + "2", objEmailH, trans)
                objEmailH = Nothing

            End If

            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 AndAlso (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0) Then
                Dim objSMSH As New clsSMSHead()
                objSMSH.arrMobilNo = New List(Of String)()
                objSMSH.arrMobilNo = arrMobileNo

                objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.MRN_No)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.MRN_Date, "dd/MMM/yyyy"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.Doc_Amount, clsCommon.myCstr(obj.MRN_Total_Amt))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.VendorNo, clsCommon.myCstr(obj.Vendor_Code))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.VendorName, clsCommon.myCstr(obj.Vendor_Name))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

                objSMSH.SaveData(clsUserMgtCode.mbtnMRN + "2", objSMSH, trans)
                objSMSH = Nothing
            End If

            If clsCommon.myLen(dtContent.Rows(0)("Notification_Text")) > 0 Then
                Dim objNotification As New clsNotificationHead()
                objNotification.Notification_Text = clsCommon.myCstr(dtContent.Rows(0)("Notification_Text"))
                objNotification.Notification_Caption = clsCommon.myCstr(dtContent.Rows(0)("Notification_Caption"))
                objNotification.Notification_On = clsCommon.myCstr(dtContent.Rows(0)("Notification_On"))

                objNotification.Notification_Text = objNotification.Notification_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.MRN_No)
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.MRN_Date, "dd/MMM/yyyy"))
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(clsEmailSMSConstants.Doc_Amount, clsCommon.myCstr(obj.MRN_Total_Amt))
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(clsEmailSMSConstants.VendorNo, clsCommon.myCstr(obj.Vendor_Code))
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(clsEmailSMSConstants.VendorName, clsCommon.myCstr(obj.Vendor_Name))
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

                objNotification.SaveData(clsUserMgtCode.mbtnMRN + "2", objNotification, trans)
                objNotification = Nothing
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
        Dim obj As clsMRNHead = clsMRNHead.GetData(strCode, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.MRN_No) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsMRNAdditionChargeInsurance.DeleteData(obj.MRN_No, trans)
                Dim qry As String = "delete from TSPL_MRN_DETAIL where MRN_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_MRN_HEAD where MRN_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Shared Function IsValidVendorForMRN(ByVal Arr As List(Of String), ByVal strVendorCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select MRN_No,Vendor_Code,Vendor_Name from TSPL_MRN_HEAD where MRN_No in (" + clsCommon.GetMulcallString(Arr) + ") and Vendor_Code not in ('" + strVendorCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "MRN No:" + clsCommon.myCstr(dr("MRN_No")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Vendor_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Vendor_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function IsValidTaxGroupForMRN(ByVal ArrPONo As List(Of String), ByVal strTaxGroupCode As String) As Boolean
        If ArrPONo IsNot Nothing AndAlso ArrPONo.Count > 0 Then
            Dim qry As String = "select MRN_No,Tax_Group from TSPL_MRN_HEAD where MRN_No  in (" + clsCommon.GetMulcallString(ArrPONo) + ") and Tax_Group not in ('" + strTaxGroupCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "MRN No:" + clsCommon.myCstr(dr("MRN_No")) + " .Tax Group is: " + clsCommon.myCstr(dr("Tax_Group"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    '   Public Shared Function CheckMRNUsedInSRN(ByVal strMRNNo As String, ByVal trans As SqlTransaction) As Boolean
    '       ''richa agarwal 13 Dec, 2016
    '       '  Dim qry As String = "select sum(fin.[cnt]) from (SELECT 1 as [cnt] from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.MRN_Id ='" + clsCommon.myCstr(strMRNNo) + "')fin"
    '       'Dim qry As String = " select isnull(sum(fin.[cnt]),0) from (SELECT 1 as [cnt] from TSPL_SRN_DETAIL left outer join TSPL_SRN_RETURN on TSPL_SRN_RETURN.SRN_No =TSPL_SRN_DETAIL.SRN_No " & _
    '       '" where TSPL_SRN_DETAIL.MRN_Id ='" + clsCommon.myCstr(strMRNNo) + "' and TSPL_SRN_RETURN.SRN_No <>TSPL_SRN_DETAIL.SRN_No)fin "
    '       Dim qry As String = "  select isnull(sum(fin.[cnt]),0) from ( " &
    '" Select 1 as [cnt] from TSPL_SRN_HEAD left outer join TSPL_SRN_DETAIL ON TSPL_SRN_HEAD.SRN_No =TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_DETAIL.MRN_Id ='" + clsCommon.myCstr(strMRNNo) + "' " &
    '" AND TSPL_SRN_HEAD .SRN_No not  IN ( sELECT SRN_No FROM TSPL_SRN_RETURN WHERE SRN_No IN ( SELECT SRN_No FROM TSPL_SRN_DETAIL WHERE TSPL_SRN_DETAIL.MRN_Id ='" + clsCommon.myCstr(strMRNNo) + "') )) " &
    '" fin "
    '       Dim count As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

    '       If count > 0 Then
    '           Return True
    '       Else
    '           Return False
    '       End If
    '   End Function
    Public Shared Function CheckMRNUsedInNIRQC(ByVal strMRNNo As String, ByVal trans As SqlTransaction) As Boolean
        ''richa agarwal 13 Dec, 2016
        '  Dim qry As String = "select sum(fin.[cnt]) from (SELECT 1 as [cnt] from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.MRN_Id ='" + clsCommon.myCstr(strMRNNo) + "')fin"
        'Dim qry As String = " select isnull(sum(fin.[cnt]),0) from (SELECT 1 as [cnt] from TSPL_SRN_DETAIL left outer join TSPL_SRN_RETURN on TSPL_SRN_RETURN.SRN_No =TSPL_SRN_DETAIL.SRN_No " & _
        '" where TSPL_SRN_DETAIL.MRN_Id ='" + clsCommon.myCstr(strMRNNo) + "' and TSPL_SRN_RETURN.SRN_No <>TSPL_SRN_DETAIL.SRN_No)fin "
        '       Dim qry As String = "  select isnull(sum(fin.[cnt]),0) from ( " &
        '" Select 1 as [cnt] from TSPL_SRN_HEAD left outer join TSPL_SRN_DETAIL ON TSPL_SRN_HEAD.SRN_No =TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_DETAIL.MRN_Id ='" + clsCommon.myCstr(strMRNNo) + "' " &
        '" AND TSPL_SRN_HEAD .SRN_No not  IN ( sELECT SRN_No FROM TSPL_SRN_RETURN WHERE SRN_No IN ( SELECT SRN_No FROM TSPL_SRN_DETAIL WHERE TSPL_SRN_DETAIL.MRN_Id ='" + clsCommon.myCstr(strMRNNo) + "') )) " &
        '" fin "
        Dim qry As String = " select isnull(sum(fin.[cnt]),0) from (  Select 1 as [cnt] from TSPL_NIR_QC where TSPL_NIR_QC.MRN_No ='" + clsCommon.myCstr(strMRNNo) + "'  )fin "

        Dim count As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function CheckMRNUsedInWITQC(ByVal strMRNNo As String, ByVal trans As SqlTransaction) As Boolean
        ''richa agarwal 13 Dec, 2016
        '  Dim qry As String = "select sum(fin.[cnt]) from (SELECT 1 as [cnt] from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.MRN_Id ='" + clsCommon.myCstr(strMRNNo) + "')fin"
        'Dim qry As String = " select isnull(sum(fin.[cnt]),0) from (SELECT 1 as [cnt] from TSPL_SRN_DETAIL left outer join TSPL_SRN_RETURN on TSPL_SRN_RETURN.SRN_No =TSPL_SRN_DETAIL.SRN_No " & _
        '" where TSPL_SRN_DETAIL.MRN_Id ='" + clsCommon.myCstr(strMRNNo) + "' and TSPL_SRN_RETURN.SRN_No <>TSPL_SRN_DETAIL.SRN_No)fin "
        '       Dim qry As String = "  select isnull(sum(fin.[cnt]),0) from ( " &
        '" Select 1 as [cnt] from TSPL_SRN_HEAD left outer join TSPL_SRN_DETAIL ON TSPL_SRN_HEAD.SRN_No =TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_DETAIL.MRN_Id ='" + clsCommon.myCstr(strMRNNo) + "' " &
        '" AND TSPL_SRN_HEAD .SRN_No not  IN ( sELECT SRN_No FROM TSPL_SRN_RETURN WHERE SRN_No IN ( SELECT SRN_No FROM TSPL_SRN_DETAIL WHERE TSPL_SRN_DETAIL.MRN_Id ='" + clsCommon.myCstr(strMRNNo) + "') )) " &
        '" fin "
        Dim qry As String = "  Select isnull(sum(fin.[cnt]),0) from (  Select 1 As [cnt] from TSPL_NIR_QC where TSPL_NIR_QC.MRN_No ='" + clsCommon.myCstr(strMRNNo) + "'  )fin "
        Dim count As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function MRNCancel(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean
        'Dim iscancel As Boolean = True
        'Dim qry As String = ""
        'qry = "update TSPL_MRN_HEAD SET TSPL_MRN_HEAD.IsCancel=1 where TSPL_MRN_HEAD.MRN_No='" + strMRNNo + "'"
        'iscancel = iscancel AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        'If iscancel Then
        '    Return True
        'Else
        '    Return False
        'End If
        ''''''''''''''''''''''''''''
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsMRNHead = clsMRNHead.GetData(Doc_No, NavigatorType.Current, trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.MRN_No) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_MRN_HEAD", "MRN_NO", "TSPL_MRN_DETAIL", "MRN_NO", trans)
            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)
            '' delete data from original table
            qry = "delete from TSPL_MRN_DETAIL where MRN_NO='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code='" & Doc_No & "' and Program_Code='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MRN_HEAD where MRN_NO='" & Doc_No & "'"
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
            Dim obj As clsMRNHead = clsMRNHead.GetData(strCode, NavigatorType.Current, trans)
            Dim qry As String = "select 1 from TSPL_MRN_HEAD where MRN_No='" + strCode + "' and Status=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Transaction status should be posted.")
            End If
            qry = "select distinct SRN_No from TSPL_SRN_DETAIL where MRN_Id ='" + strCode + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "MRN is used in following SRN"
                For Each dr As DataRow In dt.Rows
                    qry += Environment.NewLine + clsCommon.myCstr(dr("SRN_No"))
                Next
                qry += Environment.NewLine + "Can't unpost it"
                Throw New Exception(qry)
            End If

            If (obj.Nir_QC = 0) Then
                qry = "Select distinct(Document_Code) from TSPL_QC_CHECK_DETAIL where MRN_No='" + strCode + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry = "MRN is used in following WETQC"
                    Throw New Exception(qry)
                End If
            End If
            If (obj.Nir_QC = 1) Then
                qry = "Select distinct(Document_No) from TSPL_NIR_QC where MRN_No='" + strCode + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry = "MRN is used in following NIRQC"
                    Throw New Exception(qry)
                End If
            End If
            'If (obj.Nir_QC = IsDBNull(0)) Then
            '    qry = "Select distinct(Document_Code) from TSPL_QC_CHECK_DETAIL where MRN_No='" + strCode + "'"
            '    dt = clsDBFuncationality.GetDataTable(qry, trans)
            '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '        qry = "MRN is used in following WETQC"
            '        Throw New Exception(qry)
            '    End If
            'End If


            qry = "update TSPL_MRN_HEAD set Status=0,Posting_Date=null where MRN_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsMRNDetail
#Region "Variables"
    Public Requisition_Id As String = Nothing
    Public MRN_No As String = Nothing
    Public QC_Check As Boolean = True
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public Status As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public MRN_Qty As Double = 0
    Public Leak_Qty As Double = 0
    Public Burst_Qty As Double = 0
    Public Short_Qty As Double = 0
    Public Excess_Qty As Double = 0
    Public GRN_Id As String = Nothing
    Public PO_ID As String = Nothing
    Public RGP_No As String = Nothing
    Public Balance_Qty As Double = 0 '
    Public OrgGRNQty As Double = 0 '
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
    Public Detail_Discount_Amount As Decimal = 0
    Public Disc_Per_Unit As Decimal = 0
    Public Disc_Amt_Per_Unit As Decimal = 0
    Public Disc_Amt As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public Taxable_Amount_Per As Decimal = 0
    Public Taxable_Amount As Decimal = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public MRNTax_Group As String = Nothing
    Public MRP As Double = 0
    Public MFG_Date As Date? = Nothing
    Public Batch_No As String = Nothing
    Public Expiry_Date As Date? = Nothing
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
    Public Assessable As Double = 0
    Public AssessableAmt As Double = 0
    Public Accept_Qty As Double = 0
    Public Reject_Qty As Double = 0

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

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMRNDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMRNDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Accept_Qty", obj.Accept_Qty)
                clsCommon.AddColumnsForChange(coll, "Reject_Qty", obj.Reject_Qty)
                clsCommon.AddColumnsForChange(coll, "QC_Check", IIf(obj.QC_Check, 1, 0))
                clsCommon.AddColumnsForChange(coll, "MRN_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Requisition_Id", obj.Requisition_Id, True)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "MRN_Qty", obj.MRN_Qty)
                clsCommon.AddColumnsForChange(coll, "Leak_Qty", obj.Leak_Qty)
                clsCommon.AddColumnsForChange(coll, "Burst_Qty", obj.Burst_Qty)
                clsCommon.AddColumnsForChange(coll, "Short_Qty", obj.Short_Qty)
                clsCommon.AddColumnsForChange(coll, "Excess_Qty", obj.Excess_Qty)
                clsCommon.AddColumnsForChange(coll, "GRN_Id", obj.GRN_Id, True)
                clsCommon.AddColumnsForChange(coll, "PO_ID", obj.PO_ID, True)
                clsCommon.AddColumnsForChange(coll, "RGP_No", obj.RGP_No, True)
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
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)

                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Assessable", obj.Assessable)
                clsCommon.AddColumnsForChange(coll, "AssessableAmt", obj.AssessableAmt)
                If obj.MFG_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(obj.MFG_Date, "dd-MMM-yyyy"))
                End If
                If obj.Expiry_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd-MMM-yyyy"))
                End If


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

                clsCommon.AddColumnsForChange(coll, "Insurance_Per", obj.Insurance_Per)
                clsCommon.AddColumnsForChange(coll, "Insurance_Base_Amt", obj.Insurance_Base_Amt)

                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Base_Amt", obj.Item_Insurance_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Apply_On", obj.Item_Insurance_Apply_On)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Rate", obj.Item_Insurance_Rate)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Amt", obj.Item_Insurance_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Amt_After_Insurance", obj.Item_Amt_After_Insurance)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceMRNQty(ByVal strMRNCode As String, ByVal strICode As String, ByVal strCurrSRNNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " &
            " select TSPL_MRN_DETAIL.Item_Code as ICode,(TSPL_MRN_DETAIL.MRN_Qty +isnull(TSPL_MRN_DETAIL.Leak_Qty,0)+isnull(TSPL_MRN_DETAIL.Burst_Qty,0)+isnull(TSPL_MRN_DETAIL.Short_Qty,0)) as Qty,1 as RI from TSPL_MRN_DETAIL left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No where TSPL_MRN_DETAIL.Status=0 and TSPL_MRN_HEAD.Status=1 and TSPL_MRN_DETAIL.MRN_No ='" + strMRNCode + "' and TSPL_MRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_MRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_MRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_MRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' " &
            " union all " &
            " select TSPL_SRN_DETAIL.Item_Code as ICode,(TSPL_SRN_DETAIL.SRN_Qty+isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0)+isnull(TSPL_SRN_DETAIL.Short_Qty,0)+ISNULL(Rejected_Qty,0)) as Qty,-1 as RI from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.MRN_Id='" + strMRNCode + "'   and TSPL_SRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_SRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_SRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_SRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'  and TSPL_SRN_DETAIL.SRN_No not in ('" + strCurrSRNNo + "')  " &
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompleteMRN(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_MRN_DETAIL set Status=1 where MRN_No='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
End Class

<Serializable()>
Public Class clsMRNAdditionChargeInsurance
#Region "Variables"
    Public TR_Code As String = Nothing
    Public MRN_No As String = Nothing
    Public AC_Code As String = Nothing
    Public AC_Name As String = Nothing ''Not a table Field
    Public Amount As Decimal
#End Region
    Public Shared Function SaveData(ByVal DocNo As String, ByVal DocDate As DateTime, ByVal arr As List(Of clsMRNAdditionChargeInsurance), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsMRNAdditionChargeInsurance In arr
                    coll = New Hashtable()
                    objtr.TR_Code = clsERPFuncationality.GetNextCode(trans, DocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_Code", objtr.TR_Code)
                    clsCommon.AddColumnsForChange(coll, "MRN_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "AC_Code", objtr.AC_Code)
                    clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRN_ADDITION_CHARGE_INSURANCE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal DocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "delete from TSPL_MRN_ADDITION_CHARGE_INSURANCE where MRN_No='" + DocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function
    Public Shared Function GetData(ByVal DocNo As String, ByVal trans As SqlTransaction) As List(Of clsMRNAdditionChargeInsurance)
        Dim qry As String = "select TSPL_MRN_ADDITION_CHARGE_INSURANCE.*,TSPL_Additional_Charges.Description as AC_Name  from TSPL_MRN_ADDITION_CHARGE_INSURANCE left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code=TSPL_MRN_ADDITION_CHARGE_INSURANCE.AC_Code where TSPL_MRN_ADDITION_CHARGE_INSURANCE.MRN_No='" + DocNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim Arr_ACInsurance As List(Of clsMRNAdditionChargeInsurance) = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Arr_ACInsurance = New List(Of clsMRNAdditionChargeInsurance)
            For Each dr As DataRow In dt.Rows
                Dim objtr As New clsMRNAdditionChargeInsurance()
                objtr.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                objtr.MRN_No = clsCommon.myCstr(dr("MRN_No"))
                objtr.AC_Code = clsCommon.myCstr(dr("AC_Code"))
                objtr.AC_Name = clsCommon.myCstr(dr("AC_Name"))
                objtr.Amount = clsCommon.myCstr(dr("Amount"))
                Arr_ACInsurance.Add(objtr)
            Next
        End If
        Return Arr_ACInsurance
    End Function
End Class