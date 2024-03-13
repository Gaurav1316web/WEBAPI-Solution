
Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls

Public Class clsMCCMaterialSaleFarmer
#Region "Variables"
    Public Isuploader As Double = 0
    Public Is_CashSale As String = "N"
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
    Public isTaxExempted As Boolean = False '' Not a table field
    Public Document_Date As DateTime
    Public Farmer_Code As String = Nothing
    Public Farmer_Name As String = Nothing  'Not a table field
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Rate_Status As Integer = 1
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
    'Public Against_Sales_Order As String = Nothing
    Public Is_Internal As Boolean = False
    Public Tax_Calculation_Type As EnumTaxCalucationType

    Public Is_Create_Auto_Invoice As Boolean = False
    Public Sale_Invoice_No As String = Nothing
    Public Is_Create_Auto_Receipt As Boolean = False
    Public Against_POS As String = Nothing

    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public Arr As List(Of clsMCCMaterialSaleFarmerDetail) = Nothing

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
    Public Balance_Amt As Decimal

    Public EWayBillDate As Date?
    Public EWayBillNo As String = Nothing
    Public Is_Taxable As Boolean = False
    Public Electronic_Ref_No As String = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsMCCMaterialSaleFarmer, ByVal isNewEntry As Boolean) As Boolean
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

        qry = "select State_Code from TSPL_MP_MASTER where MP_Code='" + strCustCode + "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strCustState As String = clsCommon.myCstr(dt.Rows(0)("State_Code"))
        Dim strTinNo As String = "" '' clsCommon.myCstr(dt.Rows(0)("Tin_No"))
        If clsCommon.myLen(strTinNo) > 0 AndAlso clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
            Invoice_Type = "T"
        Else
            Invoice_Type = "R"
        End If
        Return Invoice_Type
    End Function
    Public Function SaveData(ByVal obj As clsMCCMaterialSaleFarmer, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            '' Anubhooti 06-Sep-2014 BM00000003735 (Locked Transaction)
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Shipment/Sale Invoice", obj.Bill_To_Location, obj.Document_Date, trans)
            ''shivani
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement MCC", "MCC Material Sale", obj.Bill_To_Location, obj.Document_Date, trans)
            ''
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmMCCMaterialFarmer, obj.Bill_To_Location, obj.Document_Date, trans)

            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_MCC_Sale_Farmer_Head", "Document_Code", "TSPL_MCC_Sale_Farmer_Detail", "Document_Code", trans)
            End If
            clsSerializeInvenotry.DeleteData("SD-IN", obj.Document_Code, trans)
            Dim qry As String = "delete from TSPL_MCC_Sale_Farmer_Detail where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsBatchInventory.DeleteData("MCC-MSALE-F", obj.Document_Code, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                'If obj.isTaxExempted Then
                '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmMCCMaterialSaleFarmer, "", obj.Bill_To_Location)
                'Else
                '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmMCCMaterialSaleFarmer, "", obj.Bill_To_Location)
                'End If
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmMCCMaterialSaleFarmer, "", obj.Bill_To_Location)


                'If clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNShipment, clsDocTransactionType.SNQuotationFinishedGoods, obj.Bill_To_Location)
                'Else
                '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNShipment, clsDocTransactionType.SNQuotationOther, obj.Bill_To_Location)
                'End If
            End If
            '''' for Invoice Type
            Dim AllowChangeInvoiceType As Boolean = False
            AllowChangeInvoiceType = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Allow_Change_InvoiceType from TSPL_inv_parameters", trans)) = 0, False, True)
            If obj.Is_Create_Auto_Invoice Then
                If AllowChangeInvoiceType Then
                    If clsCommon.myLen(obj.Invoice_Type) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select invoice  Type for creating auto invoice")
                        Return False
                    End If
                Else
                    If clsCommon.myLen(obj.Ship_To_Location) > 0 Then
                        obj.Invoice_Type = GetInvoiceType(obj.Farmer_Code, obj.Bill_To_Location, "B", trans)
                    Else
                        obj.Invoice_Type = GetInvoiceType(obj.Farmer_Code, obj.Bill_To_Location, "B", trans)
                    End If
                End If
            End If
            ''''' invoice type ends here
            Dim GSTStatus As Boolean = clsERPFuncationality.GetGSTStatus(obj.Document_Date)
            If GSTStatus Then
                If obj.Is_Taxable Then
                    obj.Invoice_Type = "T"
                Else
                    obj.Invoice_Type = "N"
                End If
            End If

            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            ''richa 13/08/2014 against Ticket No BM00000003502
            'If IsDBNull("cust_po_date") Then
            If clsCommon.myLen(obj.Podate) <= 0 Then
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "cust_po_date", clsCommon.GetPrintDate(obj.Podate, "dd/MMM/yyyy hh:mm tt"))
            End If
            '--------------------------------------------------------
            '-----------------richa 27/06/2014 Ticket No .BM00000002982--------------------------------
            Dim isIncrementCounter As Boolean = True
            If obj.Mannual_Invoice_No > 0 OrElse clsCommon.myLen(obj.InvoiceManualNowithPrefix) > 0 Then
                isIncrementCounter = False
            End If
            Dim Doc_Code As String = Nothing

            ''richa agarwal 18/03/2015 sale invoice series generation setting based
            Dim Desc As String = String.Empty
            If obj.Mannual_Invoice_No > 0 Then
                If clsCommon.CompairString(obj.Invoice_Type, "T") = CompairStringResult.Equal Then
                    ''richa agarwal 18/03/2015
                    Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesTaxatMCCSale, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesTaxatMCCSale, trans)
                    If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                        Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceTax, obj.Bill_To_Location, False, isIncrementCounter)
                    Else
                        Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceTax, obj.Bill_To_Location, False, isIncrementCounter)
                    End If
                    ' Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceTax, obj.Bill_To_Location, False, isIncrementCounter)
                ElseIf clsCommon.CompairString(obj.Invoice_Type, "R") = CompairStringResult.Equal Then
                    ''richa agarwal 18/03/2015
                    Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesRetailatMCCSale, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesRetailatMCCSale, trans)
                    If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                        Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceRetail, obj.Bill_To_Location, False, isIncrementCounter)
                    Else
                        Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceRetail, obj.Bill_To_Location, False, isIncrementCounter)
                    End If
                    '  Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceRetail, obj.Bill_To_Location, False, isIncrementCounter)
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
            'obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmMCCMaterialSaleInvoiceFarmer, "", obj.Bill_To_Location, False, isIncrementCounter)


            Dim strItemCategory As String = String.Empty
            Dim StrCustomerState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(State_Code,'') AS STATE from TSPL_MP_MASTER where MP_Code='" & clsCommon.myCstr(obj.Farmer_Code) & "'", trans))
            Dim StrLocationState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(State,'') AS STATE from TSPL_LOCATION_MASTER WHERE LOCATION_CODE='" & clsCommon.myCstr(obj.Bill_To_Location) & "'", trans))
            If clsCommon.CompairString(StrCustomerState, StrLocationState) = CompairStringResult.Equal Then
                strItemCategory = "L"
            Else
                strItemCategory = "I"
            End If
            If clsCommon.myLen(obj.Sale_Invoice_No) <= 0 Then
                If GSTStatus Then
                    'If GST On

                    If clsCommon.CompairString(obj.Invoice_Type, "T") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strItemCategory, "L") = CompairStringResult.Equal Then
                            'If clsCommon.CompairString(clsCommon.myCstr(obj.Supplementary_Type), "C") = CompairStringResult.Equal Then
                            '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SupplementaryCNoteLocal, obj.Bill_To_Location, False, isIncrementCounter)
                            'ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Supplementary_Type), "S") = CompairStringResult.Equal Then
                            '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SupplementaryLocal, obj.Bill_To_Location, False, isIncrementCounter)
                            'Else
                            '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTLocal, obj.Bill_To_Location, False, isIncrementCounter)
                            'End If
                            obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTLocal, obj.Bill_To_Location, False, isIncrementCounter)
                        Else
                            'If clsCommon.CompairString(clsCommon.myCstr(obj.Supplementary_Type), "C") = CompairStringResult.Equal Then
                            '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SupplementaryCNoteInterstate, obj.Bill_To_Location, False, isIncrementCounter)
                            'ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Supplementary_Type), "S") = CompairStringResult.Equal Then
                            '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SupplementaryInterstate, obj.Bill_To_Location, False, isIncrementCounter)
                            'Else
                            '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTInterstate, obj.Bill_To_Location, False, isIncrementCounter)
                            'End If
                            obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTInterstate, obj.Bill_To_Location, False, isIncrementCounter)
                        End If
                    Else
                        If obj.Is_Taxable = False Then
                            obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTNonTaxable, obj.Bill_To_Location, False, isIncrementCounter)
                        Else
                            If clsCommon.CompairString(strItemCategory, "L") = CompairStringResult.Equal Then
                                obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTLocal, obj.Bill_To_Location, False, isIncrementCounter)
                            Else
                                obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTInterstate, obj.Bill_To_Location, False, isIncrementCounter)
                            End If
                        End If
                        'If clsCommon.CompairString(clsCommon.myCstr(obj.Supplementary_Type), "C") = CompairStringResult.Equal Then
                        '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SupplementaryCNoteNonTaxable, obj.Bill_To_Location, False, isIncrementCounter)
                        'ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Supplementary_Type), "S") = CompairStringResult.Equal Then
                        '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SupplementaryNonTaxable, obj.Bill_To_Location, False, isIncrementCounter)
                        'Else
                        '    If IsDairyModule = False Then
                        '        obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTNonTaxable, obj.Bill_To_Location, False, isIncrementCounter)
                        '    Else
                        '        If obj.Is_Taxable = False Then
                        '            obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTNonTaxable, obj.Bill_To_Location, False, isIncrementCounter)
                        '        Else
                        '            If clsCommon.CompairString(strItemCategory, "L") = CompairStringResult.Equal Then
                        '                obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTLocal, obj.Bill_To_Location, False, isIncrementCounter)
                        '            Else
                        '                obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.GSTInterstate, obj.Bill_To_Location, False, isIncrementCounter)
                        '            End If
                        '        End If
                        '    End If
                        'End If
                    End If

                Else
                    obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmMCCMaterialSaleInvoiceFarmer, "", obj.Bill_To_Location, False, isIncrementCounter)

                    'Dim stritemcode As String = String.Empty
                    'If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                    '    For Each obj11 As clsMCCMaterialSaleFarmerDetail In Arr
                    '        stritemcode = clsCommon.myCstr(obj11.Item_Code)
                    '    Next
                    'End If
                    'Dim CreatVatSeriesOnExciseInvoice As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateVatSeriesForProductExciseinvoice, clsFixedParameterCode.CreateVatSeriesForProductExciseinvoice, trans))
                    'Dim VatInvoiceType As String = Nothing
                    'Dim strcount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT count(Item_Code) FROM TSPL_LOCATION_WISE_ITEM_MASTER where Location_Code='" & clsCommon.myCstr(obj.Bill_To_Location) & "' and Item_Category='" & strItemCategory & "' and Item_Code='" & stritemcode & "'", trans))
                    ''Dim Desc As String = String.Empty
                    'If obj.Is_CashSale Then
                    '    obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.CashSale, obj.Bill_To_Location, False, isIncrementCounter)
                    'Else
                    '    If strcount > 0 Then
                    '        obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.TaxExempted_ProductInvoice, obj.Bill_To_Location, False, isIncrementCounter)
                    '        obj.Invoice_Type = "A"
                    '    Else
                    '        ''richa agarwal 17/03/2015 sale invoice series generation setting based

                    '        Dim strExcise As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + obj.Bill_To_Location + "'", trans)) = "T", True, False)
                    '        If clsCommon.CompairString(obj.Invoice_Type, "T") = CompairStringResult.Equal Then
                    '            Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesTaxTypeatPS, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesTaxTypeatPS, trans)
                    '            If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                    '                obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceTax, obj.Bill_To_Location, False, isIncrementCounter)
                    '            Else
                    '                obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceTax, obj.Bill_To_Location, False, isIncrementCounter)
                    '            End If
                    '            'obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceTax, obj.Bill_To_Location, False, isIncrementCounter)
                    '        ElseIf clsCommon.CompairString(obj.Invoice_Type, "R") = CompairStringResult.Equal Then
                    '            Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesRetailTypeatPS, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesRetailTypeatPS, trans)
                    '            If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                    '                obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceRetail, obj.Bill_To_Location, False, isIncrementCounter)
                    '            Else
                    '                obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceRetail, obj.Bill_To_Location, False, isIncrementCounter)
                    '            End If
                    '        ElseIf clsCommon.CompairString(obj.Invoice_Type, "I") = CompairStringResult.Equal Then
                    '            Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesRetailTypeatPS, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesRetailTypeatPS, trans)
                    '            If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                    '                obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceInterstate, obj.Bill_To_Location, False, isIncrementCounter)
                    '            Else
                    '                obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceInterstate, obj.Bill_To_Location, False, isIncrementCounter)
                    '            End If
                    '            ' obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceRetail, obj.Bill_To_Location, False, isIncrementCounter)
                    '        ElseIf clsCommon.CompairString(obj.Invoice_Type, "E") = CompairStringResult.Equal Then
                    '            Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesExciseTypeatPS, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesExciseTypeatPS, trans)
                    '            If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                    '                obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceExcise, obj.Bill_To_Location, False, isIncrementCounter)
                    '            Else
                    '                obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceExcise, obj.Bill_To_Location, False, isIncrementCounter)
                    '            End If
                    '            '  obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceExcise, obj.Bill_To_Location, False, isIncrementCounter)
                    '        ElseIf clsCommon.CompairString(obj.Invoice_Type, "S") = CompairStringResult.Equal Then
                    '            obj.Sale_Invoice_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceService, obj.Bill_To_Location, False, isIncrementCounter)
                    '        End If
                    '    End If
                    'End If
                    'If clsCommon.CompairString(obj.Invoice_Type, "E") = CompairStringResult.Equal AndAlso CreatVatSeriesOnExciseInvoice = 1 Then
                    '    Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesExciseTypeatPS, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesExciseTypeatPS, trans)
                    '    VatInvoiceType = InvoiceType(obj.Bill_To_Location, obj.Customer_Code, trans)
                    '    If clsCommon.CompairString(VatInvoiceType, "R") = CompairStringResult.Equal Then
                    '        If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                    '            obj.VAT_InvoiceNo = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceRetail, obj.Bill_To_Location, False, isIncrementCounter)
                    '        Else
                    '            obj.VAT_InvoiceNo = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceRetail, obj.Bill_To_Location, False, isIncrementCounter)
                    '        End If
                    '        obj.VatInvoice_Type = VatInvoiceType
                    '    ElseIf clsCommon.CompairString(VatInvoiceType, "T") = CompairStringResult.Equal Then
                    '        If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                    '            obj.VAT_InvoiceNo = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceTax, obj.Bill_To_Location, False, isIncrementCounter)
                    '        Else
                    '            obj.VAT_InvoiceNo = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceTax, obj.Bill_To_Location, False, isIncrementCounter)
                    '        End If
                    '        obj.VatInvoice_Type = VatInvoiceType
                    '    ElseIf clsCommon.CompairString(VatInvoiceType, "I") = CompairStringResult.Equal Then
                    '        If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                    '            obj.VAT_InvoiceNo = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceInterstate, obj.Bill_To_Location, False, isIncrementCounter)
                    '        Else
                    '            obj.VAT_InvoiceNo = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceInterstate, obj.Bill_To_Location, False, isIncrementCounter)
                    '        End If
                    '        obj.VatInvoice_Type = VatInvoiceType
                    '    End If
                    'End If

                End If

            End If
            
            '---------------------------------------------------------------------------------------
            clsCommon.AddColumnsForChange(coll, "Sale_Invoice_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd-MMM-yyyy"))
            clsCommon.AddColumnsForChange(coll, "Farmer_Code", obj.Farmer_Code)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Internal", IIf(obj.Is_Internal, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Isuploader", obj.Isuploader)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Inv_No", obj.Inv_No)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Is_CashSale", obj.Is_CashSale)
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
            clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.Total_Amt)
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
            'clsCommon.AddColumnsForChange(coll, "Against_POS", obj.Against_POS, True)
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


            clsCommon.AddColumnsForChange(coll, "SO_Validity", obj.SO_Validity)
            clsCommon.AddColumnsForChange(coll, "Commission_Apply", obj.Commission_Apply)
            'clsCommon.AddColumnsForChange(coll, "Dispatch_date", clsCommon.GetPrintDate(obj.Dispatch_date, "dd/MMM/yyyy hh:mm tt"))
            If obj.Inv_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Dispatch_date", clsCommon.GetPrintDate(obj.Inv_Date, "dd/MMM/yyyy hh:mm tt"))
            End If

            clsCommon.AddColumnsForChange(coll, "Vehicle_Capacity", obj.Vehicle_Capacity)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Terms", obj.Dispatch_Terms)
            clsCommon.AddColumnsForChange(coll, "Payment_Terms", obj.Payment_Terms)
            clsCommon.AddColumnsForChange(coll, "Dispatch_Period", obj.Dispatch_Period)
            clsCommon.AddColumnsForChange(coll, "Road_Permit_No", obj.Road_Permit_No)
            clsCommon.AddColumnsForChange(coll, "WayBillNo", obj.WayBillNo)
            clsCommon.AddColumnsForChange(coll, "Total_Comm_Amt", obj.Total_Comm_Amt)
            'If clsCommon.myLen(obj.Against_Sales_Order) = 0 Then
            '    obj.Direct_Dispatch = 1
            'End If
            clsCommon.AddColumnsForChange(coll, "Direct_Dispatch", obj.Direct_Dispatch)
            'clsCommon.AddColumnsForChange(coll, "WayBillDate", clsCommon.GetPrintDate(obj.WayBillDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", IIf(obj.Is_Taxable, 1, 0))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Trans_Type", "MCC")
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Sale_Farmer_Head", OMInsertOrUpdate.Insert, "", trans)

                If obj.Rate_Status = 2 Then
                    qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                    "values ('MCC Material Sale','" & clsUserMgtCode.frmMCCMaterial & "','" & obj.Document_Code & "','" & clsCommon.GetPrintDate(obj.Document_Date, "dd-MMM-yyyy hh:mm:ss") & "','Rate',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Sale_Farmer_Head", OMInsertOrUpdate.Update, "TSPL_MCC_Sale_Farmer_Head.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsMCCMaterialSaleFarmerDetail.SaveData(obj.Document_Code, Arr, trans, obj.Document_Date)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_Code, obj.arrCustomFields, trans)
            '''' to save item weight unit
            qry = "update TSPL_MCC_Sale_Farmer_Detail set Weight_UOM= (select Weight_UOM from TSPL_ITEM_MASTER where Item_Code=TSPL_MCC_Sale_Farmer_Detail.Item_Code)  where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_Code", obj.Document_Code, "TSPL_MCC_Sale_Farmer_Head", trans)

            ''''  for automatic invoice
            'Dim objSI As clsMCCMaterialSaleFarmer = ConvertShipmentToSaleInvoice(obj)
            'If clsCommon.myLen(obj.Sale_Invoice_No) > 0 Then
            '    objSI.SaveData(objSI, False, trans)
            'Else
            '    If clsCommon.CompairString(obj.Is_CashSale, "Y") = CompairStringResult.Equal Then
            '        objSI.SaveData(objSI, True, True, trans)
            '    Else
            '        objSI.SaveData(objSI, True, trans)
            '    End If

            'End If
            'Dim sQuery As String = "update TSPL_SD_SALE_INVOICE_HEAD set trans_type='MCC' where Against_Shipment_No='" & obj.Document_Code & "'"
            'clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            'objSI.SaveData(objSI, True, trans)
            'obj.Sale_Invoice_No = objSI.Document_Code
            ''''  automatic invoice ends here
        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    'Public Shared Function checkSaveNotification(ByVal obj As clsMCCMaterialSaleFarmer, ByVal trans As SqlTransaction) As Boolean
    '    Try
    '        Dim Count As Integer = 0
    '        Dim CreditLimit As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Credit_Limit from TSPL_MP_MASTER WHERE Cust_Code='" + obj.Farmer_Code + "'", trans))
    '        Dim qry As String
    '        Dim dt As DataTable = clsScreenNotificationSchedule.GetScreenNotificationInfo(clsUserMgtCode.frmSNShipment, trans)
    '        For Each dr As DataRow In dt.Rows
    '            'Criteria, Notification, Validation
    '            If clsCommon.CompairString(dr("Criteria"), "Credit days") = CompairStringResult.Equal Then
    '                qry = "Select COUNT(*) from TSPL_MCC_Sale_Farmer_Head" & _
    '    " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_MCC_Sale_Farmer_Head.Document_Code" & _
    '    " LEFT OUTER JOIN TSPL_Customer_Invoice_Head_Farmer ON TSPL_Customer_Invoice_Head_Farmer.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
    '    " WHERE TSPL_MCC_Sale_Farmer_Head.Status = 1" & _
    '    " AND TSPL_MCC_Sale_Farmer_Head.Farmer_Code='" + obj.Farmer_Code + "'" & _
    '    " AND TSPL_MCC_Sale_Farmer_Head.Due_Date<'" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "'" & _
    '    " AND ISNULL(TSPL_Customer_Invoice_Head_Farmer.Balance_Amt,0)<>0"
    '                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
    '                    If clsCommon.CompairString(dr("Validation"), "Required Approval") = CompairStringResult.Equal Then
    '                        'clsCommon.MyMessageBoxShow(clsCommon.myCstr(dt.Rows(0)("Notification")))
    '                        If common.clsCommon.MyMessageBoxShow(clsCommon.myCstr(dr("Notification")) + Environment.NewLine + "Do you want to continue?.", "Load Out", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
    '                            Dim frm As New FrmPWD(trans)
    '                            frm.strCode = clsFixedParameterCode.CreditLimitApproval
    '                            frm.strType = clsFixedParameterType.CreditLimitApproval
    '                            frm.ShowDialog()
    '                            If frm.isPasswordCorrect Then
    '                                Count += 1
    '                            End If
    '                        Else
    '                            Return False
    '                        End If
    '                    Else
    '                        Throw New Exception(clsCommon.myCstr(dt.Rows(0)("Notification")))
    '                    End If
    '                End If
    '            ElseIf clsCommon.CompairString(dr("Criteria"), "Credit Amount") = CompairStringResult.Equal Then
    '                qry = "Select SUM(TSPL_Customer_Invoice_Head_Farmer.Balance_Amt) from TSPL_MCC_Sale_Farmer_Head" & _
    '    " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_MCC_Sale_Farmer_Head.Document_Code" & _
    '    " LEFT OUTER JOIN TSPL_Customer_Invoice_Head_Farmer ON TSPL_Customer_Invoice_Head_Farmer.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
    '    " WHERE TSPL_MCC_Sale_Farmer_Head.Status = 1" & _
    '    " AND TSPL_MCC_Sale_Farmer_Head.Farmer_Code='" + obj.Farmer_Code + "'" & _
    '    " AND TSPL_MCC_Sale_Farmer_Head.Document_Date<'" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "'" & _
    '    " AND ISNULL(TSPL_Customer_Invoice_Head_Farmer.Balance_Amt,0)<>0"
    '                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > CreditLimit Then
    '                    If clsCommon.CompairString(dr("Validation"), "Required Approval") = CompairStringResult.Equal Then
    '                        'clsCommon.MyMessageBoxShow(clsCommon.myCstr(dt.Rows(0)("Notification")))
    '                        If common.clsCommon.MyMessageBoxShow(clsCommon.myCstr(dr("Notification")) + Environment.NewLine + "Do you want to continue?.", "Load Out", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
    '                            Dim frm As New FrmPWD(trans)
    '                            frm.strCode = clsFixedParameterCode.CreditLimitApproval
    '                            frm.strType = clsFixedParameterType.CreditLimitApproval
    '                            frm.ShowDialog()
    '                            If frm.isPasswordCorrect Then
    '                                Count += 1
    '                            End If
    '                        Else
    '                            Return False
    '                        End If
    '                    Else
    '                        Throw New Exception(clsCommon.myCstr(dt.Rows(0)("Notification")))
    '                    End If
    '                End If
    '            End If
    '        Next
    '        If Count >= 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

    Public Shared Function checkApprovalDocument(ByVal Prog_code As String, ByVal document_no As String) As Boolean
        Dim sQuery As String
        sQuery = "select * from TSPL_TRANSACTION_APPROVAL where program_Code='" & Prog_code & "' and Document_No='" & document_no & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If dt.Rows.Count <= 0 Then
            Return True
        Else
            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Approve")), "0") = CompairStringResult.Equal Then
                Return False
            Else
                Return True
            End If
        End If
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsMCCMaterialSaleFarmer
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMCCMaterialSaleFarmer
        Dim obj As clsMCCMaterialSaleFarmer = Nothing
        Dim qry As String = "SELECT TSPL_MCC_Sale_Farmer_Head.Isuploader,TSPL_MCC_Sale_Farmer_Head.Road_Permit_No,TSPL_MCC_Sale_Farmer_Head.Is_Delivered,TSPL_MCC_Sale_Farmer_Head.HeadDisc_PerAmt,TSPL_MCC_Sale_Farmer_Head.cust_po_date,TSPL_MCC_Sale_Farmer_Head.Cust_PO_No,TSPL_MCC_Sale_Farmer_Head.Vehicle_Code,TSPL_MCC_Sale_Farmer_Head.price_group_code,TSPL_MCC_Sale_Farmer_Head.Invoice_Type,TSPL_MCC_Sale_Farmer_Head.HeadDisc_Per,TSPL_MCC_Sale_Farmer_Head.HeadDisc_Amt,TSPL_MCC_Sale_Farmer_Head.TotCashDiscAmt,TSPL_MCC_Sale_Farmer_Head.Route_No,TSPL_MCC_Sale_Farmer_Head.Route_Desc,TSPL_MCC_Sale_Farmer_Head.Price_Code,TSPL_MCC_Sale_Farmer_Head.Document_Code,TSPL_MCC_Sale_Farmer_Head.Document_Date,TSPL_MCC_Sale_Farmer_Head.Farmer_Code,tspl_mp_master.MP_NAME AS Farmer_Name,TSPL_MCC_Sale_Farmer_Head.Status,TSPL_MCC_Sale_Farmer_Head.On_Hold,TSPL_MCC_Sale_Farmer_Head.Ref_No,TSPL_MCC_Sale_Farmer_Head.Description,TSPL_MCC_Sale_Farmer_Head.Is_CashSale,TSPL_MCC_Sale_Farmer_Head.Remarks,TSPL_MCC_Sale_Farmer_Head.Tax_Group,TSPL_MCC_Sale_Farmer_Head.Bill_To_Location,TSPL_MCC_Sale_Farmer_Head.Ship_To_Location,TSPL_MCC_Sale_Farmer_Head.TAX1,TSPL_MCC_Sale_Farmer_Head.TAX1_Rate,TSPL_MCC_Sale_Farmer_Head.TAX1_Amt,TSPL_MCC_Sale_Farmer_Head.TAX1_Base_Amt,TSPL_MCC_Sale_Farmer_Head.TAX2,TSPL_MCC_Sale_Farmer_Head.TAX2_Rate,TSPL_MCC_Sale_Farmer_Head.TAX2_Amt,TSPL_MCC_Sale_Farmer_Head.TAX2_Base_Amt,TSPL_MCC_Sale_Farmer_Head.TAX3,TSPL_MCC_Sale_Farmer_Head.TAX3_Rate,TSPL_MCC_Sale_Farmer_Head.TAX3_Amt,TSPL_MCC_Sale_Farmer_Head.TAX3_Base_Amt,TSPL_MCC_Sale_Farmer_Head.TAX4,TSPL_MCC_Sale_Farmer_Head.TAX4_Rate,TSPL_MCC_Sale_Farmer_Head.TAX4_Amt,TSPL_MCC_Sale_Farmer_Head.TAX4_Base_Amt,TSPL_MCC_Sale_Farmer_Head.TAX5,TSPL_MCC_Sale_Farmer_Head.TAX5_Rate,TSPL_MCC_Sale_Farmer_Head.TAX5_Amt,TSPL_MCC_Sale_Farmer_Head.TAX5_Base_Amt,TSPL_MCC_Sale_Farmer_Head.TAX6,TSPL_MCC_Sale_Farmer_Head.TAX6_Rate,TSPL_MCC_Sale_Farmer_Head.TAX6_Amt,TSPL_MCC_Sale_Farmer_Head.TAX6_Base_Amt,TSPL_MCC_Sale_Farmer_Head.TAX7,TSPL_MCC_Sale_Farmer_Head.TAX7_Rate,TSPL_MCC_Sale_Farmer_Head.TAX7_Amt,TSPL_MCC_Sale_Farmer_Head.TAX7_Base_Amt,TSPL_MCC_Sale_Farmer_Head.TAX8,TSPL_MCC_Sale_Farmer_Head.TAX8_Rate,TSPL_MCC_Sale_Farmer_Head.TAX8_Amt,TSPL_MCC_Sale_Farmer_Head.TAX8_Base_Amt,TSPL_MCC_Sale_Farmer_Head.TAX9,TSPL_MCC_Sale_Farmer_Head.TAX9_Rate,TSPL_MCC_Sale_Farmer_Head.TAX9_Amt,TSPL_MCC_Sale_Farmer_Head.TAX9_Base_Amt,TSPL_MCC_Sale_Farmer_Head.TAX10,TSPL_MCC_Sale_Farmer_Head.TAX10_Rate,TSPL_MCC_Sale_Farmer_Head.TAX10_Amt,TSPL_MCC_Sale_Farmer_Head.TAX10_Base_Amt,TSPL_MCC_Sale_Farmer_Head.Discount_Base,TSPL_MCC_Sale_Farmer_Head.Discount_Amt,TSPL_MCC_Sale_Farmer_Head.Amount_Less_Discount,TSPL_MCC_Sale_Farmer_Head.Total_Tax_Amt,TSPL_MCC_Sale_Farmer_Head.Comments,TSPL_MCC_Sale_Farmer_Head.Comp_Code,TSPL_MCC_Sale_Farmer_Head.Terms_Code,TSPL_MCC_Sale_Farmer_Head.Due_Date ,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_MCC_Sale_Farmer_Head.Posting_Date,TSPL_MCC_Sale_Farmer_Head.Total_Amt,TSPL_MCC_Sale_Farmer_Head.Carrier,TSPL_MCC_Sale_Farmer_Head.VehicleNo,TSPL_MCC_Sale_Farmer_Head.GRNo,TSPL_MCC_Sale_Farmer_Head.GENo,TSPL_MCC_Sale_Farmer_Head.GEDate, TSPL_MCC_Sale_Farmer_Head.Dept,TSPL_MCC_Sale_Farmer_Head.Dept_Desc,TSPL_MCC_Sale_Farmer_Head.Item_Type,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Code1,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name1,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt1,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Code2,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name2,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt2,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Code3,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name3,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt3,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Code4,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name4,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt4,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Code5,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name5,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt5,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Code6,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name6,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt6,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Code7,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name7,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt7,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Code8,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name8,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt8,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Code9 ,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name9,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt9 ,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Code10 ,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name10,TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt10,TSPL_MCC_Sale_Farmer_Head.Total_Add_Charge,TSPL_MCC_Sale_Farmer_Head.Tax_Calculation_Type,TSPL_MCC_Sale_Farmer_Head.Challan_No, TSPL_MCC_Sale_Farmer_Head.Challan_Date, TSPL_MCC_Sale_Farmer_Head.Inv_Date,TSPL_MCC_Sale_Farmer_Head.Inv_No,TSPL_MCC_Sale_Farmer_Head.Is_Internal,TSPL_MCC_Sale_Farmer_Head.Is_Create_Auto_Invoice,TSPL_MCC_Sale_Farmer_Head.Sale_Invoice_No,TSPL_MCC_Sale_Farmer_Head.Is_Create_Auto_Receipt,TSPL_MCC_Sale_Farmer_Head.Salesman_Code ,TSPL_MCC_Sale_Farmer_Head.Salesman_Name,  "
        qry += " TSPL_MCC_Sale_Farmer_Head.CURRENCY_CODE,TSPL_MCC_Sale_Farmer_Head.CONVRATE,TSPL_MCC_Sale_Farmer_Head.APPLICABLEFROM,TSPL_MCC_Sale_Farmer_Head.PRoject_ID ,TSPL_MCC_Sale_Farmer_Head.Mannual_Invoice_No,TSPL_MCC_Sale_Farmer_Head. Mannual_Invoice_No_StringType,TSPL_MCC_Sale_Farmer_Head.Form_38_No " & _
        " ,TSPL_MCC_Sale_Farmer_Head.SO_Validity,TSPL_MCC_Sale_Farmer_Head.Commission_Apply,TSPL_MCC_Sale_Farmer_Head.Total_Comm_Amt,TSPL_MCC_Sale_Farmer_Head.Dispatch_date,TSPL_MCC_Sale_Farmer_Head.WayBillNo,TSPL_MCC_Sale_Farmer_Head.WayBillDate " & _
        " ,TSPL_MCC_Sale_Farmer_Head.Dispatch_Terms,TSPL_MCC_Sale_Farmer_Head.Payment_Terms,TSPL_MCC_Sale_Farmer_Head.Dispatch_Period,TSPL_MCC_Sale_Farmer_Head.Vehicle_Capacity,TSPL_MCC_Sale_Farmer_Head.Balance_Amt,TSPL_MCC_Sale_Farmer_Head.EWayBillNo,TSPL_MCC_Sale_Farmer_Head.EWayBillDate,TSPL_MCC_Sale_Farmer_Head.Electronic_Ref_No,TSPL_MCC_Sale_Farmer_Head.Is_Taxable "
        qry += "  FROM TSPL_MCC_Sale_Farmer_Head "
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_Sale_Farmer_Head.Bill_To_Location "
        qry += " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_MCC_Sale_Farmer_Head.Ship_To_Location "
        qry += " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_MCC_Sale_Farmer_Head.Tax_Group "
        qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_MCC_Sale_Farmer_Head.Terms_Code "
        qry += " left outer join tspl_mp_master on tspl_mp_master.MP_CODE=TSPL_MCC_Sale_Farmer_Head.Farmer_Code where 2=2"
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
            whrCls = "  and TSPL_MCC_Sale_Farmer_Head.Trans_Type='MCC' AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_MCC_Sale_Farmer_Head.Farmer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = "  and TSPL_MCC_Sale_Farmer_Head.Trans_Type='MCC' AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrCls = "  and TSPL_MCC_Sale_Farmer_Head.Trans_Type='MCC' AND TSPL_MCC_Sale_Farmer_Head.Farmer_Code in (" + strwherecls + ")"
        Else
            whrCls = " and TSPL_MCC_Sale_Farmer_Head.Trans_Type='MCC' "
        End If
        '-----------------------------------------------------

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MCC_Sale_Farmer_Head.Document_Code = (select MIN(Document_Code) from TSPL_MCC_Sale_Farmer_Head WHERE 1=1 " + whrCls + ") "
            Case NavigatorType.Last
                qry += " and TSPL_MCC_Sale_Farmer_Head.Document_Code = (select Max(Document_Code) from TSPL_MCC_Sale_Farmer_Head WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_MCC_Sale_Farmer_Head.Document_Code = '" + strPONo + "' "
            Case NavigatorType.Next
                qry += " and TSPL_MCC_Sale_Farmer_Head.Document_Code = (select Min(Document_Code) from TSPL_MCC_Sale_Farmer_Head where Document_Code>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MCC_Sale_Farmer_Head.Document_Code = (select Max(Document_Code) from TSPL_MCC_Sale_Farmer_Head where Document_Code<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMCCMaterialSaleFarmer()
            'obj.WayBillDate = clsCommon.myCDate(dt.Rows(0)("WayBillDate"))     
            obj.Is_Taxable = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Taxable")) = 1, True, False)
            If dt.Rows(0)("EWayBillDate") IsNot DBNull.Value Then
                obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            End If
            obj.Electronic_Ref_No = clsCommon.myCstr(dt.Rows(0)("Electronic_Ref_No"))
            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))

            obj.WayBillNo = clsCommon.myCstr(dt.Rows(0)("WayBillNo"))
            obj.SO_Validity = clsCommon.myCdbl(dt.Rows(0)("SO_Validity"))
            obj.Commission_Apply = clsCommon.myCdbl(dt.Rows(0)("Commission_Apply"))
            obj.Total_Comm_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Comm_Amt"))
            obj.Dispatch_date = clsCommon.myCDate(dt.Rows(0)("Dispatch_date"))
            obj.Vehicle_Capacity = clsCommon.myCdbl(dt.Rows(0)("Vehicle_Capacity"))
            obj.Dispatch_Terms = clsCommon.myCstr(dt.Rows(0)("Dispatch_Terms"))
            obj.Payment_Terms = clsCommon.myCstr(dt.Rows(0)("Payment_Terms"))
            obj.Dispatch_Period = clsCommon.myCdbl(dt.Rows(0)("Dispatch_Period"))
            obj.Road_Permit_No = clsCommon.myCstr(dt.Rows(0)("Road_Permit_No"))

            obj.Is_Delivered = clsCommon.myCdbl(dt.Rows(0)("Is_Delivered"))
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Farmer_Code = clsCommon.myCstr(dt.Rows(0)("Farmer_Code"))
            obj.Farmer_Name = clsCommon.myCstr(dt.Rows(0)("Farmer_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Is_Internal = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Internal")) = 1, True, False)
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Is_CashSale = clsCommon.myCstr(dt.Rows(0)("Is_CashSale"))
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
            obj.Balance_Amt = clsCommon.myCdbl(dt.Rows(0)("Balance_Amt"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))

            If dt.Rows(0)("Due_Date") IsNot DBNull.Value Then
                obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            End If

            '-----------------richa 26/06/2014 Ticket No .BM00000002982----------------------------
            obj.InvoiceManualNowithPrefix = clsCommon.myCstr(dt.Rows(0)("Mannual_Invoice_No_StringType"))
            obj.Mannual_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Mannual_Invoice_No"))

            '--------------------------------------------------------------------------------------

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

            'obj.Against_Sales_Order = clsCommon.myCstr(dt.Rows(0)("Against_Sales_Order"))


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
            obj.Isuploader = clsCommon.myCdbl(dt.Rows(0)("Isuploader"))
            '' END CURRENCYCONVERSION 
            obj.Invoice_No = obj.Invoice_No ' clsDBFuncationality.getSingleValue("Select Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" & obj.Document_Code & "' ", trans)
            obj.Is_Taxable = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Taxable")) = 1, True, False)

            qry = "SELECT  TSPL_MCC_Sale_Farmer_Detail.OrgUnit_code,TSPL_MCC_Sale_Farmer_Detail.Is_Mannual_Amt,TSPL_MCC_Sale_Farmer_Detail.Document_Code,TSPL_MCC_Sale_Farmer_Detail.Line_No, " & _
            "TSPL_MCC_Sale_Farmer_Detail.Status,TSPL_MCC_Sale_Farmer_Detail.Row_Type,TSPL_MCC_Sale_Farmer_Detail.status,TSPL_MCC_Sale_Farmer_Detail.Item_Code, " & _
            "TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_Sale_Farmer_Detail.Qty,TSPL_MCC_Sale_Farmer_Detail.Free_Qty, " & _
            "TSPL_MCC_Sale_Farmer_Detail.Balance_Qty,TSPL_MCC_Sale_Farmer_Detail.Unit_code, " & _
            "TSPL_MCC_Sale_Farmer_Detail.Location,TSPL_MCC_Sale_Farmer_Detail.Item_Cost,TSPL_MCC_Sale_Farmer_Detail.TAX1,TSPL_MCC_Sale_Farmer_Detail.TAX1_Rate, " & _
            "TSPL_MCC_Sale_Farmer_Detail.TAX1_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX2,TSPL_MCC_Sale_Farmer_Detail.TAX2_Rate,TSPL_MCC_Sale_Farmer_Detail.TAX2_Amt, " & _
            "TSPL_MCC_Sale_Farmer_Detail.TAX3,TSPL_MCC_Sale_Farmer_Detail.TAX3_Rate,TSPL_MCC_Sale_Farmer_Detail.TAX3_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX4 , " & _
            "TSPL_MCC_Sale_Farmer_Detail.TAX4_Rate,TSPL_MCC_Sale_Farmer_Detail.TAX4_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX5,TSPL_MCC_Sale_Farmer_Detail.TAX5_Rate , " & _
            "TSPL_MCC_Sale_Farmer_Detail.TAX5_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX6,TSPL_MCC_Sale_Farmer_Detail.TAX6_Rate,TSPL_MCC_Sale_Farmer_Detail.TAX6_Amt, " & _
            "TSPL_MCC_Sale_Farmer_Detail.TAX7,TSPL_MCC_Sale_Farmer_Detail.TAX7_Rate,TSPL_MCC_Sale_Farmer_Detail.TAX7_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX8, " & _
            "TSPL_MCC_Sale_Farmer_Detail.TAX8_Rate,TSPL_MCC_Sale_Farmer_Detail.TAX8_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX9,TSPL_MCC_Sale_Farmer_Detail.TAX9_Rate, " & _
            "TSPL_MCC_Sale_Farmer_Detail.TAX9_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX10,TSPL_MCC_Sale_Farmer_Detail.TAX10_Rate,TSPL_MCC_Sale_Farmer_Detail.TAX10_Amt, " & _
            "TSPL_MCC_Sale_Farmer_Detail.Amount,TSPL_MCC_Sale_Farmer_Detail.Disc_Per,TSPL_MCC_Sale_Farmer_Detail.Disc_Amt,TSPL_MCC_Sale_Farmer_Detail.Amt_Less_Discount, " & _
            "TSPL_MCC_Sale_Farmer_Detail.Total_Tax_Amt,TSPL_MCC_Sale_Farmer_Detail.Item_Net_Amt,TSPL_LOCATION_MASTER.Location_Desc as LocationName, " & _
            "TSPL_MCC_Sale_Farmer_Detail.TAX1_Base_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX2_Base_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX3_Base_Amt , " & _
            "TSPL_MCC_Sale_Farmer_Detail.TAX4_Base_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX5_Base_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX6_Base_Amt, " & _
            "TSPL_MCC_Sale_Farmer_Detail.TAX7_Base_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX8_Base_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX9_Base_Amt, " & _
            "TSPL_MCC_Sale_Farmer_Detail.TAX10_Base_Amt,TSPL_MCC_Sale_Farmer_Detail.MRP,TSPL_MCC_Sale_Farmer_Detail.Batch_No,TSPL_MCC_Sale_Farmer_Detail.MFG_Date, " & _
            "TSPL_MCC_Sale_Farmer_Detail.Expiry_Date,TSPL_MCC_Sale_Farmer_Detail.Specification,TSPL_MCC_Sale_Farmer_Detail.Remarks,TSPL_MCC_Sale_Farmer_Detail.Assessable, " & _
            "TSPL_MCC_Sale_Farmer_Detail.AssessableAmt,TSPL_MCC_Sale_Farmer_Detail.Bar_Code, " & _
            "TSPL_MCC_Sale_Farmer_Detail.Scheme_Applicable,TSPL_MCC_Sale_Farmer_Detail.Scheme_Code, " & _
            "TSPL_MCC_Sale_Farmer_Detail.Scheme_Item,TSPL_MCC_Sale_Farmer_Detail.Item_Tax,TSPL_MCC_Sale_Farmer_Detail.Total_MRP_Amt, " & _
            "TSPL_MCC_Sale_Farmer_Detail.Total_Basic_Amt,TSPL_MCC_Sale_Farmer_Detail.Total_Disc_Amt,TSPL_MCC_Sale_Farmer_Detail.Cust_Discount, " & _
            "TSPL_MCC_Sale_Farmer_Detail.Total_Cust_Discount,TSPL_MCC_Sale_Farmer_Detail.ActualRate,TSPL_MCC_Sale_Farmer_Detail.Cust_DiscountQty, " & _
            "TSPL_MCC_Sale_Farmer_Detail.Price_code,TSPL_MCC_Sale_Farmer_Detail.Abatement_Per,TSPL_MCC_Sale_Farmer_Detail.Abatement_Amt, " & _
            "TSPL_MCC_Sale_Farmer_Detail.FOC_Item,TSPL_MCC_Sale_Farmer_Detail.Item_Weight,TSPL_MCC_Sale_Farmer_Detail.Price_Date, " & _
            "TSPL_MCC_Sale_Farmer_Detail.HeadDiscPer,TSPL_MCC_Sale_Farmer_Detail.HeadDiscPerAmt,TSPL_MCC_Sale_Farmer_Detail.Bin_No,TSPL_MCC_Sale_Farmer_Detail.TotalItem_Weight,TSPL_MCC_Sale_Farmer_Detail.Conv_Factor,TSPL_MCC_Sale_Farmer_Detail.Purchase_Cost,TSPL_MCC_Sale_Farmer_Detail.OrgRate,  " & _
            "TSPL_MCC_Sale_Farmer_Detail.vendor_code,TSPL_MCC_Sale_Farmer_Detail.vendor_desc,TSPL_MCC_Sale_Farmer_Detail.PrincipleCode,TSPL_MCC_Sale_Farmer_Detail.PrincipleDesc,TSPL_MCC_Sale_Farmer_Detail.Markup_On,TSPL_MCC_Sale_Farmer_Detail.Markup_Percent,TSPL_MCC_Sale_Farmer_Detail.Landing_Cost,TSPL_MCC_Sale_Farmer_Detail.HeadDiscAmt,TSPL_MCC_Sale_Farmer_Detail.CustDiscPer,TSPL_MCC_Sale_Farmer_Detail.CasdDiscScheme_Code " & _
            ",TSPL_MCC_Sale_Farmer_Detail.Commission_Rate,TSPL_MCC_Sale_Farmer_Detail.Commission_Party,TSPL_MCC_Sale_Farmer_Detail.Commission_Amt,TSPL_MCC_Sale_Farmer_Detail.Amt_Less_Commission "
            qry += " FROM TSPL_MCC_Sale_Farmer_Detail "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_Sale_Farmer_Detail.Location "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MCC_Sale_Farmer_Detail.Item_Code"
            qry += " where TSPL_MCC_Sale_Farmer_Detail.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_MCC_Sale_Farmer_Detail.Line_No  asc"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsMCCMaterialSaleFarmerDetail)
                Dim objTr As clsMCCMaterialSaleFarmerDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsMCCMaterialSaleFarmerDetail
                    objTr.Commission_Rate = clsCommon.myCdbl(dr("Commission_Rate"))
                    objTr.Commission_Party = clsCommon.myCstr(dr("Commission_Party"))
                    objTr.Commission_Amt = clsCommon.myCdbl(dr("Commission_Amt"))
                    objTr.Amt_Less_Commission = clsCommon.myCdbl(dr("Amt_Less_Commission"))
                    objTr.OrgUnit_code = clsCommon.myCstr(dr("OrgUnit_code"))
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Bar_Code = clsCommon.myCstr(dr("Bar_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))


                    objTr.Free_Qty = clsCommon.myCdbl(dr("Free_Qty"))
                    'objTr.Order_Code = clsCommon.myCstr(dr("Order_Code"))

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
                    objTr.arrSrItem = clsSerializeInvenotry.GetData("SD-IN", objTr.Document_Code, objTr.Item_Code, objTr.Line_No, trans)
                    objTr.arrBatchItem = clsBatchInventory.GetData("MCC-MSALE-F", objTr.Document_Code, objTr.Item_Code, objTr.Line_No, trans)
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
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreatedOnly As String = Nothing) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Shipment No not found to Post")
            End If
            Dim obj As clsMCCMaterialSaleFarmer = clsMCCMaterialSaleFarmer.GetData(strDocNo, NavigatorType.Current, trans)
            ' clsLockMPPaymentCycle.LockMPTransaction(obj.Bill_To_Location, obj.Document_Date, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            '' Anubhooti 06-Sep-2014 BM00000003735 (Locked Transaction)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmMCCMaterialFarmer, obj.Bill_To_Location, obj.Document_Date, trans)
            ''
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Shipment No " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If

            Dim qry As String = ""

            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_MCC_Sale_Farmer_Head", "Document_Code", obj.Document_Code, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If

            HitInventoryMovement(obj, trans)

            'Create GL Entry


            'If obj.Is_Create_Auto_Invoice Then
            '    'Dim objSI As clsMCCMaterialSaleFarmer = ConvertShipmentToSaleInvoice(obj)
            '    'objSI.SaveData(objSI, True, trans)
            '    'obj.Sale_Invoice_No = objSI.Document_Code
            '    clsMCCMaterialSaleFarmer.PostData("", obj.Invoice_No, trans)   ''obj.Sale_Invoice_No remove because it has not value.by bulk posting.
            'End If
            'If clsCommon.myLen(obj.Against_Sales_Order) = 0 Then
            '    Dim objSO As clsPSSalesOrder = ConvertShipmentToSaleOrder(obj)
            '    objSO.SaveData(objSO, True, False, trans)
            '    obj.Against_Sales_Order = objSO.Document_Code
            '    clsPSSalesOrder.PostData("", obj.Against_Sales_Order, trans)
            'End If
            'comment by Balwinder cogs is handled in JE funcion on 02/02/2016

            CreateJournalEntry(obj.Document_Code, trans, strVoucherNoForRecreatedOnly)


            qry = "Update TSPL_MCC_Sale_Farmer_Head set  Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "',Sale_Invoice_No ='" + obj.Sale_Invoice_No + "' "
            qry += " where Document_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_MCC_Sale_Farmer_Head", "Document_Code", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function HitInventoryMovement(ByVal obj As clsMCCMaterialSaleFarmer, ByVal trans As SqlTransaction) As Boolean
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim intCounter As Integer = 0
        For Each objTr As clsMCCMaterialSaleFarmerDetail In obj.Arr
            intCounter = intCounter + 1
            If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Dim dblEnteredQty As Double = objTr.Qty
                Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)
                Dim dblBalQty As Double = clsItemLocationDetails.getBalance(objTr.Item_Code, obj.Bill_To_Location, obj.Document_Code, obj.Document_Date, trans, objTr.Unit_code, objTr.MRP)
                For Each objTrInner As clsMCCMaterialSaleFarmerDetail In obj.Arr
                    If objTr.Line_No = objTrInner.Line_No Then
                        Continue For
                    End If
                    Dim strICodeInner As String = objTrInner.Item_Code
                    Dim strUOMInner As String = objTrInner.Unit_code
                    Dim dblQtyInner As Double = objTrInner.Qty
                    Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, trans)
                    If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, objTr.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOMInner, objTr.Unit_code) = CompairStringResult.Equal Then
                        dblEnteredQty += dblQtyInner
                    End If
                Next
                dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                If dblEnteredQty > dblBalQty Then
                    Throw New Exception("Item - " + objTr.Item_Code + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                End If


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

                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt.InOut = "O"
                objInventoryMovemnt.Location_Code = objTr.Location

                objInventoryMovemnt.Cust_Code = "" ''obj.Farmer_Code
                objInventoryMovemnt.Cust_Name = "" ''obj.Farmer_Name

                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = objTr.Qty + objTr.Free_Qty
                objInventoryMovemnt.UOM = objTr.Unit_code
                objInventoryMovemnt.Basic_Cost = objTr.Item_Cost
                objInventoryMovemnt.MRP = objTr.MRP
                objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt
                objInventoryMovemnt.Net_Cost = objTr.Total_Tax_Amt
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
        clsInventoryMovement.SaveData("MCC-MSALE-F", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        Return True
    End Function
    Public Shared Function CreateJournalEntry(ByVal strCode As String, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreatedOnly As String = Nothing) As Boolean
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim obj As clsMCCMaterialSaleFarmer = clsMCCMaterialSaleFarmer.GetData(strCode, NavigatorType.Current, trans)
            If obj Is Nothing OrElse obj.Arr.Count <= 0 Then
                Throw New Exception("Document detail not found to create Journal Entry.")
            End If
            Dim strDiscountCode As String = clsFixedParameter.GetData(clsFixedParameterType.DiscountCodeForMPAdj, clsFixedParameterCode.DiscountCodeForMPAdj, trans)
            If clsCommon.myLen(strDiscountCode) <= 0 Then
                Throw New Exception("Please first set Value of DiscountCodeForMPAdj in fixed parameter")
            End If
            Dim qry As String = "SELECT  TSPL_DISCOUNT_MASTER.Account_Code FROM TSPL_DISCOUNT_MASTER" + Environment.NewLine +
              "where Code='" + strDiscountCode + "' "
            qry = clsDBFuncationality.getSingleValue(qry, trans)
            If clsCommon.myLen(qry) <= 0 Then
                Throw New Exception("Please first select GL Code of Discount master [" + qry + "] For Farmer Control Account.")
            End If
            Dim FarmerCtrlAccount As String = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
            Dim Acc() As String = {FarmerCtrlAccount, obj.Total_Amt}
            ArryLstGLAC.Add(Acc)

            For ii As Integer = 0 To obj.Arr.Count - 1
                qry = "select TSPL_SALES_ACCOUNTS.Sales_Account from TSPL_ITEM_MASTER " + Environment.NewLine +
                "left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code=TSPL_ITEM_MASTER.Sale_Class_Code" + Environment.NewLine +
                "where TSPL_ITEM_MASTER.Item_Code='" + obj.Arr(ii).Item_Code + "'"
                qry = clsDBFuncationality.getSingleValue(qry, trans)
                If clsCommon.myLen(qry) <= 0 Then
                    Throw New Exception("Please set sales Account for item [" + obj.Arr(ii).Item_Code + "]")
                End If
                qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)

                Dim Acc1() As String = {qry, -1 * obj.Arr(ii).Amt_Less_Discount}
                ArryLstGLAC.Add(Acc1)
            Next
            Dim isTaxRecoverable As Boolean = False
            Dim isTaxExcisable As Boolean = False
            If obj.TAX1_Amt <> 0 Then
                qry = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
                qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
                If clsCommon.myLen(qry) <= 0 Then
                    Throw New Exception("GL Acount not found for" + obj.TAX1)
                End If
                Dim AccInvDR() As String = {qry, -1 * obj.TAX1_Amt}
                ArryLstGLAC.Add(AccInvDR)
            End If
            If obj.TAX2_Amt <> 0 Then
                qry = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
                qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
                If clsCommon.myLen(qry) <= 0 Then
                    Throw New Exception("GL Acount not found for" + obj.TAX2)
                End If
                Dim AccInvDR() As String = {qry, -1 * obj.TAX2_Amt}
                ArryLstGLAC.Add(AccInvDR)
            End If
            If obj.TAX3_Amt <> 0 Then
                qry = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
                qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
                If clsCommon.myLen(qry) <= 0 Then
                    Throw New Exception("GL Acount not found for" + obj.TAX3)
                End If
                Dim AccInvDR() As String = {qry, -1 * obj.TAX3_Amt}
                ArryLstGLAC.Add(AccInvDR)
            End If
            If obj.TAX4_Amt <> 0 Then
                qry = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
                qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
                If clsCommon.myLen(qry) <= 0 Then
                    Throw New Exception("GL Acount not found for" + obj.TAX4)
                End If
                Dim AccInvDR() As String = {qry, -1 * obj.TAX4_Amt}
                ArryLstGLAC.Add(AccInvDR)
            End If
            If obj.TAX5_Amt <> 0 Then
                qry = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
                qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
                If clsCommon.myLen(qry) <= 0 Then
                    Throw New Exception("GL Acount not found for" + obj.TAX5)
                End If
                Dim AccInvDR() As String = {qry, -1 * obj.TAX5_Amt}
                ArryLstGLAC.Add(AccInvDR)
            End If
            If obj.TAX6_Amt <> 0 Then
                qry = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
                qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
                If clsCommon.myLen(qry) <= 0 Then
                    Throw New Exception("GL Acount not found for" + obj.TAX6)
                End If
                Dim AccInvDR() As String = {qry, -1 * obj.TAX6_Amt}
                ArryLstGLAC.Add(AccInvDR)
            End If
            If obj.TAX7_Amt <> 0 Then
                qry = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
                qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
                If clsCommon.myLen(qry) <= 0 Then
                    Throw New Exception("GL Acount not found for" + obj.TAX7)
                End If
                Dim AccInvDR() As String = {qry, -1 * obj.TAX7_Amt}
                ArryLstGLAC.Add(AccInvDR)
            End If
            If obj.TAX8_Amt <> 0 Then
                qry = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
                qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
                If clsCommon.myLen(qry) <= 0 Then
                    Throw New Exception("GL Acount not found for" + obj.TAX8)
                End If
                Dim AccInvDR() As String = {qry, -1 * obj.TAX8_Amt}
                ArryLstGLAC.Add(AccInvDR)
            End If
            If obj.TAX9_Amt <> 0 Then
                qry = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
                qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
                If clsCommon.myLen(qry) <= 0 Then
                    Throw New Exception("GL Acount not found for" + obj.TAX9)
                End If
                Dim AccInvDR() As String = {qry, -1 * obj.TAX9_Amt}
                ArryLstGLAC.Add(AccInvDR)
            End If
            If obj.TAX10_Amt <> 0 Then
                qry = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
                qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
                If clsCommon.myLen(qry) <= 0 Then
                    Throw New Exception("GL Acount not found for" + obj.TAX10)
                End If
                Dim AccInvDR() As String = {qry, -1 * obj.TAX10_Amt}
                ArryLstGLAC.Add(AccInvDR)
            End If



            Dim dblTotalCost As Double = 0
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0 Then
                Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where Source_Doc_No='" & obj.Document_Code & "' and Trans_Type='MCC-MSALE-F'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        qry = "select TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold from TSPL_ITEM_MASTER " + Environment.NewLine +
                      "left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code=TSPL_ITEM_MASTER.Sale_Class_Code" + Environment.NewLine +
                      "where TSPL_ITEM_MASTER.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'"
                        qry = clsDBFuncationality.getSingleValue(qry, trans)
                        If clsCommon.myLen(qry) <= 0 Then
                            Throw New Exception("Please set Cost of Goods Sold Account for item [" + clsCommon.myCstr(dr("Item_Code")) + "]")
                        End If
                        qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
                        Dim Acc2() As String = {qry, clsCommon.myCdbl(dr("Cost"))}
                        ArryLstGLAC.Add(Acc2)

                        qry = "SELECT TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account FROM TSPL_ITEM_MASTER" + Environment.NewLine +
                        "left outer JOIN  TSPL_PURCHASE_ACCOUNTS  ON  TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code = TSPL_ITEM_MASTER.Purchase_Class_Code " + Environment.NewLine +
                        "WHERE TSPL_ITEM_MASTER.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'"
                        qry = clsDBFuncationality.getSingleValue(qry, trans)
                        If clsCommon.myLen(qry) <= 0 Then
                            Throw New Exception("Please set Inventory Control Account for item [" + clsCommon.myCstr(dr("Item_Code")) + "]")
                        End If
                        qry = clsERPFuncationality.ChangeGLAccountLocationSegment(qry, obj.Bill_To_Location, trans)
                        Dim Acc1() As String = {qry, -1 * clsCommon.myCdbl(dr("Cost"))}
                        ArryLstGLAC.Add(Acc1)
                    Next
                End If
            End If
            If strVoucherNoForRecreatedOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreatedOnly) > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, strVoucherNoForRecreatedOnly, trans, obj.Document_Date, obj.Remarks, "MC-FS", "MCC Farmer Sale", obj.Document_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , obj.Description, obj.Remarks)
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, trans, obj.Document_Date, obj.Remarks, "MC-FS", "MCC Farmer Sale", obj.Document_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , obj.Description, obj.Remarks)
            End If

            '' to update VSP Code against Farmer code into Journal master
            Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where source_doc_no ='" & obj.Document_Code & "' and source_code='MC-FS'", trans))
            If clsCommon.myLen(strVoucherNo) > 0 Then
                clsDBFuncationality.ExecuteNonQuery("update tspl_journal_master set VSP_code=(select TSPL_VLC_MASTER_HEAD.VSP_Code  from TSPL_MP_MASTER inner JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MP_MASTER.VLC_Code where mp_code='" & obj.Farmer_Code & "') where Voucher_No ='" & strVoucherNo & "' and source_code='MC-FS'", trans)
            End If


        End If
        Return True
    End Function

    'Public Shared Function CreateJournalEntry(ByVal strCode As String, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreatedOnly As String = Nothing) As Boolean
    '    Dim isSkipCogsEntry As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)

    '    Dim obj As New clsMCCMaterialSaleFarmer
    '    obj = clsMCCMaterialSaleFarmer.GetData(strCode, NavigatorType.Current, trans)
    '    Dim ArryLstGLAC As ArrayList = New ArrayList()
    '    Dim strInventoryControlAc As String = ""
    '    Dim strShipmentClearingAC As String = ""
    '    Dim dblTotalCost As Double = 0
    '    If Not isSkipCogsEntry Then
    '        strShipmentClearingAC = clsDBFuncationality.getSingleValue("SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
    '          " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
    '           " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr.Item(0).Item_Code.ToString() + "'", trans)
    '        strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, obj.Bill_To_Location, trans)

    '        If clsCommon.myLen(strShipmentClearingAC) = 0 Then
    '            Throw New Exception("Please set Shipment clearing Account for first item")
    '        End If

    '        Dim dblCogsCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Source_Doc_No='" & obj.Document_Code & "'", trans))

    '        Dim Acc() As String = {strShipmentClearingAC, dblCogsCost}
    '        ArryLstGLAC.Add(Acc)

    '        Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where Source_Doc_No='" & obj.Document_Code & "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
    '        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '            For Each dr As DataRow In dt.Rows
    '                strInventoryControlAc = clsDBFuncationality.getSingleValue("SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
    '                " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
    '                " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans)
    '                strInventoryControlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControlAc, obj.Bill_To_Location, trans)

    '                If clsCommon.myLen(strInventoryControlAc) = 0 Then
    '                    Throw New Exception("Please set Inventory Control Account for first item")
    '                End If
    '                Dim Acc1() As String = {strInventoryControlAc, -1 * clsCommon.myCdbl(dr("Cost"))}
    '                ArryLstGLAC.Add(Acc1)
    '            Next
    '        End If

    '        If strVoucherNoForRecreatedOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreatedOnly) > 0 Then
    '            clsJournalMaster.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, strVoucherNoForRecreatedOnly, trans, obj.Document_Date, obj.Remarks, "SD-SH", "Shipment", obj.Document_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , obj.Description, obj.Remarks)
    '        Else
    '            clsJournalMaster.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, trans, obj.Document_Date, obj.Remarks, "SD-SH", "Shipment", obj.Document_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , obj.Description, obj.Remarks)
    '        End If

    '    End If
    '    Return True


    'End Function

    'Private Shared Function ConvertShipmentToSaleInvoice(ByVal objShipment As clsMCCMaterialSaleFarmer) As clsMCCMaterialSaleFarmer
    '    Dim obj As New clsMCCMaterialSaleFarmer()
    '    obj = New clsMCCMaterialSaleFarmer()
    '    obj.Item_Tax_Type = objShipment.isTaxExempted
    '    obj.podate = objShipment.Document_Date
    '    obj.Total_Comm_Amt = objShipment.Total_Comm_Amt
    '    obj.Invoice_Type = objShipment.Invoice_Type
    '    obj.Document_Code = objShipment.Sale_Invoice_No
    '    obj.Document_Date = objShipment.Document_Date
    '    obj.Farmer_Code = objShipment.Farmer_Code
    '    obj.Farmer_Name = objShipment.Farmer_Name
    '    obj.Status = IIf(objShipment.Status = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
    '    obj.On_Hold = IIf(objShipment.On_Hold = 1, True, False)
    '    obj.Is_Internal = IIf(objShipment.Is_Internal = 1, True, False)
    '    obj.Ref_No = objShipment.Ref_No
    '    obj.Description = objShipment.Description
    '    obj.Remarks = objShipment.Remarks
    '    obj.Bill_To_Location = objShipment.Bill_To_Location
    '    obj.Ship_To_Location = objShipment.Ship_To_Location
    '    obj.Tax_Group = objShipment.Tax_Group
    '    obj.TAX1 = objShipment.TAX1
    '    obj.TAX1_Rate = objShipment.TAX1_Rate
    '    obj.TAX1_Base_Amt = objShipment.TAX1_Base_Amt
    '    obj.TAX1_Amt = objShipment.TAX1_Amt
    '    obj.TAX2 = objShipment.TAX2
    '    obj.TAX2_Rate = objShipment.TAX2_Rate
    '    obj.TAX2_Base_Amt = objShipment.TAX2_Base_Amt
    '    obj.TAX2_Amt = objShipment.TAX2_Amt
    '    obj.TAX3 = objShipment.TAX3
    '    obj.TAX3_Base_Amt = objShipment.TAX3_Base_Amt
    '    obj.TAX3_Rate = objShipment.TAX3_Rate
    '    obj.TAX3_Amt = objShipment.TAX3_Amt
    '    obj.TAX4 = objShipment.TAX4
    '    obj.TAX4_Rate = objShipment.TAX4_Rate
    '    obj.TAX4_Base_Amt = objShipment.TAX4_Base_Amt
    '    obj.TAX4_Amt = objShipment.TAX4_Amt
    '    obj.TAX5 = objShipment.TAX5
    '    obj.TAX5_Rate = objShipment.TAX5_Rate
    '    obj.TAX5_Base_Amt = objShipment.TAX5_Base_Amt
    '    obj.TAX5_Amt = objShipment.TAX5_Amt
    '    obj.TAX6 = objShipment.TAX6
    '    obj.TAX6_Rate = objShipment.TAX6_Rate
    '    obj.TAX6_Base_Amt = objShipment.TAX6_Base_Amt
    '    obj.TAX6_Amt = objShipment.TAX6_Amt
    '    obj.TAX7 = objShipment.TAX7
    '    obj.TAX7_Rate = objShipment.TAX7_Rate
    '    obj.TAX7_Base_Amt = objShipment.TAX7_Base_Amt
    '    obj.TAX7_Amt = objShipment.TAX7_Amt
    '    obj.TAX8 = objShipment.TAX8
    '    obj.TAX8_Rate = objShipment.TAX8_Rate
    '    obj.TAX8_Base_Amt = objShipment.TAX8_Base_Amt
    '    obj.TAX8_Amt = objShipment.TAX8_Amt
    '    obj.TAX9 = objShipment.TAX9
    '    obj.TAX9_Rate = objShipment.TAX9_Rate
    '    obj.TAX9_Base_Amt = objShipment.TAX9_Base_Amt
    '    obj.TAX9_Amt = objShipment.TAX9_Amt
    '    obj.TAX10 = objShipment.TAX10
    '    obj.TAX10_Rate = objShipment.TAX10_Rate
    '    obj.TAX10_Base_Amt = objShipment.TAX10_Base_Amt
    '    obj.TAX10_Amt = objShipment.TAX10_Amt
    '    obj.Total_Tax_Amt = objShipment.Total_Tax_Amt
    '    obj.Discount_Base = objShipment.Discount_Base
    '    obj.Discount_Amt = objShipment.Discount_Amt
    '    obj.Amount_Less_Discount = objShipment.Amount_Less_Discount
    '    obj.Total_Amt = objShipment.Total_Amt
    '    obj.Comments = objShipment.Comments
    '    obj.Comp_Code = objShipment.Comp_Code
    '    obj.Terms_Code = objShipment.Terms_Code
    '    obj.Due_Date = objShipment.Due_Date
    '    obj.BillToLocationName = objShipment.BillToLocationName
    '    obj.ShipToLocationName = objShipment.ShipToLocationName
    '    obj.TaxGroupName = objShipment.TaxGroupName
    '    obj.TermsName = objShipment.TermsName
    '    obj.PROJECT_ID = objShipment.PROJECT_ID
    '    obj.Route_No = objShipment.Route_No
    '    obj.Route_Desc = objShipment.Route_Desc
    '    obj.Price_Code = objShipment.Price_Code
    '    obj.HeadDisc_Per = objShipment.HeadDisc_Per
    '    obj.HeadDisc_Amt = objShipment.HeadDisc_Amt
    '    obj.HeadDisc_PerAmt = objShipment.HeadDisc_PerAmt
    '    obj.TotCashDiscAmt = objShipment.TotCashDiscAmt
    '    obj.Cust_PO_No = objShipment.Cust_PO_No
    '    ' obj.podate = objShipment.Podate



    '    If objShipment.Posting_Date IsNot Nothing Then
    '        obj.Posting_Date = objShipment.Posting_Date
    '    End If

    '    obj.Salesman_Code = objShipment.Salesman_Code
    '    obj.Salesman_Name = objShipment.Salesman_Name

    '    obj.Challan_No = objShipment.Challan_No
    '    obj.Carrier = objShipment.Carrier
    '    obj.Vehicle_Code = objShipment.Vehicle_Code
    '    obj.VehicleNo = objShipment.VehicleNo
    '    obj.GRNo = objShipment.GRNo
    '    obj.GENo = objShipment.GENo
    '    If objShipment.GEDate IsNot Nothing Then
    '        obj.GEDate = objShipment.GEDate
    '    End If




    '    obj.Dept = objShipment.Dept
    '    obj.Dept_Desc = objShipment.Dept_Desc
    '    obj.Item_Type = objShipment.Item_Type

    '    obj.Against_Shipment_No = objShipment.Document_Code


    '    obj.Add_Charge_Code1 = objShipment.Add_Charge_Code1
    '    obj.Add_Charge_Name1 = objShipment.Add_Charge_Name1
    '    obj.Add_Charge_Amt1 = objShipment.Add_Charge_Amt1

    '    obj.Add_Charge_Code2 = objShipment.Add_Charge_Code2
    '    obj.Add_Charge_Name2 = objShipment.Add_Charge_Name2
    '    obj.Add_Charge_Amt2 = objShipment.Add_Charge_Amt2

    '    obj.Add_Charge_Code3 = objShipment.Add_Charge_Code3
    '    obj.Add_Charge_Name3 = objShipment.Add_Charge_Name3
    '    obj.Add_Charge_Amt3 = objShipment.Add_Charge_Amt3

    '    obj.Add_Charge_Code4 = objShipment.Add_Charge_Code4
    '    obj.Add_Charge_Name4 = objShipment.Add_Charge_Name4
    '    obj.Add_Charge_Amt4 = objShipment.Add_Charge_Amt4

    '    obj.Add_Charge_Code5 = objShipment.Add_Charge_Code5
    '    obj.Add_Charge_Name5 = objShipment.Add_Charge_Name5
    '    obj.Add_Charge_Amt5 = objShipment.Add_Charge_Amt5

    '    obj.Add_Charge_Code6 = objShipment.Add_Charge_Code6
    '    obj.Add_Charge_Name6 = objShipment.Add_Charge_Name6
    '    obj.Add_Charge_Amt6 = objShipment.Add_Charge_Amt6

    '    obj.Add_Charge_Code7 = objShipment.Add_Charge_Code7
    '    obj.Add_Charge_Name7 = objShipment.Add_Charge_Name7
    '    obj.Add_Charge_Amt7 = objShipment.Add_Charge_Amt7

    '    obj.Add_Charge_Code8 = objShipment.Add_Charge_Code8
    '    obj.Add_Charge_Name8 = objShipment.Add_Charge_Name8
    '    obj.Add_Charge_Amt8 = objShipment.Add_Charge_Amt8

    '    obj.Add_Charge_Code9 = objShipment.Add_Charge_Code9
    '    obj.Add_Charge_Name9 = objShipment.Add_Charge_Name9
    '    obj.Add_Charge_Amt9 = objShipment.Add_Charge_Amt9

    '    obj.Add_Charge_Code10 = objShipment.Add_Charge_Code10
    '    obj.Add_Charge_Name10 = objShipment.Add_Charge_Name10
    '    obj.Add_Charge_Amt10 = objShipment.Add_Charge_Amt10

    '    obj.Total_Add_Charge = objShipment.Total_Add_Charge
    '    obj.Inv_No = objShipment.Inv_No
    '    If clsCommon.myLen(objShipment.Challan_Date) <= 0 Then
    '        obj.Challan_Date = ""
    '    Else
    '        obj.Challan_Date = clsCommon.GetPrintDate(objShipment.Challan_Date, "dd/MMM/yyyy")
    '    End If

    '    If clsCommon.myLen(objShipment.Inv_Date) <= 0 Then
    '        obj.Inv_Date = ""
    '    Else
    '        obj.Inv_Date = clsCommon.GetPrintDate(objShipment.Inv_Date, "dd/MMM/yyyy")
    '    End If
    '    obj.SO_Validity = objShipment.SO_Validity
    '    obj.Commission_Apply = objShipment.Commission_Apply
    '    obj.Dispatch_date = objShipment.Inv_Date 'objShipment.Dispatch_date
    '    obj.Vehicle_Capacity = objShipment.Vehicle_Capacity
    '    obj.Dispatch_Terms = objShipment.Dispatch_Terms
    '    obj.Payment_Terms = objShipment.Payment_Terms
    '    obj.Dispatch_Period = objShipment.Dispatch_Period
    '    obj.WayBillNo = objShipment.WayBillNo
    '    obj.WayBillDate = objShipment.WayBillDate
    '    obj.Tax_Calculation_Type = IIf(objShipment.Tax_Calculation_Type = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
    '    obj.Is_Create_Auto_Receipt = objShipment.Is_Create_Auto_Receipt
    '    '-----------------richa 27/06/2014 Ticket No .BM00000002982----------
    '    obj.Mannual_Document_Code = objShipment.Mannual_Invoice_No
    '    obj.InvoiceManualNowithPrefix = objShipment.InvoiceManualNowithPrefix

    '    '-------------------------------------------------------------------
    '    If (objShipment.Arr IsNot Nothing AndAlso objShipment.Arr.Count > 0) Then
    '        obj.Arr = New List(Of clsMCCMaterialSaleFarmerDetail)
    '        Dim objTr As clsMCCMaterialSaleFarmerDetail
    '        For Each objShipmentDetail As clsMCCMaterialSaleFarmerDetail In objShipment.Arr
    '            objTr = New clsMCCMaterialSaleFarmerDetail
    '            objTr.PrincipleCode = objShipmentDetail.PrincipleCode
    '            objTr.PrincipleDesc = objShipmentDetail.PrincipleDesc
    '            objTr.vendor_code = objShipmentDetail.vendor_code
    '            objTr.vendor_desc = objShipmentDetail.vendor_desc
    '            objTr.Document_Code = objShipmentDetail.Document_Code
    '            objTr.Row_Type = objShipmentDetail.Row_Type
    '            objTr.Line_No = objShipmentDetail.Line_No
    '            objTr.Status = Convert.ToInt32(objShipmentDetail.Status)
    '            objTr.Item_Code = objShipmentDetail.Item_Code
    '            objTr.Item_Desc = objShipmentDetail.Item_Desc
    '            objTr.Qty = objShipmentDetail.Qty
    '            objTr.Free_Qty = objShipmentDetail.Free_Qty
    '            objTr.Shipment_Code = objShipment.Document_Code
    '            objTr.Balance_Qty = objShipmentDetail.Balance_Qty
    '            objTr.Unit_code = objShipmentDetail.Unit_code
    '            objTr.Location = objShipmentDetail.Location
    '            objTr.LocationName = objShipmentDetail.LocationName
    '            objTr.Item_Cost = objShipmentDetail.Item_Cost
    '            objTr.TAX1 = objShipmentDetail.TAX1
    '            objTr.TAX1_Base_Amt = objShipmentDetail.TAX1_Base_Amt
    '            objTr.TAX1_Rate = objShipmentDetail.TAX1_Rate
    '            objTr.TAX1_Amt = objShipmentDetail.TAX1_Amt
    '            objTr.TAX2 = objShipmentDetail.TAX2
    '            objTr.TAX2_Base_Amt = objShipmentDetail.TAX2_Base_Amt
    '            objTr.TAX2_Rate = objShipmentDetail.TAX2_Rate
    '            objTr.TAX2_Amt = objShipmentDetail.TAX2_Amt
    '            objTr.TAX3 = objShipmentDetail.TAX3
    '            objTr.TAX3_Base_Amt = objShipmentDetail.TAX3_Base_Amt
    '            objTr.TAX3_Rate = objShipmentDetail.TAX3_Rate
    '            objTr.TAX3_Amt = objShipmentDetail.TAX3_Amt
    '            objTr.TAX4 = objShipmentDetail.TAX4
    '            objTr.TAX4_Base_Amt = objShipmentDetail.TAX4_Base_Amt
    '            objTr.TAX4_Rate = objShipmentDetail.TAX4_Rate
    '            objTr.TAX4_Amt = objShipmentDetail.TAX4_Amt
    '            objTr.TAX5 = objShipmentDetail.TAX5
    '            objTr.TAX5_Base_Amt = objShipmentDetail.TAX5_Base_Amt
    '            objTr.TAX5_Rate = objShipmentDetail.TAX5_Rate
    '            objTr.TAX5_Amt = objShipmentDetail.TAX5_Amt
    '            objTr.TAX6 = objShipmentDetail.TAX6
    '            objTr.TAX6_Base_Amt = objShipmentDetail.TAX6_Base_Amt
    '            objTr.TAX6_Rate = objShipmentDetail.TAX6_Rate
    '            objTr.TAX6_Amt = objShipmentDetail.TAX6_Amt
    '            objTr.TAX7 = objShipmentDetail.TAX7
    '            objTr.TAX7_Base_Amt = objShipmentDetail.TAX7_Base_Amt
    '            objTr.TAX7_Rate = objShipmentDetail.TAX7_Rate
    '            objTr.TAX7_Amt = objShipmentDetail.TAX7_Amt
    '            objTr.TAX8 = objShipmentDetail.TAX8
    '            objTr.TAX8_Base_Amt = objShipmentDetail.TAX8_Base_Amt
    '            objTr.TAX8_Rate = objShipmentDetail.TAX8_Rate
    '            objTr.TAX8_Amt = objShipmentDetail.TAX8_Amt
    '            objTr.TAX9 = objShipmentDetail.TAX9
    '            objTr.TAX9_Base_Amt = objShipmentDetail.TAX9_Base_Amt
    '            objTr.TAX9_Rate = objShipmentDetail.TAX9_Rate
    '            objTr.TAX9_Amt = objShipmentDetail.TAX9_Amt
    '            objTr.TAX10 = objShipmentDetail.TAX10
    '            objTr.TAX10_Base_Amt = objShipmentDetail.TAX10_Base_Amt
    '            objTr.TAX10_Rate = objShipmentDetail.TAX10_Rate
    '            objTr.TAX10_Amt = objShipmentDetail.TAX10_Amt
    '            objTr.Amount = objShipmentDetail.Amount
    '            objTr.Disc_Per = objShipmentDetail.Disc_Per
    '            objTr.Disc_Amt = objShipmentDetail.Disc_Amt
    '            objTr.Amt_Less_Discount = objShipmentDetail.Amt_Less_Discount
    '            objTr.Total_Tax_Amt = objShipmentDetail.Total_Tax_Amt
    '            objTr.Item_Net_Amt = objShipmentDetail.Item_Net_Amt


    '            objTr.Is_Mannual_Amt = objShipmentDetail.Is_Mannual_Amt

    '            objTr.MRP = objShipmentDetail.MRP
    '            objTr.Assessable = objShipmentDetail.Assessable
    '            objTr.AssessableAmt = objShipmentDetail.AssessableAmt
    '            objTr.Batch_No = objShipmentDetail.Batch_No
    '            If objShipmentDetail.MFG_Date IsNot Nothing Then
    '                objTr.MFG_Date = objShipmentDetail.MFG_Date
    '            End If
    '            If objShipmentDetail.Expiry_Date IsNot Nothing Then
    '                objTr.Expiry_Date = objShipmentDetail.Expiry_Date
    '            End If
    '            objTr.Specification = objShipmentDetail.Specification
    '            objTr.Remarks = objShipmentDetail.Remarks

    '            objTr.Scheme_Applicable = IIf(objShipmentDetail.Scheme_Applicable = "Y", "Yes", "No")
    '            objTr.Scheme_Code = objShipmentDetail.Scheme_Code
    '            objTr.Scheme_Item = IIf(objShipmentDetail.Scheme_Item = "Y", "Yes", "No")
    '            objTr.Item_Tax = objShipmentDetail.Item_Tax
    '            objTr.Total_MRP_Amt = objShipmentDetail.Total_MRP_Amt
    '            objTr.Total_Basic_Amt = objShipmentDetail.Total_Basic_Amt
    '            objTr.Total_Disc_Amt = objShipmentDetail.Total_Disc_Amt
    '            objTr.Cust_Discount = objShipmentDetail.Cust_Discount
    '            objTr.Total_Cust_Discount = objShipmentDetail.Total_Cust_Discount
    '            objTr.ActualRate = objShipmentDetail.ActualRate
    '            objTr.Cust_DiscountQty = objShipmentDetail.Cust_DiscountQty
    '            objTr.Price_code = objShipmentDetail.Price_code
    '            objTr.Price_Date = objShipmentDetail.Price_Date
    '            objTr.Abatement_Per = objShipmentDetail.Abatement_Per
    '            objTr.Abatement_Amt = objShipmentDetail.Abatement_Amt
    '            objTr.FOC_Item = objShipmentDetail.FOC_Item
    '            objTr.Markup_On = objShipmentDetail.Markup_On
    '            objTr.Markup_Percent = objShipmentDetail.Markup_Percent
    '            objTr.Landing_Cost = objShipmentDetail.Landing_Cost
    '            objTr.HeadDiscAmt = objShipmentDetail.HeadDiscAmt
    '            objTr.HeadDiscPer = objShipmentDetail.HeadDiscPer
    '            objTr.HeadDiscPerAmt = objShipmentDetail.HeadDiscPerAmt
    '            objTr.CustDiscPer = objShipmentDetail.CustDiscPer
    '            objTr.CasdDiscScheme_Code = objShipmentDetail.CasdDiscScheme_Code

    '            objTr.Item_Weight = objShipmentDetail.Item_Weight
    '            objTr.TotalItem_Weight = objShipmentDetail.TotalItem_Weight
    '            objTr.Conv_Factor = objShipmentDetail.Conv_Factor
    '            objTr.Purchase_Cost = objShipmentDetail.Purchase_Cost
    '            objTr.OrgRate = objShipmentDetail.OrgRate

    '            objTr.Price_Amount1 = objShipmentDetail.Price_Amount1
    '            objTr.Price_Amount2 = objShipmentDetail.Price_Amount2
    '            objTr.Price_Amount3 = objShipmentDetail.Price_Amount3
    '            objTr.Price_Amount4 = objShipmentDetail.Price_Amount4
    '            objTr.Price_Amount5 = objShipmentDetail.Price_Amount5
    '            objTr.Price_Amount6 = objShipmentDetail.Price_Amount6
    '            objTr.Price_Amount7 = objShipmentDetail.Price_Amount7
    '            objTr.Price_Amount8 = objShipmentDetail.Price_Amount8
    '            objTr.Price_Amount9 = objShipmentDetail.Price_Amount9
    '            objTr.Price_Amount10 = objShipmentDetail.Price_Amount10

    '            objTr.TAX1_Base_Amt = objShipmentDetail.TAX1_Base_Amt
    '            objTr.TAX2_Base_Amt = objShipmentDetail.TAX2_Base_Amt
    '            objTr.TAX3_Base_Amt = objShipmentDetail.TAX3_Base_Amt
    '            objTr.TAX4_Base_Amt = objShipmentDetail.TAX4_Base_Amt
    '            objTr.TAX5_Base_Amt = objShipmentDetail.TAX5_Base_Amt
    '            objTr.TAX6_Base_Amt = objShipmentDetail.TAX6_Base_Amt
    '            objTr.TAX7_Base_Amt = objShipmentDetail.TAX7_Base_Amt
    '            objTr.TAX8_Base_Amt = objShipmentDetail.TAX8_Base_Amt
    '            objTr.TAX9_Base_Amt = objShipmentDetail.TAX9_Base_Amt
    '            objTr.TAX10_Base_Amt = objShipmentDetail.TAX10_Base_Amt

    '            objTr.Commission_Rate = objShipmentDetail.Commission_Rate
    '            objTr.Commission_Party = objShipmentDetail.Commission_Party
    '            objTr.Commission_Amt = objShipmentDetail.Commission_Amt
    '            objTr.Amt_Less_Commission = objShipmentDetail.Amt_Less_Commission

    '            obj.Arr.Add(objTr)
    '        Next
    '    End If
    '    Return obj
    'End Function
    'Private Shared Function ConvertShipmentToSaleOrder(ByVal objShipment As clsMCCMaterialSaleFarmer) As clsPSSalesOrder
    '    Dim obj As New clsPSSalesOrder()
    '    obj = New clsPSSalesOrder()
    '    obj.Auto_SaleOrder = 1
    '    obj.Total_Comm_Amt = objShipment.Total_Comm_Amt
    '    obj.Cust_PO_No = objShipment.Cust_PO_No
    '    obj.HeadDisc_Per = objShipment.HeadDisc_Per
    '    obj.HeadDisc_PerAmt = objShipment.HeadDisc_PerAmt
    '    obj.HeadDisc_Amt = objShipment.HeadDisc_Amt
    '    obj.Road_Permit_No = objShipment.Road_Permit_No
    '    obj.Price_Group_Code = objShipment.Price_Group_Code
    '    obj.Route_No = objShipment.Route_No
    '    obj.Route_Desc = objShipment.Route_Desc
    '    obj.Price_Code = objShipment.Price_Code
    '    obj.Document_Date = clsCommon.GetPrintDate(objShipment.Document_Date, "dd-MMM-yyyy hh:mm:ss")
    '    obj.CloseSO = "N"
    '    obj.Delivery_date = clsCommon.GetPrintDate(objShipment.Document_Date, "dd-MMM-yyyy hh:mm:ss")
    '    obj.Farmer_Code = objShipment.Farmer_Code
    '    obj.Farmer_Name = objShipment.Farmer_Name
    '    obj.Ref_No = objShipment.Ref_No
    '    obj.Total_Tax_Amt = objShipment.Total_Tax_Amt
    '    obj.Remarks = objShipment.Remarks
    '    obj.Bill_To_Location = objShipment.Bill_To_Location
    '    obj.Ship_To_Location = objShipment.Ship_To_Location
    '    obj.Comments = objShipment.Comments
    '    obj.On_Hold = objShipment.On_Hold
    '    obj.Mode_Of_Transport = "By Road"
    '    obj.Description = objShipment.Description
    '    obj.Tax_Group = objShipment.Tax_Group
    '    obj.SalesOrder_Type = ""
    '    obj.Item_Type = objShipment.Item_Type
    '    obj.Dept = objShipment.Dept
    '    obj.Dept_Desc = objShipment.Dept_Desc
    '    obj.PROJECT_ID = objShipment.PROJECT_ID
    '    obj.Approvel_Required = 0
    '    If clsCommon.myLen(objShipment.TAX1) > 0 Then
    '        obj.TAX1 = objShipment.TAX1
    '        obj.TAX1_Rate = objShipment.TAX1_Rate
    '        obj.TAX1_Base_Amt = objShipment.TAX1_Base_Amt
    '        obj.TAX1_Amt = objShipment.TAX1_Amt
    '    End If
    '    If clsCommon.myLen(objShipment.TAX2) > 0 Then
    '        obj.TAX2 = objShipment.TAX2
    '        obj.TAX2_Rate = objShipment.TAX2_Rate
    '        obj.TAX2_Base_Amt = objShipment.TAX2_Base_Amt
    '        obj.TAX2_Amt = objShipment.TAX2_Amt
    '    End If
    '    If clsCommon.myLen(objShipment.TAX3) > 0 Then
    '        obj.TAX3 = objShipment.TAX3
    '        obj.TAX3_Rate = objShipment.TAX3_Rate
    '        obj.TAX3_Base_Amt = objShipment.TAX3_Base_Amt
    '        obj.TAX3_Amt = objShipment.TAX3_Amt
    '    End If
    '    If clsCommon.myLen(objShipment.TAX4) > 0 Then
    '        obj.TAX4 = objShipment.TAX4
    '        obj.TAX4_Rate = objShipment.TAX4_Rate
    '        obj.TAX4_Base_Amt = objShipment.TAX4_Base_Amt
    '        obj.TAX4_Amt = objShipment.TAX4_Amt
    '    End If
    '    If clsCommon.myLen(objShipment.TAX5) > 0 Then
    '        obj.TAX5 = objShipment.TAX1
    '        obj.TAX5_Rate = objShipment.TAX5_Rate
    '        obj.TAX5_Base_Amt = objShipment.TAX5_Base_Amt
    '        obj.TAX5_Amt = objShipment.TAX5_Amt
    '    End If
    '    If clsCommon.myLen(objShipment.TAX6) > 0 Then
    '        obj.TAX6 = objShipment.TAX6
    '        obj.TAX6_Rate = objShipment.TAX6_Rate
    '        obj.TAX6_Base_Amt = objShipment.TAX6_Base_Amt
    '        obj.TAX6_Amt = objShipment.TAX6_Amt
    '    End If
    '    If clsCommon.myLen(objShipment.TAX7) > 0 Then
    '        obj.TAX7 = objShipment.TAX7
    '        obj.TAX7_Rate = objShipment.TAX7_Rate
    '        obj.TAX7_Base_Amt = objShipment.TAX7_Base_Amt
    '        obj.TAX7_Amt = objShipment.TAX7_Amt
    '    End If
    '    If clsCommon.myLen(objShipment.TAX8) > 0 Then
    '        obj.TAX8 = objShipment.TAX8
    '        obj.TAX8_Rate = objShipment.TAX8_Rate
    '        obj.TAX8_Base_Amt = objShipment.TAX8_Base_Amt
    '        obj.TAX8_Amt = objShipment.TAX8_Amt
    '    End If
    '    If clsCommon.myLen(objShipment.TAX9) > 0 Then
    '        obj.TAX9 = objShipment.TAX9
    '        obj.TAX9_Rate = objShipment.TAX9_Rate
    '        obj.TAX9_Base_Amt = objShipment.TAX9_Base_Amt
    '        obj.TAX9_Amt = objShipment.TAX9_Amt
    '    End If
    '    If clsCommon.myLen(objShipment.TAX1) > 0 Then
    '        obj.TAX10 = objShipment.TAX10
    '        obj.TAX10_Rate = objShipment.TAX10_Rate
    '        obj.TAX10_Base_Amt = objShipment.TAX10_Base_Amt
    '        obj.TAX10_Amt = objShipment.TAX10_Amt
    '    End If


    '    obj.Terms_Code = objShipment.Terms_Code
    '    obj.Due_Date = objShipment.Due_Date
    '    obj.Discount_Base = objShipment.Discount_Base
    '    obj.Discount_Amt = objShipment.Discount_Amt
    '    obj.Amount_Less_Discount = objShipment.Amount_Less_Discount
    '    obj.Total_Amt = objShipment.Total_Amt
    '    obj.Abandonment_No = 0
    '    'obj.Against_Quotation_No = txtReqNo.Value
    '    obj.Against_DeliveryNo = ""
    '    obj.SO_Validity = 0
    '    obj.Commission_Apply = objShipment.Commission_Apply
    '    obj.Dispatch_date = objShipment.Dispatch_date
    '    obj.Vehicle_Code = objShipment.Vehicle_Code
    '    obj.Vehicle_No = objShipment.VehicleNo
    '    obj.Vehicle_Capacity = objShipment.Vehicle_Capacity
    '    obj.Payment_Terms = objShipment.Payment_Terms
    '    obj.Dispatch_Terms = objShipment.Dispatch_Terms
    '    obj.Dispatch_Period = objShipment.Dispatch_Period
    '    'If clsCommon.myLen(txtReqNo.Value) = 0 Then
    '    '    txtDispatchDate.Value = txtDeliveryDate.Value.AddDays(txtDispatchPeriod.Value)
    '    'End If



    '    If clsCommon.myLen(obj.Add_Charge_Code1) > 0 Then
    '        obj.Add_Charge_Code1 = objShipment.Add_Charge_Code1
    '        obj.Add_Charge_Name1 = objShipment.Add_Charge_Name1
    '        obj.Add_Charge_Amt1 = objShipment.Add_Charge_Amt1
    '    End If
    '    If clsCommon.myLen(obj.Add_Charge_Code2) > 0 Then
    '        obj.Add_Charge_Code2 = objShipment.Add_Charge_Code2
    '        obj.Add_Charge_Name2 = objShipment.Add_Charge_Name2
    '        obj.Add_Charge_Amt2 = objShipment.Add_Charge_Amt2
    '    End If
    '    If clsCommon.myLen(obj.Add_Charge_Code3) > 0 Then
    '        obj.Add_Charge_Code3 = objShipment.Add_Charge_Code3
    '        obj.Add_Charge_Name3 = objShipment.Add_Charge_Name3
    '        obj.Add_Charge_Amt3 = objShipment.Add_Charge_Amt3
    '    End If
    '    If clsCommon.myLen(obj.Add_Charge_Code4) > 0 Then
    '        obj.Add_Charge_Code4 = objShipment.Add_Charge_Code4
    '        obj.Add_Charge_Name4 = objShipment.Add_Charge_Name4
    '        obj.Add_Charge_Amt4 = objShipment.Add_Charge_Amt4
    '    End If
    '    If clsCommon.myLen(obj.Add_Charge_Code5) > 0 Then
    '        obj.Add_Charge_Code5 = objShipment.Add_Charge_Code5
    '        obj.Add_Charge_Name1 = objShipment.Add_Charge_Name5
    '        obj.Add_Charge_Amt5 = objShipment.Add_Charge_Amt5
    '    End If
    '    If clsCommon.myLen(obj.Add_Charge_Code6) > 0 Then
    '        obj.Add_Charge_Code6 = objShipment.Add_Charge_Code6
    '        obj.Add_Charge_Name6 = objShipment.Add_Charge_Name6
    '        obj.Add_Charge_Amt6 = objShipment.Add_Charge_Amt6
    '    End If
    '    If clsCommon.myLen(obj.Add_Charge_Code7) > 0 Then
    '        obj.Add_Charge_Code7 = objShipment.Add_Charge_Code7
    '        obj.Add_Charge_Name7 = objShipment.Add_Charge_Name7
    '        obj.Add_Charge_Amt7 = objShipment.Add_Charge_Amt7
    '    End If
    '    If clsCommon.myLen(obj.Add_Charge_Code8) > 0 Then
    '        obj.Add_Charge_Code8 = objShipment.Add_Charge_Code8
    '        obj.Add_Charge_Name8 = objShipment.Add_Charge_Name8
    '        obj.Add_Charge_Amt8 = objShipment.Add_Charge_Amt8
    '    End If
    '    If clsCommon.myLen(obj.Add_Charge_Code9) > 0 Then
    '        obj.Add_Charge_Code9 = objShipment.Add_Charge_Code9
    '        obj.Add_Charge_Name9 = objShipment.Add_Charge_Name9
    '        obj.Add_Charge_Amt9 = objShipment.Add_Charge_Amt9
    '    End If
    '    If clsCommon.myLen(obj.Add_Charge_Code10) > 0 Then
    '        obj.Add_Charge_Code10 = objShipment.Add_Charge_Code10
    '        obj.Add_Charge_Name10 = objShipment.Add_Charge_Name10
    '        obj.Add_Charge_Amt10 = objShipment.Add_Charge_Amt10
    '    End If
    '    obj.Total_Add_Charge = objShipment.Total_Add_Charge

    '    obj.Salesman_Code = objShipment.Salesman_Code
    '    obj.Salesman_Name = objShipment.Salesman_Name

    '    obj.Arr = New List(Of clsPSSalesOrderDetail)
    '    For Each objShipmentDetail As clsMCCMaterialSaleFarmerDetail In objShipment.Arr

    '        Dim objTr As New clsPSSalesOrderDetail()
    '        objTr.Line_No = objShipmentDetail.Line_No
    '        objTr.Row_Type = objShipmentDetail.Row_Type
    '        objTr.Item_Code = objShipmentDetail.Item_Code
    '        objTr.Item_Desc = objShipmentDetail.Item_Desc
    '        objTr.Qty = objShipmentDetail.Qty
    '        objTr.Balance_Qty = 0
    '        objTr.Unit_code = objShipmentDetail.Unit_code
    '        objTr.OrgUnit_code = objShipmentDetail.OrgUnit_code
    '        ' objTr.BOOK_QTY_UOM = objShipmentDetail.Unit_code
    '        'objTr.BOOK_RATE_UOM = objShipmentDetail.Unit_code
    '        'objTr.Quotation_Code = clsCommon.myCstr(grow.Cells(colReqistionNo).Value)
    '        objTr.Location = objShipmentDetail.Location
    '        objTr.Ship_Party = obj.Farmer_Code
    '        objTr.Location = objShipmentDetail.Location
    '        objTr.Item_Cost = objShipmentDetail.Item_Cost
    '        objTr.Amount = objShipmentDetail.Amount
    '        objTr.Disc_Per = objShipmentDetail.Disc_Per
    '        objTr.Disc_Amt = objShipmentDetail.Disc_Amt
    '        objTr.Amt_Less_Discount = objShipmentDetail.Amt_Less_Discount
    '        objTr.TAX1 = objShipmentDetail.TAX1
    '        objTr.TAX1_Base_Amt = objShipmentDetail.TAX1_Base_Amt
    '        objTr.TAX1_Rate = objShipmentDetail.TAX1_Rate
    '        objTr.TAX1_Amt = objShipmentDetail.TAX1_Amt
    '        objTr.TAX2 = objShipmentDetail.TAX2
    '        objTr.TAX2_Base_Amt = objShipmentDetail.TAX2_Base_Amt
    '        objTr.TAX2_Rate = objShipmentDetail.TAX2_Rate
    '        objTr.TAX2_Amt = objShipmentDetail.TAX2_Amt
    '        objTr.TAX3 = objShipmentDetail.TAX3
    '        objTr.TAX3_Base_Amt = objShipmentDetail.TAX3_Base_Amt
    '        objTr.TAX3_Rate = objShipmentDetail.TAX3_Rate
    '        objTr.TAX3_Amt = objShipmentDetail.TAX3_Amt
    '        objTr.TAX4 = objShipmentDetail.TAX4
    '        objTr.TAX4_Base_Amt = objShipmentDetail.TAX4_Base_Amt
    '        objTr.TAX4_Rate = objShipmentDetail.TAX4_Rate
    '        objTr.TAX4_Amt = objShipmentDetail.TAX4_Amt
    '        objTr.TAX5 = objShipmentDetail.TAX5
    '        objTr.TAX5_Base_Amt = objShipmentDetail.TAX5_Base_Amt
    '        objTr.TAX5_Rate = objShipmentDetail.TAX5_Rate
    '        objTr.TAX5_Amt = objShipmentDetail.TAX5_Amt
    '        objTr.TAX6 = objShipmentDetail.TAX6
    '        objTr.TAX6_Base_Amt = objShipmentDetail.TAX6_Base_Amt
    '        objTr.TAX6_Rate = objShipmentDetail.TAX6_Rate
    '        objTr.TAX6_Amt = objShipmentDetail.TAX6_Amt
    '        objTr.TAX7 = objShipmentDetail.TAX7
    '        objTr.TAX7_Base_Amt = objShipmentDetail.TAX7_Base_Amt
    '        objTr.TAX7_Rate = objShipmentDetail.TAX7_Rate
    '        objTr.TAX7_Amt = objShipmentDetail.TAX7_Amt
    '        objTr.TAX8 = objShipmentDetail.TAX8
    '        objTr.TAX8_Base_Amt = objShipmentDetail.TAX8_Base_Amt
    '        objTr.TAX8_Rate = objShipmentDetail.TAX8_Rate
    '        objTr.TAX8_Amt = objShipmentDetail.TAX8_Amt
    '        objTr.TAX9 = objShipmentDetail.TAX9
    '        objTr.TAX9_Base_Amt = objShipmentDetail.TAX9_Base_Amt
    '        objTr.TAX9_Rate = objShipmentDetail.TAX9_Rate
    '        objTr.TAX9_Amt = objShipmentDetail.TAX9_Amt
    '        objTr.TAX10 = objShipmentDetail.TAX10
    '        objTr.TAX10_Base_Amt = objShipmentDetail.TAX10_Base_Amt
    '        objTr.TAX10_Rate = objShipmentDetail.TAX10_Rate
    '        objTr.TAX10_Amt = objShipmentDetail.TAX10_Amt
    '        objTr.Total_Tax_Amt = objShipmentDetail.Total_Tax_Amt
    '        objTr.Item_Net_Amt = objShipmentDetail.Item_Net_Amt
    '        objTr.Specification = objShipmentDetail.Specification
    '        objTr.Remarks = objShipmentDetail.Remarks
    '        'objTr.Location = txtBillToLocation.Value 'clsCommon.myCstr(grow.Cells(colLocationCode).Value)
    '        objTr.MRP = objShipmentDetail.MRP
    '        objTr.Scheme_Applicable = objShipmentDetail.Scheme_Applicable
    '        objTr.Scheme_Code = objShipmentDetail.Scheme_Code
    '        objTr.Scheme_Item = objShipmentDetail.Scheme_Item
    '        objTr.Item_Tax = objShipmentDetail.Item_Tax
    '        objTr.Total_MRP_Amt = objShipmentDetail.Total_MRP_Amt
    '        objTr.Total_Basic_Amt = objShipmentDetail.Total_Basic_Amt
    '        objTr.Total_Disc_Amt = objShipmentDetail.Total_Disc_Amt
    '        objTr.Cust_Discount = objShipmentDetail.Cust_Discount
    '        objTr.Total_Cust_Discount = objShipmentDetail.Total_Cust_Discount
    '        objTr.ActualRate = objShipmentDetail.ActualRate
    '        objTr.Cust_DiscountQty = objShipmentDetail.Cust_DiscountQty
    '        objTr.Price_Date = objShipmentDetail.Price_Date
    '        objTr.Price_code = objShipmentDetail.Price_code
    '        objTr.Abatement_Per = objShipmentDetail.Abatement_Per
    '        objTr.Abatement_Amt = objShipmentDetail.Abatement_Amt
    '        objTr.FOC_Item = objShipmentDetail.FOC_Item

    '        objTr.Item_Weight = objShipmentDetail.Item_Weight
    '        objTr.Conv_Factor = objShipmentDetail.Conv_Factor
    '        objTr.TotalItem_Weight = objShipmentDetail.TotalItem_Weight
    '        objTr.Batch_No = objShipmentDetail.Batch_No
    '        objTr.Bin_No = objShipmentDetail.Bin_No
    '        objTr.HeadDiscPer = objShipmentDetail.HeadDiscPer
    '        objTr.HeadDiscPerAmt = objShipmentDetail.HeadDiscPerAmt
    '        objTr.Expiry_Date = objShipmentDetail.Expiry_Date
    '        objTr.MFG_Date = objShipmentDetail.MFG_Date
    '        objTr.Markup_On = objShipmentDetail.Markup_On
    '        objTr.Markup_Percent = objShipmentDetail.Markup_Percent
    '        objTr.Landing_Cost = objShipmentDetail.Landing_Cost
    '        objTr.CustDiscPer = objShipmentDetail.CustDiscPer
    '        objTr.HeadDiscAmt = objShipmentDetail.HeadDiscAmt
    '        objTr.CasdDiscScheme_Code = objShipmentDetail.CasdDiscScheme_Code
    '        objTr.Purchase_Cost = objShipmentDetail.Purchase_Cost
    '        objTr.OrgRate = objShipmentDetail.OrgRate
    '        objTr.PrincipleCode = objShipmentDetail.PrincipleCode
    '        objTr.PrincipleDesc = objShipmentDetail.PrincipleDesc
    '        objTr.vendor_code = objShipmentDetail.vendor_code
    '        objTr.vendor_desc = objShipmentDetail.vendor_desc

    '        objTr.HeadDiscPer = objShipmentDetail.HeadDiscPer
    '        objTr.HeadDiscPerAmt = objShipmentDetail.HeadDiscPerAmt

    '        objTr.Commission_Rate = objShipmentDetail.Commission_Rate
    '        objTr.Commission_Party = objShipmentDetail.Commission_Party
    '        objTr.Commission_Amt = objShipmentDetail.Commission_Amt
    '        objTr.Amt_Less_Commission = objShipmentDetail.Amt_Less_Commission
    '        objTr.Ship_Party = obj.Farmer_Code
    '        objTr.delivery_code = ""
    '        ''objTr.Assessable = clsCommon.myCdbl(grow.Cells(colAssessableRate).Value)
    '        ''objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
    '        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
    '            obj.Arr.Add(objTr)
    '        End If
    '    Next

    '    Return obj
    'End Function

    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsMCCMaterialSaleFarmerDetail)) As String


        For Each objtr As clsMCCMaterialSaleFarmerDetail In Arr
            If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Return objtr.Item_Code
            End If
        Next
        Return ""
    End Function


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
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsMCCMaterialSaleFarmer = clsMCCMaterialSaleFarmer.GetData(strCode, NavigatorType.Current, trans)
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                '' Anubhooti 06-Sep-2014 BM00000003735 (Locked Transaction)
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleFarmerPayment, clsUserMgtCode.frmMCCMaterialFarmer, obj.Bill_To_Location, obj.Document_Date, trans)
                ''
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsSerializeInvenotry.DeleteData("SD-IN", strCode, trans)
                clsBatchInventory.DeleteData("MCC-MSALE-F", obj.Document_Code, trans)

                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_MCC_Sale_Farmer_Head", "Document_Code", "TSPL_MCC_Sale_Farmer_Detail", "Document_Code", trans)

                Dim qry As String = "delete from TSPL_MCC_Sale_Farmer_Detail where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                'qry = "delete from TSPL_SD_SALE_INVOICE_Detail where DOCUMENT_CODE in (select DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" + strCode + "')"
                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                'qry = "delete from TSPL_SD_SALE_INVOICE_HEAD where Against_Shipment_No='" + strCode + "'"
                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_MCC_Sale_Farmer_Head where Document_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)
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

    Public Shared Function IsValidCustomer(ByVal Arr As List(Of String), ByVal strCustomerCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select TSPL_MCC_Sale_Farmer_Head.Document_Code,TSPL_MCC_Sale_Farmer_Head.Farmer_Code,TSPL_MP_MASTER.Farmer_Name from TSPL_MCC_Sale_Farmer_Head left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.Cust_Code=TSPL_MCC_Sale_Farmer_Head.Farmer_Code where Document_Code in (" + clsCommon.GetMulcallString(Arr) + ") and Farmer_Code not in ('" + strCustomerCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "SRN No:" + clsCommon.myCstr(dr("Document_Code")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Farmer_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Farmer_Name"))
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
    '                qry = "select Document_Code,MAX(ItemType )as ItemType,MAX(MRN_Date) as Document_Date,MAX(Farmer_Name) as Farmer_Name,MAX(GRNo) as GRNo,MAX(GENo) as GENo,MAX(GEDate) as GEDate,Item_Code,MAX(Item_Desc) as Item_Desc,MAX(VehicleNo) as VehicleNo, SUM(ISNULL( FCS,0)) as FCS, SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ECS,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, SUM(Leak_Qty) as HF,SUM(Burst_Qty) as Burst,SUM(Short_Qty) as Short,MAX(Remarks) as Remarks,max(Ref_No)as Ref_No from( " & _
    '         "select TSPL_MCC_Sale_Farmer_Head.Document_Code,TSPL_MCC_Sale_Farmer_Head .Item_Type as ItemType," & _
    '         "(replace( CONVERT(varchar(11), TSPL_MCC_Sale_Farmer_Head.Document_Date,104),'.','/')+' '+CONVERT(varchar(100),TSPL_MCC_Sale_Farmer_Head.Document_Date,108) )as MRN_Date,TSPL_MCC_Sale_Farmer_Head.Farmer_Name,TSPL_MCC_Sale_Farmer_Head.GRNo,TSPL_MCC_Sale_Farmer_Head.GENo," & _
    '         "(case when LEN(TSPL_MCC_Sale_Farmer_Head.GEDate)>0  then REPLACE( CONVERT(varchar(11), TSPL_MCC_Sale_Farmer_Head.GEDate,104),'.','/') else '' end) as GEDate,TSPL_MCC_Sale_Farmer_Head.VehicleNo,TSPL_MCC_Sale_Farmer_Head.Remarks ,TSPL_MCC_Sale_Farmer_Head.Ref_No,TSPL_MCC_Sale_Farmer_Detail.Item_Code,TSPL_MCC_Sale_Farmer_Detail.Item_Desc,TSPL_MCC_Sale_Farmer_Detail.Unit_code," & _
    '         "case when Unit_code='FC' then Qty + ISNULL( Free_Qty,0) end as FCS, " & _
    '         "case when Unit_code='FB' then Qty + ISNULL( Free_Qty,0) end as FBS, " & _
    '         "case when Unit_code='SH' then Qty + ISNULL( Free_Qty,0) end as FSH, " & _
    '         "case when Unit_code='EC' then Qty + ISNULL( Free_Qty,0) end as ECS," & _
    '         "case when Unit_code='EB' then Qty + ISNULL( Free_Qty,0) end as EBS, " & _
    '         "TSPL_MCC_Sale_Farmer_Detail.Leak_Qty,TSPL_MCC_Sale_Farmer_Detail.Burst_Qty,TSPL_MCC_Sale_Farmer_Detail.Short_Qty from TSPL_MCC_Sale_Farmer_Detail left outer join TSPL_MCC_Sale_Farmer_Head on TSPL_MCC_Sale_Farmer_Head.Document_Code= TSPL_MCC_Sale_Farmer_Detail.Document_Code " & _
    '         " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_MCC_Sale_Farmer_Head.Bill_To_Location   where Item_Type ='F'"
    '                If FromDate.HasValue AndAlso ToDate.HasValue Then
    '                    qry += " and Convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date,103)<=Convert(date,'" + ToDate + "',103) "
    '                End If

    '                If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
    '                    qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
    '                End If
    '                If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
    '                    qry += " and TSPL_MCC_Sale_Farmer_Head.Document_Code in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
    '                End If
    '                If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
    '                    qry += " and TSPL_MCC_Sale_Farmer_Head.Farmer_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")" 'ADDED BY ABHISHEK AS ON 30 AUG 2012
    '                End If
    '                qry += " )xxx group by Document_Code,Item_Code order by Item_Desc"
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                    common.clsCommon.MyMessageBoxShow("No Record Found")
    '                Else
    '                    PurchaseOrderViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x6, "rptSRNCustomReport", "SRN Report")

    '                End If
    '            Else ''For RM Other Print out
    '                Dim strquery As String = "SELECT TSPL_MCC_Sale_Farmer_Head.Document_Code, TSPL_MCC_Sale_Farmer_Head.Document_Date,TSPL_MCC_Sale_Farmer_Head.Farmer_Name,(case when len(against_mrn)>0 then (select MRN_Date  from tspl_mrn_head where tspl_mrn_head.MRN_No =against_mrn) else Document_Date end ) as Challan_Date, TSPL_MCC_Sale_Farmer_Head.Ref_No  " & _
    '                      "as Challan_No, TSPL_MCC_Sale_Farmer_Head.Inv_No, TSPL_MCC_Sale_Farmer_Head.Inv_Date, TSPL_MCC_Sale_Farmer_Head.GRNo,TSPL_MCC_Sale_Farmer_Head.Amount_Less_Discount ,TSPL_MCC_Sale_Farmer_Head.GENo,TSPL_MCC_Sale_Farmer_Head.Total_Amt, " & _
    '                      "TSPL_MCC_Sale_Farmer_Head.GEDate, TSPL_MCC_Sale_Farmer_Head.VehicleNo, TSPL_MCC_Sale_Farmer_Head.Carrier,TSPL_MCC_Sale_Farmer_Head.Remarks,TSPL_MCC_Sale_Farmer_Detail.Landed_Cost_Rate,TSPL_MCC_Sale_Farmer_Detail.Landed_Cost_Amount , TSPL_MCC_Sale_Farmer_Detail.Item_Code,TSPL_MCC_Sale_Farmer_Detail.Row_Type,TSPL_MCC_Sale_Farmer_Detail.Amt_Less_Discount," & _
    '"TSPL_MCC_Sale_Farmer_Detail.Item_Cost as basicRate,TSPL_MCC_Sale_Farmer_Detail.Item_Net_Amt as BasicTotal,TSPL_MCC_Sale_Farmer_Detail.Unit_Cost_Tax_Rate as UCTR," & _
    '"TSPL_MCC_Sale_Farmer_Detail.Unit_Cost_Tax as uctax,TSPL_MCC_Sale_Farmer_Detail.Item_Desc,TSPL_MCC_Sale_Farmer_Detail.Unit_code,TSPL_MCC_Sale_Farmer_Detail.Qty,TSPL_MCC_Sale_Farmer_Detail.Rejected_Qty,TSPL_MCC_Sale_Farmer_Head.Farmer_Code,TSPL_MCC_Sale_Farmer_Head.Total_Amt,TSPL_MCC_Sale_Farmer_Detail.ITEM_COST," & _
    ' "TSPL_VENDOR_MASTER.Add1 as venAdd1, TSPL_VENDOR_MASTER.Add2 as vanadd2, TSPL_VENDOR_MASTER.Add3 as venadd3, " & _
    '"tax1.Tax_Code_Desc as tax1name,isnull (TSPL_MCC_Sale_Farmer_Head.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name," & _
    '"isnull (TSPL_MCC_Sale_Farmer_Head.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_MCC_Sale_Farmer_Head.tax3_amt,0) as txt3amt," & _
    '"tax4.Tax_Code_Desc as tax4name,isnull (TSPL_MCC_Sale_Farmer_Head.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name," & _
    '"isnull (TSPL_MCC_Sale_Farmer_Head.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_MCC_Sale_Farmer_Head.tax6_amt,0) as txt6amt " & _
    '",tax7.Tax_Code_Desc as tax7name,isnull (TSPL_MCC_Sale_Farmer_Head.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name," & _
    '"isnull (TSPL_MCC_Sale_Farmer_Head.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (TSPL_MCC_Sale_Farmer_Head.tax9_amt,0) as txt9amt," & _
    '"tax10.Tax_Code_Desc as tax10name,isnull (TSPL_MCC_Sale_Farmer_Head.tax10_amt,0) as txt10amt, TSPL_COMPANY_MASTER.Comp_Name as compname, " & _
    '"TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_MCC_Sale_Farmer_Detail.Qty," & _
    '"case when tax1.Tax_Recoverable='Y' then TSPL_MCC_Sale_Farmer_Head.tax1_amt else null end as Tax1Recoverable," & _
    '"case when tax2.Tax_Recoverable='Y' then TSPL_MCC_Sale_Farmer_Head.TAX2_Amt else null end as Tax2Recoverable, " & _
    '"case when tax3.Tax_Recoverable='Y' then TSPL_MCC_Sale_Farmer_Head.tax3_amt else null end as Tax3Recoverable, " & _
    '"case when tax4.Tax_Recoverable='Y' then TSPL_MCC_Sale_Farmer_Head.tax4_amt else null end as Tax4Recoverable, " & _
    '"case when tax5.Tax_Recoverable='Y' then TSPL_MCC_Sale_Farmer_Head.tax5_amt else null end as Tax5Recoverable, " & _
    '"case when tax6.Tax_Recoverable='Y' then TSPL_MCC_Sale_Farmer_Head.tax6_amt else null end as Tax6Recoverable," & _
    '"case when tax7.Tax_Recoverable='Y' then TSPL_MCC_Sale_Farmer_Head.tax7_amt else null end as Tax7Recoverable, " & _
    '"case when tax8.Tax_Recoverable='Y' then TSPL_MCC_Sale_Farmer_Head.tax8_amt else null end as Tax8Recoverable, " & _
    '"case when tax9.Tax_Recoverable='Y' then TSPL_MCC_Sale_Farmer_Head.tax9_amt else null end as Tax9Recoverable," & _
    '"case when tax10.Tax_Recoverable='Y' then TSPL_MCC_Sale_Farmer_Head.tax10_amt else null end as Tax10Recoverable, " & _
    '"convert(varchar,isnull (TSPL_MCC_Sale_Farmer_Head.TAX1_Rate ,0),103)+'%' as txt1Rate," & _
    '"convert(varchar,isnull (TSPL_MCC_Sale_Farmer_Head.TAX2_Rate   ,0),103)+'%' as txt2Rate, " & _
    '"convert(varchar,isnull (TSPL_MCC_Sale_Farmer_Head.TAX3_Rate  ,0),103)+'%' as txt3Rate, " & _
    '"convert(varchar,isnull (TSPL_MCC_Sale_Farmer_Head.TAX4_Rate  ,0),103)+'%' as txt4Rate, " & _
    '"convert(varchar,isnull (TSPL_MCC_Sale_Farmer_Head.TAX5_Rate  ,0),103)+'%' as txt5Rate, " & _
    '"convert(varchar,isnull (TSPL_MCC_Sale_Farmer_Head.TAX6_Rate  ,0),103)+'%' as txt6Rate, " & _
    '"convert(varchar,isnull (TSPL_MCC_Sale_Farmer_Head.TAX7_Rate  ,0),103)+'%' as txt7Rate, " & _
    '"convert(varchar,isnull (TSPL_MCC_Sale_Farmer_Head.TAX8_Rate  ,0),103)+'%' as txt8Rate, " & _
    '"convert(varchar,isnull (TSPL_MCC_Sale_Farmer_Head.TAX9_Rate  ,0),103)+'%' as txt9Rate, " & _
    '"convert(varchar,isnull (TSPL_MCC_Sale_Farmer_Head.TAX10_Rate  ,0),103)+'%' as txt10Rate," & _
    '"TSPL_MCC_Sale_Farmer_Detail.Amt_Less_Discount as Value,(select SUM(rejected_qty) from TSPL_MCC_Sale_Farmer_Detail where Document_Code=TSPL_MCC_Sale_Farmer_Head.Document_Code) as Rej_qty, (select SUM(TSPL_MRN_DETAIL.MRN_Qty) from TSPL_MCC_Sale_Farmer_Detail left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .MRN_No=TSPL_MCC_Sale_Farmer_Detail.Order_Code and TSPL_MRN_DETAIL.Item_Code=TSPL_MCC_Sale_Farmer_Detail.Item_Code where Document_Code =TSPL_MCC_Sale_Farmer_Head.Document_Code)as MrnTotQty, (select SUM(Qty) from TSPL_MCC_Sale_Farmer_Detail where Document_Code=TSPL_MCC_Sale_Farmer_Head.Document_Code) as SRNQtyTotal, (select case when COUNT(xxx.PI_No)>1 then Min(xxx.PI_No)+ ' *' else Min(xxx.PI_No)end as PINO from" & _
    '" ( select TSPL_PI_DETAIL.PI_No from TSPL_PI_DETAIL  where  TSPL_PI_DETAIL.SRN_Id= TSPL_MCC_Sale_Farmer_Head.Document_Code " & _
    '" GROUP by TSPL_PI_DETAIL.PI_No)xxx) as PInvNo  ,    " & _
    '       " TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name1 as Add1Name, " & _
    '     " TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt1 as Add1 , " & _
    '     "     TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name2 as Add2Name, " & _
    '     "   TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt2 as Add2 , " & _
    '     "    TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name3 as Add3Name, " & _
    '     "   TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt3 as Add3 , " & _
    '     "    TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name4 as Add4Name, " & _
    '     "    TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt4 as Add4 , " & _
    '     "     TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name5 as Add5Name, " & _
    '      "     TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt5 as Add5 , " & _
    '      "     TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name6 as Add6Name, " & _
    '      "    TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt6 as Add6 , " & _
    '      "    TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name7 as Add7Name, " & _
    '      "     TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt7 as Add7 , " & _
    '      "       TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name8 as Add8Name, " & _
    '      "      TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt8 as Add8 , " & _
    '       "      TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name9 as Add9Name, " & _
    '       "      TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt9 as Add9 , " & _
    '       "      TSPL_MCC_Sale_Farmer_Head.Add_Charge_Name10 as Add10Name, " & _
    '       "     TSPL_MCC_Sale_Farmer_Head.Add_Charge_Amt10 as Add10,TSPL_MCC_Sale_Farmer_Head.Against_RGP,TSPL_MCC_Sale_Farmer_Detail .Specification   " & _
    ' " FROM  TSPL_MCC_Sale_Farmer_Detail INNER JOIN TSPL_MCC_Sale_Farmer_Head ON TSPL_MCC_Sale_Farmer_Detail.Document_Code = TSPL_MCC_Sale_Farmer_Head.Document_Code " & _
    ' "INNER JOIN TSPL_COMPANY_MASTER ON TSPL_MCC_Sale_Farmer_Head.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  " & _
    ' "INNER JOIN TSPL_VENDOR_MASTER ON TSPL_MCC_Sale_Farmer_Head.Farmer_Code = TSPL_VENDOR_MASTER.Farmer_Code " & _
    ' "left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_MCC_Sale_Farmer_Head.tax1  " & _
    ' "left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_MCC_Sale_Farmer_Head.tax2 " & _
    ' "left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_MCC_Sale_Farmer_Head .TAX3 " & _
    ' "left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_MCC_Sale_Farmer_Head .tax4 " & _
    ' "left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_MCC_Sale_Farmer_Head .tax5 " & _
    ' "left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_MCC_Sale_Farmer_Head .TAX6  " & _
    ' "left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_MCC_Sale_Farmer_Head .TAX7  " & _
    ' "left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_MCC_Sale_Farmer_Head .TAX8 " & _
    ' "left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_MCC_Sale_Farmer_Head .TAX9 " & _
    ' " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_MCC_Sale_Farmer_Head .TAX10  " & _
    ' "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_MCC_Sale_Farmer_Head.Bill_To_Location  " & _
    ' " where TSPL_MCC_Sale_Farmer_Head .Item_Type not in('F')"

    '                If FromDate.HasValue AndAlso ToDate.HasValue Then
    '                    strquery += " and Convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date,103)<=Convert(date,'" + ToDate + "',103) "

    '                End If
    '                If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
    '                    strquery += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
    '                End If
    '                If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
    '                    strquery += " and TSPL_MCC_Sale_Farmer_Head.Document_Code in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
    '                End If
    '                If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
    '                    strquery += " and TSPL_MCC_Sale_Farmer_Head.Farmer_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")  "

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
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = "select Status from TSPL_MCC_Sale_Farmer_Head where Document_Code='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select distinct DOCUMENT_CODE from TSPL_MCC_Sale_Return_detail_Farmer where invoice_code='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current Document is used in following MCC Farmer sale return -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("DOCUMENT_CODE"))
                Next
                Throw New Exception(Qry)
            End If

            Qry = "select distinct Doc_No from TSPL_MP_PAY_PROCESS_MCC_SALE where Shipment_Doc_No='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current Document is used in following Payment Process Farmer No -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Doc_No"))
                Next
                Throw New Exception(Qry)
            End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='MC-FS' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            'Ticket No : ERO/31/10/19-001084 By Prabhakar 
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='MCC-MSALE-F'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_MCC_Sale_Farmer_Head set Status = 0 where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_MCC_Sale_Farmer_Head", "Document_Code", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    'Public Shared Function createARInvoiceFarmer(ByVal obj As clsMCCMaterialSaleFarmer, ByVal trans As SqlTransaction, ByVal strARNoForRecreate As String, ByVal strVoucherNoRecreate As String, ByVal strFormID As String) As Boolean
    '    ''''''''''''''''''''''''''''''''''For Making AR Invoice
    '    Dim objCustInv As New clsCustomerInvoiceHeadFarmer()
    '    ''objCustInv.Document_No ''Will be Generateed
    '    objCustInv.RoundOffAmount = 0 'obj.RoundOffAmount
    '    objCustInv.Document_Date = obj.Document_Date
    '    objCustInv.Document_Type = "I"
    '    objCustInv.Trans_Type = "MMSF" 'obj.Trans_type '"PS"
    '    objCustInv.Document_Total = obj.Total_Amt
    '    objCustInv.Farmer_Code = obj.Farmer_Code
    '    objCustInv.Farmer_Name = obj.Farmer_Name
    '    objCustInv.Posting_Date = obj.Document_Date
    '    Dim qry As String = ""
    '    'Dim qry As String = " select Cust_Account from TSPL_MP_MASTER where Cust_Code='" + obj.Farmer_Code + "'"
    '    'objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)
    '    ''objCustInv.Order_No
    '    objCustInv.loc_code = clsLocation.GetSegmentCode(obj.Bill_To_Location, trans)
    '    objCustInv.On_Hold = 0
    '    objCustInv.Remarks = obj.Remarks
    '    objCustInv.Description = obj.Description
    '    objCustInv.Tax_Group = obj.Tax_Group
    '    objCustInv.TAX1 = obj.TAX1
    '    objCustInv.TAX1_Rate = obj.TAX1_Rate
    '    objCustInv.TAX1_Amt = obj.TAX1_Amt
    '    objCustInv.TAX2 = obj.TAX2
    '    objCustInv.TAX2_Rate = obj.TAX2_Rate
    '    objCustInv.TAX2_Amt = obj.TAX2_Amt
    '    objCustInv.TAX3 = obj.TAX3
    '    objCustInv.TAX3_Rate = obj.TAX3_Rate
    '    objCustInv.TAX3_Amt = obj.TAX3_Amt
    '    objCustInv.TAX4 = obj.TAX4
    '    objCustInv.TAX4_Rate = obj.TAX4_Rate
    '    objCustInv.TAX4_Amt = obj.TAX4_Amt
    '    objCustInv.TAX5 = obj.TAX5
    '    objCustInv.TAX5_Rate = obj.TAX5_Rate
    '    objCustInv.TAX5_Amt = obj.TAX5_Amt
    '    objCustInv.TAX6 = obj.TAX6
    '    objCustInv.TAX6_Rate = obj.TAX6_Rate
    '    objCustInv.TAX6_Amt = obj.TAX6_Amt
    '    objCustInv.TAX7 = obj.TAX7
    '    objCustInv.TAX7_Rate = obj.TAX7_Rate
    '    objCustInv.TAX7_Amt = obj.TAX7_Amt
    '    objCustInv.TAX8 = obj.TAX8
    '    objCustInv.TAX8_Rate = obj.TAX8_Rate
    '    objCustInv.TAX8_Amt = obj.TAX8_Amt
    '    objCustInv.TAX9 = obj.TAX9
    '    objCustInv.TAX9_Rate = obj.TAX9_Rate
    '    objCustInv.TAX9_Amt = obj.TAX9_Amt
    '    objCustInv.TAX10 = obj.TAX10
    '    objCustInv.TAX10_Rate = obj.TAX10_Rate
    '    objCustInv.TAX10_Amt = obj.TAX10_Amt
    '    objCustInv.Total_Tax = obj.Total_Tax_Amt
    '    objCustInv.Tax1_BAmount = obj.TAX1_Base_Amt
    '    objCustInv.Tax2_BAmount = obj.TAX2_Base_Amt
    '    objCustInv.Tax3_BAmount = obj.TAX3_Base_Amt
    '    objCustInv.Tax4_BAmount = obj.TAX4_Base_Amt
    '    objCustInv.Tax5_BAmount = obj.TAX5_Base_Amt
    '    objCustInv.Tax6_BAmount = obj.TAX6_Base_Amt
    '    objCustInv.Tax7_BAmount = obj.TAX7_Base_Amt
    '    objCustInv.Tax8_BAmount = obj.TAX8_Base_Amt
    '    objCustInv.Tax9_BAmount = obj.TAX9_Base_Amt
    '    objCustInv.Tax10_BAmount = obj.TAX10_Base_Amt
    '    objCustInv.Balance_Amt = obj.Total_Amt
    '    objCustInv.Terms_Code = obj.Terms_Code
    '    objCustInv.PROJECT_ID = obj.PROJECT_ID

    '    '' currency details
    '    objCustInv.CURRENCY_CODE = obj.CURRENCY_CODE
    '    objCustInv.ConvRate = obj.ConvRate
    '    objCustInv.ApplicableFrom = obj.ApplicableFrom
    '    Dim dt As DataTable
    '    If clsCommon.myLen(obj.Terms_Code) > 0 Then
    '        qry = "select Terms_Code,Terms_Desc,No_Days from TSPL_TERMS_MASTER where Terms_Code='" + obj.Terms_Code + "'"
    '        dt = clsDBFuncationality.GetDataTable(qry, trans)
    '        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '            objCustInv.Terms_Description = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
    '            objCustInv.Due_Date = obj.Document_Date.AddDays(clsCommon.myCdbl(dt.Rows(0)("No_Days")))
    '        End If
    '    End If


    '    objCustInv.Discount_Percentage = IIf(obj.Discount_Base > 0, obj.Discount_Amt * 100 / obj.Discount_Base, 0)
    '    objCustInv.Discount_Base = obj.Discount_Base
    '    objCustInv.Discount_Amount = obj.Discount_Amt + obj.HeadDisc_Amt + obj.HeadDisc_PerAmt
    '    ''objCustInv.Amount_Less_Discount = 
    '    dt = clsDBFuncationality.GetDataTable("select Receivable_Control_acct,Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", trans)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
    '        If clsCommon.myCdbl(obj.Discount_Amt) > 0 Then
    '            objCustInv.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Receipts_Discount_acct"))
    '        End If
    '    End If

    '    If obj.TAX1_Amt > 0 AndAlso clsCommon.myLen(obj.TAX1) > 0 Then
    '        objCustInv.TAX1_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
    '        objCustInv.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX1_GLAC, obj.Bill_To_Location, trans)
    '    End If
    '    If obj.TAX2_Amt > 0 AndAlso clsCommon.myLen(obj.TAX2) > 0 Then
    '        objCustInv.TAX2_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
    '        objCustInv.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX2_GLAC, obj.Bill_To_Location, trans)
    '    End If
    '    If obj.TAX3_Amt > 0 AndAlso clsCommon.myLen(obj.TAX3) > 0 Then
    '        objCustInv.TAX3_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
    '        objCustInv.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX3_GLAC, obj.Bill_To_Location, trans)
    '    End If
    '    If obj.TAX4_Amt > 0 AndAlso clsCommon.myLen(obj.TAX4) > 0 Then
    '        objCustInv.TAX4_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
    '        objCustInv.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX4_GLAC, obj.Bill_To_Location, trans)
    '    End If
    '    If obj.TAX5_Amt > 0 AndAlso clsCommon.myLen(obj.TAX5) > 0 Then
    '        objCustInv.TAX5_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
    '        objCustInv.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX5_GLAC, obj.Bill_To_Location, trans)
    '    End If
    '    If obj.TAX6_Amt > 0 AndAlso clsCommon.myLen(obj.TAX6) > 0 Then
    '        objCustInv.TAX6_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
    '        objCustInv.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX6_GLAC, obj.Bill_To_Location, trans)
    '    End If
    '    If obj.TAX7_Amt > 0 AndAlso clsCommon.myLen(obj.TAX7) > 0 Then
    '        objCustInv.TAX7_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
    '        objCustInv.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX7_GLAC, obj.Bill_To_Location, trans)
    '    End If
    '    If obj.TAX8_Amt > 0 AndAlso clsCommon.myLen(obj.TAX8) > 0 Then
    '        objCustInv.TAX8_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
    '        objCustInv.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX8_GLAC, obj.Bill_To_Location, trans)
    '    End If
    '    If obj.TAX9_Amt > 0 AndAlso clsCommon.myLen(obj.TAX9) > 0 Then
    '        objCustInv.TAX9_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
    '        objCustInv.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX9_GLAC, obj.Bill_To_Location, trans)
    '    End If
    '    If obj.TAX10_Amt > 0 AndAlso clsCommon.myLen(obj.TAX10) > 0 Then
    '        objCustInv.TAX10_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
    '        objCustInv.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX10_GLAC, obj.Bill_To_Location, trans)
    '    End If

    '    'objCustInv.RefDocType=
    '    'objCustInv.RefDocNo
    '    objCustInv.Add_Charge_Code1 = obj.Add_Charge_Code1
    '    objCustInv.Add_Charge_Name1 = obj.Add_Charge_Name1
    '    objCustInv.Add_Charge_Amt1 = obj.Add_Charge_Amt1
    '    objCustInv.Add_Charge_Code2 = obj.Add_Charge_Code2
    '    objCustInv.Add_Charge_Name2 = obj.Add_Charge_Name2
    '    objCustInv.Add_Charge_Amt2 = obj.Add_Charge_Amt2
    '    objCustInv.Add_Charge_Code3 = obj.Add_Charge_Code3
    '    objCustInv.Add_Charge_Name3 = obj.Add_Charge_Name3
    '    objCustInv.Add_Charge_Amt3 = obj.Add_Charge_Amt3
    '    objCustInv.Add_Charge_Code4 = obj.Add_Charge_Code4
    '    objCustInv.Add_Charge_Name4 = obj.Add_Charge_Name4
    '    objCustInv.Add_Charge_Amt4 = obj.Add_Charge_Amt4
    '    objCustInv.Add_Charge_Code5 = obj.Add_Charge_Code5
    '    objCustInv.Add_Charge_Name5 = obj.Add_Charge_Name5
    '    objCustInv.Add_Charge_Amt5 = obj.Add_Charge_Amt5
    '    objCustInv.Add_Charge_Code6 = obj.Add_Charge_Code6
    '    objCustInv.Add_Charge_Name6 = obj.Add_Charge_Name6
    '    objCustInv.Add_Charge_Amt6 = obj.Add_Charge_Amt6
    '    objCustInv.Add_Charge_Code7 = obj.Add_Charge_Code7
    '    objCustInv.Add_Charge_Name7 = obj.Add_Charge_Name7
    '    objCustInv.Add_Charge_Amt7 = obj.Add_Charge_Amt7
    '    objCustInv.Add_Charge_Code8 = obj.Add_Charge_Code8
    '    objCustInv.Add_Charge_Name8 = obj.Add_Charge_Name8
    '    objCustInv.Add_Charge_Amt8 = obj.Add_Charge_Amt8
    '    objCustInv.Add_Charge_Code9 = obj.Add_Charge_Code9
    '    objCustInv.Add_Charge_Name9 = obj.Add_Charge_Name9
    '    objCustInv.Add_Charge_Amt9 = obj.Add_Charge_Amt9
    '    objCustInv.Add_Charge_Code10 = obj.Add_Charge_Code10
    '    objCustInv.Add_Charge_Name10 = obj.Add_Charge_Name10
    '    objCustInv.Add_Charge_Amt10 = obj.Add_Charge_Amt10
    '    objCustInv.Total_Add_Charge = obj.Total_Add_Charge
    '    objCustInv.Tax_Calculation_Type = obj.Tax_Calculation_Type
    '    ''objCustInv.Status
    '    ''objCustInv.AgainstScrap
    '    objCustInv.Against_Sale_No = obj.Document_Code
    '    Dim counter As Integer = 1
    '    objCustInv.Arr = New List(Of clsCustomerInvoiceDetailFarmer)
    '    For Each objTr As clsMCCMaterialSaleFarmerDetail In obj.Arr

    '        If clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal Then
    '            Dim objCustInvTR As clsCustomerInvoiceDetailFarmer = New clsCustomerInvoiceDetailFarmer()
    '            objCustInvTR.SNo = counter
    '            If clsCommon.CompairString(objTr.Row_Type, "Item") = CompairStringResult.Equal And clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal Then
    '                dt = clsItemMaster.GetSaleAccGLAC(objTr.Item_Code, trans)
    '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                    Throw New Exception("Please set sale account for item" + objTr.Item_Code)
    '                End If
    '                objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Sales_Account"))
    '                objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Bill_To_Location, trans)
    '                objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
    '            Else ''for row type misl.
    '                If clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal Then
    '                    Dim objAC As clsAdditionalCharge = clsAdditionalCharge.GetData(objTr.Item_Code, NavigatorType.Current, trans)
    '                    If objAC Is Nothing Then
    '                        Throw New Exception("Please set GL Ac from addition charge" + objTr.Item_Code)
    '                    End If
    '                    objCustInvTR.GL_Account_Code = objAC.Account_Code
    '                    objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, objTr.Location, trans)
    '                    objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
    '                End If
    '            End If

    '            objCustInvTR.Amount = objTr.Amount
    '            objCustInvTR.Discount_Per = objTr.Disc_Per
    '            objCustInvTR.Discount = objTr.Disc_Amt
    '            objCustInvTR.Amount_less_Discount = objTr.Amt_Less_Discount
    '            objCustInvTR.TAX1 = objTr.TAX1
    '            objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
    '            objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
    '            objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
    '            objCustInvTR.TAX2 = objTr.TAX2
    '            objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
    '            objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
    '            objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
    '            objCustInvTR.TAX3 = objTr.TAX3
    '            objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
    '            objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
    '            objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
    '            objCustInvTR.TAX4 = objTr.TAX4
    '            objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
    '            objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
    '            objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
    '            objCustInvTR.TAX5 = objTr.TAX5
    '            objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
    '            objCustInvTR.TAX5_Amt = objTr.TAX5_Amt
    '            objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
    '            objCustInvTR.TAX6 = objTr.TAX6
    '            objCustInvTR.TAX6_Rate = objTr.TAX6_Rate
    '            objCustInvTR.TAX6_Amt = objTr.TAX6_Amt
    '            objCustInvTR.TAX6_Base_Amt = objTr.TAX6_Base_Amt
    '            objCustInvTR.TAX7 = objTr.TAX7
    '            objCustInvTR.TAX7_Rate = objTr.TAX7_Rate
    '            objCustInvTR.TAX7_Amt = objTr.TAX7_Amt
    '            objCustInvTR.TAX7_Base_Amt = objTr.TAX7_Base_Amt
    '            objCustInvTR.TAX8 = objTr.TAX8
    '            objCustInvTR.TAX8_Rate = objTr.TAX8_Rate
    '            objCustInvTR.TAX8_Amt = objTr.TAX8_Amt
    '            objCustInvTR.TAX8_Base_Amt = objTr.TAX8_Base_Amt
    '            objCustInvTR.TAX9 = objTr.TAX9
    '            objCustInvTR.TAX9_Rate = objTr.TAX9_Rate
    '            objCustInvTR.TAX9_Amt = objTr.TAX9_Amt
    '            objCustInvTR.TAX9_Base_Amt = objTr.TAX9_Base_Amt
    '            objCustInvTR.TAX10 = objTr.TAX10
    '            objCustInvTR.TAX10_Rate = objTr.TAX10_Rate
    '            objCustInvTR.TAX10_Amt = objTr.TAX10_Amt
    '            objCustInvTR.TAX10_Base_Amt = objTr.TAX10_Base_Amt
    '            objCustInvTR.Total_Tax = objTr.Total_Tax_Amt
    '            objCustInvTR.Total_Amount = objTr.Item_Net_Amt
    '            objCustInvTR.Remarks = objTr.Remarks
    '            objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
    '            objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
    '            objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
    '            objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
    '            objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
    '            objCustInvTR.TAX6_Base_Amt = objTr.TAX6_Base_Amt
    '            objCustInvTR.TAX7_Base_Amt = objTr.TAX7_Base_Amt
    '            objCustInvTR.TAX8_Base_Amt = objTr.TAX8_Base_Amt
    '            objCustInvTR.TAX9_Base_Amt = objTr.TAX9_Base_Amt
    '            objCustInvTR.TAX10_Base_Amt = objTr.TAX10_Base_Amt
    '            'objCustInvTR.Comments=objTr.Comments
    '            objCustInv.Arr.Add(objCustInvTR)
    '            counter += 1
    '        End If
    '        If clsCommon.CompairString(objTr.Scheme_Item, "Y") = CompairStringResult.Equal Then
    '            objCustInv.TAX1_ExciseFOCAmt += objTr.TAX1_Amt
    '            objCustInv.TAX2_ExciseFOCAmt += objTr.TAX2_Amt
    '            objCustInv.TAX3_ExciseFOCAmt += objTr.TAX3_Amt
    '            objCustInv.TAX4_ExciseFOCAmt += objTr.TAX4_Amt
    '        End If
    '    Next
    '    objCustInv.SaveData(objCustInv, True, trans, strFormID, strVoucherNoRecreate, strARNoForRecreate)
    '    clsCustomerInvoiceHeadFarmer.PostData("", objCustInv.Document_No, "", trans)
    '    Return True
    'End Function

    Public Shared Function UpdateAfterPosting(ByVal obj As clsMCCMaterialSaleFarmer, ByVal DocumentCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(DocumentCode) > 0 Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "EWayBillNo", obj.EWayBillNo)
                clsCommon.AddColumnsForChange(coll, "Electronic_Ref_No", obj.Electronic_Ref_No)

                If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", Nothing, True)
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Sale_Farmer_Head", OMInsertOrUpdate.Update, "TSPL_MCC_Sale_Farmer_Head.Document_Code='" + DocumentCode + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsMCCMaterialSaleFarmerDetail
#Region "Variables"
    Public OrgUnit_code As String = ""
    Public Commission_Party As String = Nothing
    Public Commission_Rate As Double = 0
    Public Commission_Amt As Double = 0
    Public Amt_Less_Commission As Double = 0

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
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMCCMaterialSaleFarmerDetail), ByVal trans As SqlTransaction, ByVal DocDate As DateTime) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then

            For Each obj As clsMCCMaterialSaleFarmerDetail In Arr
                Dim coll As New Hashtable()
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
                'clsCommon.AddColumnsForChange(coll, "Unit_Cogs", clsItemLocationDetails.GetUnitCogs(obj.Item_Code, obj.Location, trans))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_Sale_Farmer_Detail", OMInsertOrUpdate.Insert, "", trans)
                clsSerializeInvenotry.SaveData("SD-IN", strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, obj.arrSrItem, trans)
                clsBatchInventory.SaveData("MCC-MSALE-F", strDocNo, DocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, obj.MRP, obj.Unit_code, obj.arrBatchItem, trans) ' Change by Prabhakar

            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceSRNQty(ByVal strSRNCode As String, ByVal strICode As String, ByVal strCurrPINNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_MCC_Sale_Farmer_Detail.Item_Code as ICode,TSPL_MCC_Sale_Farmer_Detail.Qty as Qty,1 as RI from TSPL_MCC_Sale_Farmer_Detail left outer join TSPL_MCC_Sale_Farmer_Head on TSPL_MCC_Sale_Farmer_Head.Document_Code=TSPL_MCC_Sale_Farmer_Detail.Document_Code where TSPL_MCC_Sale_Farmer_Detail.Status=0 and TSPL_MCC_Sale_Farmer_Head.Status=1 and TSPL_MCC_Sale_Farmer_Detail.Document_Code ='" + strSRNCode + "' and TSPL_MCC_Sale_Farmer_Detail.Item_Code='" + strICode + "' and  TSPL_MCC_Sale_Farmer_Detail.Unit_code='" + strUOM + "' and isnull(TSPL_MCC_Sale_Farmer_Detail.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_MCC_Sale_Farmer_Detail.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' " & _
            " union all " & _
            " select TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.PI_Qty as Qty,-1 as RI from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_DETAIL.SRN_Id='" + strSRNCode + "'   and TSPL_PI_DETAIL.Item_Code='" + strICode + "'  and  TSPL_PI_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_PI_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_PI_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'  and TSPL_PI_DETAIL.PI_No not in ('" + strCurrPINNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompleteSRN(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_MCC_Sale_Farmer_Detail set Status=1 where Document_Code='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
End Class

Public Class clsCustomerInvoiceHeadFarmer

#Region "Variables"
    Public TAX1_ExciseFOCAmt As Double = 0
    Public TAX2_ExciseFOCAmt As Double = 0
    Public TAX3_ExciseFOCAmt As Double = 0
    Public TAX4_ExciseFOCAmt As Double = 0
    Public Trans_Type As String = Nothing
    Public Return_Type As String = Nothing
    Public PROJECT_ID As String = Nothing
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public Farmer_Code As String = Nothing
    Public Farmer_Name As String = Nothing
    '-------- added by usha--
    Public loc_code As String = Nothing
    '-------end---
    Public Posting_Date As DateTime? = Nothing
    Public Account_Set As String = Nothing
    Public Document_Type As String = Nothing
    Public Order_No As String = Nothing
    Public Document_Total As Double = 0
    Public On_Hold As Boolean = Nothing
    Public Remarks As String = Nothing
    Public Description As String = Nothing
    Public Tax_Group As String = Nothing
    Public RefDocType As String = Nothing
    Public RefDocNo As String = Nothing
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public Tax1_BAmount As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public Tax2_BAmount As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public Tax3_BAmount As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public Tax4_BAmount As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public Tax5_BAmount As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public Tax6_BAmount As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public Tax7_BAmount As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public Tax8_BAmount As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public Tax9_BAmount As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public Tax10_BAmount As Double = 0
    Public TAX10_Amt As Double = 0
    Public Total_Tax As Double = 0
    Public Terms_Code As String = Nothing
    Public Terms_Description As String = Nothing
    Public Due_Date As DateTime
    Public Discount_Percentage As Double = 0
    Public Discount_Base As Double = 0
    Public Discount_Amount As Double = 0
    Public Amount_Less_Discount As Double = 0
    Public Comp_Code As String = Nothing
    Public Balance_Amt As Double = 0
    Public Customer_Control_AC As String = Nothing
    Public Discount_GL_AC As String = Nothing
    Public TAX1_GLAC As String = Nothing
    Public TAX2_GLAC As String = Nothing
    Public TAX3_GLAC As String = Nothing
    Public TAX4_GLAC As String = Nothing
    Public TAX5_GLAC As String = Nothing
    Public TAX6_GLAC As String = Nothing
    Public TAX7_GLAC As String = Nothing
    Public TAX8_GLAC As String = Nothing
    Public TAX9_GLAC As String = Nothing
    Public TAX10_GLAC As String = Nothing
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
    Public RoundOffAmount As Double = 0
    Public AgainstScrap As String = Nothing
    Public Against_Sale_No As String = Nothing
    Public Against_Sale_Return_No As String = Nothing
    Public Against_MCC_Material_Sale_Return As String = Nothing
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public SecurityDeposit As Boolean = False
    Public SecurityDepositType As Char = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Arr As List(Of clsCustomerInvoiceDetailFarmer) = Nothing

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public Against_VCGL As String = Nothing
    Public Against_Service_Visit_Code As String = Nothing
    Public Against_Asset_Disposal As String = Nothing
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsCustomerInvoiceHeadFarmer, ByVal isNewEntry As Boolean, ByVal FormId As String) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = obj.SaveData(obj, isNewEntry, trans, FormId)
            If (isSaved) Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As clsCustomerInvoiceHeadFarmer, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, ByVal FormId As String, Optional ByVal strVoucherNoForRecreateOnly As String = Nothing, Optional ByVal strARNoForRecreateOnly As String = Nothing) As Boolean
        Dim isSaved As Boolean = True
        If clsCommon.myLen(obj.loc_code) <= 0 Then
            Throw New Exception("Please first select Location")
        End If

        clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Receivables", "AR Invoice Entry", obj.loc_code, obj.Document_Date, trans)

        Dim qry As String = "delete from TSPL_Customer_Invoice_Detail_Farmer where Document_No='" + obj.Document_No + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_REMITTANCE where Document_No='" + obj.Document_No + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Dim strDocNo As String = ""
        If (isNewEntry) Then
            If obj.Arr.Count <= 0 Then
                Throw New Exception("Please fill at list one Account")
            End If
            Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code='" + obj.Arr(0).GL_Account_Code + "'", trans))
            If clsCommon.myLen(strLocation) <= 0 Then
                Throw New Exception("Please enter account wiht location segment")
            End If
            If clsCommon.myLen(strARNoForRecreateOnly) > 0 Then
                obj.Document_No = strARNoForRecreateOnly
            Else
                If (clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal) Then
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.ARInvoiceFarmer, "", strLocation, True)
                ElseIf (clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal) Then
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.ARDebitNoteFarmer, "", strLocation, True)
                ElseIf (clsCommon.CompairString(obj.Document_Type, "C") = CompairStringResult.Equal) Then
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.ARCreditNoteFarmer, "", strLocation, True)
                End If
            End If


        End If
        If (clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
        clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Farmer_Code", obj.Farmer_Code)
        clsCommon.AddColumnsForChange(coll, "Farmer_Name", obj.Farmer_Name)
        '--------added by usha----
        clsCommon.AddColumnsForChange(coll, "Loc_code", obj.loc_code)
        '--------------
        clsCommon.AddColumnsForChange(coll, "Account_Set", obj.Account_Set)
        clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
        clsCommon.AddColumnsForChange(coll, "RefDocType", obj.RefDocType)
        clsCommon.AddColumnsForChange(coll, "RefDocNo", obj.RefDocNo)
        clsCommon.AddColumnsForChange(coll, "Order_No", obj.Order_No)
        clsCommon.AddColumnsForChange(coll, "Document_Total", obj.Document_Total)
        clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
        clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
        clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
        clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
        clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)
        clsCommon.AddColumnsForChange(coll, "Return_Type", obj.Return_Type)
        clsCommon.AddColumnsForChange(coll, "SecurityDeposit", IIf(obj.SecurityDeposit = True, "Y", "N"))
        clsCommon.AddColumnsForChange(coll, "SecurityDepositType", obj.SecurityDepositType)
        clsCommon.AddColumnsForChange(coll, "TAX1_ExciseFOCAmt", obj.TAX1_ExciseFOCAmt)
        clsCommon.AddColumnsForChange(coll, "TAX2_ExciseFOCAmt", obj.TAX2_ExciseFOCAmt)
        clsCommon.AddColumnsForChange(coll, "TAX3_ExciseFOCAmt", obj.TAX3_ExciseFOCAmt)
        clsCommon.AddColumnsForChange(coll, "TAX4_ExciseFOCAmt", obj.TAX4_ExciseFOCAmt)
        clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
        clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax1_BAmount", obj.Tax1_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
        clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax2_BAmount", obj.Tax2_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
        clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax3_BAmount", obj.Tax3_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
        clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax4_BAmount", obj.Tax4_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
        clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax5_BAmount", obj.Tax5_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
        clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax6_BAmount", obj.Tax6_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
        clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax7_BAmount", obj.Tax7_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
        clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax8_BAmount", obj.Tax8_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
        clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax9_BAmount", obj.Tax9_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
        clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax10_BAmount", obj.Tax10_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
        clsCommon.AddColumnsForChange(coll, "Total_Tax", obj.Total_Tax)

        clsCommon.AddColumnsForChange(coll, "Customer_Control_AC", obj.Customer_Control_AC, True)
        clsCommon.AddColumnsForChange(coll, "Discount_GL_AC", obj.Discount_GL_AC, True)
        clsCommon.AddColumnsForChange(coll, "TAX1_GLAC", obj.TAX1_GLAC, True)
        clsCommon.AddColumnsForChange(coll, "TAX2_GLAC", obj.TAX2_GLAC, True)
        clsCommon.AddColumnsForChange(coll, "TAX3_GLAC", obj.TAX3_GLAC, True)
        clsCommon.AddColumnsForChange(coll, "TAX4_GLAC", obj.TAX4_GLAC, True)
        clsCommon.AddColumnsForChange(coll, "TAX5_GLAC", obj.TAX5_GLAC, True)
        clsCommon.AddColumnsForChange(coll, "TAX6_GLAC", obj.TAX6_GLAC, True)
        clsCommon.AddColumnsForChange(coll, "TAX7_GLAC", obj.TAX7_GLAC, True)
        clsCommon.AddColumnsForChange(coll, "TAX8_GLAC", obj.TAX8_GLAC, True)
        clsCommon.AddColumnsForChange(coll, "TAX9_GLAC", obj.TAX9_GLAC, True)
        clsCommon.AddColumnsForChange(coll, "TAX10_GLAC", obj.TAX10_GLAC, True)

        ' ''richa agarwal changes on 12 Aug,2016 against AR Aging report for invoice type
        'If (clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal) AndAlso clsCommon.myLen(obj.Terms_Code) <= 0 Then
        '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("Select TSPL_MP_MASTER.Terms_Code ,TSPL_TERMS_MASTER.Terms_Desc,TSPL_TERMS_MASTER.No_Days   from TSPL_MP_MASTER left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code =TSPL_MP_MASTER.Terms_Code where TSPL_MP_MASTER.Cust_Code ='" & clsCommon.myCstr(obj.Farmer_Code) & "'", trans)
        '    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
        '        obj.Terms_Code = clsCommon.myCstr(dt1.Rows(0)("Terms_Code"))
        '        obj.Terms_Description = clsCommon.myCstr(dt1.Rows(0)("Terms_Desc"))
        '        obj.Due_Date = clsCommon.myCDate(obj.Document_Date).AddDays(clsCommon.myCdbl(dt1.Rows(0)("No_Days")))
        '    Else
        '        Throw New Exception("Please enter Terms Code for Customer " & obj.Farmer_Code & " in Customer Master")
        '    End If
        'End If
        ''----------------------

        clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
        clsCommon.AddColumnsForChange(coll, "Terms_Description", obj.Terms_Description)
        clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Discount_Percentage", obj.Discount_Percentage)
        clsCommon.AddColumnsForChange(coll, "Discount_Base", clsCommon.myCdbl(obj.Discount_Base))
        clsCommon.AddColumnsForChange(coll, "Discount_Amount", obj.Discount_Amount)
        clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)
        clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.Balance_Amt)
        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
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
        clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)
        clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
        clsCommon.AddColumnsForChange(coll, "AgainstScrap", obj.AgainstScrap)
        clsCommon.AddColumnsForChange(coll, "Against_Sale_No", obj.Against_Sale_No)
        clsCommon.AddColumnsForChange(coll, "Against_Sale_Return_No", obj.Against_Sale_Return_No)
        '' Anubhooti 18-Mar-2015 (Save Against_VCGL)
        clsCommon.AddColumnsForChange(coll, "Against_VCGL", obj.Against_VCGL, True)
        ''
        '' Anubhooti 30-Oct-2015 (Save Against_Service_Visit_Code)
        clsCommon.AddColumnsForChange(coll, "Against_Service_Visit_Code", obj.Against_Service_Visit_Code, True)
        ''
        clsCommon.AddColumnsForChange(coll, "Against_MCC_Material_Sale_Return", obj.Against_MCC_Material_Sale_Return, True)
        clsCommon.AddColumnsForChange(coll, "Against_Asset_Disposal", obj.Against_Asset_Disposal, True)
        '' currencyconversion BM00000007917
        If clsCommon.myLen(obj.CURRENCY_CODE) = 0 Then
            obj.CURRENCY_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BaseCurrencyCode from TSPL_COMPANY_MASTER where Comp_Code='" & objCommonVar.CurrentCompanyCode & "'", trans))
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", 1)
        Else
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
        End If

        If clsCommon.myLen(obj.ApplicableFrom) > 0 Then
            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"), True)
        Else
            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", Nothing, True)
        End If

        '' End currencyconversion

        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Customer_Invoice_Head_Farmer", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Customer_Invoice_Head_Farmer", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
        End If
        isSaved = isSaved AndAlso clsCustomerInvoiceDetailFarmer.SaveData(obj.Document_No, Arr, trans)

        'isSaved = isSaved AndAlso CreateGLEntryForAllCases(obj, trans, True, FormId, strVoucherNoForRecreateOnly)

        isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_No, obj.arrCustomFields, trans)

        isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_No", obj.Document_No, "TSPL_Customer_Invoice_Head_Farmer", trans)

        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String) As clsCustomerInvoiceHeadFarmer
        Return GetData(strDocumentNo, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal trans As SqlTransaction) As clsCustomerInvoiceHeadFarmer
        Dim obj As clsCustomerInvoiceHeadFarmer = Nothing
        Dim qry As String = "Select * from TSPL_Customer_Invoice_Head_Farmer where Document_No='" + strDocumentNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCustomerInvoiceHeadFarmer()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            obj.Farmer_Code = clsCommon.myCstr(dt.Rows(0)("Farmer_Code"))
            obj.Farmer_Name = clsCommon.myCstr(dt.Rows(0)("Farmer_Name"))
            '----------added by usha
            obj.loc_code = clsCommon.myCstr(dt.Rows(0)("Loc_code"))
            '-----------
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If

            obj.Account_Set = clsCommon.myCstr(dt.Rows(0)("Account_Set"))
            obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("Document_Type"))
            obj.RefDocType = clsCommon.myCstr(dt.Rows(0)("RefDocType"))
            obj.RefDocNo = clsCommon.myCstr(dt.Rows(0)("RefDocNo"))
            obj.Order_No = clsCommon.myCstr(dt.Rows(0)("Order_No"))
            obj.Document_Total = clsCommon.myCdbl(dt.Rows(0)("Document_Total"))
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.SecurityDeposit = IIf(clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("SecurityDeposit")), "Y") = CompairStringResult.Equal, True, False)
            obj.SecurityDepositType = clsCommon.myCstr(dt.Rows(0)("SecurityDepositType"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.Tax1_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax1_BAmount"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.Tax2_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax2_BAmount"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.Tax3_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax3_BAmount"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.Tax4_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax4_BAmount"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.Tax5_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax5_BAmount"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
            obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
            obj.Tax6_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax6_BAmount"))
            obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
            obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
            obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
            obj.Tax7_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax7_BAmount"))
            obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
            obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
            obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
            obj.Tax8_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax8_BAmount"))
            obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
            obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
            obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
            obj.Tax9_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax9_BAmount"))
            obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
            obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
            obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
            obj.Tax10_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax10_BAmount"))
            obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
            obj.Total_Tax = clsCommon.myCdbl(dt.Rows(0)("Total_Tax"))
            obj.TAX1_ExciseFOCAmt = clsCommon.myCdbl(dt.Rows(0)("TAX1_ExciseFOCAmt"))
            obj.TAX2_ExciseFOCAmt = clsCommon.myCdbl(dt.Rows(0)("TAX2_ExciseFOCAmt"))
            obj.TAX3_ExciseFOCAmt = clsCommon.myCdbl(dt.Rows(0)("TAX3_ExciseFOCAmt"))
            obj.TAX4_ExciseFOCAmt = clsCommon.myCdbl(dt.Rows(0)("TAX4_ExciseFOCAmt"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Terms_Description = clsCommon.myCstr(dt.Rows(0)("Terms_Description"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            obj.Discount_Percentage = clsCommon.myCdbl(dt.Rows(0)("Discount_Percentage"))
            obj.Discount_Base = clsCommon.myCdbl(dt.Rows(0)("Discount_Base"))
            obj.Discount_Amount = clsCommon.myCdbl(dt.Rows(0)("Discount_Amount"))
            obj.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("Amount_Less_Discount"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Balance_Amt = clsCommon.myCdbl(dt.Rows(0)("Balance_Amt"))
            obj.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Customer_Control_AC"))
            obj.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_GL_AC"))
            obj.TAX1_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX1_GLAC"))
            obj.TAX2_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX2_GLAC"))
            obj.TAX3_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX3_GLAC"))
            obj.TAX4_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX4_GLAC"))
            obj.TAX5_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX5_GLAC"))
            obj.TAX6_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX6_GLAC"))
            obj.TAX7_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX7_GLAC"))
            obj.TAX8_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX8_GLAC"))
            obj.TAX9_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX9_GLAC"))
            obj.TAX10_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX10_GLAC"))
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
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Total_Add_Charge = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge"))
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.AgainstScrap = clsCommon.myCstr(dt.Rows(0)("AgainstScrap"))
            obj.Against_Sale_No = clsCommon.myCstr(dt.Rows(0)("Against_Sale_No"))
            obj.Against_Sale_Return_No = clsCommon.myCstr(dt.Rows(0)("Against_Sale_Return_No"))
            '' Anubhooti 18-Mar-2015 (Fetch Against_VCGL)
            obj.Against_VCGL = clsCommon.myCstr(dt.Rows(0)("Against_VCGL"))
            obj.Against_Asset_Disposal = clsCommon.myCstr(dt.Rows(0)("Against_Asset_Disposal"))
            ''
            '' Anubhooti 30-Oct-2015 (Fetch Against_Service_Visit_Code)
            obj.Against_Service_Visit_Code = clsCommon.myCstr(dt.Rows(0)("Against_Service_Visit_Code"))
            obj.Trans_Type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))
            ''
            obj.Against_MCC_Material_Sale_Return = clsCommon.myCstr(dt.Rows(0)("Against_MCC_Material_Sale_Return"))
            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))
            obj.Return_Type = clsCommon.myCstr(dt.Rows(0)("Return_Type"))

            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            '' END CURRENCYCONVERSION 

            qry = "Select * from TSPL_Customer_Invoice_Detail_Farmer where Document_No='" + strDocumentNo + "' ORDER BY SNo"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsCustomerInvoiceDetailFarmer)
                Dim objTr As clsCustomerInvoiceDetailFarmer
                For Each dr As DataRow In dt.Rows
                    objTr = New clsCustomerInvoiceDetailFarmer
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.SNo = clsCommon.myCstr(dr("SNo"))
                    objTr.GL_Account_Code = clsCommon.myCstr(dr("GL_Account_Code"))
                    objTr.GL_Account_Desc = clsCommon.myCstr(dr("GL_Account_Desc"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Discount_Per = clsCommon.myCdbl(dr("Discount_Per"))
                    objTr.Discount = clsCommon.myCdbl(dr("Discount"))
                    objTr.Amount_less_Discount = clsCommon.myCdbl(dr("Amount_less_Discount"))
                    objTr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objTr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objTr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objTr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objTr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objTr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objTr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objTr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objTr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objTr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objTr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objTr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objTr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objTr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objTr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objTr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objTr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objTr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objTr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objTr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objTr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objTr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objTr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objTr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    objTr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objTr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    objTr.Total_Tax = clsCommon.myCdbl(dr("Total_Tax"))
                    objTr.Total_Amount = clsCommon.myCdbl(dr("Total_Amount"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.Comments = clsCommon.myCstr(dr("Comments"))

                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))

                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal strRefDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = clsCustomerInvoiceHeadFarmer.PostData(FormId, strDocNo, strRefDocNo, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal strRefDocNo As String, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreatedOnly As String = Nothing) As Boolean
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Post")
        End If

        Dim obj As clsCustomerInvoiceHeadFarmer = clsCustomerInvoiceHeadFarmer.GetData(strDocNo, trans)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If

        clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Receivables", "AR Invoice Entry", obj.loc_code, obj.Document_Date, trans)

        If (obj.Status = ERPTransactionStatus.Approved) Then
            Throw New Exception("Already Post on :" + obj.Posting_Date)
        End If
        If (obj.On_Hold) Then
            Throw New Exception("Document No " + obj.Document_No + " Is currently On Hold.Can't Post it")
        End If
        If clsCommon.myLen(obj.Customer_Control_AC) <= 0 Then
            Throw New Exception("Customer's Control A/C Not found")
        End If

        Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_Customer_Invoice_Head_Farmer", "Document_No", strDocNo, trans)
        If isResult = False Then
            trans.Commit()
            Return False
        End If
        'CreateGLEntty(obj, trans, False, FormId)
        'CreateGLEntryForAllCases(obj, trans, False, FormId, strVoucherNoForRecreatedOnly)


        Dim qry As String = "update TSPL_REMITTANCE set Remit_TDS='N' where Document_No='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "Update TSPL_Customer_Invoice_Head_Farmer set Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',Modify_By='" + objCommonVar.CurrentUserCode + "' , Status=1 where Document_No='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function

    Private Shared Function GetTaxAmt(ByVal objPIDetail As clsCustomerInvoiceDetailFarmer, ByVal tans As SqlTransaction) As Double
        Dim dblTotalTax As Double = 0
        Dim isTaxRecoverable As Boolean = False
        If objPIDetail.TAX1_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX1, tans) Then
            dblTotalTax += objPIDetail.TAX1_Amt
        End If
        If objPIDetail.TAX2_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX2, tans) Then
            dblTotalTax += objPIDetail.TAX2_Amt
        End If
        If objPIDetail.TAX3_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX3, tans) Then
            dblTotalTax += objPIDetail.TAX3_Amt
        End If
        If objPIDetail.TAX4_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX4, tans) Then
            dblTotalTax += objPIDetail.TAX4_Amt
        End If
        If objPIDetail.TAX5_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX5, tans) Then
            dblTotalTax += objPIDetail.TAX5_Amt
        End If
        If objPIDetail.TAX6_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX6, tans) Then
            dblTotalTax += objPIDetail.TAX6_Amt
        End If
        If objPIDetail.TAX7_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX7, tans) Then
            dblTotalTax += objPIDetail.TAX7_Amt
        End If
        If objPIDetail.TAX8_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX8, tans) Then
            dblTotalTax += objPIDetail.TAX8_Amt
        End If
        If objPIDetail.TAX9_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX9, tans) Then
            dblTotalTax += objPIDetail.TAX9_Amt
        End If
        If objPIDetail.TAX10_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX10, tans) Then
            dblTotalTax += objPIDetail.TAX10_Amt
        End If
        Return dblTotalTax
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsCustomerInvoiceHeadFarmer = clsCustomerInvoiceHeadFarmer.GetData(strDocNo)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Receivables", "AR Invoice Entry", obj.loc_code, obj.Document_Date, trans)

                If obj.Status = ERPTransactionStatus.Approved Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                'If (clsCommon.myLen(obj.Posting_Date) > 0) Then
                '    Throw New Exception("Already Post on :" + obj.Posting_Date)
                'End If
                Dim qry As String = "delete from TSPL_Customer_Invoice_Detail_Farmer where Document_No='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                ''changes done by richa agarwal in journal master query from 'AR-CN' to 'AR-CR'
                '' changed by Panch Raj against Ticket No: BM00000008161
                '' delete journal details
                qry = "delete from TSPL_JOURNAL_DETAILS where Journal_No in (select Journal_No from TSPL_JOURNAL_MASTER where Source_Code in ('AR-IN','AR-CR','AR-DN') and Source_Doc_No='" + strDocNo + "')"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER   where Source_Code in ('AR-IN','AR-CR','AR-DN') and Source_Doc_No='" + strDocNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_REMITTANCE where Document_No='" + strDocNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_Customer_Invoice_Head_Farmer where Document_No='" + strDocNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strDocNo, trans)
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

    Public Shared Function GetSecurityDepositType() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Value", GetType(String))

        dt.Rows.Add("Select", "")
        dt.Rows.Add("Security", "S")
        dt.Rows.Add("Crate Security", "C")
        dt.Rows.Add("Refrigerator Security", "R")
        dt.Rows.Add("Others", "O")
        Return dt
    End Function
    ''

    Public Shared Function CreateGLEntryForAllCases(ByVal obj As clsCustomerInvoiceHeadFarmer, ByVal trans As SqlTransaction, ByVal isForUnpostedTransaction As Boolean, ByVal FormId As String, Optional ByVal strVoucherNoForRecreatedOnly As String = Nothing) As Boolean
        Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)
        'Dim IsAllowPurchaseAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0, False, True)

        Dim qry As String = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + obj.Document_No + "' and Source_Code in ('AR-IN','AR-DN','AR-CR')"
        Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

        If strVoucherNoForRecreatedOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreatedOnly) > 0 Then
            strVoucherNo = strVoucherNoForRecreatedOnly
        End If

        ''===========these column is for GL entry=========do for multicurrency==if currency is differ form basecurrency=======15/04/2015==(Monika)================================
        Dim coll As New Hashtable()
        If clsCommon.myLen(obj.CURRENCY_CODE) > 0 AndAlso clsCommon.CompairString(obj.CURRENCY_CODE, objCommonVar.BaseCurrencyCode) <> CompairStringResult.Equal Then
            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            clsCommon.AddColumnsForChange(coll, "ConvRateOld", obj.ConvRate)
        End If
        ''===============================================================================================


        Dim isTaxRecoverable As Boolean = False
        Dim NonRecovTaxAmount As Decimal = 0
        Dim Tax_Liability_Account As String = ""
        Dim strEntryDesc As String = ""
        Dim strSrcType As String = ""
        Dim strSrcDesc As String = ""
        Dim strRemarks As String = ""
        If (clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal) AndAlso (clsCommon.CompairString(FormId, "CSA-SALE") <> CompairStringResult.Equal) Then
            strEntryDesc = "AR Invoice Entry Against-"
            strSrcType = "AR-IN"
            strSrcDesc = "AR Invoice"
        ElseIf (clsCommon.CompairString(FormId, "CSA-SALE") = CompairStringResult.Equal) Then
            strEntryDesc = "AR Invoice Against CSA Sale Patti No." + obj.Against_Sale_No
            strSrcType = "AR-IN"
            strSrcDesc = "AR Invoice"
        ElseIf (clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal) Then
            strEntryDesc = "AR Debit Note Entry Against-"
            strSrcType = "AR-DN"
            strSrcDesc = "AR Debit"
            strRemarks = " AR invoice for customer: " + obj.Farmer_Code + " - " + obj.Farmer_Name + "  "
        ElseIf clsCommon.myLen(clsCommon.myCstr(obj.Against_MCC_Material_Sale_Return)) > 0 Then
            strEntryDesc = "AR Credit Note Entry Against-"
            strSrcType = "AR-CR"
            strSrcDesc = "AR Credit Note"
            strRemarks = " AR invoice for customer: " + obj.Farmer_Code + " - " + obj.Farmer_Name + "  For Mcc Material Sale Return No -: " & obj.Against_MCC_Material_Sale_Return & " "

        ElseIf (clsCommon.CompairString(obj.Document_Type, "C") = CompairStringResult.Equal) Then
            strEntryDesc = "AR Credit Note Entry Against-"
            strSrcType = "AR-CR"
            strSrcDesc = "AR Credit Note"
            strRemarks = " AR invoice for customer: " + obj.Farmer_Code + " - " + obj.Farmer_Name + "  For Sale Return No " & obj.Against_Sale_Return_No & " "
        End If
        Dim ArryLst As ArrayList = New ArrayList()
        '' *********************************************** Conditionally GL Entry(Fresh,Bulk,CSA) ******************************************* 


        '''''''' For Invoice GL entry 
        If clsCommon.myLen(obj.Against_Sale_No) > 0 Then
            If ((clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal)) AndAlso (FormId = "" OrElse FormId = "FreshSaleInvoice" OrElse FormId = "CSA-SALE") Then
                Dim objInv As clsSNInvoiceHead
                Dim isTaxExcisable As Boolean = False
                Dim arr As New List(Of String)
                Dim dblCogsCost As Double
                Dim strCogsAcct As String
                objInv = clsSNInvoiceHead.GetData(obj.Against_Sale_No, NavigatorType.Current, "", trans)
                ''''' GL entry for Tax and retail Invoice
                '' Updated by richa agarwal add condition in below line invoice type=S----------------- added A for TAx Exempted type,I= inter state
                If clsCommon.CompairString(objInv.Invoice_Type, "T") = CompairStringResult.Equal OrElse clsCommon.CompairString(objInv.Invoice_Type, "E") = CompairStringResult.Equal OrElse clsCommon.CompairString(objInv.Invoice_Type, "R") = CompairStringResult.Equal OrElse clsCommon.CompairString(objInv.Invoice_Type, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objInv.Invoice_Type, "N") = CompairStringResult.Equal OrElse clsCommon.CompairString(objInv.Invoice_Type, "A") = CompairStringResult.Equal OrElse clsCommon.CompairString(objInv.Invoice_Type, "I") = CompairStringResult.Equal Then

                    ''  for tax gl entry start here
                    Dim objTM As clsTaxMaster
                    Dim dblExcise As Double = 0
                    If obj.TAX1_Amt <> 0 Then
                        isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans)

                        ' for excisable tax start here
                        isTaxExcisable = clsTaxMaster.IsTaxExcisable(obj.TAX1, trans)
                        If isTaxExcisable AndAlso clsCommon.CompairString(obj.Trans_Type, "CSA") <> CompairStringResult.Equal Then
                            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX1, trans)
                            If objTM IsNot Nothing Then
                                If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                                    Throw New Exception("Please set Tax Net Payable Account of Tax Authority " + obj.TAX1)
                                End If
                                objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.loc_code, True, trans)
                                Dim Acc1() As String = {objTM.Tax_Net_Payable, obj.TAX1_Amt}
                                ArryLst.Add(Acc1)
                                dblExcise += obj.TAX1_Amt

                                '' For excisable FOC entry start here on 26/10/2016 for product sale
                                If obj.TAX1_ExciseFOCAmt > 0 Then
                                    Acc1 = {objTM.Tax_Net_Payable, obj.TAX1_ExciseFOCAmt}
                                    ArryLst.Add(Acc1)
                                End If
                                '' For excisable FOC entry ends here
                            End If
                        End If

                        'Excisable tax ends here

                        If clsCommon.myLen(obj.TAX1_GLAC) <= 0 Then
                            Throw New Exception("GL Acount not found for" + obj.TAX1)
                        End If

                        Dim AccInvDR() As String = {obj.TAX1_GLAC, -1 * obj.TAX1_Amt}
                        ArryLst.Add(AccInvDR)
                        If obj.TAX1_ExciseFOCAmt > 0 Then
                            AccInvDR = {obj.TAX1_GLAC, -1 * obj.TAX1_ExciseFOCAmt}
                            ArryLst.Add(AccInvDR)
                        End If
                        If clsCommon.CompairString(obj.Trans_Type, "CSA") = CompairStringResult.Equal Then
                            dblExcise += obj.TAX1_Amt
                        End If
                    End If

                    If obj.TAX2_Amt <> 0 Then
                        isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans)

                        ' for excisable tax start here
                        isTaxExcisable = clsTaxMaster.IsTaxExcisable(obj.TAX2, trans)
                        If isTaxExcisable AndAlso clsCommon.CompairString(obj.Trans_Type, "CSA") <> CompairStringResult.Equal Then
                            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX2, trans)
                            If objTM IsNot Nothing Then
                                If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                                    Throw New Exception("Please set  Tax Net Payable Account of Tax Authority " + obj.TAX2)
                                End If
                                objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.loc_code, True, trans)
                                Dim Acc1() As String = {objTM.Tax_Net_Payable, obj.TAX2_Amt}
                                ArryLst.Add(Acc1)
                                dblExcise += obj.TAX2_Amt

                                '' For excisable FOC entry start here on 26/10/2016 for product sale
                                If obj.TAX2_ExciseFOCAmt > 0 Then
                                    Acc1 = {objTM.Tax_Net_Payable, obj.TAX2_ExciseFOCAmt}
                                    ArryLst.Add(Acc1)
                                    dblExcise += obj.TAX2_ExciseFOCAmt
                                End If
                                '' For excisable FOC entry ends here
                            End If
                        End If
                        'Excisable tax ends here

                        If clsCommon.myLen(obj.TAX2_GLAC) <= 0 Then
                            Throw New Exception("GL Acount not found for" + obj.TAX2)
                        End If

                        Dim AccInvDR() As String = {obj.TAX2_GLAC, -1 * obj.TAX2_Amt}
                        ArryLst.Add(AccInvDR)
                        If clsCommon.CompairString(obj.Trans_Type, "CSA") = CompairStringResult.Equal Then
                            dblExcise += obj.TAX2_Amt
                        End If
                    End If

                    If obj.TAX3_Amt <> 0 Then
                        isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans)

                        ' for excisable tax start here
                        isTaxExcisable = clsTaxMaster.IsTaxExcisable(obj.TAX3, trans)
                        If isTaxExcisable AndAlso clsCommon.CompairString(obj.Trans_Type, "CSA") <> CompairStringResult.Equal Then
                            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX3, trans)
                            If objTM IsNot Nothing Then
                                If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                                    Throw New Exception("Please set  Tax Net Payable Account of Tax Authority " + obj.TAX3)
                                End If
                                objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.loc_code, True, trans)
                                Dim Acc1() As String = {objTM.Tax_Net_Payable, obj.TAX3_Amt}
                                ArryLst.Add(Acc1)
                                dblExcise += obj.TAX3_Amt
                                '' For excisable FOC entry start here on 26/10/2016 for product sale
                                If obj.TAX3_ExciseFOCAmt > 0 Then
                                    Acc1 = {objTM.Tax_Net_Payable, obj.TAX3_ExciseFOCAmt}
                                    ArryLst.Add(Acc1)
                                    dblExcise += obj.TAX3_ExciseFOCAmt
                                End If
                                '' For excisable FOC entry ends here
                            End If
                        End If
                        'Excisable tax ends here

                        If clsCommon.myLen(obj.TAX3_GLAC) <= 0 Then
                            Throw New Exception("GL Acount not found for" + obj.TAX3)
                        End If

                        If clsCommon.CompairString(obj.Trans_Type, "CSA") = CompairStringResult.Equal Then
                            dblExcise += obj.TAX3_Amt
                        End If
                        Dim AccInvDR() As String = {obj.TAX3_GLAC, -1 * obj.TAX3_Amt}
                        ArryLst.Add(AccInvDR)
                    End If

                    If obj.TAX4_Amt <> 0 Then
                        isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans)

                        ' for excisable tax start here
                        isTaxExcisable = clsTaxMaster.IsTaxExcisable(obj.TAX4, trans)
                        If isTaxExcisable AndAlso clsCommon.CompairString(obj.Trans_Type, "CSA") <> CompairStringResult.Equal Then
                            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX4, trans)
                            If objTM IsNot Nothing Then
                                If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                                    Throw New Exception("Please set  Tax Net Payable Account of Tax Authority " + obj.TAX4)
                                End If
                                objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.loc_code, True, trans)
                                Dim Acc1() As String = {objTM.Tax_Net_Payable, obj.TAX4_Amt}
                                ArryLst.Add(Acc1)
                                dblExcise += obj.TAX4_Amt
                                '' For excisable FOC entry start here on 26/10/2016 for product sale
                                If obj.TAX4_ExciseFOCAmt > 0 Then
                                    Acc1 = {objTM.Tax_Net_Payable, obj.TAX4_ExciseFOCAmt}
                                    ArryLst.Add(Acc1)
                                    dblExcise += obj.TAX4_ExciseFOCAmt
                                End If
                                '' For excisable FOC entry ends here
                            End If
                        End If
                        'Excisable tax ends here

                        If clsCommon.myLen(obj.TAX4_GLAC) <= 0 Then
                            Throw New Exception("GL Acount not found for" + obj.TAX4)
                        End If

                        If clsCommon.CompairString(obj.Trans_Type, "CSA") = CompairStringResult.Equal Then
                            dblExcise += obj.TAX4_Amt
                        End If
                        Dim AccInvDR() As String = {obj.TAX4_GLAC, -1 * obj.TAX4_Amt}
                        ArryLst.Add(AccInvDR)
                    End If
                    If obj.TAX5_Amt <> 0 Then
                        isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans)

                        ' for excisable tax start here
                        isTaxExcisable = clsTaxMaster.IsTaxExcisable(obj.TAX5, trans)
                        If isTaxExcisable AndAlso clsCommon.CompairString(obj.Trans_Type, "CSA") <> CompairStringResult.Equal Then
                            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX5, trans)
                            If objTM IsNot Nothing Then
                                If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                                    Throw New Exception("Please set  Tax Net Payable Account of Tax Authority " + obj.TAX5)
                                End If
                                objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.loc_code, True, trans)
                                Dim Acc1() As String = {objTM.Tax_Net_Payable, obj.TAX5_Amt}
                                ArryLst.Add(Acc1)
                                dblExcise += obj.TAX5_Amt
                            End If
                        End If
                        'Excisable tax ends here

                        If clsCommon.myLen(obj.TAX5_GLAC) <= 0 Then
                            Throw New Exception("GL Acount not found for" + obj.TAX5)
                        End If

                        If clsCommon.CompairString(obj.Trans_Type, "CSA") = CompairStringResult.Equal Then
                            dblExcise += obj.TAX5_Amt
                        End If
                        Dim AccInvDR() As String = {obj.TAX5_GLAC, -1 * obj.TAX5_Amt}
                        ArryLst.Add(AccInvDR)
                    End If

                    If obj.TAX6_Amt <> 0 Then
                        isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans)

                        ' for excisable tax start here
                        isTaxExcisable = clsTaxMaster.IsTaxExcisable(obj.TAX6, trans)
                        If isTaxExcisable AndAlso clsCommon.CompairString(obj.Trans_Type, "CSA") <> CompairStringResult.Equal Then
                            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX6, trans)
                            If objTM IsNot Nothing Then
                                If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                                    Throw New Exception("Please set  Tax Net Payable Account of Tax Authority " + obj.TAX6)
                                End If
                                objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.loc_code, True, trans)
                                Dim Acc1() As String = {objTM.Tax_Net_Payable, obj.TAX6_Amt}
                                ArryLst.Add(Acc1)
                                dblExcise += obj.TAX6_Amt
                            End If
                        End If
                        'Excisable tax ends here

                        If clsCommon.myLen(obj.TAX6_GLAC) <= 0 Then
                            Throw New Exception("GL Acount not found for" + obj.TAX6)
                        End If

                        If clsCommon.CompairString(obj.Trans_Type, "CSA") = CompairStringResult.Equal Then
                            dblExcise += obj.TAX6_Amt
                        End If
                        Dim AccInvDR() As String = {obj.TAX6_GLAC, -1 * obj.TAX6_Amt}
                        ArryLst.Add(AccInvDR)
                    End If

                    If obj.TAX7_Amt <> 0 Then
                        isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans)

                        ' for excisable tax start here
                        isTaxExcisable = clsTaxMaster.IsTaxExcisable(obj.TAX7, trans)
                        If isTaxExcisable AndAlso clsCommon.CompairString(obj.Trans_Type, "CSA") <> CompairStringResult.Equal Then
                            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX7, trans)
                            If objTM IsNot Nothing Then
                                If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                                    Throw New Exception("Please set  Tax Net Payable Account of Tax Authority " + obj.TAX7)
                                End If
                                objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.loc_code, True, trans)
                                Dim Acc1() As String = {objTM.Tax_Net_Payable, obj.TAX7_Amt}
                                ArryLst.Add(Acc1)
                                dblExcise += obj.TAX7_Amt
                            End If
                        End If
                        'Excisable tax ends here

                        If clsCommon.myLen(obj.TAX7_GLAC) <= 0 Then
                            Throw New Exception("GL Acount not found for" + obj.TAX7)
                        End If

                        If clsCommon.CompairString(obj.Trans_Type, "CSA") = CompairStringResult.Equal Then
                            dblExcise += obj.TAX7_Amt
                        End If
                        Dim AccInvDR() As String = {obj.TAX7_GLAC, -1 * obj.TAX7_Amt}
                        ArryLst.Add(AccInvDR)

                    End If

                    If obj.TAX8_Amt <> 0 Then
                        isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans)

                        ' for excisable tax start here
                        isTaxExcisable = clsTaxMaster.IsTaxExcisable(obj.TAX8, trans)
                        If isTaxExcisable AndAlso clsCommon.CompairString(obj.Trans_Type, "CSA") <> CompairStringResult.Equal Then
                            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX8, trans)
                            If objTM IsNot Nothing Then
                                If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                                    Throw New Exception("Please set  Tax Net Payable Account of Tax Authority " + obj.TAX8)
                                End If
                                objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.loc_code, True, trans)
                                Dim Acc1() As String = {objTM.Tax_Net_Payable, obj.TAX8_Amt}
                                ArryLst.Add(Acc1)
                                dblExcise += obj.TAX8_Amt
                            End If
                        End If
                        'Excisable tax ends here

                        If clsCommon.myLen(obj.TAX8_GLAC) <= 0 Then
                            Throw New Exception("GL Acount not found for" + obj.TAX8)
                        End If

                        If clsCommon.CompairString(obj.Trans_Type, "CSA") = CompairStringResult.Equal Then
                            dblExcise += obj.TAX8_Amt
                        End If
                        Dim AccInvDR() As String = {obj.TAX8_GLAC, -1 * obj.TAX8_Amt}
                        ArryLst.Add(AccInvDR)
                    End If

                    If obj.TAX9_Amt <> 0 Then
                        isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans)

                        ' for excisable tax start here
                        isTaxExcisable = clsTaxMaster.IsTaxExcisable(obj.TAX9, trans)
                        If isTaxExcisable AndAlso clsCommon.CompairString(obj.Trans_Type, "CSA") <> CompairStringResult.Equal Then
                            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX9, trans)
                            If objTM IsNot Nothing Then
                                If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                                    Throw New Exception("Please set  Tax Net Payable Account of Tax Authority " + obj.TAX9)
                                End If
                                objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.loc_code, True, trans)
                                Dim Acc1() As String = {objTM.Tax_Net_Payable, obj.TAX9_Amt}
                                ArryLst.Add(Acc1)
                                dblExcise += obj.TAX9_Amt
                            End If
                        End If
                        'Excisable tax ends here

                        If clsCommon.myLen(obj.TAX9_GLAC) <= 0 Then
                            Throw New Exception("GL Acount not found for" + obj.TAX9)
                        End If

                        If clsCommon.CompairString(obj.Trans_Type, "CSA") = CompairStringResult.Equal Then
                            dblExcise += obj.TAX9_Amt
                        End If
                        Dim AccInvDR() As String = {obj.TAX9_GLAC, -1 * obj.TAX9_Amt}
                        ArryLst.Add(AccInvDR)
                    End If

                    If obj.TAX10_Amt <> 0 Then
                        isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans)

                        ' for excisable tax start here
                        isTaxExcisable = clsTaxMaster.IsTaxExcisable(obj.TAX10, trans)
                        If isTaxExcisable AndAlso clsCommon.CompairString(obj.Trans_Type, "CSA") <> CompairStringResult.Equal Then
                            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX10, trans)
                            If objTM IsNot Nothing Then
                                If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                                    Throw New Exception("Please set  Tax Net Payable Account of Tax Authority " + obj.TAX10)
                                End If
                                objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.loc_code, True, trans)
                                Dim Acc1() As String = {objTM.Tax_Net_Payable, obj.TAX10_Amt}
                                ArryLst.Add(Acc1)
                                dblExcise += obj.TAX10_Amt
                            End If
                        End If
                        'Excisable tax ends here

                        If clsCommon.myLen(obj.TAX10_GLAC) <= 0 Then
                            Throw New Exception("GL Acount not found for" + obj.TAX10)
                        End If

                        If clsCommon.CompairString(obj.Trans_Type, "CSA") = CompairStringResult.Equal Then
                            dblExcise += obj.TAX10_Amt
                        End If
                        Dim AccInvDR() As String = {obj.TAX10_GLAC, -1 * obj.TAX10_Amt}
                        ArryLst.Add(AccInvDR)

                    End If





                    ''  tax gl entry ends here
                    '' FOR Additional Cost START here
                    If obj.Add_Charge_Amt1 <> 0 Then
                        Dim AddCharge_GL_Acc1 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code1, trans)
                        AddCharge_GL_Acc1 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc1, obj.loc_code, True, trans)

                        If clsCommon.myLen(AddCharge_GL_Acc1) <= 0 Then
                            Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code1 & " Not found")
                        End If

                        Dim AccAddCostCR() As String = {AddCharge_GL_Acc1, -1 * obj.Add_Charge_Amt1}
                        ArryLst.Add(AccAddCostCR)
                    End If
                    If obj.Add_Charge_Amt2 <> 0 Then
                        Dim AddCharge_GL_Acc2 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code2, trans)
                        AddCharge_GL_Acc2 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc2, obj.loc_code, True, trans)

                        If clsCommon.myLen(AddCharge_GL_Acc2) <= 0 Then
                            Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code2 & " Not found")
                        End If

                        Dim AccAddCostCR() As String = {AddCharge_GL_Acc2, -1 * obj.Add_Charge_Amt2}
                        ArryLst.Add(AccAddCostCR)
                    End If
                    If obj.Add_Charge_Amt3 <> 0 Then
                        Dim AddCharge_GL_Acc3 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code3, trans)
                        AddCharge_GL_Acc3 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc3, obj.loc_code, True, trans)

                        If clsCommon.myLen(AddCharge_GL_Acc3) <= 0 Then
                            Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code3 & " Not found")
                        End If

                        Dim AccAddCostCR() As String = {AddCharge_GL_Acc3, -1 * obj.Add_Charge_Amt3}
                        ArryLst.Add(AccAddCostCR)
                    End If
                    If obj.Add_Charge_Amt4 <> 0 Then
                        Dim AddCharge_GL_Acc4 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code4, trans)
                        AddCharge_GL_Acc4 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc4, obj.loc_code, True, trans)

                        If clsCommon.myLen(AddCharge_GL_Acc4) <= 0 Then
                            Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code4 & " Not found")
                        End If

                        Dim AccAddCostCR() As String = {AddCharge_GL_Acc4, -1 * obj.Add_Charge_Amt4}
                        ArryLst.Add(AccAddCostCR)
                    End If
                    If obj.Add_Charge_Amt5 <> 0 Then
                        Dim AddCharge_GL_Acc5 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code5, trans)
                        If clsCommon.myLen(AddCharge_GL_Acc5) <= 0 Then
                            Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code5 & " Not found")
                        End If
                        AddCharge_GL_Acc5 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc5, obj.loc_code, True, trans)

                        Dim AccAddCostCR() As String = {AddCharge_GL_Acc5, -1 * obj.Add_Charge_Amt5}
                        ArryLst.Add(AccAddCostCR)
                    End If
                    If obj.Add_Charge_Amt6 <> 0 Then
                        Dim AddCharge_GL_Acc6 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code6, trans)
                        AddCharge_GL_Acc6 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc6, obj.loc_code, True, trans)

                        If clsCommon.myLen(AddCharge_GL_Acc6) <= 0 Then
                            Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code6 & " Not found")
                        End If
                        Dim AccAddCostCR() As String = {AddCharge_GL_Acc6, -1 * obj.Add_Charge_Amt6}
                        ArryLst.Add(AccAddCostCR)
                    End If
                    If obj.Add_Charge_Amt7 <> 0 Then
                        Dim AddCharge_GL_Acc7 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code7, trans)
                        AddCharge_GL_Acc7 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc7, obj.loc_code, True, trans)

                        If clsCommon.myLen(AddCharge_GL_Acc7) <= 0 Then
                            Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code7 & " Not found")
                        End If
                        Dim AccAddCostCR() As String = {AddCharge_GL_Acc7, -1 * obj.Add_Charge_Amt7}
                        ArryLst.Add(AccAddCostCR)
                    End If
                    If obj.Add_Charge_Amt8 <> 0 Then
                        Dim AddCharge_GL_Acc8 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code8, trans)
                        AddCharge_GL_Acc8 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc8, obj.loc_code, True, trans)

                        If clsCommon.myLen(AddCharge_GL_Acc8) <= 0 Then
                            Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code8 & " Not found")
                        End If
                        Dim AccAddCostCR() As String = {AddCharge_GL_Acc8, -1 * obj.Add_Charge_Amt8}
                        ArryLst.Add(AccAddCostCR)
                    End If
                    If obj.Add_Charge_Amt9 <> 0 Then
                        Dim AddCharge_GL_Acc9 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code9, trans)
                        AddCharge_GL_Acc9 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc9, obj.loc_code, trans)

                        If clsCommon.myLen(AddCharge_GL_Acc9) <= 0 Then
                            Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code9 & " Not found")
                        End If
                        Dim AccAddCostCR() As String = {AddCharge_GL_Acc9, -1 * obj.Add_Charge_Amt9}
                        ArryLst.Add(AccAddCostCR)
                    End If
                    If obj.Add_Charge_Amt10 <> 0 Then
                        Dim AddCharge_GL_Acc10 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code10, trans)
                        AddCharge_GL_Acc10 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc10, obj.loc_code, True, trans)

                        If clsCommon.myLen(AddCharge_GL_Acc10) <= 0 Then
                            Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code10 & " Not found")
                        End If
                        Dim AccAddCostCR() As String = {AddCharge_GL_Acc10, -1 * obj.Add_Charge_Amt10}
                        ArryLst.Add(AccAddCostCR)
                    End If
                    '' Additional cost ends here

                    ''richa agarwal added on 02-jan-2015
                    If clsCommon.CompairString(objInv.Invoice_Type, "S") <> CompairStringResult.Equal Then
                        Dim isFirstTime As Boolean = True
                        For Each objTR As clsCustomerInvoiceDetailFarmer In obj.Arr
                            'Dim dblLedgeerNonRecoverableAmt As Double = clsCustomerInvoiceHeadFarmer.GetTaxAmt(objTR, trans)
                            Dim AccInvDR() As String = {objTR.GL_Account_Code, -1 * (objTR.Amount_less_Discount)}
                            ArryLst.Add(AccInvDR)

                            If isFirstTime AndAlso clsCommon.CompairString(obj.Trans_Type, "CSA") <> CompairStringResult.Equal Then
                                Dim AccExciseDR() As String = {objTR.GL_Account_Code, -1 * dblExcise}
                                ArryLst.Add(AccExciseDR)

                            End If
                            isFirstTime = False
                            ''''''added by priti for discount entry of invoice
                            If FormId = "FreshSaleInvoice" Then
                                If objTR.Amount_less_Discount = 0 AndAlso objTR.Discount > 0 Then
                                    Dim AccDiscDR() As String = {objTR.GL_Account_Code, 1 * (objTR.Discount)}
                                    ArryLst.Add(AccDiscDR)
                                End If
                            End If
                            ''''''code ends here
                        Next
                        Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code='" + obj.Arr(0).GL_Account_Code + "'", trans))
                        Dim strACWithLocation As String = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Customer_Control_AC, strLocation, True, trans)

                        Dim AccInvCR() As String = {strACWithLocation, obj.Document_Total}
                        ArryLst.Add(AccInvCR)
                        If clsCommon.CompairString(obj.Trans_Type, "CSA") = CompairStringResult.Equal Then
                            Dim AccExciseDR() As String = {strACWithLocation, 1 * dblExcise}
                            ArryLst.Add(AccExciseDR)
                        End If

                        ' for  rounding off account
                        If obj.RoundOffAmount <> 0 Then
                            Dim strACRoundInvCr As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DefaultRoundOffGLAccount, clsFixedParameterCode.DefaultRoundOffGLAccount, trans))

                            If clsCommon.myLen(strACRoundInvCr) <= 0 Then
                                Throw New Exception("Please set round off account in Sales Setting")
                            End If

                            '================Changed By Rohit on Apr 3,2015 .showing Error on Post Dispatch .Because it was searching AccountSegmentof Location not Segment.==============
                            ' strACRoundInvCr = clsERPFuncationality.ChangeGLAccountLocationSegment(strACRoundInvCr, objInv.Bill_To_Location, True, trans)

                            strACRoundInvCr = clsERPFuncationality.ChangeGLAccountLocationSegment(strACRoundInvCr, objInv.Bill_To_Location, False, trans)
                            '==========================================================================================================================================================
                            Dim AccRoundInvCR() As String = {strACRoundInvCr, -1 * obj.RoundOffAmount}
                            ArryLst.Add(AccRoundInvCR)

                        End If

                        If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL'And Not IsAllowPurchaseAccounting
                            For Each objInvDetail As clsSNInvoiceDetail In objInv.Arr
                                Dim strCode As String = objInvDetail.Shipment_Code
                                If Not arr.Contains(strCode) Then
                                    arr.Add(strCode)
                                    dblCogsCost += clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Source_Doc_No='" & objInvDetail.Shipment_Code & "'", trans))

                                    ''''' for cogs entry item wise
                                    Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code " & _
                                    "where Source_Doc_No='" & objInvDetail.Shipment_Code & "'"
                                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
                                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                                        For Each dr As DataRow In dt.Rows
                                            strCogsAcct = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Of_Goods_Sold from TSPL_ITEM_MASTER left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code=TSPL_ITEM_MASTER.Sale_Class_Code where Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans))
                                            If clsCommon.myLen(strCogsAcct) = 0 Then
                                                Throw New Exception("Please set Cost Of Goods Account for first item")
                                            End If
                                            '=================rohit Done on Nov 11,2014 =====Discussed with Priti Mam and Balwinder Sir==============
                                            strCogsAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(strCogsAcct, obj.loc_code, True, trans)
                                            Dim Acc1() As String = {strCogsAcct, clsCommon.myCdbl(dr("Cost"))}
                                            ArryLst.Add(Acc1)
                                        Next
                                    End If
                                    ''''' cogs entry item wise ends here

                                End If
                            Next


                            Dim strShipmentClearingAC = clsDBFuncationality.getSingleValue("SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                      " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                       " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objInv.Arr.Item(0).Item_Code.ToString() + "'", trans)
                            If clsCommon.myLen(strShipmentClearingAC) = 0 Then
                                Throw New Exception("Please set Shipment clearing Account for first item")
                            End If
                            strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, objInv.Bill_To_Location, trans)

                            'Dim strCogsAcct = clsDBFuncationality.getSingleValue("select Cost_Of_Goods_Sold from TSPL_ITEM_MASTER left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code=TSPL_ITEM_MASTER.Sale_Class_Code where Item_Code='" + objInv.Arr.Item(0).Item_Code.ToString() + "'", trans)
                            'If clsCommon.myLen(strCogsAcct) = 0 Then
                            '    Throw New Exception("Please set Cost of Goods Sold Account for first item")
                            'End If
                            'strCogsAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(strCogsAcct, objInv.Bill_To_Location, trans)

                            Dim Acc() As String = {strShipmentClearingAC, -1 * dblCogsCost}


                            ArryLst.Add(Acc)

                        End If  '' Done By Pankaj Jha For Skipping Cogs GL


                        strRemarks = " AR invoice for customer: " + obj.Farmer_Code + " - " + obj.Farmer_Name + "  For Sale Invoice No " & objInv.Document_Code & " "
                    Else
                        ''richa 
                        ''''' GL entry for Service Invoice start here
                        For Each objTR As clsCustomerInvoiceDetailFarmer In obj.Arr
                            Dim AccInvDR() As String = {objTR.GL_Account_Code, -1 * (objTR.Amount_less_Discount)}
                            ArryLst.Add(AccInvDR)
                        Next
                        Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code='" + obj.Arr(0).GL_Account_Code + "'", trans))
                        Dim strACWithLocation As String = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Customer_Control_AC, strLocation, True, trans)

                        Dim AccInvCR() As String = {strACWithLocation, obj.Document_Total}
                        ArryLst.Add(AccInvCR)

                        strRemarks = " AR invoice for customer: " + obj.Farmer_Code + " - " + obj.Farmer_Name + "  For Service Invoice No " & objInv.Document_Code & " "

                    End If

                Else
                    ''''' GL entry for Service Invoice start here
                    For Each objTR As clsCustomerInvoiceDetailFarmer In obj.Arr
                        Dim AccInvDR() As String = {objTR.GL_Account_Code, -1 * (objTR.Amount_less_Discount)}
                        ArryLst.Add(AccInvDR)
                    Next
                    Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code='" + obj.Arr(0).GL_Account_Code + "'", trans))
                    Dim strACWithLocation As String = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Customer_Control_AC, strLocation, True, trans)

                    Dim AccInvCR() As String = {strACWithLocation, obj.Document_Total}
                    ArryLst.Add(AccInvCR)

                    strRemarks = " AR invoice for customer: " + obj.Farmer_Code + " - " + obj.Farmer_Name + "  For Service Invoice No " & objInv.Document_Code & " "
                    If clsCommon.CompairString(FormId, "CSA-SALE") = CompairStringResult.Equal Then
                        strRemarks = " AR invoice for customer: " + obj.Farmer_Code + " - " + obj.Farmer_Name + "  For CSA Sale Patti No " & objInv.Document_Code & " "
                    End If
                End If


                '' cOGS ENDS HERE


            ElseIf ((clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal)) AndAlso FormId = "BulkSaleInvoice" Then
                Dim objInv As ClsInvoiceBulkSale
                Dim arr As New List(Of String)
                Dim dblCogsCost As Double
                Dim strCogsAcct As String
                objInv = ClsInvoiceBulkSale.GetData(obj.Against_Sale_No, "", NavigatorType.Current, trans)
                ''''' GL entry for Tax and retail Invoice
                'If clsCommon.CompairString(objInv.Invoice_Type, "T") = CompairStringResult.Equal OrElse clsCommon.CompairString(objInv.Invoice_Type, "R") = CompairStringResult.Equal Then


                '''' FOR Additional Cost START here
                If obj.Add_Charge_Amt1 <> 0 Then
                    Dim AddCharge_GL_Acc1 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code1, trans)
                    AddCharge_GL_Acc1 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc1, obj.loc_code, trans)

                    If clsCommon.myLen(AddCharge_GL_Acc1) <= 0 Then
                        Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code1 & " Not found")
                    End If

                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc1, -1 * obj.Add_Charge_Amt1}
                    ArryLst.Add(AccAddCostCR)
                End If
                If obj.Add_Charge_Amt2 <> 0 Then
                    Dim AddCharge_GL_Acc2 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code2, trans)
                    AddCharge_GL_Acc2 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc2, obj.loc_code, trans)

                    If clsCommon.myLen(AddCharge_GL_Acc2) <= 0 Then
                        Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code2 & " Not found")
                    End If

                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc2, -1 * obj.Add_Charge_Amt2}
                    ArryLst.Add(AccAddCostCR)
                End If
                If obj.Add_Charge_Amt3 <> 0 Then
                    Dim AddCharge_GL_Acc3 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code3, trans)
                    AddCharge_GL_Acc3 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc3, obj.loc_code, trans)

                    If clsCommon.myLen(AddCharge_GL_Acc3) <= 0 Then
                        Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code3 & " Not found")
                    End If

                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc3, -1 * obj.Add_Charge_Amt3}
                    ArryLst.Add(AccAddCostCR)
                End If
                If obj.Add_Charge_Amt4 <> 0 Then
                    Dim AddCharge_GL_Acc4 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code4, trans)
                    AddCharge_GL_Acc4 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc4, obj.loc_code, trans)

                    If clsCommon.myLen(AddCharge_GL_Acc4) <= 0 Then
                        Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code4 & " Not found")
                    End If

                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc4, -1 * obj.Add_Charge_Amt4}
                    ArryLst.Add(AccAddCostCR)
                End If
                If obj.Add_Charge_Amt5 <> 0 Then
                    Dim AddCharge_GL_Acc5 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code5, trans)
                    If clsCommon.myLen(AddCharge_GL_Acc5) <= 0 Then
                        Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code5 & " Not found")
                    End If
                    AddCharge_GL_Acc5 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc5, obj.loc_code, trans)

                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc5, -1 * obj.Add_Charge_Amt5}
                    ArryLst.Add(AccAddCostCR)
                End If
                If obj.Add_Charge_Amt6 <> 0 Then
                    Dim AddCharge_GL_Acc6 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code6, trans)
                    AddCharge_GL_Acc6 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc6, obj.loc_code, trans)

                    If clsCommon.myLen(AddCharge_GL_Acc6) <= 0 Then
                        Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code6 & " Not found")
                    End If
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc6, -1 * obj.Add_Charge_Amt6}
                    ArryLst.Add(AccAddCostCR)
                End If
                If obj.Add_Charge_Amt7 <> 0 Then
                    Dim AddCharge_GL_Acc7 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code7, trans)
                    AddCharge_GL_Acc7 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc7, obj.loc_code, trans)

                    If clsCommon.myLen(AddCharge_GL_Acc7) <= 0 Then
                        Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code7 & " Not found")
                    End If
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc7, -1 * obj.Add_Charge_Amt7}
                    ArryLst.Add(AccAddCostCR)
                End If
                If obj.Add_Charge_Amt8 <> 0 Then
                    Dim AddCharge_GL_Acc8 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code8, trans)
                    AddCharge_GL_Acc8 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc8, obj.loc_code, trans)

                    If clsCommon.myLen(AddCharge_GL_Acc8) <= 0 Then
                        Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code8 & " Not found")
                    End If
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc8, -1 * obj.Add_Charge_Amt8}
                    ArryLst.Add(AccAddCostCR)
                End If
                If obj.Add_Charge_Amt9 <> 0 Then
                    Dim AddCharge_GL_Acc9 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code9, trans)
                    AddCharge_GL_Acc9 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc9, obj.loc_code, trans)

                    If clsCommon.myLen(AddCharge_GL_Acc9) <= 0 Then
                        Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code9 & " Not found")
                    End If
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc9, -1 * obj.Add_Charge_Amt9}
                    ArryLst.Add(AccAddCostCR)
                End If
                If obj.Add_Charge_Amt10 <> 0 Then
                    Dim AddCharge_GL_Acc10 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code10, trans)
                    AddCharge_GL_Acc10 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc10, obj.loc_code, trans)

                    If clsCommon.myLen(AddCharge_GL_Acc10) <= 0 Then
                        Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code10 & " Not found")
                    End If
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc10, -1 * obj.Add_Charge_Amt10}
                    ArryLst.Add(AccAddCostCR)
                End If
                '' Additional cost ends here

                Dim isFirstTime As Boolean = True
                For Each objTR As clsCustomerInvoiceDetailFarmer In obj.Arr
                    'Dim dblLedgeerNonRecoverableAmt As Double = clsCustomerInvoiceHeadFarmer.GetTaxAmt(objTR, trans)
                    Dim AccInvDR() As String = {objTR.GL_Account_Code, -1 * (objTR.Total_Amount)}

                    ArryLst.Add(AccInvDR)
                    isFirstTime = False
                Next
                Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code='" + obj.Arr(0).GL_Account_Code + "'", trans))
                Dim strACWithLocation As String = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Customer_Control_AC, strLocation, True, trans)

                ''richa agarwal 14/10/2014
                'Dim creditamount As Double = 0
                'creditamount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select SUM(InvoiceAmount) from TSPL_INVOICE_DETAIL_BULKSALE  where Document_No='" & objInv.Document_No & "'", trans))
                ' Dim AccInvCR() As String = {strACWithLocation, obj.Document_Total}
                'Dim AccInvCR() As String = {strACWithLocation, obj.Discount_Base}
                Dim AccInvCR() As String = {strACWithLocation, obj.Discount_Base + obj.RoundOffAmount}
                ArryLst.Add(AccInvCR)


                If obj.RoundOffAmount <> 0 Then
                    Dim strACRoundInvCr As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DefaultRoundOffGLAccount, clsFixedParameterCode.DefaultRoundOffGLAccount, trans))

                    If clsCommon.myLen(strACRoundInvCr) <= 0 Then
                        Throw New Exception("Please set round off account in Sales Setting")
                    End If

                    strACRoundInvCr = clsERPFuncationality.ChangeGLAccountLocationSegment(strACRoundInvCr, strLocation, True, trans)
                    Dim AccRoundInvCR() As String = {strACRoundInvCr, -1 * obj.RoundOffAmount}
                    ArryLst.Add(AccRoundInvCR)
                End If

                ''============

                If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL'And Not IsAllowPurchaseAccounting
                    Dim Costincaseoflossandgain As Double = 0
                    For Each objInvDetail As ClsInvoiceDetailBulkSale In objInv.arrInvoiceDetailBulkSale
                        Dim strCode As String = objInvDetail.Dispatch_Code
                        If Not arr.Contains(strCode) Then
                            arr.Add(strCode)
                            '' changes by richa agarwal against ticket BM00000006070
                            ''updation by richa agarwal according to gain or loss amount
                            Dim dblCogsCosttemp As Double = 0
                            dblCogsCosttemp = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Source_Doc_No='" & objInvDetail.Dispatch_Code & "'", trans))
                            ' dblCogsCost += clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select (sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end)/Qty) * " & objInvDetail.InvoiceQty & " as COst from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Source_Doc_No='" & objInvDetail.Dispatch_Code & "'", trans))
                            dblCogsCost += dblCogsCosttemp
                            ''''' for cogs entry item wise
                            Dim strSql As String = String.Empty
                            If clsCommon.CompairString(objInv.InvoiceAgainst, "Against Dispatch") = CompairStringResult.Equal Then
                                strSql = "select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,((case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end)/TSPL_INVENTORY_MOVEMENT_NEW.Qty)* " & objInvDetail.InvoiceQty & "  as Cost from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code" & _
                               " where Source_Doc_No='" & objInvDetail.Dispatch_Code & "'"
                            Else
                                strSql = "select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code " & _
                                "where Source_Doc_No='" & objInvDetail.Dispatch_Code & "'"
                            End If
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
                            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                                For Each dr As DataRow In dt.Rows
                                    strCogsAcct = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Of_Goods_Sold from TSPL_ITEM_MASTER left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code=TSPL_ITEM_MASTER.Sale_Class_Code where Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans))
                                    If clsCommon.myLen(strCogsAcct) = 0 Then
                                        Throw New Exception("Please set Cost of Goods Sold for first item")
                                    End If
                                    ''richa agarwal discussed with Balwinder sir
                                    'strCogsAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(strCogsAcct, obj.loc_code, trans)
                                    strCogsAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(strCogsAcct, obj.loc_code, True, trans)
                                    ''----------------------------------
                                    Dim Acc1() As String = {strCogsAcct, clsCommon.myCdbl(dr("Cost"))}

                                    ArryLst.Add(Acc1)

                                    Costincaseoflossandgain = clsCommon.myCdbl(dr("Cost"))
                                Next
                            End If
                            ''richa agarwal 06/04/2015
                            If clsCommon.CompairString(objInv.InvoiceAgainst, "Against Dispatch") = CompairStringResult.Equal Then
                                ' If dblCogsCost <> Costincaseoflossandgain Then
                                If dblCogsCosttemp <> Costincaseoflossandgain Then
                                    Dim strGainorLossAC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT SA.Gain_Loss_Account  FROM TSPL_ITEM_MASTER AS IM INNER JOIN  TSPL_SALES_ACCOUNTS  AS SA ON IM.Sale_Class_Code  = SA.Sales_Class_Code  INNER JOIN  TSPL_GL_ACCOUNTS AS GLA ON SA.Gain_Loss_Account = GLA.Account_Code WHERE IM.Item_Code='" + objInv.arrInvoiceDetailBulkSale.Item(0).Item_Code.ToString() + "'", trans))
                                    If clsCommon.myLen(strGainorLossAC) = 0 Then
                                        Throw New Exception("Please set Gain/Loss Account for first item")
                                    End If
                                    strGainorLossAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strGainorLossAC, obj.loc_code, True, trans)

                                    'Dim Acc2() As String = {strGainorLossAC, 1 * (dblCogsCost - Costincaseoflossandgain)}
                                    Dim Acc2() As String = {strGainorLossAC, 1 * (dblCogsCosttemp - Costincaseoflossandgain)}
                                    ArryLst.Add(Acc2)

                                End If
                            End If
                            ''------------------
                            ''''' cogs entry item wise ends here
                        End If
                    Next

                    ' ''richa agarwal 23/02/2015
                    'If clsCommon.CompairString(objInv.InvoiceAgainst, "Against Dispatch") = CompairStringResult.Equal Then
                    '    If dblCogsCost <> Costincaseoflossandgain Then
                    '        Dim strGainorLossAC = clsDBFuncationality.getSingleValue("SELECT SA.Gain_Loss_Account  FROM TSPL_ITEM_MASTER AS IM INNER JOIN  TSPL_SALES_ACCOUNTS  AS SA ON IM.Sale_Class_Code  = SA.Sales_Class_Code  INNER JOIN  TSPL_GL_ACCOUNTS AS GLA ON SA.Gain_Loss_Account = GLA.Account_Code WHERE IM.Item_Code='" + objInv.arrInvoiceDetailBulkSale.Item(0).Item_Code.ToString() + "'", trans)
                    '        If clsCommon.myLen(strGainorLossAC) = 0 Then
                    '            Throw New Exception("Please set Gain/Loss Account for first item")
                    '        End If
                    '        strGainorLossAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strGainorLossAC, obj.loc_code, True, trans)

                    '        Dim Acc2() As String = {strGainorLossAC, 1 * (dblCogsCost - Costincaseoflossandgain)}
                    '        ArryLst.Add(Acc2)

                    '    End If
                    'End If
                    ' ''------------------


                    Dim strShipmentClearingAC = clsDBFuncationality.getSingleValue("SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                     " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objInv.arrInvoiceDetailBulkSale.Item(0).Item_Code.ToString() + "'", trans)
                    If clsCommon.myLen(strShipmentClearingAC) = 0 Then
                        Throw New Exception("Please set Shipment clearing Account for first item")
                    End If
                    ''richa 13/09/2014 change 
                    '  strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, objInv.Location_Code, trans)
                    strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, obj.loc_code, True, trans)

                    Dim Acc() As String = {strShipmentClearingAC, -1 * dblCogsCost}
                    ArryLst.Add(Acc)



                End If  '' Done By Pankaj Jha For Skipping Cogs GL

                ''richa agarwal
                If clsCommon.CompairString(objInv.InvoiceAgainst, "Against Dispatch") = CompairStringResult.Equal Then
                    strRemarks = " AR invoice for customer: " + obj.Farmer_Code + " - " + obj.Farmer_Name + "  For Invoice Bulk Sale No " & objInv.Document_No & " "
                Else
                    strRemarks = " AR invoice for customer: " + obj.Farmer_Code + " - " + obj.Farmer_Name + "  For Invoice Bulk Sale Trade No " & objInv.Document_No & " "
                End If

                ''=====================
                'strRemarks = " AR invoice for customer: " + obj.Farmer_Code + " - " + obj.Farmer_Name + "  For Sale Invoice No " & objInv.Document_No & " "

            End If
        ElseIf clsCommon.myLen(obj.AgainstScrap) > 0 Then
            If obj.TAX1_Amt <> 0 Then
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX1)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, True, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX1_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX2_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX2)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX2_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX3_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX3)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX3_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX4_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX4)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX4_Amt}
                ArryLst.Add(AccInvDR)
            End If
            If obj.TAX5_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX5)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX5_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX6_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX6)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX6_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX7_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX7)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX7_Amt}
                ArryLst.Add(AccInvDR)

            End If

            If obj.TAX8_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX8)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX8_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX9_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX9)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX9_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX10_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX10)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX10_Amt}
                ArryLst.Add(AccInvDR)
            End If

            '' FOR Additional Cost START here
            If obj.Add_Charge_Amt1 <> 0 Then
                Dim AddCharge_GL_Acc1 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code1, trans)
                AddCharge_GL_Acc1 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc1, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc1) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code1 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc1, -1 * obj.Add_Charge_Amt1}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt2 <> 0 Then
                Dim AddCharge_GL_Acc2 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code2, trans)
                AddCharge_GL_Acc2 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc2, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc2) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code2 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc2, -1 * obj.Add_Charge_Amt2}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt3 <> 0 Then
                Dim AddCharge_GL_Acc3 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code3, trans)
                AddCharge_GL_Acc3 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc3, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc3) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code3 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc3, -1 * obj.Add_Charge_Amt3}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt4 <> 0 Then
                Dim AddCharge_GL_Acc4 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code4, trans)
                AddCharge_GL_Acc4 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc4, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc4) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code4 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc4, -1 * obj.Add_Charge_Amt4}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt5 <> 0 Then
                Dim AddCharge_GL_Acc5 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code5, trans)
                AddCharge_GL_Acc5 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc5, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc5) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code5 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc5, -1 * obj.Add_Charge_Amt5}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt6 <> 0 Then
                Dim AddCharge_GL_Acc6 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code6, trans)
                AddCharge_GL_Acc6 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc6, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc6) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code6 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc6, -1 * obj.Add_Charge_Amt6}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt7 <> 0 Then
                Dim AddCharge_GL_Acc7 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code7, trans)
                AddCharge_GL_Acc7 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc7, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc7) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code7 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc7, -1 * obj.Add_Charge_Amt7}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt8 <> 0 Then
                Dim AddCharge_GL_Acc8 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code8, trans)
                AddCharge_GL_Acc8 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc8, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc8) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code8 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc8, -1 * obj.Add_Charge_Amt8}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt9 <> 0 Then
                Dim AddCharge_GL_Acc9 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code9, trans)
                AddCharge_GL_Acc9 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc9, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc9) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code9 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc9, -1 * obj.Add_Charge_Amt9}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt10 <> 0 Then
                Dim AddCharge_GL_Acc10 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code10, trans)
                AddCharge_GL_Acc10 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc10, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc10) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code10 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc10, -1 * obj.Add_Charge_Amt10}
                ArryLst.Add(AccAddCostCR)
            End If

            For Each objTR As clsCustomerInvoiceDetailFarmer In obj.Arr
                Dim dblLedgeerNonRecoverableAmt As Double = 0
                Dim AccInvDR1() As String = {objTR.GL_Account_Code, -1 * objTR.Amount_less_Discount}
                ArryLst.Add(AccInvDR1)
            Next

            If obj.RoundOffAmount <> 0 Then
                Dim strACRoundInvCr As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DefaultRoundOffGLAccount, clsFixedParameterCode.DefaultRoundOffGLAccount, trans))
                If clsCommon.myLen(strACRoundInvCr) <= 0 Then
                    Throw New Exception("Please set round off account in Sales Setting")
                End If
                strACRoundInvCr = clsERPFuncationality.ChangeGLAccountLocationSegment(strACRoundInvCr, obj.loc_code, True, trans)
                Dim AccRoundInvCR() As String = {strACRoundInvCr, obj.RoundOffAmount}
                ArryLst.Add(AccRoundInvCR)
            End If

            Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code='" + obj.Arr(0).GL_Account_Code + "'", trans))
            Dim strACWithLocation As String = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Customer_Control_AC, strLocation, True, trans)
            Dim AccInvCR1() As String = {strACWithLocation, obj.Document_Total}
            ArryLst.Add(AccInvCR1)

            If Not isSkipCogsGL Then 'And Not IsAllowPurchaseAccounting
                Dim strInventoryControlAc As String
                Dim strCogsAcct As String
                Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where Source_Doc_No='" & obj.AgainstScrap & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        strInventoryControlAc = clsDBFuncationality.getSingleValue("SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                        " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                        " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans)
                        strInventoryControlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControlAc, obj.loc_code, True, trans)

                        If clsCommon.myLen(strInventoryControlAc) = 0 Then
                            Throw New Exception("Please set Inventory Control Account for first item")
                        End If

                        strCogsAcct = clsDBFuncationality.getSingleValue("select Cost_Of_Goods_Sold from TSPL_ITEM_MASTER left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code=TSPL_ITEM_MASTER.Sale_Class_Code where Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans)
                        If clsCommon.myLen(strCogsAcct) = 0 Then
                            Throw New Exception("Please set Cost of Goods Sold for first item")
                        End If
                        strCogsAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(strCogsAcct, obj.loc_code, True, trans)
                        Dim Acc() As String = {strInventoryControlAc, -1 * clsCommon.myCdbl(dr("Cost"))}
                        ArryLst.Add(Acc)
                        Dim Acc1() As String = {strCogsAcct, clsCommon.myCdbl(dr("Cost"))}
                        ArryLst.Add(Acc1)
                    Next
                End If
            End If
        ElseIf clsCommon.myLen(obj.Against_Asset_Disposal) > 0 Then
            Dim qryAsset As String = clsAssetScrapSaleHead.GetAssetDisposalJEQuery(obj.Against_Asset_Disposal)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryAsset, trans)

            '' customer control acc debit with sale amt
            Dim Acc() As String = {clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Customer_Control_AC, obj.loc_code, trans), obj.Document_Total}
            ArryLst.Add(Acc)
            For Each dr As DataRow In dt.Rows
                '' get accounts
                'Dim Receivable_Control_acct As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr.Item("Receivable_Control_acct")), obj.loc_code, trans)
                Dim Disposal_Account As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr.Item("Disposal_Account")), obj.loc_code, trans)
                Dim Disposal_Cost_Account As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr.Item("Disposal_Cost_Account")), obj.loc_code, trans)
                Dim Ac_Accum_Dep As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr.Item("Ac_Accum_Dep")), obj.loc_code, trans)
                Dim Ac_Control As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr.Item("Ac_Control")), obj.loc_code, trans)
                Dim PROFIT_AC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr.Item("PROFIT_AC")), obj.loc_code, trans)
                Dim LOSS_AC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr.Item("LOSS_AC")), obj.loc_code, trans)

                '' get amounts
                Dim Book_Source_value As Decimal = clsCommon.myCdbl(dr.Item("Book_Source_value"))
                Dim Sale_Amount As Decimal = clsCommon.myCdbl(dr.Item("Sale_Amount"))
                Dim Perm_Dep_Amount As Decimal = clsCommon.myCdbl(dr.Item("Perm_Dep_Amount"))

                '' disposal acc credit with sale amt
                Dim Acc1() As String = {Disposal_Account, -1 * Sale_Amount}
                ArryLst.Add(Acc1)

                '' Accumulated Depreciation a/c debit with depreciation amt
                Dim Acc2() As String = {Ac_Accum_Dep, Perm_Dep_Amount}
                ArryLst.Add(Acc2)

                '' Assets Control a/c credit with book source value 
                Dim Acc3() As String = {Ac_Control, -1 * Book_Source_value}
                ArryLst.Add(Acc3)

                If (Sale_Amount - Book_Source_value + Perm_Dep_Amount) > 0 Then
                    '' profit a/c credit with depreciation amt
                    Dim Acc4() As String = {PROFIT_AC, -1 * (Sale_Amount - Book_Source_value + Perm_Dep_Amount)}
                    ArryLst.Add(Acc4)
                Else
                    '' loss a/c debit with depreciation amt
                    Dim Acc5() As String = {LOSS_AC, -1 * (Sale_Amount - Book_Source_value + Perm_Dep_Amount)}
                    ArryLst.Add(Acc5)
                End If

                '' customer control acc debit with sale amt
                Dim Acc6() As String = {Disposal_Cost_Account, Sale_Amount}
                ArryLst.Add(Acc6)
            Next
            If obj.TAX1_Amt <> 0 Then
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX1)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX1_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX2_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX2)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX2_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX3_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX3)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX3_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX4_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX4)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX4_Amt}
                ArryLst.Add(AccInvDR)
            End If
            If obj.TAX5_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX5)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX5_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX6_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX6)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX6_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX7_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX7)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX7_Amt}
                ArryLst.Add(AccInvDR)

            End If

            If obj.TAX8_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX8)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX8_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX9_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX9)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX9_Amt}
                ArryLst.Add(AccInvDR)
            End If

            If obj.TAX10_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX10)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, trans)
                Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX10_Amt}
                ArryLst.Add(AccInvDR)
            End If

            '' FOR Additional Cost START here
            If obj.Add_Charge_Amt1 <> 0 Then
                Dim AddCharge_GL_Acc1 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code1, trans)
                AddCharge_GL_Acc1 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc1, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc1) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code1 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc1, -1 * obj.Add_Charge_Amt1}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt2 <> 0 Then
                Dim AddCharge_GL_Acc2 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code2, trans)
                AddCharge_GL_Acc2 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc2, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc2) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code2 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc2, -1 * obj.Add_Charge_Amt2}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt3 <> 0 Then
                Dim AddCharge_GL_Acc3 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code3, trans)
                AddCharge_GL_Acc3 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc3, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc3) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code3 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc3, -1 * obj.Add_Charge_Amt3}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt4 <> 0 Then
                Dim AddCharge_GL_Acc4 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code4, trans)
                AddCharge_GL_Acc4 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc4, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc4) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code4 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc4, -1 * obj.Add_Charge_Amt4}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt5 <> 0 Then
                Dim AddCharge_GL_Acc5 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code5, trans)
                AddCharge_GL_Acc5 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc5, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc5) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code5 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc5, -1 * obj.Add_Charge_Amt5}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt6 <> 0 Then
                Dim AddCharge_GL_Acc6 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code6, trans)
                AddCharge_GL_Acc6 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc6, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc6) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code6 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc6, -1 * obj.Add_Charge_Amt6}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt7 <> 0 Then
                Dim AddCharge_GL_Acc7 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code7, trans)
                AddCharge_GL_Acc7 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc7, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc7) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code7 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc7, -1 * obj.Add_Charge_Amt7}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt8 <> 0 Then
                Dim AddCharge_GL_Acc8 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code8, trans)
                AddCharge_GL_Acc8 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc8, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc8) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code8 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc8, -1 * obj.Add_Charge_Amt8}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt9 <> 0 Then
                Dim AddCharge_GL_Acc9 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code9, trans)
                AddCharge_GL_Acc9 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc9, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc9) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code9 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc9, -1 * obj.Add_Charge_Amt9}
                ArryLst.Add(AccAddCostCR)
            End If
            If obj.Add_Charge_Amt10 <> 0 Then
                Dim AddCharge_GL_Acc10 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code10, trans)
                AddCharge_GL_Acc10 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc10, obj.loc_code, trans)
                If clsCommon.myLen(AddCharge_GL_Acc10) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code10 & " Not found")
                End If
                Dim AccAddCostCR() As String = {AddCharge_GL_Acc10, -1 * obj.Add_Charge_Amt10}
                ArryLst.Add(AccAddCostCR)
            End If

        ElseIf clsCommon.myLen(obj.Against_Sale_Return_No) > 0 OrElse _
               clsCommon.myLen(obj.Against_MCC_Material_Sale_Return) > 0 OrElse _
               clsCommon.myLen(obj.Against_VCGL) > 0 OrElse _
           (clsCommon.myLen(obj.Against_Sale_No) <= 0 AndAlso clsCommon.myLen(obj.AgainstScrap) <= 0 AndAlso clsCommon.myLen(obj.Against_Sale_Return_No) <= 0 AndAlso clsCommon.myLen(obj.Against_MCC_Material_Sale_Return) <= 0 AndAlso clsCommon.myLen(obj.Against_VCGL) <= 0) Then ''BM00000007723 For direct AR Invoice
            If obj.TAX1_Amt <> 0 Then
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX1)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, True, trans)
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX1_Amt}
                    ArryLst.Add(AccInvDR)
                Else
                    Dim AccInvDR() As String = {Tax_Liability_Account, obj.TAX1_Amt}
                    ArryLst.Add(AccInvDR)
                End If

            End If

            If obj.TAX2_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX2)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, True, trans)
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX2_Amt}
                    ArryLst.Add(AccInvDR)

                Else
                    Dim AccInvDR() As String = {Tax_Liability_Account, obj.TAX2_Amt}
                    ArryLst.Add(AccInvDR)
                End If

            End If

            If obj.TAX3_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX3)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, True, trans)
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX3_Amt}
                    ArryLst.Add(AccInvDR)


                Else
                    Dim AccInvDR() As String = {Tax_Liability_Account, obj.TAX3_Amt}
                    ArryLst.Add(AccInvDR)
                End If

            End If

            If obj.TAX4_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX4)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, True, trans)
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX4_Amt}
                    ArryLst.Add(AccInvDR)


                Else
                    Dim AccInvDR() As String = {Tax_Liability_Account, obj.TAX4_Amt}
                    ArryLst.Add(AccInvDR)
                End If

            End If
            If obj.TAX5_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX5)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, True, trans)
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX5_Amt}
                    ArryLst.Add(AccInvDR)
                Else
                    Dim AccInvDR() As String = {Tax_Liability_Account, obj.TAX5_Amt}
                    ArryLst.Add(AccInvDR)
                End If

            End If

            If obj.TAX6_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX6)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, True, trans)
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX6_Amt}
                    ArryLst.Add(AccInvDR)
                Else
                    Dim AccInvDR() As String = {Tax_Liability_Account, obj.TAX6_Amt}
                    ArryLst.Add(AccInvDR)
                End If

            End If

            If obj.TAX7_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX7)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, True, trans)
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX7_Amt}
                    ArryLst.Add(AccInvDR)
                Else
                    Dim AccInvDR() As String = {Tax_Liability_Account, obj.TAX7_Amt}
                    ArryLst.Add(AccInvDR)
                End If
            End If

            If obj.TAX8_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX8)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, True, trans)
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX8_Amt}
                    ArryLst.Add(AccInvDR)
                Else
                    Dim AccInvDR() As String = {Tax_Liability_Account, obj.TAX8_Amt}
                    ArryLst.Add(AccInvDR)
                End If

            End If

            If obj.TAX9_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX9)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, True, trans)
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX9_Amt}
                    ArryLst.Add(AccInvDR)
                Else
                    Dim AccInvDR() As String = {Tax_Liability_Account, obj.TAX9_Amt}
                    ArryLst.Add(AccInvDR)
                End If

            End If

            If obj.TAX10_Amt <> 0 Then
                'isTaxRecoverable = clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans)
                Tax_Liability_Account = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
                If clsCommon.myLen(Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Liability Acount not found for Tax " + obj.TAX10)
                End If
                Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Tax_Liability_Account, obj.loc_code, True, trans)
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccInvDR() As String = {Tax_Liability_Account, -1 * obj.TAX10_Amt}
                    ArryLst.Add(AccInvDR)
                Else
                    Dim AccInvDR() As String = {Tax_Liability_Account, obj.TAX10_Amt}
                    ArryLst.Add(AccInvDR)
                End If

            End If

            '' FOR Additional Cost START here
            If obj.Add_Charge_Amt1 <> 0 Then
                Dim AddCharge_GL_Acc1 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code1, trans)
                AddCharge_GL_Acc1 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc1, obj.loc_code, True, trans)
                If clsCommon.myLen(AddCharge_GL_Acc1) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code1 & " Not found")
                End If
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc1, -1 * obj.Add_Charge_Amt1}
                    ArryLst.Add(AccAddCostCR)
                Else
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc1, obj.Add_Charge_Amt1}
                    ArryLst.Add(AccAddCostCR)
                End If

            End If
            If obj.Add_Charge_Amt2 <> 0 Then
                Dim AddCharge_GL_Acc2 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code2, trans)
                AddCharge_GL_Acc2 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc2, obj.loc_code, True, trans)
                If clsCommon.myLen(AddCharge_GL_Acc2) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code2 & " Not found")
                End If
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc2, -1 * obj.Add_Charge_Amt2}
                    ArryLst.Add(AccAddCostCR)
                Else
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc2, obj.Add_Charge_Amt2}
                    ArryLst.Add(AccAddCostCR)
                End If

            End If
            If obj.Add_Charge_Amt3 <> 0 Then
                Dim AddCharge_GL_Acc3 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code3, trans)
                AddCharge_GL_Acc3 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc3, obj.loc_code, True, trans)
                If clsCommon.myLen(AddCharge_GL_Acc3) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code3 & " Not found")
                End If
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc3, -1 * obj.Add_Charge_Amt3}
                    ArryLst.Add(AccAddCostCR)
                Else
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc3, obj.Add_Charge_Amt3}
                    ArryLst.Add(AccAddCostCR)
                End If

            End If
            If obj.Add_Charge_Amt4 <> 0 Then
                Dim AddCharge_GL_Acc4 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code4, trans)
                AddCharge_GL_Acc4 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc4, obj.loc_code, True, trans)
                If clsCommon.myLen(AddCharge_GL_Acc4) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code4 & " Not found")
                End If
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc4, -1 * obj.Add_Charge_Amt4}
                    ArryLst.Add(AccAddCostCR)
                Else
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc4, obj.Add_Charge_Amt4}
                    ArryLst.Add(AccAddCostCR)
                End If

            End If
            If obj.Add_Charge_Amt5 <> 0 Then
                Dim AddCharge_GL_Acc5 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code5, trans)
                AddCharge_GL_Acc5 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc5, obj.loc_code, True, trans)
                If clsCommon.myLen(AddCharge_GL_Acc5) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code5 & " Not found")
                End If
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc5, -1 * obj.Add_Charge_Amt5}
                    ArryLst.Add(AccAddCostCR)
                Else
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc5, obj.Add_Charge_Amt5}
                    ArryLst.Add(AccAddCostCR)
                End If

            End If
            If obj.Add_Charge_Amt6 <> 0 Then
                Dim AddCharge_GL_Acc6 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code6, trans)
                AddCharge_GL_Acc6 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc6, obj.loc_code, True, trans)
                If clsCommon.myLen(AddCharge_GL_Acc6) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code6 & " Not found")
                End If
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc6, -1 * obj.Add_Charge_Amt6}
                    ArryLst.Add(AccAddCostCR)
                Else
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc6, obj.Add_Charge_Amt6}
                    ArryLst.Add(AccAddCostCR)
                End If

            End If
            If obj.Add_Charge_Amt7 <> 0 Then
                Dim AddCharge_GL_Acc7 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code7, trans)
                AddCharge_GL_Acc7 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc7, obj.loc_code, True, trans)
                If clsCommon.myLen(AddCharge_GL_Acc7) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code7 & " Not found")
                End If
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc7, -1 * obj.Add_Charge_Amt7}
                    ArryLst.Add(AccAddCostCR)
                Else
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc7, obj.Add_Charge_Amt7}
                    ArryLst.Add(AccAddCostCR)
                End If

            End If
            If obj.Add_Charge_Amt8 <> 0 Then
                Dim AddCharge_GL_Acc8 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code8, trans)
                AddCharge_GL_Acc8 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc8, obj.loc_code, True, trans)
                If clsCommon.myLen(AddCharge_GL_Acc8) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code8 & " Not found")
                End If
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc8, -1 * obj.Add_Charge_Amt8}
                    ArryLst.Add(AccAddCostCR)
                Else
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc8, obj.Add_Charge_Amt8}
                    ArryLst.Add(AccAddCostCR)
                End If

            End If
            If obj.Add_Charge_Amt9 <> 0 Then
                Dim AddCharge_GL_Acc9 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code9, trans)
                AddCharge_GL_Acc9 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc9, obj.loc_code, True, trans)
                If clsCommon.myLen(AddCharge_GL_Acc9) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code9 & " Not found")
                End If
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc9, -1 * obj.Add_Charge_Amt9}
                    ArryLst.Add(AccAddCostCR)
                Else
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc9, obj.Add_Charge_Amt9}
                    ArryLst.Add(AccAddCostCR)
                End If
            End If
            If obj.Add_Charge_Amt10 <> 0 Then
                Dim AddCharge_GL_Acc10 = clsAdditionalCharge.GetAdditonalChACC(obj.Add_Charge_Code10, trans)
                AddCharge_GL_Acc10 = clsERPFuncationality.ChangeGLAccountLocationSegment(AddCharge_GL_Acc10, obj.loc_code, True, trans)
                If clsCommon.myLen(AddCharge_GL_Acc10) <= 0 Then
                    Throw New Exception("Additional GL Account for " & obj.Add_Charge_Code10 & " Not found")
                End If
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc10, -1 * obj.Add_Charge_Amt10}
                    ArryLst.Add(AccAddCostCR)
                Else
                    Dim AccAddCostCR() As String = {AddCharge_GL_Acc10, obj.Add_Charge_Amt10}
                    ArryLst.Add(AccAddCostCR)
                End If

            End If
            '' Additional cost ends here

            Dim isFirstTime As Boolean = True
            '' Anubhooti 19-Mar-2015 (IF Entry is against VCGL then GL will get opposite(DR/CR))
            If clsCommon.myLen(obj.Against_VCGL) <= 0 Then 'AndAlso clsCommon.CompairString(obj.Trans_Type, "VC") <> CompairStringResult.Equal
                For Each objTR As clsCustomerInvoiceDetailFarmer In obj.Arr
                    Dim dblLedgeerNonRecoverableAmt As Double = 0
                    ''richa agarwal 14/05/2015 BM00000006615 credit gl account in case of direct ar invoice which type of Invoice
                    If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                        Dim AccInvDR1() As String = {objTR.GL_Account_Code, objTR.Amount_less_Discount * -1}
                        ArryLst.Add(AccInvDR1)
                    Else
                        Dim AccInvDR() As String = {objTR.GL_Account_Code, objTR.Amount_less_Discount}
                        ArryLst.Add(AccInvDR)
                    End If
                    ''--------------------------------------
                    isFirstTime = False

                    ''''''added by priti for discount entry of Return
                    If FormId = "FreshSaleReturn" Then
                        If objTR.Amount_less_Discount = 0 AndAlso objTR.Discount > 0 Then
                            Dim AccDiscDR() As String = {objTR.GL_Account_Code, -1 * (objTR.Discount)}
                            ArryLst.Add(AccDiscDR)
                        End If
                    End If
                    ''''''code ends here
                Next
            Else '' New Part 
                For Each objTR As clsCustomerInvoiceDetailFarmer In obj.Arr
                    Dim dblLedgeerNonRecoverableAmt As Double = 0
                    ''richa agarwal 21/07/2015  debit/credit customer account in case of vcgl
                    If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                        Dim AccInvDR1() As String = {objTR.GL_Account_Code, objTR.Amount_less_Discount * -1}
                        ArryLst.Add(AccInvDR1)
                    Else
                        Dim AccInvDR() As String = {objTR.GL_Account_Code, objTR.Amount_less_Discount}
                        ArryLst.Add(AccInvDR)
                    End If
                    isFirstTime = False
                Next
            End If
            ''richa agarwal 24/11/2014

            If obj.RoundOffAmount <> 0 Then
                Dim strACRoundInvCr As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DefaultRoundOffGLAccount, clsFixedParameterCode.DefaultRoundOffGLAccount, trans))
                If clsCommon.myLen(strACRoundInvCr) <= 0 Then
                    Throw New Exception("Please set round off account in Sales Setting")
                End If
                strACRoundInvCr = clsERPFuncationality.ChangeGLAccountLocationSegment(strACRoundInvCr, obj.loc_code, True, trans)
                Dim AccRoundInvCR() As String = {strACRoundInvCr, obj.RoundOffAmount}
                ArryLst.Add(AccRoundInvCR)
            End If
            ''------------------------

            Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code='" + obj.Arr(0).GL_Account_Code + "'", trans))
            Dim strACWithLocation As String = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Customer_Control_AC, obj.loc_code, True, trans)

            '' Anubhooti 19-Mar-2015 (IF Entry is against VCGL then GL will get opposite(DR/CR))
            If clsCommon.myLen(obj.Against_VCGL) <= 0 Then 'AndAlso clsCommon.CompairString(obj.Trans_Type, "VC") = CompairStringResult.Equal
                ''richa agarwal 14/05/2015 BM00000006615 debit customer account in case of direct ar invoice which type of Invoice
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccInvCR1() As String = {strACWithLocation, obj.Document_Total}
                    ArryLst.Add(AccInvCR1)
                Else
                    ''richa agarwal add/subtract round off amount from customer amount
                    Dim AccInvCR() As String = {strACWithLocation, -1 * obj.Document_Total}
                    ' Dim AccInvCR() As String = {strACWithLocation, -1 * (obj.Document_Total - obj.RoundOffAmount)}
                    ArryLst.Add(AccInvCR)
                End If

            Else '' New Part
                ''richa agarwal 21/07/2015  debit/credit customer account in case of vcgl
                If clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    Dim AccInvCR1() As String = {strACWithLocation, obj.Document_Total}
                    ArryLst.Add(AccInvCR1)
                Else
                    Dim AccInvCR() As String = {strACWithLocation, -1 * obj.Document_Total}
                    ArryLst.Add(AccInvCR)
                End If
                'Dim AccInvCR() As String = {strACWithLocation, obj.Document_Total}
                'ArryLst.Add(AccInvCR)
                ''-----------------------------
            End If

            If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL'And Not IsAllowPurchaseAccounting
                '' FOR cogs START here
                If ((clsCommon.CompairString(obj.Document_Type, "C") = CompairStringResult.Equal)) Then
                    ' Dim objInv As clsSNInvoiceHead
                    Dim strFirstItem As String
                    Dim arr As New List(Of String)
                    ' Dim dblCogsCost As Double
                    Dim strInventoryControlAc As String
                    Dim strCogsAcct As String
                    Dim strInvNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Code from TSPL_sd_SALE_RETURN_DETAIL where DOCUMENT_CODE='" & obj.Against_Sale_Return_No & "'", trans))
                    strFirstItem = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Item_Code from TSPL_sd_SALE_RETURN_DETAIL where DOCUMENT_CODE='" & obj.Against_Sale_Return_No & "'", trans))

                    Dim strSql As String = "select inv.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from " & _
                        " ( select Item_Code,Source_Doc_No,Avg_Cost,FIFO_Cost,LIFO_Cost from TSPL_INVENTORY_MOVEMENT where TSPL_INVENTORY_MOVEMENT.Source_Doc_No='" & obj.Against_Sale_Return_No & "' " & _
                        " union all " & _
                        " select Item_Code,Source_Doc_No,Avg_Cost,FIFO_Cost,LIFO_Cost from TSPL_INVENTORY_MOVEMENT_NEW where TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No='" & obj.Against_Sale_Return_No & "' " & _
                        " ) inv " & _
                        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=inv.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where Source_Doc_No='" & obj.Against_Sale_Return_No & "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        For Each dr As DataRow In dt.Rows
                            strInventoryControlAc = clsDBFuncationality.getSingleValue("SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                            " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                            " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans)
                            strInventoryControlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControlAc, obj.loc_code, True, trans)

                            If clsCommon.myLen(strInventoryControlAc) = 0 Then
                                Throw New Exception("Please set Inventory Control Account for first item")
                            End If

                            strCogsAcct = clsDBFuncationality.getSingleValue("select Cost_Of_Goods_Sold from TSPL_ITEM_MASTER left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code=TSPL_ITEM_MASTER.Sale_Class_Code where Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans)
                            If clsCommon.myLen(strCogsAcct) = 0 Then
                                Throw New Exception("Please set Cost of Goods Sold for first item")
                            End If

                            ''change by rohit on 11/11/14 set parameter true for islicationsegment discussed by balwinder singh premi
                            strCogsAcct = clsERPFuncationality.ChangeGLAccountLocationSegment(strCogsAcct, obj.loc_code, True, trans)
                            '' change ends here
                            Dim Acc() As String = {strInventoryControlAc, clsCommon.myCdbl(dr("Cost"))}
                            ArryLst.Add(Acc)
                            Dim Acc1() As String = {strCogsAcct, -1 * clsCommon.myCdbl(dr("Cost"))}
                            ArryLst.Add(Acc1)
                        Next
                    End If
                End If
            End If
        Else
            Throw New Exception("Document is not implemented")
        End If
        If clsCommon.CompairString(FormId, "CSA-SALE") <> CompairStringResult.Equal Then
            strEntryDesc = strEntryDesc + obj.Document_No
        End If

        'If IsAllowPurchaseAccounting Then
        clsJournalMaster.FunGrnlEntryWithTrans(obj.loc_code, True, isForUnpostedTransaction, strVoucherNo, trans, obj.Document_Date, strEntryDesc, strSrcType, strSrcDesc, obj.Document_No, obj.Description, "C", obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", strRemarks, Nothing, coll)
        'End If

        'If (clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal) Then
        '    clsJournalMaster.FunGrnlEntryWithTrans(obj.loc_code, True, isForUnpostedTransaction, strVoucherNo, trans, obj.Document_Date, strEntryDesc, strSrcType, strSrcDesc, obj.Document_No, obj.Description, "C", obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", strRemarks, Nothing, coll)
        'ElseIf (clsCommon.CompairString(obj.Document_Type, "C") = CompairStringResult.Equal) Then
        '    Dim ArryLstNew As ArrayList = New ArrayList()
        '    For Each Str() As String In ArryLst
        '        Dim strNew() As String = {Str(0), 1 * Str(1)}
        '        ArryLstNew.Add(strNew)
        '    Next
        '    clsJournalMaster.FunGrnlEntryWithTrans(obj.loc_code, True, isForUnpostedTransaction, strVoucherNo, trans, obj.Document_Date, strEntryDesc, strSrcType, strSrcDesc, obj.Document_No, obj.Description, "C", obj.Farmer_Code, obj.Farmer_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstNew, "", strRemarks, Nothing, coll)
        'Else
        '    Throw New Exception("Invoice Type not found to Post")
        'End If
        
        Return True
    End Function
End Class
Public Class clsCustomerInvoiceDetailFarmer
#Region "Variables"
    Public SNo As Integer = 0
    Public Document_No As String = Nothing
    Public GL_Account_Code As String = Nothing
    Public GL_Account_Desc As String = Nothing
    Public Amount As Double = 0
    Public Discount_Per As Double = 0
    Public Discount As Double = 0
    Public Amount_less_Discount As String = Nothing
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public Total_Tax As Double = 0
    Public Total_Amount As Double = 0
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public TAX1_Base_Amt As Double = 0
    Public TAX2_Base_Amt As Double = 0
    Public TAX3_Base_Amt As Double = 0
    Public TAX4_Base_Amt As Double = 0
    Public TAX5_Base_Amt As Double = 0
    Public TAX6_Base_Amt As Double = 0
    Public TAX7_Base_Amt As Double = 0
    Public TAX8_Base_Amt As Double = 0
    Public TAX9_Base_Amt As Double = 0
    Public TAX10_Base_Amt As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsCustomerInvoiceDetailFarmer), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomerInvoiceDetailFarmer In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "GL_Account_Code", obj.GL_Account_Code)
                clsCommon.AddColumnsForChange(coll, "GL_Account_Desc", obj.GL_Account_Desc)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Discount_Per", obj.Discount_Per)
                clsCommon.AddColumnsForChange(coll, "Discount", obj.Discount)
                clsCommon.AddColumnsForChange(coll, "Amount_less_Discount", obj.Amount_less_Discount)
                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Tax", obj.Total_Tax)
                clsCommon.AddColumnsForChange(coll, "Total_Amount", obj.Total_Amount)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
                clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Customer_Invoice_Detail_Farmer", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class
Public Structure structMP
    Dim MP_Code As String
    Dim Doc_Date As Date
End Structure
