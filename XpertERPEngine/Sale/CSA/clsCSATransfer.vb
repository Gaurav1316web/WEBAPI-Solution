Imports common
Imports System.Data.SqlClient
Public Class clsCSATransfer
#Region "Variables"
    Public Freight_Type As String = Nothing
    Public EmptyCharge As Decimal = Nothing
    Public FixedCharge As Decimal = Nothing
    Public Removal_Date As DateTime? = Nothing
    Public Secondary_Doc_Code As String = Nothing
    Public Transporter_Name_Manual As String = Nothing
    Public Ship_To_Location As String = Nothing
    Public Ship_To_Location_Desc As String = Nothing
    Public Excisable As String = Nothing
    Public Vehicle_Charge As Decimal = Nothing
    Public Vehicle_Capacity As Decimal = Nothing
    Public Total_Item_Wt As Decimal = Nothing
    Public Gross_Item_Wt As Decimal = Nothing
    Public GR_No As String = Nothing
    Public GR_Date As Date = Nothing
    Public Transport_Id As String = Nothing
    Public Against_F As String = Nothing
    Public Vehicle_code As String = ""
    Public Transport_Desc As String = ""
    Public Waybill_No As String = Nothing
    Public Waybill_Date As Date = Nothing
    Public DELEVERY_ORDER_NO As String = Nothing
    Public Document_Type As String = Nothing
    Public Approvel_Required As Double = 0
    Public Is_Approved As Double = 0
    Public EWayBillNo As String = Nothing
    Public EWayBillDate As Date?
    Public Electronic_Ref_No As String = Nothing
    Public Approved_Date As Date = Nothing
    Public Approved_By As String = Nothing
    Public DOC_CODE As String = Nothing
    Public Transfer_Date As DateTime = Nothing
    Public Inculding_Tax As String = Nothing
    Public Cust_Code As String = Nothing
    Public Customer_Name As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending

    Public CancelFlag As Boolean = Nothing

    Public State_Code As String = Nothing
    Public Description As String = Nothing

    Public From_Location_Code As String = Nothing
    Public From_Location_Name As String = Nothing
    Public To_Location_Code As String = Nothing
    Public To_Location_Name As String = Nothing
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

    Public Document_Amount As Double = 0
    Public CSA_Rate As Double = 0

    Public Comp_Code As String = Nothing

    Public Posting_Date As DateTime? = Nothing

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

    Public Total_Commission_Chrage As Decimal = 0
    Public Item_Tax_Type As Integer = 0
    Public Modify_By As String = Nothing
    Public Modify_Date As DateTime = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As DateTime = Nothing
    Public Arr As List(Of clsCSATransferDetail) = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing

#End Region
    ''Note Very Important If any change mad in SO Head or SO Detail table allso update it's History table.
    ''Richa on 16/06/2020
    ''Checkout Richa on 17/06/2020
    Public Shared Function GetTransferDescrptn() As String
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 description from TSPL_CSA_TRANSFER_HEAD where isnull(description,'')<>'' order by transfer_date desc"))

        Return str
    End Function
    Public Function SaveData(ByVal obj As clsCSATransfer, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSATransfer, obj.From_Location_Code, obj.Transfer_Date, trans)
            Dim qry As String = "delete from TSPL_CSA_TRANSFER_DETAIL where DOC_CODE='" + obj.DOC_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsBatchInventory.DeleteData("SD-CSATRANS", obj.DOC_CODE, trans)


            Dim strDocNo As String = ""
            If isNewEntry Then

                Dim GSTStatus As Boolean = clsERPFuncationality.GetGSTStatus(obj.Transfer_Date)
                If GSTStatus Then
                    'If GST On
                    Dim strTaxType As String = clsCommon.myCstr(clsLocationWiseTax.TaxType(obj.From_Location_Code, obj.Cust_Code, "S", obj.Transfer_Date, trans))
                    If clsCommon.CompairString(strTaxType, "I") = CompairStringResult.Equal Then
                        obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.CSATransfer, clsDocTransactionType.GSTTaxable, obj.From_Location_Code)
                    Else
                        obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.CSATransfer, clsDocTransactionType.GSTNonTaxable, obj.From_Location_Code)
                    End If

                Else

                    Dim strExcise As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location_Code + "'", trans)) = "T", True, False)
                    If obj.Item_Tax_Type = 2 AndAlso strExcise = True Then
                        'Dim strExcise As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + obj.From_Location_Code + "'", trans)) = "T", True, False)
                        'If strExcise = False Then
                        '    Throw New Exception("Both Location and Item should be excisable.")
                        'End If
                        obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceExcise, obj.From_Location_Code)
                    ElseIf clsCommon.CompairString(obj.Excisable, "1") = CompairStringResult.Equal Then 'in excisable case product sale,mcc sale and csa transfer series should be in continue manner
                        ''changes by richa agarwal Sale Invoice Series against ticket no BM00000005919 on 18/03/2015
                        obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Transfer_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceTax, obj.From_Location_Code)
                        'obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Transfer_Date), clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceExcise, obj.From_Location_Code)
                        obj.Document_Type = "Tax Invoice"
                    Else
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TransferWithProductionSale_Retail_Series, obj.From_Location_Code, trans)) = 10 Then
                            ''changes by richa agarwal Sale Invoice Series against ticket no BM00000005919 on 18/03/2015
                            obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceRetail, obj.From_Location_Code)
                            '-----Production SaleINvoice Series(Retail)
                            ' obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceRetail, obj.From_Location_Code)
                            obj.Document_Type = "Retail Invoice"
                        ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TransferWithProductionSale_Retail_Series, obj.From_Location_Code, trans)) = 1 Then
                            '-----Stock Transfer Series
                            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TransferLocalInterState, clsFixedParameterType.TransferLocalInterState, trans)) = 1 Then
                            '    If clsDBFuncationality.getSingleValue("Select Case When (select State from TSPL_LOCATION_MASTER WHERE Location_Code='" & obj.From_Location_Code & "')=(Select State from TSPL_CUSTOMER_MASTER Where Cust_Code='" & obj.Cust_Code & "') Then 'True' Else 'False' End", trans) = True Then
                            '        obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.TransferDCC, clsDocTransactionType.TransferLocalOut, obj.From_Location_Code)
                            '        obj.Document_Type = "Stock Transfer (Local)"
                            '    Else
                            '        obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.TransferDCC, clsDocTransactionType.TransferInterStateOut, obj.From_Location_Code)
                            '        obj.Document_Type = "Stock Transfer (InterState)"
                            '    End If
                            'Else
                            obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.TransferDCC, clsDocTransactionType.TransferOut, obj.From_Location_Code)
                            obj.Document_Type = "Stock Transfer"
                            'End If
                        Else
                            '-----CSA Transfer Series
                            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TransferLocalInterState, clsFixedParameterType.TransferLocalInterState, trans)) = 1 Then
                            '    If clsDBFuncationality.getSingleValue("Select Case When (select State from TSPL_LOCATION_MASTER WHERE Location_Code='" & obj.From_Location_Code & "')=(Select State from TSPL_CUSTOMER_MASTER Where Cust_Code='" & obj.Cust_Code & "') Then 'True' Else 'False' End", trans) = True Then
                            '        obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Transfer_Date), clsDocType.CSATransfer, clsDocTransactionType.CSATransferLocal, obj.From_Location_Code)
                            '        obj.Document_Type = "CSA Transfer (Local)"
                            '    Else
                            '        obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Transfer_Date), clsDocType.CSATransfer, clsDocTransactionType.CSATransferInterState, obj.From_Location_Code)
                            '        obj.Document_Type = "CSA Transfer (InterState)"
                            '    End If
                            'Else
                            obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Transfer_Date), clsDocType.CSATransfer, clsDocTransactionType.NA, obj.From_Location_Code)
                            obj.Document_Type = "CSA Transfer"
                            'End If
                        End If
                    End If

                    ''================================excise second series=========================
                    If obj.Item_Tax_Type = 2 AndAlso strExcise = True AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ForUDLOnly, clsFixedParameterCode.ForUDLOnly, trans)) = 1 Then
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TransferWithProductionSale_Retail_Series, obj.From_Location_Code, trans)) = 10 Then
                            obj.Secondary_Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceRetail, obj.From_Location_Code)
                        ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TransferWithProductionSale_Retail_Series, obj.From_Location_Code, trans)) = 1 Then
                            obj.Secondary_Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Transfer_Date, clsDocType.TransferDCC, clsDocTransactionType.TransferOut, obj.From_Location_Code)
                        Else
                            obj.Secondary_Doc_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Transfer_Date), clsDocType.CSATransfer, clsDocTransactionType.NA, obj.From_Location_Code)
                        End If
                    End If

                End If
                ''==========================================================

            End If
            If (clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Secondary_Doc_Code", obj.Secondary_Doc_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Tax_Type", obj.Item_Tax_Type)
            clsCommon.AddColumnsForChange(coll, "DELEVERY_ORDER_NO", obj.DELEVERY_ORDER_NO)
            clsCommon.AddColumnsForChange(coll, "Transfer_Date", clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MMM/yyyy hh:mm tt"))

            clsCommon.AddColumnsForChange(coll, "Inculding_Tax", obj.Inculding_Tax)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "CSA_Rate", obj.CSA_Rate)
            ''richa agarwal 29 Nov,2016
            If clsCommon.myLen(obj.Removal_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Removal_Date", clsCommon.GetPrintDate(obj.Removal_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Removal_Date", Nothing, True)
            End If
            ''--------------------
            clsCommon.AddColumnsForChange(coll, "State_Code", obj.State_Code)

            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "From_Location_Code", obj.From_Location_Code)
            clsCommon.AddColumnsForChange(coll, "To_Location_Code", obj.To_Location_Code)
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

            clsCommon.AddColumnsForChange(coll, "Document_Amount", obj.Document_Amount)

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
            clsCommon.AddColumnsForChange(coll, "Total_Commission_Chrage", obj.Total_Commission_Chrage)


            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", obj.ApplicableFrom, True)
            '' End currencyconversion

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "WayBill_No", obj.Waybill_No)
            clsCommon.AddColumnsForChange(coll, "WayBill_Date", clsCommon.GetPrintDate(obj.Waybill_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Id", obj.Vehicle_code, True)
            clsCommon.AddColumnsForChange(coll, "Transport_Id", obj.Transport_Id)
            clsCommon.AddColumnsForChange(coll, "Against_Form", obj.Against_F)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Capacity", obj.Vehicle_Capacity)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Charge", obj.Vehicle_Charge)
            clsCommon.AddColumnsForChange(coll, "Total_Item_Wt", obj.Total_Item_Wt)
            clsCommon.AddColumnsForChange(coll, "Gross_Item_Wt", obj.Gross_Item_Wt)
            clsCommon.AddColumnsForChange(coll, "GR_No", obj.GR_No)
            clsCommon.AddColumnsForChange(coll, "GR_Date", clsCommon.GetPrintDate(obj.GR_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Excisable", obj.Excisable)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Location", obj.Ship_To_Location)
            clsCommon.AddColumnsForChange(coll, "Transporter_Name_Manual", obj.Transporter_Name_Manual)


            clsCommon.AddColumnsForChange(coll, "FixedCharge", obj.FixedCharge)
            clsCommon.AddColumnsForChange(coll, "EmptyCharge", obj.EmptyCharge)
            clsCommon.AddColumnsForChange(coll, "Freight_Type", obj.Freight_Type)
            clsCommon.AddColumnsForChange(coll, "Electronic_Ref_No", obj.Electronic_Ref_No)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", obj.DOC_CODE)
                clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_TRANSFER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_TRANSFER_HEAD", OMInsertOrUpdate.Update, "TSPL_CSA_TRANSFER_HEAD.DOC_CODE='" + obj.DOC_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsCSATransferDetail.SaveData(obj.DOC_CODE, obj.Transfer_Date, obj.From_Location_Code, obj.To_Location_Code, Arr, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.DOC_CODE, obj.arrCustomFields, trans)

            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "DOC_CODE", obj.DOC_CODE, "TSPL_CSA_TRANSFER_HEAD", trans)

            'qry = "update TSPL_VEHICLE_MASTER set InOut='O' where vehicle_id='" + obj.Vehicle_code + "'"
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetProvisionCharge(ByVal Loc_Code As String, ByVal Cust_Code As String, ByVal gross_wt As Decimal, ByVal Capacity As Decimal, Optional ByVal Transport_Id As String = Nothing) As DataTable
        Dim value As Decimal = 0
        Dim Ret_Dt As New DataTable()
        Ret_Dt.Columns.Add("FixedCharge", GetType(Decimal))
        Ret_Dt.Columns.Add("EmptyCharge", GetType(Decimal))
        Ret_Dt.Columns.Add("FreightCharge", GetType(Decimal))
        Ret_Dt.Columns.Add("FreightType", GetType(String))
        Dim dr As DataRow = Nothing

        Dim city As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city_code from tspl_customer_master where cust_code='" + Cust_Code + "'"))

        Dim qry As String = "select top 1 capacitymt,freight,Fixed,Type from TSPL_ROUTE_FREIGHT_DETAILS where location_code='" + Loc_Code + "' and city_code='" + city + "' and capacitymt='" + clsCommon.myCstr(Capacity) + "' and transport_id='" + Transport_Id + "' and TransType='P' order by effective_date desc"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim capacitymt As Decimal = clsCommon.myCdbl(dt.Rows(0)("capacitymt"))
            Dim charge As Decimal = clsCommon.myCdbl(dt.Rows(0)("freight"))
            Dim Fixed As Decimal = clsCommon.myCdbl(dt.Rows(0)("Fixed"))
            Dim Freight_Type As String = clsCommon.myCstr(dt.Rows(0)("Type"))
            Dim FixedCharge As Double = Fixed
            Dim EmptyCharge As Double = charge

            'DONE FOR mt TO kg CONVERSION
            Dim Weight_MT_Unit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_FIXED_PARAMETER where code='" + clsFixedParameterCode.VehicleCapacityUnit + "' and type='" + clsFixedParameterType.VehicleCapacityUnit + "'"))
            Dim Gross_Weight_Unit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_FIXED_PARAMETER where code='" + clsFixedParameterCode.GrossWeightUnit + "' and type='" + clsFixedParameterType.GrossWeightUnit + "'"))
            qry = "select top 1 CF from (select (case when (Container_UOM='" & Gross_Weight_Unit & "' and Contained_UOM='" & Weight_MT_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_MT_Unit & "' and Contained_UOM='" & Gross_Weight_Unit & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
            Dim gross_uom_cnvrsn As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.CompairString(Weight_MT_Unit, Gross_Weight_Unit) = CompairStringResult.Equal Then
                gross_uom_cnvrsn = 1
            End If
            If gross_uom_cnvrsn <= 0 Then
                Throw New Exception("Provide weight conversion of unit [" + Gross_Weight_Unit + "] to [" + Weight_MT_Unit + "] at Weight conversion screen.")
            End If
            gross_wt = gross_wt * gross_uom_cnvrsn
            ''====================================================

            If gross_wt > capacitymt Then
                If charge > 0 Then
                    value = System.Math.Round((charge / capacitymt) * gross_wt, 2)
                Else
                    value = System.Math.Round(Fixed, 2)
                End If
            ElseIf gross_wt <= capacitymt Then
                value = charge + Fixed
            End If

            dr = Ret_Dt.NewRow()
            dr("FreightType") = Freight_Type
            dr("FixedCharge") = FixedCharge
            dr("FreightCharge") = value
            dr("EmptyCharge") = EmptyCharge
            Ret_Dt.Rows.Add(dr)
        End If

        Return Ret_Dt
    End Function
    Private Shared Function ConvertProvision(ByVal objTrans As clsCSATransfer, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsProvisionEntry()
        Try
            Dim Qry As String = "select max(TSPL_PROVISION_ENTRY.Doc_No) from TSPL_PROVISION_ENTRY left outer join TSPL_CSA_TRANSFER_HEAD on " & _
         "TSPL_PROVISION_ENTRY.Ref_Doc_No=TSPL_CSA_TRANSFER_HEAD.DOC_CODE where Prog_Code='CSA_Transfer' and " & _
         "convert(date,Doc_Date,103)='" & clsCommon.GetPrintDate(objTrans.Transfer_Date, "dd/MMM/yyyy") & "' and TSPL_CSA_TRANSFER_HEAD.Vehicle_Id='" & objTrans.Vehicle_code & "' and " & _
         "TSPL_PROVISION_ENTRY.Loc_Code='" & objTrans.From_Location_Code & "' and TSPL_PROVISION_ENTRY.Vendor_Code='" & objTrans.Transport_Id & "' "
            Dim strProvisionNo = clsDBFuncationality.getSingleValue(Qry, trans)

            Qry = "Select Amount from TSPL_PROVISION_ENTRY where Doc_No='" & strProvisionNo & "'"
            Dim dblProvAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))

            If clsCommon.myLen(strProvisionNo) = 0 Then
                obj = New clsProvisionEntry()
                obj.isNewEntry = True
                obj.Doc_Date = objTrans.Transfer_Date
                obj.Vendor_Code = objTrans.Transport_Id
                obj.Vendor_Desc = objTrans.Transport_Desc
                obj.Vendor_Type = "Transporter For CSA Transfer"
                obj.Status = "No"
                obj.Ref_Doc_No = objTrans.DOC_CODE
                obj.Prov_type = "Freight"
                obj.Amount = objTrans.Vehicle_Charge
                obj.Prog_Code = clsUserMgtCode.frmCSATransfer
                obj.Prov_Month = Month(objTrans.Transfer_Date)
                obj.Prov_Year = Year(objTrans.Transfer_Date)
                obj.Loc_Code = objTrans.From_Location_Code
                obj.Loc_Desc = objTrans.From_Location_Name
                obj.FixedCharge = objTrans.FixedCharge
                obj.EmptyCharge = objTrans.EmptyCharge
                obj.Freight_Type = objTrans.Freight_Type

                If clsProvisionEntry.SaveData(obj, trans) Then
                    If clsProvisionEntry.PostData(obj.Doc_No, trans, True) Then

                    End If
                End If
                Return True
            Else
                If dblProvAmount < objTrans.Vehicle_Charge Then
                    Qry = "Update TSPL_PROVISION_ENTRY set Ref_Doc_No='" & objTrans.DOC_CODE & "',Amount=" & objTrans.Vehicle_Charge & " where TSPL_PROVISION_ENTRY.Doc_No='" & strProvisionNo & "' "
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    Return True
                End If

            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
        End Try

    End Function
    Public Shared Function GetState_Inter_Local(ByVal Cust_Code As String, ByVal Bill_To_Loc_Code As String) As String
        Dim str As String = ""
        Dim cust_State As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state from tspl_customer_master where cust_code='" + Cust_Code + "'"))
        Dim loc_State As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state from tspl_location_master where cust_code='" + Bill_To_Loc_Code + "'"))

        If clsCommon.CompairString(cust_State, loc_State) = CompairStringResult.Equal Then
            str = "L"
        Else
            str = "I"
        End If
        Return str
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsCSATransfer
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCSATransfer
        Dim obj As clsCSATransfer = Nothing
        Dim qry As String = "SELECT  TSPL_CSA_TRANSFER_HEAD.EWayBillDate,TSPL_CSA_TRANSFER_HEAD.EWayBillNo,TSPL_CSA_TRANSFER_HEAD.Electronic_Ref_No,TSPL_CSA_TRANSFER_HEAD.Removal_Date,TSPL_CSA_TRANSFER_HEAD.Freight_Type,TSPL_CSA_TRANSFER_HEAD.EmptyCharge,TSPL_CSA_TRANSFER_HEAD.FixedCharge,TSPL_CSA_TRANSFER_HEAD.Secondary_Doc_Code,ISNULL(TSPL_CSA_TRANSFER_HEAD.Transporter_Name_Manual,'') AS Transporter_Name_Manual ,TSPL_SHIP_TO_LOCATION.Ship_To_Desc,TSPL_CSA_TRANSFER_HEAD.Ship_To_Location,TSPL_CSA_TRANSFER_HEAD.Excisable,Vehicle_Charge,Vehicle_Capacity,Total_Item_Wt,Gross_Item_Wt,GR_No,GR_Date,Against_Form,waybill_date,TSPL_CSA_TRANSFER_HEAD.vehicle_id,TSPL_CSA_TRANSFER_HEAD.Transport_Id,TSPL_TRANSPORT_MASTER.Transporter_Name as vehicle_desc,TSPL_CSA_TRANSFER_HEAD.WayBill_No,TSPL_CSA_TRANSFER_HEAD.DELEVERY_ORDER_NO,TSPL_CSA_TRANSFER_HEAD.DOC_CODE,TSPL_CSA_TRANSFER_HEAD.Inculding_Tax , " & _
        " TSPL_CSA_TRANSFER_HEAD.Transfer_Date,  TSPL_CSA_TRANSFER_HEAD.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ," & _
        " TSPL_CSA_TRANSFER_HEAD.Status,TSPL_CSA_TRANSFER_HEAD.Is_Approved,TSPL_CSA_TRANSFER_HEAD.State_Code,TSPL_CSA_TRANSFER_HEAD.Description,  " & _
        " TSPL_CSA_TRANSFER_HEAD.Tax_Group,TSPL_CSA_TRANSFER_HEAD.From_Location_Code,TSPL_CSA_TRANSFER_HEAD.To_Location_Code, " & _
        " TSPL_CSA_TRANSFER_HEAD.TAX1,TSPL_CSA_TRANSFER_HEAD.TAX1_Rate,TSPL_CSA_TRANSFER_HEAD.TAX1_Amt,TSPL_CSA_TRANSFER_HEAD.TAX1_Base_Amt," & _
        " TSPL_CSA_TRANSFER_HEAD.TAX2,TSPL_CSA_TRANSFER_HEAD.TAX2_Rate,TSPL_CSA_TRANSFER_HEAD.TAX2_Amt,TSPL_CSA_TRANSFER_HEAD.TAX2_Base_Amt," & _
        " TSPL_CSA_TRANSFER_HEAD.TAX3,TSPL_CSA_TRANSFER_HEAD.TAX3_Rate,TSPL_CSA_TRANSFER_HEAD.TAX3_Amt,TSPL_CSA_TRANSFER_HEAD.TAX3_Base_Amt," & _
        " TSPL_CSA_TRANSFER_HEAD.TAX4,TSPL_CSA_TRANSFER_HEAD.TAX4_Rate,TSPL_CSA_TRANSFER_HEAD.TAX4_Amt,TSPL_CSA_TRANSFER_HEAD.TAX4_Base_Amt," & _
        " TSPL_CSA_TRANSFER_HEAD.TAX5,TSPL_CSA_TRANSFER_HEAD.TAX5_Rate,TSPL_CSA_TRANSFER_HEAD.TAX5_Amt,TSPL_CSA_TRANSFER_HEAD.TAX5_Base_Amt," & _
        " TSPL_CSA_TRANSFER_HEAD.TAX6,TSPL_CSA_TRANSFER_HEAD.TAX6_Rate,TSPL_CSA_TRANSFER_HEAD.TAX6_Amt,TSPL_CSA_TRANSFER_HEAD.TAX6_Base_Amt," & _
        " TSPL_CSA_TRANSFER_HEAD.TAX7,TSPL_CSA_TRANSFER_HEAD.TAX7_Rate,TSPL_CSA_TRANSFER_HEAD.TAX7_Amt,TSPL_CSA_TRANSFER_HEAD.TAX7_Base_Amt," & _
        " TSPL_CSA_TRANSFER_HEAD.TAX8,TSPL_CSA_TRANSFER_HEAD.TAX8_Rate,TSPL_CSA_TRANSFER_HEAD.TAX8_Amt,TSPL_CSA_TRANSFER_HEAD.TAX8_Base_Amt," & _
        " TSPL_CSA_TRANSFER_HEAD.TAX9,TSPL_CSA_TRANSFER_HEAD.TAX9_Rate,TSPL_CSA_TRANSFER_HEAD.TAX9_Amt,TSPL_CSA_TRANSFER_HEAD.TAX9_Base_Amt," & _
        " TSPL_CSA_TRANSFER_HEAD.TAX10,TSPL_CSA_TRANSFER_HEAD.TAX10_Rate,TSPL_CSA_TRANSFER_HEAD.TAX10_Amt,TSPL_CSA_TRANSFER_HEAD.TAX10_Base_Amt, " & _
        " TSPL_CSA_TRANSFER_HEAD.Discount_Base,TSPL_CSA_TRANSFER_HEAD.Discount_Amt,TSPL_CSA_TRANSFER_HEAD.Amount_Less_Discount, " & _
        " TSPL_CSA_TRANSFER_HEAD.Total_Tax_Amt,TSPL_CSA_TRANSFER_HEAD.Document_Amount,TSPL_CSA_TRANSFER_HEAD.Comp_Code , " & _
        " TSPL_LOCATION_MASTER.Location_Desc as From_Location_Name,To_location.Location_Desc as To_Location_Name, " & _
        " TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_CSA_TRANSFER_HEAD.Posting_Date, " & _
        " TSPL_CSA_TRANSFER_HEAD.Modify_By,TSPL_CSA_TRANSFER_HEAD.Modify_Date,TSPL_CSA_TRANSFER_HEAD.Created_By, TSPL_CSA_TRANSFER_HEAD.Created_Date, " & _
        " TSPL_CSA_TRANSFER_HEAD.Add_Charge_Code1,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name1,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt1," & _
        " TSPL_CSA_TRANSFER_HEAD.Add_Charge_Code2,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name2,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt2, " & _
        " TSPL_CSA_TRANSFER_HEAD.Add_Charge_Code3,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name3,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt3, " & _
        " TSPL_CSA_TRANSFER_HEAD.Add_Charge_Code4,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name4,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt4, " & _
        " TSPL_CSA_TRANSFER_HEAD.Add_Charge_Code5,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name5,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt5, " & _
        " TSPL_CSA_TRANSFER_HEAD.Add_Charge_Code6,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name6,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt6, " & _
        " TSPL_CSA_TRANSFER_HEAD.Add_Charge_Code7,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name7,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt7, " & _
        " TSPL_CSA_TRANSFER_HEAD.Add_Charge_Code8,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name8,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt8, " & _
        " TSPL_CSA_TRANSFER_HEAD.Add_Charge_Code9 ,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name9,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt9," & _
        " TSPL_CSA_TRANSFER_HEAD.Add_Charge_Code10 ,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name10,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt10," & _
        " TSPL_CSA_TRANSFER_HEAD.Total_Add_Charge,Total_Commission_Chrage,TSPL_CSA_TRANSFER_HEAD.CURRENCY_CODE,TSPL_CSA_TRANSFER_HEAD.CONVRATE, " & _
        " TSPL_CSA_TRANSFER_HEAD.APPLICABLEFROM ,TSPL_CSA_TRANSFER_HEAD.Total_Commission_Chrage,TSPL_CSA_TRANSFER_HEAD.Status,TSPL_CSA_TRANSFER_HEAD.CSA_RATE," & _
        " TSPL_CSA_TRANSFER_HEAD.Item_Tax_Type FROM TSPL_CSA_TRANSFER_HEAD " & _
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_CSA_TRANSFER_HEAD.From_Location_Code  " & _
        " left outer join TSPL_LOCATION_MASTER AS To_location on To_location.Location_Code=TSPL_CSA_TRANSFER_HEAD.To_Location_Code " & _
        " left outer join   TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_CSA_TRANSFER_HEAD.Tax_Group " & _
        " left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=tspl_csa_transfer_head.Transport_Id " & _
        " left outer join TSPL_SHIP_TO_LOCATION on TSPL_CSA_TRANSFER_HEAD.Ship_To_Location=TSPL_SHIP_TO_LOCATION.Ship_To_Code " & _
        " left  outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSA_TRANSFER_HEAD.Cust_Code  where 2=2"
        Dim whrClas As String = ""


        Dim strwherecls As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(NavType).ToUpper(), "CURRENT") <> CompairStringResult.Equal Then
            strwherecls = FrmMainTranScreen.CustomerPermission()
        End If

        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    whrClas = " AND From_Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        'End If

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " AND From_Location_Code in (" + objCommonVar.strCurrUserLocations + ") and TSPL_CSA_TRANSFER_HEAD.Cust_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " AND From_Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " AND TSPL_CSA_TRANSFER_HEAD.Cust_Code in (" + strwherecls + ")"
        End If
        '-----------------------------------------------------
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CSA_TRANSFER_HEAD.DOC_CODE = (select MIN(DOC_CODE) from TSPL_CSA_TRANSFER_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_CSA_TRANSFER_HEAD.DOC_CODE = (select Max(DOC_CODE) from TSPL_CSA_TRANSFER_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_CSA_TRANSFER_HEAD.DOC_CODE = (select Min(DOC_CODE) from TSPL_CSA_TRANSFER_HEAD where DOC_CODE>'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_CSA_TRANSFER_HEAD.DOC_CODE = (select Max(DOC_CODE) from TSPL_CSA_TRANSFER_HEAD where DOC_CODE<'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_CSA_TRANSFER_HEAD.DOC_CODE = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCSATransfer()
            obj.Transporter_Name_Manual = clsCommon.myCstr(dt.Rows(0)("Transporter_Name_Manual"))
            obj.DOC_CODE = clsCommon.myCstr(dt.Rows(0)("DOC_CODE"))

            obj.Secondary_Doc_Code = clsCommon.myCstr(dt.Rows(0)("Secondary_Doc_Code"))

            obj.DELEVERY_ORDER_NO = clsCommon.myCstr(dt.Rows(0)("DELEVERY_ORDER_NO"))
            obj.Waybill_No = clsCommon.myCstr(dt.Rows(0)("WayBill_No"))
            obj.Against_F = clsCommon.myCstr(dt.Rows(0)("Against_Form"))

            If dt.Rows(0)("waybill_date") Is DBNull.Value Then
                obj.Waybill_Date = clsCommon.GETSERVERDATE(trans)
            Else
                obj.Waybill_Date = clsCommon.myCDate(dt.Rows(0)("waybill_date"))
            End If

            If dt.Rows(0)("EWayBillDate") Is DBNull.Value Then
                obj.EWayBillDate = clsCommon.GETSERVERDATE(trans)
            Else
                obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            End If
            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            obj.Electronic_Ref_No = clsCommon.myCstr(dt.Rows(0)("Electronic_Ref_No"))

            ''richa agarwal 29 Nov,2016
            If dt.Rows(0)("Removal_Date") IsNot DBNull.Value Then
                obj.Removal_Date = clsCommon.myCstr(dt.Rows(0)("Removal_Date"))
            End If
            ''-------------
            obj.Vehicle_Charge = clsCommon.myCdbl(dt.Rows(0)("Vehicle_Charge"))
            obj.Vehicle_Capacity = clsCommon.myCdbl(dt.Rows(0)("Vehicle_Capacity"))
            obj.Vehicle_code = clsCommon.myCstr(dt.Rows(0)("vehicle_id"))
            obj.Total_Item_Wt = clsCommon.myCdbl(dt.Rows(0)("Total_Item_Wt"))
            obj.Gross_Item_Wt = clsCommon.myCdbl(dt.Rows(0)("Gross_Item_Wt"))
            obj.GR_No = clsCommon.myCstr(dt.Rows(0)("GR_No"))

            If dt.Rows(0)("GR_Date") Is DBNull.Value Then
                obj.GR_Date = Nothing
            Else
                obj.GR_Date = clsCommon.myCDate(dt.Rows(0)("GR_Date"))
            End If

            obj.Excisable = clsCommon.myCstr(dt.Rows(0)("Excisable"))
            obj.Transport_Id = clsCommon.myCstr(dt.Rows(0)("Transport_Id"))
            obj.Transport_Desc = clsCommon.myCstr(dt.Rows(0)("vehicle_desc"))
            obj.Transfer_Date = clsCommon.myCstr(dt.Rows(0)("Transfer_Date"))

            obj.Inculding_Tax = clsCommon.myCstr(dt.Rows(0)("Inculding_Tax"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            obj.State_Code = clsCommon.myCstr(dt.Rows(0)("State_Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))

            obj.Freight_Type = clsCommon.myCstr(dt.Rows(0)("Freight_Type"))
            obj.EmptyCharge = clsCommon.myCdbl(dt.Rows(0)("EmptyCharge"))
            obj.FixedCharge = clsCommon.myCdbl(dt.Rows(0)("FixedCharge"))

            obj.From_Location_Code = clsCommon.myCstr(dt.Rows(0)("From_Location_Code"))
            obj.To_Location_Code = clsCommon.myCstr(dt.Rows(0)("To_Location_Code"))
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

            obj.Document_Amount = clsCommon.myCdbl(dt.Rows(0)("Document_Amount"))

            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))

            obj.From_Location_Name = clsCommon.myCstr(dt.Rows(0)("From_Location_Name"))
            obj.To_Location_Name = clsCommon.myCstr(dt.Rows(0)("To_Location_Name"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.CSA_Rate = clsCommon.myCdbl(dt.Rows(0)("CSA_Rate"))

            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If

            'obj.Approvel_Required = clsCommon.myCdbl(dt.Rows(0)("Approvel_Required"))
            obj.Is_Approved = clsCommon.myCdbl(dt.Rows(0)("Is_Approved"))
            obj.Status = clsCommon.myCdbl(dt.Rows(0)("Status"))

            obj.DELEVERY_ORDER_NO = clsCommon.myCstr(dt.Rows(0)("DELEVERY_ORDER_NO"))

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
            obj.Total_Commission_Chrage = clsCommon.myCdbl(dt.Rows(0)("Total_Commission_Chrage"))

            obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
            obj.Ship_To_Location_Desc = clsCommon.myCstr(dt.Rows(0)("Ship_To_Desc"))

            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            '' END CURRENCYCONVERSION 
            qry = "SELECT TSPL_CSA_TRANSFER_DETAIL.MRP,TSPL_CSA_TRANSFER_DETAIL.Is_MRP_Mandatory,TSPL_CSA_TRANSFER_DETAIL.Abatement_Pers,TSPL_CSA_TRANSFER_DETAIL.Abatement_Amt,TSPL_CSA_TRANSFER_DETAIL.DELEVERY_ORDER_NO,TSPL_CSA_TRANSFER_DETAIL.DO_Pending_Qty,TSPL_CSA_TRANSFER_DETAIL.DO_Qty,TSPL_CSA_TRANSFER_DETAIL.Alt_Unit_Code,TSPL_CSA_TRANSFER_DETAIL.CSA_Commission_RS_PERS,TSPL_CSA_TRANSFER_DETAIL.Item_Unit_Wt,TSPL_CSA_TRANSFER_DETAIL.Item_Net_Wt,TSPL_CSA_TRANSFER_DETAIL.Item_Net_MT_Wt,TSPL_CSA_TRANSFER_DETAIL.Cash_Scheme_Amount,TSPL_CSA_TRANSFER_DETAIL.Cash_Scheme_Type,TSPL_CSA_TRANSFER_DETAIL.Cash_Scheme_Pers,TSPL_CSA_TRANSFER_DETAIL.Cash_Scheme_Code,TSPL_CSA_TRANSFER_DETAIL.FOC,TSPL_CSA_TRANSFER_DETAIL.Scheme_Item_UOM,TSPL_CSA_TRANSFER_DETAIL.Scheme_Qty,TSPL_CSA_TRANSFER_DETAIL.Scheme_Item_Code,TSPL_CSA_TRANSFER_DETAIL.Scheme_Type,TSPL_CSA_TRANSFER_DETAIL.Scheme_Code,TSPL_CSA_TRANSFER_DETAIL.Scheme_Applicable,TSPL_CSA_TRANSFER_DETAIL.DOC_CODE,TSPL_CSA_TRANSFER_DETAIL.Line_No,TSPL_ITEM_MASTER.CSA_TYPE, TSPL_CSA_TRANSFER_DETAIL.Calc_Type, " & _
            " TSPL_CSA_TRANSFER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_CSA_TRANSFER_DETAIL.Qty, TSPL_CSA_TRANSFER_DETAIL.Balance_Qty," & _
            " TSPL_CSA_TRANSFER_DETAIL.Unit_code, TSPL_CSA_TRANSFER_DETAIL.Unit_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX1," & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX1_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX1_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX2, " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX2_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX2_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX3, " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX3_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX3_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX4, " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX4_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX4_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX5, " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX5_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX5_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX6, " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX6_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX6_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX7, " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX7_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX7_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX8, " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX8_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX8_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX9, " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX9_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX9_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX10," & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX10_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX10_Amt,TSPL_CSA_TRANSFER_DETAIL.Amount, " & _
            " TSPL_CSA_TRANSFER_DETAIL.Total_Tax_Amt,TSPL_CSA_TRANSFER_DETAIL.Item_Net_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX1_Base_Amt, " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX2_Base_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX3_Base_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX4_Base_Amt, " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX5_Base_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX6_Base_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX7_Base_Amt, " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX8_Base_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX9_Base_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX10_Base_Amt," & _
            " TSPL_CSA_TRANSFER_DETAIL.Transfer_Rate,TSPL_CSA_TRANSFER_DETAIL.Item_Net_Amt, " & _
            " TSPL_CSA_TRANSFER_DETAIL.Including_Tax,TSPL_CSA_TRANSFER_DETAIL.CALC_TYPE,TSPL_CSA_TRANSFER_DETAIL.Item_Pack_Size,TSPL_CSA_TRANSFER_DETAIL.Item_Master_Pack_Size," & _
            " TSPL_CSA_TRANSFER_DETAIL.Commision_Rate,TSPL_CSA_TRANSFER_DETAIL.Other_Chrage,TSPL_CSA_TRANSFER_DETAIL.Commission_Chrage FROM TSPL_CSA_TRANSFER_DETAIL " & _
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CSA_TRANSFER_DETAIL.Item_Code " & _
            " where TSPL_CSA_TRANSFER_DETAIL.DOC_CODE='" + obj.DOC_CODE + "' ORDER BY TSPL_CSA_TRANSFER_DETAIL.Line_No asc"

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsCSATransferDetail)
                Dim objTr As clsCSATransferDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsCSATransferDetail
                    objTr.arrBatchItem = New List(Of clsBatchInventory)

                    objTr.DOC_CODE = clsCommon.myCstr(dr("DOC_CODE"))
                    objTr.Calc_Type = clsCommon.myCstr(dr("Calc_Type"))
                    objTr.CSA_Type = clsCommon.myCstr(dr("CSA_Type"))
                    objTr.Including_Tax = clsCommon.myCstr(dr("Including_Tax"))
                    objTr.Line_No = Convert.ToInt32(clsCommon.myCdbl(dr("Line_No")))
                    'objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.Item_Unit_Wt = clsCommon.myCdbl(dr("Item_Unit_Wt"))
                    objTr.Item_Net_Wt = clsCommon.myCdbl(dr("Item_Net_Wt"))
                    objTr.Item_Net_MT_Wt = clsCommon.myCdbl(dr("Item_Net_MT_Wt"))
                    objTr.Alt_Unit_Code = clsCommon.myCstr(dr("Alt_Unit_Code"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    'objTr.Location = clsCommon.myCstr(dr("Location"))
                    'objTr.LocationName = clsCommon.myCstr(dr("LocationName"))

                    objTr.MRP = clsCommon.myCdbl(dr("mrp"))
                    objTr.Is_MRP_Mandatory = CInt(clsCommon.myCdbl(dr("Is_MRP_Mandatory")))
                    objTr.Abatement_Pers = clsCommon.myCdbl(dr("Abatement_Pers"))
                    objTr.Abatement_Amt = clsCommon.myCdbl(dr("Abatement_Amt"))

                    objTr.Unit_Rate = clsCommon.myCdbl(dr("Unit_Rate"))
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
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Total_Basic_Amt = clsCommon.myCdbl(dr("Amount"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))

                    objTr.Item_Pack_Size = clsCommon.myCdbl(dr("Item_Pack_Size"))
                    objTr.Item_Master_Pack_Size = clsCommon.myCdbl(dr("Item_Master_Pack_Size"))
                    objTr.Commision_Rate = clsCommon.myCdbl(dr("Commision_Rate"))
                    objTr.Commission_Chrage = clsCommon.myCdbl(dr("Commission_Chrage"))
                    objTr.CSA_Commission_RS_PERS = clsCommon.myCstr(dr("CSA_Commission_RS_PERS"))
                    objTr.Other_Chrage = clsCommon.myCdbl(dr("Other_Chrage"))

                    objTr.Transfer_Rate = clsCommon.myCdbl(dr("Transfer_Rate"))

                    objTr.Scheme_Type = clsCommon.myCstr(dr("Scheme_Type"))
                    objTr.Scheme_Qty = clsCommon.myCdbl(dr("Scheme_Qty"))
                    objTr.Scheme_Item_UOM = clsCommon.myCstr(dr("Scheme_Item_UOM"))
                    objTr.Scheme_Item_Code = clsCommon.myCstr(dr("Scheme_Item_Code"))
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    objTr.Scheme_Applicable = clsCommon.myCstr(dr("Scheme_Applicable"))
                    objTr.FOC = clsCommon.myCstr(dr("FOC"))
                    objTr.Cash_Scheme_Code = clsCommon.myCstr(dr("Cash_Scheme_Code"))
                    objTr.Cash_Scheme_Type = clsCommon.myCstr(dr("Cash_Scheme_Type"))
                    objTr.Cash_Scheme_Pers = clsCommon.myCstr(dr("Cash_Scheme_Pers"))
                    objTr.Cash_Scheme_Amount = clsCommon.myCstr(dr("Cash_Scheme_Amount"))

                    objTr.DO_Qty = clsCommon.myCdbl(dr("DO_Qty"))
                    objTr.DO_Pending_Qty = clsCommon.myCdbl(dr("DO_Pending_Qty"))
                    objTr.DELEVERY_ORDER_NO = clsCommon.myCstr(dr("DELEVERY_ORDER_NO"))

                    objTr.arrBatchItem = clsBatchInventory.GetData("SD-CSATRANS", obj.DOC_CODE, objTr.Item_Code, objTr.Line_No, trans)

                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("CSA Transfer No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsCSATransfer = clsCSATransfer.GetData(strDocNo, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSATransfer, obj.From_Location_Code, obj.Transfer_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = ""
            'Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_CSA_TRANSFER_HEAD", "DOC_CODE", strDocNo, trans)
            'If isResult = False Then
            '    trans.Commit()
            '    Return False
            'End If
            Dim isSaved As Boolean = True

            If clsCommon.myLen(obj.Transport_Id) > 0 Then
                ConvertProvision(obj, trans)
            End If

            isSaved = isSaved AndAlso SendToInventoryMovement(obj, trans)
            'comment by Balwinder cogs is handled in JE funcion on 02/02/2016
            'If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) Then
            isSaved = isSaved AndAlso CreateJournalEntry(obj.DOC_CODE, trans)
            'End If
            qry = "Update TSPL_CSA_TRANSFER_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "', Modify_Date='" + strPostDate + "' where DOC_CODE='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved = True Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
            Return isSaved

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function UnPostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("CSA Transfer No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsCSATransfer = clsCSATransfer.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 0) Then
                Throw New Exception("Document is already unposted :" + obj.DOC_CODE)
            End If

            Dim qry As String = ""
            'Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_CSA_TRANSFER_HEAD", "DOC_CODE", strDocNo, trans)
            'If isResult = False Then
            '    trans.Commit()
            '    Return False
            'End If
            Dim isSaved As Boolean = True
            '' delete provision entry
            qry = "delete from TSPL_PROVISION_ENTRY where Ref_Doc_No='" & obj.DOC_CODE & "' and Prog_Code='CSA_Transfer'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete from inventory movement
            qry = "update tspl_batch_item set against_inv_movement_trans_id=NULL where document_type='SD-CSATRANS' and document_code='" + obj.DOC_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & obj.DOC_CODE & "' and Trans_Type= 'SD-CSATRANS'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT_New where Source_Doc_No='" & obj.DOC_CODE & "' and Trans_Type= 'SD-CSATRANS'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete journal entry
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='CS-TR' and Source_Doc_No='" & obj.DOC_CODE & "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            qry = "Update TSPL_CSA_TRANSFER_HEAD set Status=0, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "', Modify_Date='" + strPostDate + "' where DOC_CODE='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved = True Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
            Return isSaved

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function SendToInventoryMovement(ByVal obj As clsCSATransfer, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try

            Dim ArrInventoryMovementOut As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim ArrInventoryMovementIn As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim isSaved As Boolean = True

            Dim strRgpNo As String = Nothing
            Dim intCounter As Integer = 0
            For Each objTr As clsCSATransferDetail In obj.Arr
                intCounter = intCounter + 1
                'If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                '' out from from location
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

                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.Unit_code)
                End If

                Dim objInventoryMovemnt As New clsInventoryMovement()

                objInventoryMovemnt.Ref_Line_No = objTr.Line_No

                objInventoryMovemnt.InOut = "O"
                objInventoryMovemnt.Location_Code = obj.From_Location_Code
                objInventoryMovemnt.Other_Location_Code = obj.To_Location_Code
                objInventoryMovemnt.Other_Location_Desc = obj.To_Location_Name
                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = objTr.Qty '+ objTr.Free_Qty
                objInventoryMovemnt.UOM = objTr.Unit_code
                objInventoryMovemnt.Basic_Cost = objTr.Unit_Rate
                objInventoryMovemnt.MRP = 0
                objInventoryMovemnt.Add_Cost = 0
                objInventoryMovemnt.Net_Cost = objTr.Unit_Rate
                objInventoryMovemnt.Cust_Code = obj.Cust_Code
                objInventoryMovemnt.Cust_Name = obj.Customer_Name
                ''================calculate====cost=================================
                Dim cost As Decimal = 0

                cost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.From_Location_Code, objTr.Qty, obj.Transfer_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                objInventoryMovemnt.FIFO_Cost = cost

                cost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.From_Location_Code, objTr.Qty, obj.Transfer_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                objInventoryMovemnt.Avg_Cost = cost

                cost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.From_Location_Code, objTr.Qty, obj.Transfer_Date, clsCommon.GETSERVERDATE(trans), True, trans)
                objInventoryMovemnt.LIFO_Cost = cost
                '==================================================================

                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "FT"
                End If
                objInventoryMovemnt.ItemType = strItemTypeToSave
                ArrInventoryMovementOut.Add(objInventoryMovemnt)

                '' In To To location

                'Dim objInventoryMovemntIn As New clsInventoryMovement()
                'objInventoryMovemntIn.InOut = "I"
                'objInventoryMovemntIn.Location_Code = obj.To_Location_Code
                'objInventoryMovemntIn.Item_Code = objTr.Item_Code
                'objInventoryMovemntIn.Item_Desc = objTr.Item_Desc
                'objInventoryMovemntIn.Qty = objTr.Qty '+ objTr.Free_Qty
                'objInventoryMovemntIn.UOM = objTr.Unit_code
                'objInventoryMovemntIn.Basic_Cost = objTr.Unit_Rate
                'objInventoryMovemntIn.MRP = 0
                'objInventoryMovemntIn.Add_Cost = 0
                'objInventoryMovemntIn.Net_Cost = objTr.Unit_Rate
                'If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                '    objInventoryMovemntIn.ItemType = "RM"
                'ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                '    objInventoryMovemntIn.ItemType = "OT"
                'ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                '    objInventoryMovemntIn.ItemType = "FT"
                'End If
                'objInventoryMovemntIn.ItemType = strItemTypeToSave
                'ArrInventoryMovementIn.Add(objInventoryMovemntIn)
                'End If
            Next
            isSaved = isSaved AndAlso clsInventoryMovement.SaveData("SD-CSATRANS", obj.DOC_CODE, obj.Transfer_Date, clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MM/yyyy"), ArrInventoryMovementOut, trans)
            For Each objInventoryMovemntIn As clsInventoryMovement In ArrInventoryMovementOut
                Dim objToInsert As clsInventoryMovement = clsInventoryMovement.DeepCopyObject(objInventoryMovemntIn)
                objToInsert.InOut = "I"
                objToInsert.Location_Code = obj.To_Location_Code
                objToInsert.Other_Location_Code = obj.From_Location_Code
                objToInsert.Other_Location_Desc = obj.From_Location_Name

                ArrInventoryMovementIn.Add(objToInsert)
            Next
            isSaved = isSaved AndAlso clsInventoryMovement.SaveData("SD-CSATRANS", obj.DOC_CODE, obj.Transfer_Date, clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MM/yyyy"), ArrInventoryMovementIn, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function CreateJournalEntry(ByVal strCode As String, ByVal trans As SqlTransaction)
        Try

            Dim ItemWiseCSAAccount As Boolean = False
            ItemWiseCSAAccount = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowItemWiseCSAAccountingON_CSASale, clsFixedParameterCode.AllowItemWiseCSAAccountingON_CSASale, trans)) = "1", True, False))

            Dim StopGLForConsignment As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.StopGLEntryForConsignmentAtCSATransfer, clsFixedParameterCode.StopGLEntryForConsignmentAtCSATransfer, trans))
            ''if gl setting is ON then no consignment a/c debit and no GOSC a/c credit

            Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)

            If StopGLForConsignment = "1" Then
                isSkipCogsGL = False
            End If

            Dim obj As New clsCSATransfer
            obj = clsCSATransfer.GetData(strCode, NavigatorType.Current, trans)
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim strCostOfGoodSold_Acc As String = ""
            Dim strConsignment_Acc As String = ""
            Dim strInventoryControl_Acc As String = ""
            Dim strReceivableControlAcc As String = ""
            Dim strGSOL_Acc As String = ""
            Dim isSaved As Boolean = True
            Dim qry As String
            Dim InnerQry As String



            InnerQry = " select TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct,isnull(TSPL_CSA_TRANSFER_DETAIL.Total_Tax_Amt,0) as Total_Tax_Amt,TSPL_CSA_TRANSFER_DETAIL.DOC_CODE,TSPL_CSA_TRANSFER_HEAD.From_Location_Code,TSPL_CSA_TRANSFER_HEAD.To_Location_Code, " & _
                  " TSPL_CSA_TRANSFER_HEAD.Cust_Code,TSPL_CSA_TRANSFER_DETAIL.Line_No,TSPL_CSA_TRANSFER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc, " & _
                  " TSPL_CSA_TRANSFER_DETAIL.Qty,TSPL_CSA_TRANSFER_DETAIL.Transfer_Rate* TSPL_CSA_TRANSFER_DETAIL.Qty as Transfer_Amount, " & _
                  " (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=1 then Inv_Movement.Avg_Cost when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then FIFO_Cost" & _
                  " when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then LIFO_Cost end) as Item_Cost,TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold, " & _
                  " Cost_Good_GL.Description as Cost_Of_Goods_Sold_Desc, " & _
                  " TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,Inv_Conrol_GL.Description as Inv_Control_Account_Desc, "

            If Not ItemWiseCSAAccount Then
                InnerQry += " TSPL_CUSTOMER_ACCOUNT_SET.GSOC_Acct," & _
                    " TSPL_CUSTOMER_ACCOUNT_SET.Consignment_Acct,Consignment_Gl.Description as Consignment_Acct_Desc, "
            Else
                InnerQry += " ItemCustAcc.GSOC_Acct," & _
                    " ItemCustAcc.Consignment_Acct,Consignment_Gl.Description as Consignment_Acct_Desc, "
            End If
            InnerQry += " GSOC_GL.Description as GSOC_Acct_Desc  from TSPL_CSA_TRANSFER_DETAIL " & _
                  " inner join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_DETAIL.DOC_CODE=TSPL_CSA_TRANSFER_HEAD.DOC_CODE " & _
                  " left join TSPL_ITEM_MASTER on TSPL_CSA_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
                  " left join TSPL_SALES_ACCOUNTS on TSPL_ITEM_MASTER.Sale_Class_Code=TSPL_SALES_ACCOUNTS.Sales_Class_Code " & _
                  " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
                  " left join TSPL_GL_ACCOUNTS as Cost_Good_GL on TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold=Cost_Good_GL.Account_Code " & _
                  " left join TSPL_GL_ACCOUNTS as Inv_Conrol_GL on TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account=Inv_Conrol_GL.Account_Code " & _
                  " left join TSPL_CUSTOMER_MASTER on TSPL_CSA_TRANSFER_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
                  " left join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_MASTER.Cust_Account=TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account "


            If Not ItemWiseCSAAccount Then
                InnerQry += " left join TSPL_GL_ACCOUNTS as GSOC_GL on TSPL_CUSTOMER_ACCOUNT_SET.GSOC_Acct=GSOC_GL.Account_Code " & _
                    " left join TSPL_GL_ACCOUNTS as Consignment_Gl on TSPL_CUSTOMER_ACCOUNT_SET.Consignment_Acct=Consignment_Gl.Account_Code "
            Else
                InnerQry += " left join TSPL_CUSTOMER_ACCOUNT_SET as ItemCustAcc on tspl_item_master.Cust_Account=ItemCustAcc.Cust_Account left join TSPL_GL_ACCOUNTS as GSOC_GL on ItemCustAcc.GSOC_Acct=GSOC_GL.Account_Code " & _
                    " left join TSPL_GL_ACCOUNTS as Consignment_Gl on ItemCustAcc.Consignment_Acct=Consignment_Gl.Account_Code "
            End If

            InnerQry += " left join (select item_code,sum(FIFO_Cost) as FIFO_Cost,sum(Avg_Cost) as Avg_Cost ,sum(LIFO_Cost) as LIFO_Cost " & _
                  " from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & strCode & "' and InOut='O' group by item_code) as Inv_Movement " & _
                  " on TSPL_CSA_TRANSFER_DETAIL.Item_Code=Inv_Movement.Item_Code " & _
                  " where TSPL_CSA_TRANSFER_HEAD.DOC_CODE='" & strCode & "'"

            qry = " select Receivable_Control_acct,Item_Code,Item_Desc,Cost_Of_Goods_Sold,Cost_Of_Goods_Sold_Desc,Consignment_Acct,Consignment_Acct_Desc," & _
                  " Inv_Control_Account,Inv_Control_Account_Desc,GSOC_Acct,GSOC_Acct_Desc,SUM(qty) as qty, " & _
                  " SUM(Transfer_Amount) as Transfer_Amount,SUM(coalesce(Item_Cost,0)) as Item_Cost from ( " & InnerQry & "" & _
                  " ) as Final group by Item_Code,Item_Desc,Cost_Of_Goods_Sold,Cost_Of_Goods_Sold_Desc, " & _
                  " Consignment_Acct, Consignment_Acct_Desc, Inv_Control_Account, Inv_Control_Account_Desc,GSOC_Acct,GSOC_Acct_Desc,Receivable_Control_acct"

            '' Validation of GL Itemwise
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    strCostOfGoodSold_Acc = clsCommon.myCstr(dr.Item("Cost_Of_Goods_Sold"))
                    strConsignment_Acc = clsCommon.myCstr(dr.Item("Consignment_Acct"))

                    strInventoryControl_Acc = clsCommon.myCstr(dr.Item("Inv_Control_Account"))
                    strGSOL_Acc = clsCommon.myCstr(dr.Item("GSOC_Acct"))

                    strReceivableControlAcc = clsCommon.myCstr(dr.Item("Receivable_Control_acct"))

                    If StopGLForConsignment = "1" Then
                        strConsignment_Acc = ""
                        strGSOL_Acc = ""
                        strCostOfGoodSold_Acc = clsCommon.myCstr(dr.Item("Consignment_Acct"))
                    End If
                    If StopGLForConsignment <> "1" AndAlso isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
                        strCostOfGoodSold_Acc = ""
                        strInventoryControl_Acc = ""
                    End If

                    '' dr cost of goods sold
                    strCostOfGoodSold_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strCostOfGoodSold_Acc, obj.From_Location_Code, trans))
                    If clsCommon.myLen(strCostOfGoodSold_Acc) = 0 Then
                        If StopGLForConsignment = "1" Then
                            Throw New Exception("Please set Consignment Account for item " & dr.Item("Item_Code") & "")
                        ElseIf Not isSkipCogsGL Then
                            Throw New Exception("Please set Cost of Goods Sold Account for item " & dr.Item("Item_Code") & "")
                        End If

                    End If

                    '' dr consignment acc
                    strConsignment_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strConsignment_Acc, obj.From_Location_Code, trans))
                    If clsCommon.myLen(strConsignment_Acc) = 0 AndAlso StopGLForConsignment <> "1" Then
                        Throw New Exception("Please set Consignment Account for item " & dr.Item("Item_Code") & "")
                    End If

                    '' cr Inv_Control_Account
                    strInventoryControl_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControl_Acc, obj.From_Location_Code, trans))
                    If clsCommon.myLen(strInventoryControl_Acc) = 0 AndAlso Not isSkipCogsGL Then
                        Throw New Exception("Please set Inventory Control Account for item " & dr.Item("Item_Code") & "")
                    End If

                    '' cr GSOC_Acct
                    strGSOL_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strGSOL_Acc, obj.From_Location_Code, trans))
                    If clsCommon.myLen(strGSOL_Acc) = 0 AndAlso StopGLForConsignment <> "1" Then
                        Throw New Exception("Please set GSOC Account for item " & dr.Item("Item_Code") & "")
                    End If

                    '' dr Receivable_Acct
                    strReceivableControlAcc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strReceivableControlAcc, obj.From_Location_Code, trans))
                    If clsCommon.myLen(strReceivableControlAcc) = 0 AndAlso StopGLForConsignment <> "1" Then
                        Throw New Exception("Please set Receivable Account for item " & dr.Item("Item_Code") & "")
                    End If
                Next
            End If
            Dim GSTStatus As Boolean = clsERPFuncationality.GetGSTStatus(clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MMM/yyyy"))

            '' Create Financial Entry
            qry = " select Receivable_Control_acct,Cost_Of_Goods_Sold,Cost_Of_Goods_Sold_Desc,Consignment_Acct,Consignment_Acct_Desc," & _
                  " Inv_Control_Account,Inv_Control_Account_Desc,GSOC_Acct,GSOC_Acct_Desc,SUM(qty) as qty, " & _
                  " SUM(Transfer_Amount) as Transfer_Amount,SUM(coalesce(Item_Cost,0)) as Item_Cost,sum(Total_Tax_Amt) as Total_Tax_Amt from ( " & InnerQry & "" & _
                  " ) as Final group by Cost_Of_Goods_Sold,Cost_Of_Goods_Sold_Desc, " & _
                  " Consignment_Acct, Consignment_Acct_Desc, Inv_Control_Account, Inv_Control_Account_Desc,GSOC_Acct,GSOC_Acct_Desc,Receivable_Control_acct"

            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                For Each dr As DataRow In dt.Rows
                    strCostOfGoodSold_Acc = clsCommon.myCstr(dr.Item("Cost_Of_Goods_Sold"))
                    strConsignment_Acc = clsCommon.myCstr(dr.Item("Consignment_Acct"))

                    strInventoryControl_Acc = clsCommon.myCstr(dr.Item("Inv_Control_Account"))
                    strGSOL_Acc = clsCommon.myCstr(dr.Item("GSOC_Acct"))

                    strReceivableControlAcc = clsCommon.myCstr(dr.Item("Receivable_Control_acct"))
                    If StopGLForConsignment = "1" Then
                        strConsignment_Acc = ""
                        strGSOL_Acc = ""
                        strCostOfGoodSold_Acc = clsCommon.myCstr(dr.Item("Consignment_Acct"))
                    End If
                    If StopGLForConsignment <> "1" AndAlso isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
                        strCostOfGoodSold_Acc = ""
                        strInventoryControl_Acc = ""
                    End If

                    '' dr cost of goods sold
                    strCostOfGoodSold_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strCostOfGoodSold_Acc, obj.From_Location_Code, trans))
                    If clsCommon.myLen(strCostOfGoodSold_Acc) = 0 Then
                        If StopGLForConsignment = "1" Then
                            Throw New Exception("Invalid Consigment Account " & strCostOfGoodSold_Acc & "")
                        ElseIf Not isSkipCogsGL Then
                            Throw New Exception("Invalid Cost of Goods Sold Account " & strCostOfGoodSold_Acc & "")
                        End If

                    End If
                    Dim Acc1() As String = {strCostOfGoodSold_Acc, 1 * clsCommon.myCdbl(dr("Item_Cost"))}

                    If Not isSkipCogsGL Then
                        ArryLstGLAC.Add(Acc1)
                    End If

                    '' dr consignment acc
                    strConsignment_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strConsignment_Acc, obj.From_Location_Code, trans))
                    If clsCommon.myLen(strConsignment_Acc) = 0 AndAlso StopGLForConsignment <> "1" Then
                        Throw New Exception("Invalid Consignment Account " & strConsignment_Acc & "")
                    End If

                    '' dr Receivable_Acct
                    strReceivableControlAcc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strReceivableControlAcc, obj.From_Location_Code, trans))
                    If clsCommon.myLen(strReceivableControlAcc) = 0 AndAlso StopGLForConsignment <> "1" Then
                        Throw New Exception("Please set Receivable Account for item " & dr.Item("Item_Code") & "")
                    End If

                    Dim Acc2() As String = Nothing

                    If GSTStatus Then
                        ''richa 26/10/2017
                        'Acc2 = {strConsignment_Acc, 1 * (clsCommon.myCdbl(dr("Transfer_Amount")) + clsCommon.myCdbl(dr("Total_Tax_Amt")))}
                        Acc2 = {strConsignment_Acc, 1 * clsCommon.myCdbl(dr("Transfer_Amount"))}
                    Else
                        Acc2 = {strConsignment_Acc, 1 * clsCommon.myCdbl(dr("Transfer_Amount"))}
                    End If

                    If clsCommon.myLen(strConsignment_Acc) > 0 Then
                        ArryLstGLAC.Add(Acc2)
                    End If

                    ''richa 26/10/2017
                    If GSTStatus Then
                        Acc2 = {strReceivableControlAcc, 1 * clsCommon.myCdbl(dr("Total_Tax_Amt"))}
                        If clsCommon.myLen(strReceivableControlAcc) > 0 Then
                            ArryLstGLAC.Add(Acc2)
                        End If
                    End If
                    ''---------------

                    '' cr Inv_Control_Account
                    strInventoryControl_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControl_Acc, obj.From_Location_Code, trans))
                    If clsCommon.myLen(strInventoryControl_Acc) = 0 AndAlso Not isSkipCogsGL Then
                        Throw New Exception("Invalid Inventory Control Account " & strInventoryControl_Acc & "")
                    End If
                    Dim Acc3() As String = {strInventoryControl_Acc, -1 * clsCommon.myCdbl(dr("Item_Cost"))}
                    If Not isSkipCogsGL Then
                        ArryLstGLAC.Add(Acc3)
                    End If

                    '' cr GSOC_Acct
                    strGSOL_Acc = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(strGSOL_Acc, obj.From_Location_Code, trans))
                    If clsCommon.myLen(strGSOL_Acc) = 0 AndAlso StopGLForConsignment <> "1" Then
                        Throw New Exception("Invalid GSOC Account  " & strGSOL_Acc & "")
                    End If
                    Dim Acc4() As String = {strGSOL_Acc, -1 * clsCommon.myCdbl(dr("Transfer_Amount"))}
                    If clsCommon.myLen(strGSOL_Acc) > 0 Then
                        ArryLstGLAC.Add(Acc4)
                    End If


                Next
            End If

            ''-----------tax included in case of gst
            If GSTStatus Then
                JournalEntryForGST_Common(obj, ArryLstGLAC, obj.From_Location_Code, trans)
            End If
            ''-----------------

            '===============gl entry for excisable tax======================================
            If clsCommon.CompairString(obj.Excisable, "1") = CompairStringResult.Equal Then
                JE_Excisable_Common(obj, ArryLstGLAC, obj.From_Location_Code, trans)
            End If
            '=============================================================================

            Dim GLDesc As String = "Journal Entry Against CSA Transfer- Doc No." & strCode & " "
            Dim Remarks As String = "Journal Entry against CSA Transfer for customer: CAE - " & obj.Customer_Name & "  For Doc No. " & strCode & ""

            '====================if already JV exist then update only========================
            qry = "select voucher_no from tspl_journal_master where Source_Doc_No='" + obj.DOC_CODE + "' and source_code='CS-TR'"
            Dim strRecreateVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

            If clsCommon.myLen(strRecreateVoucherNo) > 0 Then
                isSaved = clsJournalMaster.FunGrnlEntryWithTrans(obj.From_Location_Code, False, strRecreateVoucherNo, trans, obj.Transfer_Date, GLDesc, "CS-TR", "CSA Transfer", obj.DOC_CODE, "", "C", obj.Cust_Code, obj.Customer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , Remarks, "", Nothing)
            Else
                isSaved = clsJournalMaster.FunGrnlEntryWithTrans(obj.From_Location_Code, False, trans, obj.Transfer_Date, GLDesc, "CS-TR", "CSA Transfer", obj.DOC_CODE, "", "C", obj.Cust_Code, obj.Customer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , Remarks, "", Nothing)
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    ''richa agarwal journal entry for common

    Public Shared Function JournalEntryForGST_Common(ByVal obj As clsCSATransfer, ByVal ArryLstGLAC As ArrayList, ByVal strLocationSegment As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            qry = " select Doc.doc_code as Document_No,TaxM1.Tax_Liability_Account as Tax1_GLAC,TaxM2.Tax_Liability_Account as Tax2_GLAC," & _
                     " TaxM3.Tax_Liability_Account as Tax3_GLAC,TaxM4.Tax_Liability_Account as Tax4_GLAC," & _
                     " TaxM5.Tax_Liability_Account as Tax5_GLAC,TaxM6.Tax_Liability_Account as Tax6_GLAC, " & _
                     " TaxM7.Tax_Liability_Account as Tax7_GLAC,TaxM8.Tax_Liability_Account as Tax8_GLAC, " & _
                     " TaxM9.Tax_Liability_Account as Tax9_GLAC,TaxM10.Tax_Liability_Account as Tax10_GLAC, " & _
                     " TaxM1.Tax_Net_Payable as Tax1_GLAC_Payable,TaxM2.Tax_Net_Payable as Tax2_GLAC_Payable, " & _
                     " TaxM3.Tax_Net_Payable as Tax3_GLAC_Payable,TaxM4.Tax_Net_Payable as Tax4_GLAC_Payable, " & _
                     " TaxM5.Tax_Net_Payable as Tax5_GLAC_Payable,TaxM6.Tax_Net_Payable as Tax6_GLAC_Payable, " & _
                     " TaxM7.Tax_Net_Payable as Tax7_GLAC_Payable,TaxM8.Tax_Net_Payable as Tax8_GLAC_Payable, " & _
                     " TaxM9.Tax_Net_Payable as Tax9_GLAC_Payable,TaxM10.Tax_Net_Payable as Tax10_GLAC_Payable from TSPL_CSA_TRANSFER_HEAD doc " & _
                     " left join TSPL_TAX_MASTER TaxM1 on Doc.TAX1=TaxM1.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM2 on Doc.TAX2=TaxM2.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM3 on Doc.TAX3=TaxM3.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM4 on Doc.TAX4=TaxM4.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM5 on Doc.TAX5=TaxM5.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM6 on Doc.TAX6=TaxM6.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM7 on Doc.TAX7=TaxM7.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM8 on Doc.TAX8=TaxM8.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM9 on Doc.TAX9=TaxM9.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM10 on Doc.TAX10=TaxM10.Tax_Code where doc.Doc_code='" & obj.DOC_CODE & "'"
            Dim dtTax As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtTax.Rows.Count = 0 Then
                Throw New Exception("Tax details of transfer document not found.")
            End If
            Dim TAX1_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax1_GLAC"))
            Dim TAX2_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax2_GLAC"))
            Dim TAX3_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax3_GLAC"))
            Dim TAX4_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax4_GLAC"))
            Dim TAX5_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax5_GLAC"))
            Dim TAX6_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax6_GLAC"))
            Dim TAX7_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax7_GLAC"))
            Dim TAX8_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax8_GLAC"))
            Dim TAX9_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax9_GLAC"))
            Dim TAX10_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax10_GLAC"))

            'Dim TAX1_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX1_GLAC_Payable"))
            'Dim TAX2_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX2_GLAC_Payable"))
            'Dim TAX3_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX3_GLAC_Payable"))
            'Dim TAX4_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX4_GLAC_Payable"))
            'Dim TAX5_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX5_GLAC_Payable"))
            'Dim TAX6_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX6_GLAC_Payable"))
            'Dim TAX7_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX7_GLAC_Payable"))
            'Dim TAX8_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX8_GLAC_Payable"))
            'Dim TAX9_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX9_GLAC_Payable"))
            'Dim TAX10_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX10_GLAC_Payable"))

            '' validation for gl
            If obj.TAX1_Amt > 0 Then
                If clsCommon.myLen(TAX1_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX1)
                End If
                'If clsCommon.myLen(TAX1_GLAC_Payable) <= 0 Then
                '    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX1)
                'End If
            End If
            If obj.TAX2_Amt > 0 Then
                If clsCommon.myLen(TAX2_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX2)
                End If
                'If clsCommon.myLen(TAX2_GLAC_Payable) <= 0 Then
                '    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX2)
                'End If
            End If
            If obj.TAX3_Amt > 0 Then
                If clsCommon.myLen(TAX3_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX3)
                End If
                'If clsCommon.myLen(TAX3_GLAC_Payable) <= 0 Then
                '    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX3)
                'End If
            End If
            If obj.TAX4_Amt > 0 Then
                If clsCommon.myLen(TAX4_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX4)
                End If
                'If clsCommon.myLen(TAX4_GLAC_Payable) <= 0 Then
                '    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX4)
                'End If
            End If
            If obj.TAX5_Amt > 0 Then
                If clsCommon.myLen(TAX5_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX5)
                End If
                'If clsCommon.myLen(TAX5_GLAC_Payable) <= 0 Then
                '    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX5)
                'End If
            End If
            If obj.TAX6_Amt > 0 Then
                If clsCommon.myLen(TAX6_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX6)
                End If
                'If clsCommon.myLen(TAX6_GLAC_Payable) <= 0 Then
                '    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX6)
                'End If
            End If

            If obj.TAX7_Amt > 0 Then
                If clsCommon.myLen(TAX7_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX7)
                End If
                'If clsCommon.myLen(TAX7_GLAC_Payable) <= 0 Then
                '    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX7)
                'End If
            End If

            If obj.TAX8_Amt > 0 Then
                If clsCommon.myLen(TAX8_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX8)
                End If
                'If clsCommon.myLen(TAX8_GLAC_Payable) <= 0 Then
                '    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX8)
                'End If
            End If

            If obj.TAX9_Amt > 0 Then
                If clsCommon.myLen(TAX9_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX9)
                End If
                'If clsCommon.myLen(TAX9_GLAC_Payable) <= 0 Then
                '    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX9)
                'End If
            End If


            If obj.TAX10_Amt > 0 Then
                If clsCommon.myLen(TAX10_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX10)
                End If
                'If clsCommon.myLen(TAX10_GLAC_Payable) <= 0 Then
                '    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX10)
                'End If
            End If
            '' taxes - from locaton
            If obj.TAX1_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX1_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX1_Amt}
                ArryLstGLAC.Add(accDR)

                ' ''debit
                'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX1_GLAC_Payable, strLocationSegment, True, trans)
                'Dim accCR() As String = {strTemp, obj.TAX1_Amt}
                'ArryLstGLAC.Add(accCR)
            End If
            If obj.TAX2_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX2_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX2_Amt}
                ArryLstGLAC.Add(accDR)

                ' ''debit
                'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX2_GLAC_Payable, strLocationSegment, True, trans)
                'Dim accCR() As String = {strTemp, obj.TAX2_Amt}
                'ArryLstGLAC.Add(accCR)
            End If
            If obj.TAX3_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX3_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX3_Amt}
                ArryLstGLAC.Add(accDR)

                ' ''debit
                'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX3_GLAC_Payable, strLocationSegment, True, trans)
                'Dim accCR() As String = {strTemp, obj.TAX3_Amt}
                'ArryLstGLAC.Add(accCR)
            End If
            If obj.TAX4_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX4_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX4_Amt}
                ArryLstGLAC.Add(accDR)

                ' ''debit
                'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX4_GLAC_Payable, strLocationSegment, True, trans)
                'Dim accCR() As String = {strTemp, obj.TAX4_Amt}
                'ArryLstGLAC.Add(accCR)
            End If
            If obj.TAX5_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX5_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX5_Amt}
                ArryLstGLAC.Add(accDR)

                ' ''debit
                'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX5_GLAC_Payable, strLocationSegment, True, trans)
                'Dim accCR() As String = {strTemp, obj.TAX5_Amt}
                'ArryLstGLAC.Add(accCR)
            End If
            If obj.TAX6_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX6_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, obj.TAX6_Amt}
                ArryLstGLAC.Add(accDR)

                ' ''debit
                'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX6_GLAC_Payable, strLocationSegment, True, trans)
                'Dim accCR() As String = {strTemp, obj.TAX6_Amt}
                'ArryLstGLAC.Add(accCR)
            End If

            If obj.TAX7_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX7_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX7_Amt}
                ArryLstGLAC.Add(accDR)

                ' ''debit
                'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX7_GLAC_Payable, strLocationSegment, True, trans)
                'Dim accCR() As String = {strTemp, obj.TAX7_Amt}
                'ArryLstGLAC.Add(accCR)
            End If

            If obj.TAX8_Amt > 0 Then
                '' credi
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX8_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX8_Amt}
                ArryLstGLAC.Add(accDR)

                ' ''debit
                'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX8_GLAC_Payable, strLocationSegment, True, trans)
                'Dim accCR() As String = {strTemp, obj.TAX8_Amt}
                'ArryLstGLAC.Add(accCR)
            End If

            If obj.TAX9_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX9_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX9_Amt}
                ArryLstGLAC.Add(accDR)

                ' ''debit
                'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX9_GLAC_Payable, strLocationSegment, True, trans)
                'Dim accCR() As String = {strTemp, obj.TAX9_Amt}
                'ArryLstGLAC.Add(accCR)
            End If


            If obj.TAX10_Amt > 0 Then
                '' credi
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX10_GLAC, strLocationSegment, False, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX10_Amt}
                ArryLstGLAC.Add(accDR)

                ' ''debit
                'strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX10_GLAC_Payable, strLocationSegment, True, trans)
                'Dim accCR() As String = {strTemp, obj.TAX10_Amt}
                'ArryLstGLAC.Add(accCR)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    ''----------


    Public Shared Function JE_Excisable_Common(ByVal obj As clsCSATransfer, ByVal ArryLstGLAC As ArrayList, ByVal strLocationSegment As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            qry = " select Doc.doc_code as Document_No,TaxM1.Tax_Liability_Account as Tax1_GLAC,TaxM2.Tax_Liability_Account as Tax2_GLAC," & _
                     " TaxM3.Tax_Liability_Account as Tax3_GLAC,TaxM4.Tax_Liability_Account as Tax4_GLAC," & _
                     " TaxM5.Tax_Liability_Account as Tax5_GLAC,TaxM6.Tax_Liability_Account as Tax6_GLAC, " & _
                     " TaxM7.Tax_Liability_Account as Tax7_GLAC,TaxM8.Tax_Liability_Account as Tax8_GLAC, " & _
                     " TaxM9.Tax_Liability_Account as Tax9_GLAC,TaxM10.Tax_Liability_Account as Tax10_GLAC, " & _
                     " TaxM1.Tax_Net_Payable as Tax1_GLAC_Payable,TaxM2.Tax_Net_Payable as Tax2_GLAC_Payable, " & _
                     " TaxM3.Tax_Net_Payable as Tax3_GLAC_Payable,TaxM4.Tax_Net_Payable as Tax4_GLAC_Payable, " & _
                     " TaxM5.Tax_Net_Payable as Tax5_GLAC_Payable,TaxM6.Tax_Net_Payable as Tax6_GLAC_Payable, " & _
                     " TaxM7.Tax_Net_Payable as Tax7_GLAC_Payable,TaxM8.Tax_Net_Payable as Tax8_GLAC_Payable, " & _
                     " TaxM9.Tax_Net_Payable as Tax9_GLAC_Payable,TaxM10.Tax_Net_Payable as Tax10_GLAC_Payable from TSPL_CSA_TRANSFER_HEAD doc " & _
                     " left join TSPL_TAX_MASTER TaxM1 on Doc.TAX1=TaxM1.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM2 on Doc.TAX2=TaxM2.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM3 on Doc.TAX3=TaxM3.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM4 on Doc.TAX4=TaxM4.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM5 on Doc.TAX5=TaxM5.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM6 on Doc.TAX6=TaxM6.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM7 on Doc.TAX7=TaxM7.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM8 on Doc.TAX8=TaxM8.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM9 on Doc.TAX9=TaxM9.Tax_Code " & _
                     " left join TSPL_TAX_MASTER TaxM10 on Doc.TAX10=TaxM10.Tax_Code where doc.Doc_code='" & obj.DOC_CODE & "'"
            Dim dtTax As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtTax.Rows.Count = 0 Then
                Throw New Exception("Tax details of transfer document not found.")
            End If
            Dim TAX1_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax1_GLAC"))
            Dim TAX2_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax2_GLAC"))
            Dim TAX3_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax3_GLAC"))
            Dim TAX4_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax4_GLAC"))
            Dim TAX5_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax5_GLAC"))
            Dim TAX6_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax6_GLAC"))
            Dim TAX7_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax7_GLAC"))
            Dim TAX8_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax8_GLAC"))
            Dim TAX9_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax9_GLAC"))
            Dim TAX10_GLAC As String = clsCommon.myCstr(dtTax.Rows(0).Item("Tax10_GLAC"))

            Dim TAX1_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX1_GLAC_Payable"))
            Dim TAX2_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX2_GLAC_Payable"))
            Dim TAX3_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX3_GLAC_Payable"))
            Dim TAX4_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX4_GLAC_Payable"))
            Dim TAX5_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX5_GLAC_Payable"))
            Dim TAX6_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX6_GLAC_Payable"))
            Dim TAX7_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX7_GLAC_Payable"))
            Dim TAX8_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX8_GLAC_Payable"))
            Dim TAX9_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX9_GLAC_Payable"))
            Dim TAX10_GLAC_Payable As String = clsCommon.myCstr(dtTax.Rows(0).Item("TAX10_GLAC_Payable"))

            '' validation for gl
            If obj.TAX1_Amt > 0 Then
                If clsCommon.myLen(TAX1_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX1)
                End If
                If clsCommon.myLen(TAX1_GLAC_Payable) <= 0 Then
                    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX1)
                End If
            End If
            If obj.TAX2_Amt > 0 Then
                If clsCommon.myLen(TAX2_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX2)
                End If
                If clsCommon.myLen(TAX2_GLAC_Payable) <= 0 Then
                    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX2)
                End If
            End If
            If obj.TAX3_Amt > 0 Then
                If clsCommon.myLen(TAX3_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX3)
                End If
                If clsCommon.myLen(TAX3_GLAC_Payable) <= 0 Then
                    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX3)
                End If
            End If
            If obj.TAX4_Amt > 0 Then
                If clsCommon.myLen(TAX4_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX4)
                End If
                If clsCommon.myLen(TAX4_GLAC_Payable) <= 0 Then
                    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX4)
                End If
            End If
            If obj.TAX5_Amt > 0 Then
                If clsCommon.myLen(TAX5_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX5)
                End If
                If clsCommon.myLen(TAX5_GLAC_Payable) <= 0 Then
                    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX5)
                End If
            End If
            If obj.TAX6_Amt > 0 Then
                If clsCommon.myLen(TAX6_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX6)
                End If
                If clsCommon.myLen(TAX6_GLAC_Payable) <= 0 Then
                    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX6)
                End If
            End If

            If obj.TAX7_Amt > 0 Then
                If clsCommon.myLen(TAX7_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX7)
                End If
                If clsCommon.myLen(TAX7_GLAC_Payable) <= 0 Then
                    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX7)
                End If
            End If

            If obj.TAX8_Amt > 0 Then
                If clsCommon.myLen(TAX8_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX8)
                End If
                If clsCommon.myLen(TAX8_GLAC_Payable) <= 0 Then
                    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX8)
                End If
            End If

            If obj.TAX9_Amt > 0 Then
                If clsCommon.myLen(TAX9_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX9)
                End If
                If clsCommon.myLen(TAX9_GLAC_Payable) <= 0 Then
                    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX9)
                End If
            End If


            If obj.TAX10_Amt > 0 Then
                If clsCommon.myLen(TAX10_GLAC) <= 0 Then
                    Throw New Exception("Tax Liability Acount not found for" + obj.TAX10)
                End If
                If clsCommon.myLen(TAX10_GLAC_Payable) <= 0 Then
                    Throw New Exception("Tax Net Payable Acount not found for" + obj.TAX10)
                End If
            End If
            '' taxes - from locaton
            If obj.TAX1_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX1_GLAC, strLocationSegment, True, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX1_Amt}
                ArryLstGLAC.Add(accDR)

                ''debit
                strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX1_GLAC_Payable, strLocationSegment, True, trans)
                Dim accCR() As String = {strTemp, obj.TAX1_Amt}
                ArryLstGLAC.Add(accCR)
            End If
            If obj.TAX2_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX2_GLAC, strLocationSegment, True, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX2_Amt}
                ArryLstGLAC.Add(accDR)

                ''debit
                strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX2_GLAC_Payable, strLocationSegment, True, trans)
                Dim accCR() As String = {strTemp, obj.TAX2_Amt}
                ArryLstGLAC.Add(accCR)
            End If
            If obj.TAX3_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX3_GLAC, strLocationSegment, True, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX3_Amt}
                ArryLstGLAC.Add(accDR)

                ''debit
                strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX3_GLAC_Payable, strLocationSegment, True, trans)
                Dim accCR() As String = {strTemp, obj.TAX3_Amt}
                ArryLstGLAC.Add(accCR)
            End If
            If obj.TAX4_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX4_GLAC, strLocationSegment, True, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX4_Amt}
                ArryLstGLAC.Add(accDR)

                ''debit
                strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX4_GLAC_Payable, strLocationSegment, True, trans)
                Dim accCR() As String = {strTemp, obj.TAX4_Amt}
                ArryLstGLAC.Add(accCR)
            End If
            If obj.TAX5_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX5_GLAC, strLocationSegment, True, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX5_Amt}
                ArryLstGLAC.Add(accDR)

                ''debit
                strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX5_GLAC_Payable, strLocationSegment, True, trans)
                Dim accCR() As String = {strTemp, obj.TAX5_Amt}
                ArryLstGLAC.Add(accCR)
            End If
            If obj.TAX6_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX6_GLAC, strLocationSegment, True, trans)
                Dim accDR() As String = {strTemp, obj.TAX6_Amt}
                ArryLstGLAC.Add(accDR)

                ''debit
                strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX6_GLAC_Payable, strLocationSegment, True, trans)
                Dim accCR() As String = {strTemp, obj.TAX6_Amt}
                ArryLstGLAC.Add(accCR)
            End If

            If obj.TAX7_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX7_GLAC, strLocationSegment, True, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX7_Amt}
                ArryLstGLAC.Add(accDR)

                ''debit
                strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX7_GLAC_Payable, strLocationSegment, True, trans)
                Dim accCR() As String = {strTemp, obj.TAX7_Amt}
                ArryLstGLAC.Add(accCR)
            End If

            If obj.TAX8_Amt > 0 Then
                '' credi
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX8_GLAC, strLocationSegment, True, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX8_Amt}
                ArryLstGLAC.Add(accDR)

                ''debit
                strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX8_GLAC_Payable, strLocationSegment, True, trans)
                Dim accCR() As String = {strTemp, obj.TAX8_Amt}
                ArryLstGLAC.Add(accCR)
            End If

            If obj.TAX9_Amt > 0 Then
                '' credit
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX9_GLAC, strLocationSegment, True, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX9_Amt}
                ArryLstGLAC.Add(accDR)

                ''debit
                strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX9_GLAC_Payable, strLocationSegment, True, trans)
                Dim accCR() As String = {strTemp, obj.TAX9_Amt}
                ArryLstGLAC.Add(accCR)
            End If


            If obj.TAX10_Amt > 0 Then
                '' credi
                Dim strTemp As String = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX10_GLAC, strLocationSegment, True, trans)
                Dim accDR() As String = {strTemp, -1 * obj.TAX10_Amt}
                ArryLstGLAC.Add(accDR)

                ''debit
                strTemp = clsERPFuncationality.ChangeGLAccountLocationSegment(TAX10_GLAC_Payable, strLocationSegment, True, trans)
                Dim accCR() As String = {strTemp, obj.TAX10_Amt}
                ArryLstGLAC.Add(accCR)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("CSA Transfer No not found to Delete")
        End If
        Dim obj As clsCSATransfer = clsCSATransfer.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSATransfer, obj.From_Location_Code, obj.Transfer_Date, trans)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.DOC_CODE) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_CSA_TRANSFER_DETAIL where DOC_CODE='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                clsBatchInventory.DeleteData("SD-CSATRANS", obj.DOC_CODE, trans)

                qry = "delete from TSPL_CSA_TRANSFER_HEAD where DOC_CODE='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)

                'qry = "update TSPL_VEHICLE_MASTER set InOut='I' where vehicle_id='" + obj.Vehicle_code + "'"
                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

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
    Public Shared Function IsValidCustomer(ByVal ArrPONo As List(Of String), ByVal strVendorCode As String) As Boolean
        If ArrPONo IsNot Nothing AndAlso ArrPONo.Count > 0 Then
            Dim qry As String = "select TSPL_CSA_TRANSFER_HEAD.DOC_CODE,TSPL_CSA_TRANSFER_HEAD.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_CSA_TRANSFER_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSA_TRANSFER_HEAD.Cust_Code where DOC_CODE  in (" + clsCommon.GetMulcallString(ArrPONo) + ") and Cust_Code not in ('" + strVendorCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "Order No:" + clsCommon.myCstr(dr("DOC_CODE")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Cust_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Customer_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function
    Public Shared Function IsValidTaxGroupForPO(ByVal ArrPONo As List(Of String), ByVal strTaxGroupCode As String) As Boolean
        If ArrPONo IsNot Nothing AndAlso ArrPONo.Count > 0 Then
            Dim qry As String = "select DOC_CODE,Tax_Group from TSPL_CSA_TRANSFER_HEAD where DOC_CODE  in (" + clsCommon.GetMulcallString(ArrPONo) + ") and Tax_Group not in ('" + strTaxGroupCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "PO No:" + clsCommon.myCstr(dr("DOC_CODE")) + " .Tax Group is: " + clsCommon.myCstr(dr("Tax_Group"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function
    Public Shared Function IsValidProjectForSO(ByVal strPONo As String, ByVal strProject As String) As String
        Dim strProj As String = clsDBFuncationality.getSingleValue("select PROJECT_ID from TSPL_CSA_TRANSFER_HEAD where DOC_CODE ='" + strPONo + "'")
        Return strProj
    End Function
    Public Shared Function UpdateCustomerAfterSavePost(ByVal obj As clsCSATransfer) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim qry As String = ""
            '' check for sale/patti is created
            qry = "select COUNT(*) from TSPL_CSA_SALE_TRANSFER_DETAIL where Against_Transfer_Code='" & obj.DOC_CODE & "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                Throw New Exception("Sale Patti Created for CSA Transfer Doc-" & obj.DOC_CODE & "")
            End If

            '' check for CSA Transfer Return is created
            qry = " select count(*) from TSPL_SD_SALE_RETURN_HEAD CSARH " & _
                  " inner join TSPL_SD_SALE_RETURN_detail CSARD on CSARH.Document_Code=CSARD.DOCUMENT_CODE " & _
                  " where CSARH.Trans_Type='CSA' and CSARD.Transfer_No='" & obj.DOC_CODE & "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                Throw New Exception("CSA Transfer Return Created for CSA Transfer Doc-" & obj.DOC_CODE & "")
            End If
            '' update delevery order docs
            Dim To_Loc_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Location_Code from  TSPL_LOCATION_MASTER where Cust_Code='" & obj.Cust_Code & "'", trans))
            qry = "update TSPL_CSA_DO_HEAD set Cust_Code='" & obj.Cust_Code & "', To_Location_Code = '" & To_Loc_Code & "' where Doc_No='" & obj.DELEVERY_ORDER_NO & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = " update TSPL_INVENTORY_MOVEMENT set Cust_Code='" & obj.Cust_Code & "',Cust_Name='" & obj.Customer_Name & "',Location_Code=(case when InOut='O' then Location_Code else '" & To_Loc_Code & "' end),Other_Location_Code=(case when InOut='O' then Location_Code else '" & To_Loc_Code & "' end),Other_Location_Desc=(case when InOut='O' then Other_Location_Desc else '" & clsLocation.GetName(To_Loc_Code, trans) & "' end) where Source_Doc_No='" & obj.DOC_CODE & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = " update TSPL_INVENTORY_MOVEMENT_NEW set Cust_Code='" & obj.Cust_Code & "',Cust_Name='" & obj.Customer_Name & "',Location_Code=(case when InOut='O' then Location_Code else '" & To_Loc_Code & "' end), Other_Location_Code=(case when InOut='O' then Location_Code else '" & To_Loc_Code & "' end),Other_Location_Desc=(case when InOut='O' then Other_Location_Desc else '" & clsLocation.GetName(To_Loc_Code, trans) & "' end) where Source_Doc_No='" & obj.DOC_CODE & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = " update TSPL_JOURNAL_MASTER set CustVend_Code='" & obj.Cust_Code & "',CustVend_Name='" & obj.Customer_Name & "' where Source_Code='CS-TR' and  Source_Doc_No='" & obj.DOC_CODE & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = " update TSPL_JOURNAL_DETAILS set CustVend_Code='" & obj.Cust_Code & "',CustVend_Name='" & obj.Customer_Name & "' where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='CS-TR' and  Source_Doc_No='" & obj.DOC_CODE & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_CSA_TRANSFER_HEAD set Cust_Code='" & obj.Cust_Code & "',To_Location_Code = '" & To_Loc_Code & "' where DOC_CODE='" & obj.DOC_CODE & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsCSATransferDetail
#Region "Variables"
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public MRP As Decimal = Nothing
    Public Is_MRP_Mandatory As Integer = Nothing
    Public Abatement_Pers As Decimal = Nothing
    Public Abatement_Amt As Decimal = Nothing
    Public CSA_Commission_RS_PERS As String = Nothing
    Public Item_Unit_Wt As Decimal = Nothing
    Public Item_Net_Wt As Decimal = Nothing
    Public Item_Net_MT_Wt As Decimal = Nothing
    Public DOC_CODE As String = Nothing
    Public Line_No As Integer = 0
    Public Including_Tax As String = Nothing
    Public Calc_Type As String = Nothing
    Public CSA_Type As String = Nothing
    Public Status As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0 '
    Public Alt_Unit_Code As String = Nothing
    Public Balance_Qty As Double = 0 '
    Public Unit_code As String = Nothing '
    Public Location As String = Nothing '
    Public LocationName As String = Nothing 'Not a Table Field
    Public Unit_Rate As Double = 0 '
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
    Public Item_Net_Amt As Double = 0
    Public Total_Basic_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0

    Public Item_Pack_Size As Double = 0
    Public Item_Master_Pack_Size As Double = 0
    Public Commision_Rate As Double = 0
    Public Other_Chrage As Double = 0
    Public Commission_Chrage As Double = 0
    Public Transfer_Rate As Double = 0

    Public Scheme_Applicable As String = Nothing
    Public FOC As String = Nothing
    Public Scheme_Code As String = Nothing
    Public Scheme_Type As String = Nothing
    Public Scheme_Item_Code As String = Nothing
    Public Scheme_Qty As Decimal = Nothing
    Public Scheme_Item_UOM As String = Nothing
    Public Cash_Scheme_Code As String = Nothing
    Public Cash_Scheme_Type As String = Nothing
    Public Cash_Scheme_Pers As Decimal = Nothing
    Public Cash_Scheme_Amount As Decimal = Nothing

    Public DELEVERY_ORDER_NO As String = Nothing
    Public DO_Qty As Decimal = Nothing
    Public DO_Pending_Qty As Decimal = Nothing
#End Region

    ''Note Very Important If any change mad in PO Head or PO Detail table allso update it's History table.
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strDocDate As DateTime, ByVal strFromLocation As String, ByVal strToLocation As String, ByVal Arr As List(Of clsCSATransferDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCSATransferDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Including_Tax", obj.Including_Tax)
                clsCommon.AddColumnsForChange(coll, "Calc_Type", obj.Calc_Type)

                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Item_Unit_Wt", obj.Item_Unit_Wt)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Wt", obj.Item_Net_Wt)
                clsCommon.AddColumnsForChange(coll, "Item_Net_MT_Wt", obj.Item_Net_MT_Wt)
                clsCommon.AddColumnsForChange(coll, "Alt_Unit_Code", obj.Alt_Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                'clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Unit_Rate", obj.Unit_Rate)

                clsCommon.AddColumnsForChange(coll, "DO_Pending_Qty", obj.DO_Pending_Qty)
                clsCommon.AddColumnsForChange(coll, "DO_Qty", obj.DO_Qty)
                clsCommon.AddColumnsForChange(coll, "DELEVERY_ORDER_NO", obj.DELEVERY_ORDER_NO, True)

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

                clsCommon.AddColumnsForChange(coll, "Item_Pack_Size", obj.Item_Pack_Size)
                clsCommon.AddColumnsForChange(coll, "Commision_Rate", obj.Commision_Rate)
                clsCommon.AddColumnsForChange(coll, "Other_Chrage", obj.Other_Chrage)
                clsCommon.AddColumnsForChange(coll, "Commission_Chrage", obj.Commission_Chrage)
                clsCommon.AddColumnsForChange(coll, "CSA_Commission_RS_PERS", obj.CSA_Commission_RS_PERS)
                clsCommon.AddColumnsForChange(coll, "Transfer_Rate", obj.Transfer_Rate)

                clsCommon.AddColumnsForChange(coll, "Amount", obj.Total_Basic_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Master_Pack_Size", obj.Item_Master_Pack_Size)

                clsCommon.AddColumnsForChange(coll, "Scheme_Applicable", obj.Scheme_Applicable)
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_Code", obj.Scheme_Item_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_UOM", obj.Scheme_Item_UOM)
                clsCommon.AddColumnsForChange(coll, "Scheme_Qty", obj.Scheme_Qty)
                clsCommon.AddColumnsForChange(coll, "Scheme_Type", obj.Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "FOC", obj.FOC)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Code", obj.Cash_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Type", obj.Cash_Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Pers", obj.Cash_Scheme_Pers)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Amount", obj.Cash_Scheme_Amount)

                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Is_MRP_Mandatory", obj.Is_MRP_Mandatory)
                clsCommon.AddColumnsForChange(coll, "Abatement_Pers", obj.Abatement_Pers)
                clsCommon.AddColumnsForChange(coll, "Abatement_Amt", obj.Abatement_Amt)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_TRANSFER_DETAIL", OMInsertOrUpdate.Insert, "", trans)


                If obj.arrBatchItem IsNot Nothing AndAlso obj.arrBatchItem.Count > 0 Then
                    clsBatchInventory.SaveData("SD-CSATRANS", strDocNo, strDocDate, "O", obj.Item_Code, strFromLocation, obj.Line_No, obj.MRP, obj.Unit_code, obj.arrBatchItem, trans)
                    clsBatchInventory.SaveData("SD-CSATRANS", strDocNo, strDocDate, "I", obj.Item_Code, strToLocation, obj.Line_No, obj.MRP, obj.Unit_code, obj.arrBatchItem, trans)
                End If

            Next
        End If
        Return True
    End Function

End Class

Public Class clsSchemeApplyOnDairy
#Region "Variables"
    Public schm_Type As String = Nothing
    Public Schm_Code As String = Nothing
    Public Schm_Item_CSA_Type As String = Nothing
    Public Schm_Icode As String = Nothing
    Public Schm_Iname As String = Nothing
    Public Schm_Item_Uom As String = Nothing
    Public Schm_IUnit_Rate As Decimal = Nothing
    Public Schm_Qty As Decimal = Nothing

    Public Cash_Pers As Decimal = Nothing
    Public Cash_Amt As Decimal = Nothing

    Public Arr As List(Of clsSchemeApplyOnDairy) = Nothing
#End Region

    Public Shared Function GetSchemeData(ByVal Main_Item_Code As String, ByVal Main_Item_Unit As String, ByVal Main_Item_Qty As Decimal, ByVal Cust_Code As String, ByVal Scheme_Type As String, Optional ByVal Return_Scheme_Code As String = Nothing, Optional ByVal DOCdate As Date? = Nothing, Optional ByVal trans As SqlTransaction = Nothing) As clsSchemeApplyOnDairy
        Try
            Dim MainQty As Decimal = Nothing
            Dim MainUOM As String = Nothing
            Dim freeqty As Decimal = Nothing
            Dim count As Integer = 0
            Dim wt_unit As String = Nothing
            Dim wt_qty As Double = Nothing
            Dim obj As New clsSchemeApplyOnDairy()
            obj.Arr = New List(Of clsSchemeApplyOnDairy)

            Dim Product_Type As String = clsCommon.myCstr(clsItemMaster.GetItemProductType(Main_Item_Code, trans))
            If Not DOCdate.HasValue Then
                DOCdate = clsCommon.GETSERVERDATE(trans)
            End If
            'Dim qry As String = "select TSPL_SCHEME_MASTER_NEW.Apply_Slab,TSPL_SCHEME_MASTER_NEW.Scheme_Code,TSPL_SCHEME_MASTER_NEW.Scheme_Type,TSPL_SCHEME_DETAIL_NEW.MainItem_Code,TSPL_SCHEME_DETAIL_NEW.MainQty,TSPL_SCHEME_DETAIL_NEW.MainUnit_Code,TSPL_SCHEME_DETAIL_NEW.Item_Code as free_item,TSPL_SCHEME_DETAIL_NEW.Qty as free_qty,TSPL_SCHEME_DETAIL_NEW.Unit_Code as free_uom,TSPL_SCHEME_MASTER_NEW.Quantitive_Type,TSPL_SCHEME_DETAIL_NEW.Max_Limit,TSPL_SCHEME_DETAIL_NEW.Increment_Value from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code "
            'qry += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code "
            'If clsCommon.myLen(Return_Scheme_Code) > 0 Then
            '    qry += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in ('" + Return_Scheme_Code + "')   and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + Main_Item_Code + "' "
            'Else
            '    qry += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by MaxlimitStart_Date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from "
            '    qry += " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type<>'Cash' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date<='" & clsCommon.GetPrintDate(DOCdate, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.MaxlimitEnd_Date >= '" & clsCommon.GetPrintDate(DOCdate, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.MaxlimitEnd_Date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from "
            '    qry += " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + Main_Item_Code + "' and Cust_Code='" + Cust_Code + "'))a where a.sno=1)"
            '    qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'"
            '    qry += " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + Main_Item_Code + "' "
            '    If (Scheme_Type Is Nothing OrElse clsCommon.myLen(Scheme_Type) <= 0) AndAlso IsVolumeDefaultScheme(trans) Then
            '        count = clsDBFuncationality.getSingleValue("select count(*) from (" + qry + " and TSPL_SCHEME_MASTER_NEW.Scheme_Type='Volume')axa", trans)
            '    End If
            '    If count = 1 Then
            '        qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Type='Volume'"
            '    Else
            '        If clsCommon.myLen(Scheme_Type) > 0 AndAlso clsCommon.CompairString(Scheme_Type, "Quantitive") = CompairStringResult.Equal Then
            '            qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive'"
            '        End If
            '        If clsCommon.myLen(Scheme_Type) > 0 AndAlso clsCommon.CompairString(Scheme_Type, "Volume") = CompairStringResult.Equal Then
            '            qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Type='Volume'"
            '        End If

            '    End If
            'End If

            Dim qry As String = "select TSPL_SCHEME_MASTER_NEW.Apply_Slab,TSPL_SCHEME_MASTER_NEW.Scheme_Code,TSPL_SCHEME_MASTER_NEW.Scheme_Type,TSPL_SCHEME_DETAIL_NEW.MainItem_Code,TSPL_SCHEME_DETAIL_NEW.MainQty,TSPL_SCHEME_DETAIL_NEW.MainUnit_Code,TSPL_SCHEME_DETAIL_NEW.Item_Code as free_item,TSPL_SCHEME_DETAIL_NEW.Qty as free_qty,TSPL_SCHEME_DETAIL_NEW.Unit_Code as free_uom,TSPL_SCHEME_MASTER_NEW.Quantitive_Type,TSPL_SCHEME_DETAIL_NEW.Max_Limit,TSPL_SCHEME_DETAIL_NEW.Increment_Value from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code "
            qry += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code "
            If clsCommon.myLen(Return_Scheme_Code) > 0 Then
                qry += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in ('" + Return_Scheme_Code + "')  and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + Main_Item_Code + "'  "
            Else
                qry += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by MaxlimitStart_Date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from "
                qry += " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type<>'Cash' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date<='" & clsCommon.GetPrintDate(DOCdate, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.MaxlimitEnd_Date >= '" & clsCommon.GetPrintDate(DOCdate, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.MaxlimitEnd_Date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from "
                qry += " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + Main_Item_Code + "' and Cust_Code='" + Cust_Code + "'))a where a.sno=1)"
                qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'"
                qry += " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + Main_Item_Code + "' "
                If (Scheme_Type Is Nothing OrElse clsCommon.myLen(Scheme_Type) <= 0) AndAlso IsVolumeDefaultScheme(trans) Then
                    count = clsDBFuncationality.getSingleValue("select count(*) from (" + qry + " and TSPL_SCHEME_MASTER_NEW.Scheme_Type='Volume')axa", trans)
                End If
                If count = 1 Then
                    qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Type='Volume'"
                Else
                    If clsCommon.myLen(Scheme_Type) > 0 AndAlso clsCommon.CompairString(Scheme_Type, "Quantitive") = CompairStringResult.Equal Then
                        qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive'"
                    End If
                    If clsCommon.myLen(Scheme_Type) > 0 AndAlso clsCommon.CompairString(Scheme_Type, "Volume") = CompairStringResult.Equal Then
                        qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Type='Volume'"
                    End If

                End If
            End If

            qry += " order by TSPL_SCHEME_MASTER_NEW.Scheme_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Dim mode As Decimal = 0
            Dim Arr_Temp As New List(Of clsSchemeApplyOnDairy)
            Dim chk As Integer = 0
            Arr_Temp = New List(Of clsSchemeApplyOnDairy)
            Dim oldSchemeType As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsSchemeApplyOnDairy()
                    MainQty = clsCommon.myCstr(dr("mainqty"))
                    MainUOM = clsCommon.myCstr(dr("mainunit_code"))
                    objtr.schm_Type = clsCommon.myCstr(dr("Scheme_Type"))
                    If chk = 0 Then
                        oldSchemeType = objtr.schm_Type
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(dr("Scheme_Type")), "Quantitive") = CompairStringResult.Equal Then
                        objtr.Schm_Code = clsCommon.myCstr(dr("Scheme_Code"))

                        If clsCommon.myCdbl(dr("Apply_Slab")) = 1 Then
                            objtr.Schm_Qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select top 1 value from TSPL_SCHEME_MASTER_NEW_SLAB where Scheme_Code='" + clsCommon.myCstr(dr("Scheme_Code")) + "' and Min_Range<=" + clsCommon.myCstr(Main_Item_Qty) + " order by Min_Range DESC", trans))
                        Else
                            If clsCommon.myCdbl(dr("Quantitive_Type")) = 1 Then ''For Max Limit
                                If Main_Item_Qty > clsCommon.myCdbl(dr("Max_Limit")) Then
                                    Main_Item_Qty = clsCommon.myCdbl(dr("Max_Limit"))
                                End If
                            End If

                            If clsCommon.myCdbl(dr("Quantitive_Type")) = 2 Then ''For Increment Value
                                objtr.Schm_Qty = 0
                                If clsCommon.CompairString(MainUOM, Main_Item_Unit) = CompairStringResult.Equal AndAlso Main_Item_Qty >= MainQty Then
                                    Dim qty As Integer = Math.Floor((Main_Item_Qty - MainQty) / clsCommon.myCdbl(dr("Increment_Value")))
                                    objtr.Schm_Qty = System.Math.Round(((1 + qty) * clsCommon.myCdbl(dr("free_qty"))), 2)
                                End If
                            Else
                                '=in qty scheme if main item unit and filled item unit is same only then free item given else no free item
                                If clsCommon.CompairString(MainUOM, Main_Item_Unit) = CompairStringResult.Equal AndAlso Main_Item_Qty >= MainQty Then
                                    mode = Main_Item_Qty Mod MainQty
                                    Dim qty As Double = Main_Item_Qty - mode
                                    qty = qty / MainQty
                                    objtr.Schm_Qty = System.Math.Round(clsCommon.myCdbl(dr("free_qty")) * qty, 2)
                                Else
                                    Dim strStockingUnit As String = clsItemMaster.GetStockUnit(Main_Item_Code, trans)
                                    Dim Cnvrsn_qty As Double = 0
                                    If clsCommon.CompairString(MainUOM, Main_Item_Unit) <> CompairStringResult.Equal Then
                                        Dim qrySchemeItemUom As String = "select CurrentUnit.conversion_factor " &
                                       " From " &
                                       " tspl_item_uom_detail LtrUnit" &
                                       " left join tspl_item_uom_detail StockUnit on StockUnit.item_code='" & Main_Item_Code & "'     and StockUnit.Stocking_Unit ='Y'" &
                                       " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code='" & Main_Item_Code & "'  " &
                                       " and   CurrentUnit.uom_code= '" & MainUOM & "' " &
                                       " where   LtrUnit.item_code='" & Main_Item_Code & "'       and LtrUnit.UOM_Code='" & strStockingUnit & "'"

                                        Dim dblSchemeItem As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qrySchemeItemUom, trans))

                                        Dim qryTransactionItemUom As String = "select convert(decimal(18,2),(" & Main_Item_Qty & "/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor) as Ltr_Qty " & _
                                     " From " & _
                                     " tspl_item_uom_detail LtrUnit" & _
                                     " left join tspl_item_uom_detail StockUnit on StockUnit.item_code='" & Main_Item_Code & "'     and StockUnit.Stocking_Unit ='Y'" & _
                                     " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code='" & Main_Item_Code & "'  " & _
                                     " and   CurrentUnit.uom_code= '" & Main_Item_Unit & "' " & _
                                     " where   LtrUnit.item_code='" & Main_Item_Code & "'       and LtrUnit.UOM_Code='" & strStockingUnit & "'"

                                        Dim TransactionItem As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qryTransactionItemUom, trans))
                                        Dim qty As Double = 0
                                        If dblSchemeItem > 0 Then
                                            qty = clsCommon.myCdbl(TransactionItem / dblSchemeItem)
                                            qty = Math.Floor(qty)
                                        End If

                                        'mode = Main_Item_Qty Mod MainQty
                                        'Dim qty As Double = Main_Item_Qty - mode
                                        'qty = qty / MainQty
                                        'objtr.Schm_Qty = System.Math.Round(clsCommon.myCdbl(dr("free_qty")) * qty, 2)


                                        If qty >= MainQty Then
                                            mode = Main_Item_Qty Mod MainQty
                                            qty = (qty - mode) / MainQty
                                            qty = CInt(qty)
                                            objtr.Schm_Qty = System.Math.Round(clsCommon.myCdbl(dr("free_qty")) * qty, 2)
                                        Else
                                            objtr.Schm_Qty = 0
                                        End If

                                    End If

                                   
                                End If
                            End If
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("Scheme_Type")), "Volume") = CompairStringResult.Equal Then
                        objtr.Schm_Code = clsCommon.myCstr(dr("Scheme_Code"))

                        '=in qty scheme if main item unit and filled item unit is same only then free item given else no free item
                        If clsCommon.CompairString(MainUOM, Main_Item_Unit) = CompairStringResult.Equal AndAlso Main_Item_Qty >= MainQty Then
                            mode = Main_Item_Qty Mod MainQty
                            Dim qty As Double = Main_Item_Qty - mode
                            qty = qty / MainQty

                            objtr.Schm_Qty = System.Math.Round(clsCommon.myCdbl(dr("free_qty")) * qty, 2)
                        Else
                            wt_qty = clsItemMaster.GetItemWeightValue(Main_Item_Code, trans)
                            wt_unit = clsItemMaster.GetItemWeightUnit(Main_Item_Code, trans)
                            Dim qty As Double = 0
                            '=============if wt uom and main item uom is match then divide main qty with wt qty and then get free qty========
                            Main_Item_Qty = Main_Item_Qty * wt_qty * clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(Main_Item_Code, Main_Item_Unit, trans))

                            Dim Cnvrsn_qty As Double = 0
                            If clsCommon.CompairString(wt_unit, MainUOM) <> CompairStringResult.Equal Then
                                '==========if not same then get conversion from wt conversion in scheme free item uom.
                                Cnvrsn_qty = System.Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select CF from (select (case when (Contained_UOM='" + MainUOM + "' and Container_UOM='" + wt_unit + "') then round(Container_Qty/Contained_Qty,4) else (case when (Contained_UOM='" + wt_unit + "' and Container_UOM='" + MainUOM + "') then round(Contained_Qty/Container_Qty,4) end)end) as CF,product_type from TSPL_WEIGHT_CONVERSION where (product_type='" + Product_Type + "' or product_type='ALL'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc", trans)), 2)
                                If Cnvrsn_qty <= 0 Then
                                    Throw New Exception("Do weight conversion for Product Type: " + ProductType(Product_Type) + " in " + clsCommon.myCstr(wt_unit) + " and " + MainUOM + " units.")
                                End If
                            Else
                                Cnvrsn_qty = 1
                            End If

                            qty = (Main_Item_Qty / Cnvrsn_qty)

                            If qty >= MainQty Then
                                mode = Main_Item_Qty Mod MainQty
                                qty = (qty - mode) / MainQty
                                qty = CInt(qty)
                                objtr.Schm_Qty = System.Math.Round(clsCommon.myCdbl(dr("free_qty")) * qty, 2)
                            Else
                                objtr.Schm_Qty = 0
                            End If

                        End If
                    End If

                    objtr.Schm_Icode = clsCommon.myCstr(dr("free_item"))
                    objtr.Schm_Iname = clsItemMaster.GetItemName(objtr.Schm_Icode, trans)
                    objtr.Schm_Item_CSA_Type = clsItemMaster.GetItemCSAType(objtr.Schm_Icode, trans)
                    objtr.Schm_Item_Uom = clsCommon.myCstr(dr("free_uom"))
                    objtr.Schm_IUnit_Rate = 0


                    '--check if volumn default set and also main item consist volumn scheme then only vol schm fill in array other wise all

                    If count > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(dr("Scheme_Type")), "Volume") = CompairStringResult.Equal AndAlso objtr.Schm_Qty > 0 Then
                        obj.Arr.Add(objtr)
                        chk += 1
                        'Exit For
                    ElseIf count <= 0 AndAlso objtr.Schm_Qty > 0 Then
                        'when both qty and vol. scheme are ther on item then only first schm. would apply not both. for this do comparision.
                        If clsCommon.CompairString(objtr.schm_Type, oldSchemeType) = CompairStringResult.Equal Then
                            obj.Arr.Add(objtr)
                            chk += 1
                        End If
                        'Exit For
                    End If

                    '=fill temp data============
                    If objtr.Schm_Qty > 0 Then
                        Arr_Temp.Add(objtr)
                    End If
                Next
            End If
            If Arr_Temp IsNot Nothing AndAlso Arr_Temp.Count > 0 AndAlso (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                obj.Arr = Arr_Temp
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetSchemeDataStructure(ByVal Main_StructureCode As String, ByVal Main_Unit As String, ByVal Main_Qty As Decimal, ByVal Customer_Code As String, ByVal DOC_Date As Date, ByVal trans As SqlTransaction) As clsSchemeApplyOnDairy
        Try
            Dim MainQty As Decimal = Nothing
            Dim MainUOM As String = Nothing
            Dim freeqty As Decimal = Nothing
            Dim count As Integer = 0
            Dim obj As New clsSchemeApplyOnDairy()
            obj.Arr = New List(Of clsSchemeApplyOnDairy)

            Dim qry As String = "select top 1 TSPL_SCHEME_MASTER_NEW.Scheme_Code,TSPL_SCHEME_MASTER_NEW.Scheme_Type,TSPL_SCHEME_MASTER_NEW.Quantitive_Type_Structure_Main_Qty,TSPL_SCHEME_MASTER_NEW.Quantitive_Type_Structure_Main_UOM,TSPL_SCHEME_MASTER_NEW.Quantitive_Type_Structure_Free_Item ,TSPL_SCHEME_MASTER_NEW.Quantitive_Type_Structure_Free_Qty" + Environment.NewLine + _
            ",TSPL_SCHEME_MASTER_NEW.Quantitive_Type_Structure_Free_UOM  " + Environment.NewLine + _
            "from TSPL_SCHEME_MASTER_NEW" + Environment.NewLine + _
            "left outer join TSPL_SCHEME_QUANTITIVE_STRUCTURE on TSPL_SCHEME_QUANTITIVE_STRUCTURE.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code" + Environment.NewLine + _
            "where 2 = 2" + Environment.NewLine + _
            "and  TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date<='" & clsCommon.GetPrintDate(DOC_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.MaxlimitEnd_Date >= '" & clsCommon.GetPrintDate(DOC_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.MaxlimitEnd_Date is null)" + Environment.NewLine + _
            "and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + Customer_Code + "') " + Environment.NewLine + _
            "and TSPL_SCHEME_MASTER_NEW.Status='Active'  and TSPL_SCHEME_QUANTITIVE_STRUCTURE.Structure_Code='" + Main_StructureCode + "' and TSPL_SCHEME_MASTER_NEW.Scheme_Type='Structure' " + Environment.NewLine + _
            "order by TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date desc  ,TSPL_SCHEME_MASTER_NEW.Scheme_Code desc"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Dim mode As Decimal = 0
            Dim Arr_Temp As New List(Of clsSchemeApplyOnDairy)

            Dim oldSchemeType As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsSchemeApplyOnDairy()
                    MainQty = clsCommon.myCstr(dr("Quantitive_Type_Structure_Main_Qty"))
                    MainUOM = clsCommon.myCstr(dr("Quantitive_Type_Structure_Main_UOM"))
                    If clsCommon.CompairString(MainUOM, Main_Unit) = CompairStringResult.Equal AndAlso Main_Qty >= MainQty Then
                        mode = Main_Qty Mod MainQty
                        Dim qty As Double = Main_Qty - mode
                        qty = qty / MainQty
                        objtr.Schm_Qty = System.Math.Round(clsCommon.myCdbl(dr("Quantitive_Type_Structure_Free_Qty")) * qty, 2)
                    Else
                        objtr.Schm_Qty = 0
                    End If
                    If objtr.Schm_Qty > 0 Then
                        objtr.schm_Type = clsCommon.myCstr(dr("Scheme_Type"))
                        objtr.Schm_Code = clsCommon.myCstr(dr("Scheme_Code"))
                        objtr.Schm_Icode = clsCommon.myCstr(dr("Quantitive_Type_Structure_Free_Item"))
                        objtr.Schm_Iname = clsItemMaster.GetItemName(objtr.Schm_Icode, trans)
                        objtr.Schm_Item_CSA_Type = clsItemMaster.GetItemCSAType(objtr.Schm_Icode, trans)
                        objtr.Schm_Item_Uom = clsCommon.myCstr(dr("Quantitive_Type_Structure_Free_UOM"))
                        objtr.Schm_IUnit_Rate = 0
                        Arr_Temp.Add(objtr)
                    End If
                Next
            End If
            If Arr_Temp IsNot Nothing AndAlso Arr_Temp.Count > 0 AndAlso (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                obj.Arr = Arr_Temp
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetSchemeDataFixed(ByVal Customer_Code As String, ByVal DOC_Date As Date, ByVal trans As SqlTransaction) As clsSchemeApplyOnDairy
        Try
            Dim MainQty As Decimal = Nothing
            Dim MainUOM As String = Nothing
            Dim freeqty As Decimal = Nothing
            Dim count As Integer = 0
            Dim obj As New clsSchemeApplyOnDairy()
            obj.Arr = New List(Of clsSchemeApplyOnDairy)

            Dim qry As String = "select TSPL_SCHEME_MASTER_NEW.Scheme_Code,TSPL_SCHEME_MASTER_NEW.Scheme_Type,TSPL_SCHEME_DETAIL_NEW.Item_Code ,TSPL_SCHEME_DETAIL_NEW.Qty ,TSPL_SCHEME_DETAIL_NEW.Unit_Code " + Environment.NewLine + _
            "from TSPL_SCHEME_MASTER_NEW" + Environment.NewLine + _
            "left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code" + Environment.NewLine + _
            "where 2 = 2" + Environment.NewLine + _
            "and  TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date<='" & clsCommon.GetPrintDate(DOC_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.MaxlimitEnd_Date >= '" & clsCommon.GetPrintDate(DOC_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.MaxlimitEnd_Date is null)" + Environment.NewLine + _
            "and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + Customer_Code + "') " + Environment.NewLine + _
            "and TSPL_SCHEME_MASTER_NEW.Status='Active'  and TSPL_SCHEME_MASTER_NEW.Scheme_Type='Fixed' " + Environment.NewLine + _
            "order by TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date desc  ,TSPL_SCHEME_MASTER_NEW.Scheme_Code desc"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Dim mode As Decimal = 0
            Dim Arr_Temp As New List(Of clsSchemeApplyOnDairy)

            Dim oldSchemeType As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsSchemeApplyOnDairy()
                    objtr.schm_Type = clsCommon.myCstr(dr("Scheme_Type"))
                    objtr.Schm_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    objtr.Schm_Icode = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Schm_Iname = clsItemMaster.GetItemName(objtr.Schm_Icode, trans)
                    objtr.Schm_Item_CSA_Type = clsItemMaster.GetItemCSAType(objtr.Schm_Icode, trans)
                    objtr.Schm_Item_Uom = clsCommon.myCstr(dr("Unit_Code"))
                    objtr.Schm_IUnit_Rate = 0
                    objtr.Schm_Qty = clsCommon.myCdbl(dr("Qty"))
                    Arr_Temp.Add(objtr)
                Next
            End If
            If Arr_Temp IsNot Nothing AndAlso Arr_Temp.Count > 0 AndAlso (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                obj.Arr = Arr_Temp
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function ProductType(ByVal Product_type As String) As String
        Dim values As String = Nothing
        If clsCommon.CompairString(Product_type, "MI") = CompairStringResult.Equal Then
            values = "Milk"
        ElseIf clsCommon.CompairString(Product_type, "CH") = CompairStringResult.Equal Then
            values = "Cheese"
        ElseIf clsCommon.CompairString(Product_type, "MB") = CompairStringResult.Equal Then
            values = "Melted Butter"
        ElseIf clsCommon.CompairString(Product_type, "CU") = CompairStringResult.Equal Then
            values = "Curd"
        ElseIf clsCommon.CompairString(Product_type, "CA") = CompairStringResult.Equal Then
            values = "Cream"
        ElseIf clsCommon.CompairString(Product_type, "BU") = CompairStringResult.Equal Then
            values = "Butter"
        ElseIf clsCommon.CompairString(Product_type, "BM") = CompairStringResult.Equal Then
            values = "Butter Milk"
        ElseIf clsCommon.CompairString(Product_type, "") = CompairStringResult.Equal Then
            values = "Others"
        ElseIf clsCommon.CompairString(Product_type, "ALL") = CompairStringResult.Equal Then
            values = "ALL"
        End If

        Return values
    End Function

    Public Shared Function GetSchemeTypes(Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        'in case of qty scheme main item uom should be equal to scheme uom else no scheme code

        Dim qry As String = "select distinct TSPL_SCHEME_MASTER_NEW.Scheme_Type as Code,TSPL_SCHEME_MASTER_NEW.Scheme_Type as Name from TSPL_SCHEME_MASTER_NEW "
        qry += "where TSPL_SCHEME_MASTER_NEW.Status='Active' and Scheme_Type in ('Quantitive','Volume','VolumeSlab','Structure','Fixed') "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        Return dt
    End Function

    Public Shared Function IsVolumeDefaultScheme(Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isDefault As Boolean = True
        isDefault = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.IsVolumeSchemeBydefault, clsFixedParameterCode.IsVolumeSchemeBydefault, trans) = "1", True, False))

        Return isDefault
    End Function

    Public Shared Function GetPriceSchemeData(ByVal Main_Item_Code As String, ByVal Main_Item_UOM As String, ByVal Main_Item_Qty As Double, ByVal Cust_Code As String, Optional ByVal Return_Scheme_Code As String = Nothing, Optional ByVal DOCdate As Date? = Nothing, Optional ByVal trans As SqlTransaction = Nothing) As clsSchemeApplyOnDairy
        Try
            Dim obj As New clsSchemeApplyOnDairy()

            '' changed by priti on 19102016 against ticket BM00000009160
            If Not DOCdate.HasValue Then
                DOCdate = clsCommon.GETSERVERDATE(trans)
            End If
            Dim qry As String = "select top 1 TSPL_SCHEME_MASTER_NEW.Scheme_Code,TSPL_SCHEME_MASTER_NEW.Scheme_Type,TSPL_SCHEME_DETAIL_NEW.MainItem_Code,TSPL_SCHEME_DETAIL_NEW.MainQty,TSPL_SCHEME_DETAIL_NEW.MainUnit_Code,TSPL_SCHEME_DETAIL_NEW.casddisc_percentage,TSPL_SCHEME_DETAIL_NEW.cashdisc_amount from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code "
            If clsCommon.myLen(Return_Scheme_Code) > 0 Then 'if scheme code passed then only that scheme shows
                qry += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in ('" + Return_Scheme_Code + "') and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + Main_Item_Code + "'  "
            Else
                qry += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date<='" & clsCommon.GetPrintDate(DOCdate, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.MaxlimitEnd_Date >= '" & clsCommon.GetPrintDate(DOCdate, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.MaxlimitEnd_Date is null) and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + Main_Item_Code + "'"
            End If

            qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Type='Cash' order by TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            Dim Item_Code As String = ""
            Dim Qty As Decimal = 0
            Dim Unit As String = ""
            Dim mode As Decimal = 0
            Dim FInal_Qty As Decimal = 0
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Item_Code = clsCommon.myCstr(dr("MainItem_Code"))
                    Unit = clsCommon.myCstr(dr("MainUnit_Code"))
                    Qty = clsCommon.myCdbl(dr("MainQty"))
                    obj.Schm_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    obj.schm_Type = clsCommon.myCstr(dr("Scheme_Type"))

                    If clsCommon.CompairString(Unit, Main_Item_UOM) = CompairStringResult.Equal AndAlso Main_Item_Qty >= Qty Then
                        mode = Main_Item_Qty Mod Qty
                        FInal_Qty = (Main_Item_Qty - mode) / Qty

                        If FInal_Qty > 0 Then
                            If clsCommon.myCdbl(dr("casddisc_percentage")) = 0 Then
                                obj.Cash_Amt = clsCommon.myCdbl(dr("cashdisc_amount")) * FInal_Qty
                            Else
                                obj.Cash_Pers = clsCommon.myCdbl(dr("casddisc_percentage"))
                            End If

                        Else
                            obj.Schm_Code = ""
                            obj.schm_Type = ""
                            obj.Cash_Pers = 0
                            obj.Cash_Amt = 0
                        End If

                    ElseIf clsCommon.CompairString(Unit, Main_Item_UOM) <> CompairStringResult.Equal Then
                        Dim cnvrsn_unit As Decimal = clsItemMaster.GetConvertionFactor(Main_Item_Code, Unit, trans)
                        Dim cnvrsn_Main As Decimal = clsItemMaster.GetConvertionFactor(Main_Item_Code, Main_Item_UOM, trans)

                        If cnvrsn_unit > 0 Then
                            Main_Item_Qty = (Main_Item_Qty * cnvrsn_Main) / cnvrsn_unit

                            If Main_Item_Qty >= Qty Then
                                mode = Main_Item_Qty Mod Qty
                                FInal_Qty = (Main_Item_Qty - mode) / Qty

                                If FInal_Qty > 0 Then
                                    If clsCommon.myCdbl(dr("casddisc_percentage")) = 0 Then
                                        obj.Cash_Amt = clsCommon.myCdbl(dr("cashdisc_amount")) * FInal_Qty
                                    Else
                                        obj.Cash_Pers = clsCommon.myCdbl(dr("casddisc_percentage"))
                                    End If
                                Else
                                    obj.Schm_Code = ""
                                    obj.schm_Type = ""
                                    obj.Cash_Pers = 0
                                    obj.Cash_Amt = 0
                                End If
                            Else
                                obj.Schm_Code = ""
                                obj.schm_Type = ""
                                obj.Cash_Pers = 0
                                obj.Cash_Amt = 0
                            End If
                        Else
                            obj.Schm_Code = ""
                            obj.schm_Type = ""
                            obj.Cash_Pers = 0
                            obj.Cash_Amt = 0
                        End If
                    End If
                Next
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function GetDiscountSchemeData(ByVal Main_Item_Code As String, ByVal Main_Item_UOM As String, ByVal Main_Item_Qty As Double, ByVal Cust_Code As String, Optional ByVal Return_Scheme_Code As String = Nothing, Optional trans As SqlTransaction = Nothing) As clsSchemeApplyOnDairy
        Try
            Dim obj As New clsSchemeApplyOnDairy()

            Dim qry As String = "select top 1 TSPL_SCHEME_MASTER_NEW.Scheme_Code,TSPL_SCHEME_MASTER_NEW.Scheme_Type,TSPL_SCHEME_DETAIL_NEW.MainItem_Code,TSPL_SCHEME_DETAIL_NEW.MainQty,TSPL_SCHEME_DETAIL_NEW.MainUnit_Code,TSPL_SCHEME_DETAIL_NEW.casddisc_percentage,TSPL_SCHEME_DETAIL_NEW.cashdisc_amount from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code "
            If clsCommon.myLen(Return_Scheme_Code) > 0 Then 'if scheme code passed then only that scheme shows
                qry += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in ('" + Return_Scheme_Code + "') and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + Main_Item_Code + "'  "
            Else
                qry += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active' and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + Main_Item_Code + "'"
            End If

            qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Type='Discount' order by TSPL_SCHEME_MASTER_NEW.start_date desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            Dim Item_Code As String = ""
            Dim Qty As Decimal = 0
            Dim Unit As String = ""
            Dim mode As Decimal = 0
            Dim FInal_Qty As Decimal = 0
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Item_Code = clsCommon.myCstr(dr("MainItem_Code"))
                    Unit = clsCommon.myCstr(dr("MainUnit_Code"))
                    Qty = clsCommon.myCdbl(dr("MainQty"))
                    obj.Schm_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    obj.schm_Type = clsCommon.myCstr(dr("Scheme_Type"))

                    If clsCommon.CompairString(Unit, Main_Item_UOM) = CompairStringResult.Equal Then
                        obj.Cash_Pers = clsCommon.myCdbl(dr("casddisc_percentage"))
                        obj.Cash_Amt = clsCommon.myCdbl(dr("cashdisc_amount"))
                    End If

                Next
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
End Class