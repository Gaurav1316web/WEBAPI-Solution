Imports System.Data.SqlClient
Public Class scrapinvoicehead
#Region "Variables"
    Public ActualTCSBaseAmount As Double = 0
    Public ChangedTCSBaseAmount As Double = 0
    Public shipment_No As String = Nothing
    Public invoice_No As String = Nothing
    Public Status As String = Nothing
    Public Po_No As String = Nothing
    Public NRG_No As String = Nothing
    Public cust_Code As String = Nothing
    Public cust_Name As String = Nothing
    Public shipment_Date As String = Nothing
    Public posting_Date As String = Nothing
    Public expship_Date As String = Nothing
    Public Loc_Code As String = Nothing
    Public Loc_Name As String = Nothing
    Public ToLoc_Code As String = Nothing
    Public CreateInvoice As String = Nothing
    Public Description As String = Nothing
    Public reff As String = Nothing
    Public Tax_Group As String = Nothing
    Public Tax_Desc As String = Nothing
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
    Public Addcost As String = Nothing
    Public AddcostDesc As String = Nothing
    Public Add_Amt As Double = 0
    Public Before_Add_Amt As Double = 0
    Public After_Add_Amt As Double = 0
    Public Discount_Base As Double = 0
    Public Discount_Amt As Double = 0
    Public Amount_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public ship_Total_Amt As Double = 0
    Public doc_Amt As Double = 0
    Public Comp_Code As String = Nothing
    Public ispost As Integer = 0

    Public AddCode1 As String = Nothing
    Public AddDesc1 As String = Nothing
    Public AddAmt1 As Double = 0
    Public AddCode2 As String = Nothing
    Public AddDesc2 As String = Nothing
    Public AddAmt2 As Double = 0
    Public AddCode3 As String = Nothing
    Public AddDesc3 As String = Nothing
    Public AddAmt3 As Double = 0
    Public AddCode4 As String = Nothing
    Public AddDesc4 As String = Nothing
    Public AddAmt4 As Double = 0
    Public AddCode5 As String = Nothing
    Public AddDesc5 As String = Nothing
    Public AddAmt5 As Double = 0
    Public AddCode6 As String = Nothing
    Public AddDesc6 As String = Nothing
    Public AddAmt6 As Double = 0
    Public AddCode7 As String = Nothing
    Public AddDesc7 As String = Nothing
    Public AddAmt7 As Double = 0
    Public AddCode8 As String = Nothing
    Public AddDesc8 As String = Nothing
    Public AddAmt8 As Double = 0
    Public AddCode9 As String = Nothing
    Public AddDesc9 As String = Nothing
    Public AddAmt9 As Double = 0
    Public AddCode10 As String = Nothing
    Public AddDesc10 As String = Nothing
    Public AddAmt10 As Double = 0
    Public Terms_Code As String = Nothing
    Public Due_Date As String = Nothing
    Public BalanceAmt As Double = 0
    Public Inter_Branch As Boolean = False
    Public Specification As String = Nothing
    Public Invoice_Type As String = ""

    Public is_Asset_Type As Boolean = False
    'Public ispost As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public ArrIn As List(Of scrapinvoicedetail) = Nothing
    'Public Arr As List(Of ClsScrapSaleDetail) = Nothing
    Public Total_Gross_Weight As Double = 0
    Public Total_Net_Weight As Double = 0

    Public Doc_Type As String
    Public Is_Taxable As Boolean = False
    Public RoundOffAmount As Double = 0
    Public Vehicle_Id As String = Nothing
    Public Transporter_code As String = Nothing
    Public Vehicle_code As String = Nothing
    Public Freight_Distance As Integer = 0
#End Region

    Public Shared Function SaveDatainvoice(ByVal shipment As String, ByVal strScrapSaleInvoiceNo As String, ByVal trans As SqlTransaction, Optional ByVal strInvoiceType As String = "", Optional ByVal Arr As List(Of ClsScrapSaleDetail) = Nothing) As Boolean
        Try
            Dim obj As ClsScrapSaleHead = ClsScrapSaleHead.GetData(shipment, NavigatorType.Current, trans, False)
            Dim objin As scrapinvoicehead = New scrapinvoicehead()

            objin.shipment_No = obj.shipment_No
            objin.invoice_No = obj.invoice_No
            objin.Status = obj.Status
            objin.Po_No = obj.Po_No
            objin.NRG_No = obj.NRG_No
            objin.cust_Code = obj.cust_Code
            objin.cust_Name = obj.cust_Name
            objin.shipment_Date = obj.shipment_Date
            objin.posting_Date = obj.posting_Date
            objin.expship_Date = obj.expship_Date
            objin.Loc_Code = obj.Loc_Code
            objin.Loc_Name = obj.Loc_Name
            objin.ToLoc_Code = obj.ToLoc_Code
            objin.Specification = obj.Specification
            objin.CreateInvoice = obj.CreateInvoice
            objin.Description = obj.Description
            objin.reff = obj.reff
            objin.Tax_Group = obj.Tax_Group
            objin.Tax_Desc = obj.Tax_Desc
            objin.Add_Amt = obj.Add_Amt
            objin.Before_Add_Amt = obj.Before_Add_Amt
            objin.Discount_Base = obj.Discount_Base
            objin.Discount_Amt = obj.Discount_Amt
            objin.Amount_Less_Discount = obj.Amount_Less_Discount
            objin.Total_Tax_Amt = obj.Total_Tax_Amt
            objin.ship_Total_Amt = obj.ship_Total_Amt
            objin.doc_Amt = obj.doc_Amt
            objin.RoundOffAmount = obj.RoundOffAmount
            objin.Transporter_code = obj.Transporter_code
            objin.Vehicle_Id = obj.Vehicle_Id
            objin.Vehicle_code = obj.Vehicle_code
            objin.Freight_Distance = obj.Freight_Distance
            objin.ispost = obj.ispost
            objin.ActualTCSBaseAmount = obj.ActualTCSBaseAmount
            objin.ChangedTCSBaseAmount = obj.ChangedTCSBaseAmount
            objin.TAX1 = obj.TAX1
            objin.TAX1_Rate = obj.TAX1_Rate
            objin.TAX1_Base_Amt = obj.TAX1_Base_Amt
            objin.TAX1_Amt = obj.TAX1_Amt

            objin.TAX2 = obj.TAX2
            objin.TAX2_Rate = obj.TAX2_Rate
            objin.TAX2_Base_Amt = obj.TAX2_Base_Amt
            objin.TAX2_Amt = obj.TAX2_Amt

            objin.TAX3 = obj.TAX3
            objin.TAX3_Rate = obj.TAX3_Rate
            objin.TAX3_Base_Amt = obj.TAX3_Base_Amt
            objin.TAX3_Amt = obj.TAX3_Amt

            objin.TAX4 = obj.TAX4
            objin.TAX4_Rate = obj.TAX4_Rate
            objin.TAX4_Base_Amt = obj.TAX4_Base_Amt
            objin.TAX4_Amt = obj.TAX4_Amt

            objin.TAX5 = obj.TAX5
            objin.TAX5_Rate = obj.TAX5_Rate
            objin.TAX5_Base_Amt = obj.TAX5_Base_Amt
            objin.TAX5_Amt = obj.TAX5_Amt

            objin.TAX6 = obj.TAX6
            objin.TAX6_Rate = obj.TAX6_Rate
            objin.TAX6_Base_Amt = obj.TAX6_Base_Amt
            objin.TAX6_Amt = obj.TAX6_Amt

            objin.TAX7 = obj.TAX7
            objin.TAX7_Rate = obj.TAX7_Rate
            objin.TAX7_Base_Amt = obj.TAX7_Base_Amt
            objin.TAX7_Amt = obj.TAX7_Amt

            objin.TAX8 = obj.TAX8
            objin.TAX8_Rate = obj.TAX8_Rate
            objin.TAX8_Base_Amt = obj.TAX8_Base_Amt
            objin.TAX8_Amt = obj.TAX8_Amt

            objin.TAX9 = obj.TAX9
            objin.TAX9_Rate = obj.TAX9_Rate
            objin.TAX9_Base_Amt = obj.TAX9_Base_Amt
            objin.TAX9_Amt = obj.TAX9_Amt

            objin.TAX10 = obj.TAX10
            objin.TAX10_Rate = obj.TAX10_Rate
            objin.TAX10_Base_Amt = obj.TAX10_Base_Amt
            objin.TAX10_Amt = obj.TAX10_Amt

            objin.AddCode1 = obj.AddCode1
            objin.AddDesc1 = obj.AddDesc1
            objin.AddAmt1 = obj.AddAmt1
            objin.AddCode2 = obj.AddCode2
            objin.AddDesc2 = obj.AddDesc2
            objin.AddAmt2 = obj.AddAmt2
            objin.AddCode3 = obj.AddCode3
            objin.AddDesc3 = obj.AddDesc3
            objin.AddAmt3 = obj.AddAmt3
            objin.AddCode4 = obj.AddCode4
            objin.AddDesc4 = obj.AddDesc4
            objin.AddAmt4 = obj.AddAmt4
            objin.AddCode5 = obj.AddCode5
            objin.AddDesc5 = obj.AddDesc5
            objin.AddAmt5 = obj.AddAmt5
            objin.AddCode6 = obj.AddCode6
            objin.AddDesc6 = obj.AddDesc6
            objin.AddAmt6 = obj.AddAmt6
            objin.AddCode7 = obj.AddCode7
            objin.AddDesc7 = obj.AddDesc7
            objin.AddAmt7 = obj.AddAmt7
            objin.AddCode8 = obj.AddCode8
            objin.AddDesc8 = obj.AddDesc8
            objin.AddAmt8 = obj.AddAmt8
            objin.AddCode9 = obj.AddCode9
            objin.AddDesc9 = obj.AddDesc9
            objin.AddAmt9 = obj.AddAmt9
            objin.AddCode10 = obj.AddCode10
            objin.AddDesc10 = obj.AddDesc10
            objin.AddAmt10 = obj.AddAmt10
            objin.Due_Date = obj.Due_Date
            objin.Terms_Code = obj.Terms_Code
            objin.BalanceAmt = obj.doc_Amt
            objin.Inter_Branch = obj.Inter_Branch
            objin.is_Asset_Type = obj.is_Asset_Type

            objin.Total_Gross_Weight = obj.Total_Gross_Weight
            objin.Total_Net_Weight = obj.Total_Net_Weight
            objin.Doc_Type = obj.Doc_Type

            objin.Is_Taxable = obj.Is_Taxable
            objin.ArrIn = New List(Of scrapinvoicedetail)

            '-----------------------------------For Generating Scrap Sale Invoice No--------------------------------------
            Dim invoice As String = ""
            If clsCommon.myLen(strScrapSaleInvoiceNo) > 0 Then
                invoice = strScrapSaleInvoiceNo
            End If
            Dim custState As String = ""
            Dim custTin As String = ""
            Dim transtype As String = ""
            Dim isNewEntry As Boolean = False
            Dim qry As String = "select State,Tin_No,transaction_type  from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.cust_Code + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                custState = clsCommon.myCstr(dt.Rows(0)("State"))
                custTin = clsCommon.myCstr(dt.Rows(0)("Tin_No"))
                transtype = clsCommon.myCstr(dt.Rows(0)("transaction_type"))
            End If

            Dim locstate As String = clsDBFuncationality.getSingleValue("select state from tspl_location_master where location_code='" + obj.Loc_Code + "'", trans)
            ''richa agarwal 17/03/2015 sale invoice series generation setting based
            transtype = strInvoiceType
            Dim Desc As String = String.Empty

            If clsCommon.myLen(invoice) <= 0 Then
                isNewEntry = True
                Dim CreateCommonSeriesLocationwiseForAllSale As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateCommonSeriesLocationwiseForAllSale, clsFixedParameterCode.CreateCommonSeriesLocationwiseForAllSale, trans))
                If clsCommon.CompairString(obj.Is_CashSale, "Y") = CompairStringResult.Equal AndAlso CreateCommonSeriesLocationwiseForAllSale = 0 Then
                    If clsCommon.myLen(invoice) <= 0 Then
                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.CashSale, objin.Loc_Code)
                    End If
                Else
                    If clsCommon.CompairString(obj.Doc_Type, "J") = CompairStringResult.Equal Then
                        If clsCommon.myLen(invoice) <= 0 Then
                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.JobWorkInvoice, "", objin.Loc_Code)
                        End If
                    Else
                        If clsERPFuncationality.GetGSTStatus(obj.shipment_Date) Then
                            If CreateCommonSeriesLocationwiseForAllSale = 0 Then

                                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                                    If obj.Is_Taxable Then
                                        Dim strTaxType = clsLocationWiseTax.TaxType(obj.Loc_Code, obj.cust_Code, "S", obj.shipment_Date, trans)
                                        If clsCommon.CompairString(obj.Is_Scrap, "Y") = CompairStringResult.Equal Then
                                            If clsCommon.CompairString(strTaxType, "L") = CompairStringResult.Equal Then
                                                invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.GSTLocal, objin.Loc_Code)
                                            Else
                                                invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.GST_Interstate, objin.Loc_Code)
                                            End If
                                        Else
                                            If clsCommon.CompairString(strTaxType, "L") = CompairStringResult.Equal Then
                                                invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.GSTLocal, objin.Loc_Code)
                                            Else
                                                invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.GST_Interstate, objin.Loc_Code)
                                            End If
                                        End If
                                        objin.Invoice_Type = "Taxable"
                                    Else
                                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.GSTNonTaxable, objin.Loc_Code)
                                        objin.Invoice_Type = "Non Taxable"
                                    End If
                                Else
                                    If obj.Is_Taxable Then
                                        Dim strTaxType = clsLocationWiseTax.TaxType(obj.Loc_Code, obj.cust_Code, "S", obj.shipment_Date, trans)
                                        If clsCommon.CompairString(strTaxType, "L") = CompairStringResult.Equal Then
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.GSTLocal, objin.Loc_Code)
                                        Else
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.GSTInterstate, objin.Loc_Code)
                                        End If
                                        objin.Invoice_Type = "Taxable"
                                    Else
                                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.GSTNonTaxable, objin.Loc_Code)
                                        objin.Invoice_Type = "Non Taxable"
                                    End If
                                End If
                                ' common sale series start here
                            Else
                                If obj.Is_Taxable Then
                                    Dim intExempted As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Is_Tax_Exempted from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" & obj.Tax_Group & "'", trans))
                                    If intExempted = 0 Then
                                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.CommonSaleSeries, clsDocTransactionType.GSTTaxable, objin.Loc_Code)
                                    Else
                                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.CommonSaleSeries, clsDocTransactionType.GSTBillofSupply, objin.Loc_Code)
                                    End If
                                    objin.Invoice_Type = "Taxable"
                                Else
                                    invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.CommonSaleSeries, clsDocTransactionType.GSTBillofSupply, objin.Loc_Code)
                                    objin.Invoice_Type = "Non Taxable"
                                End If
                            End If
                            ' common sale series ends here
                        Else
                            If clsCommon.CompairString(obj.Excisable, "Y") = CompairStringResult.Equal Then
                                If clsCommon.myLen(invoice) <= 0 Then
                                    Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesExciseatMiscSale, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesExciseatMiscSale, trans)
                                    If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(obj.Is_Scrap, "Y") = CompairStringResult.Equal Then
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.SaleInvoiceExcise, objin.Loc_Code)
                                        Else
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceExcise, objin.Loc_Code)
                                        End If
                                    Else
                                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SaleInvoice, clsDocTransactionType.SaleInvoiceExcise, objin.Loc_Code)
                                    End If
                                    ' invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SaleInvoice, clsDocTransactionType.SaleInvoiceExcise, objin.Loc_Code)
                                End If
                                objin.Invoice_Type = "Excise"
                            ElseIf clsCommon.CompairString(transtype, "T") = CompairStringResult.Equal Then
                                'If clsCommon.myLen(custState) >= 0 And clsCommon.CompairString(custState, locstate) = CompairStringResult.Equal And clsCommon.myLen(custTin) > 0 Then


                                If clsCommon.myLen(custTin) <= 0 Then
                                    Throw New Exception("Please Insert Tin No. for Customer:" + obj.cust_Name)
                                End If
                                If clsCommon.myLen(invoice) <= 0 Then
                                    Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesTaxatMiscSale, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesTaxatMiscSale, trans)
                                    If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(obj.Is_Scrap, "Y") = CompairStringResult.Equal Then
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceTax, objin.Loc_Code)
                                        Else
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceTax, objin.Loc_Code)
                                        End If

                                    Else
                                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceTax, objin.Loc_Code)
                                    End If
                                    'invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceTax, objin.Loc_Code)
                                End If
                                objin.Invoice_Type = "Tax"
                            ElseIf clsCommon.CompairString(transtype, "R") = CompairStringResult.Equal Then
                                'If clsCommon.myLen(invoice) <= 0 Then
                                If clsCommon.myLen(invoice) <= 0 Then
                                    Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesRetailatMiscSale, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesRetailatMiscSale, trans)
                                    If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(obj.Is_Scrap, "Y") = CompairStringResult.Equal Then
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceRetail, objin.Loc_Code)
                                        Else
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceRetail, objin.Loc_Code)
                                        End If

                                    Else
                                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceRetail, objin.Loc_Code)
                                    End If
                                    'invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceRetail, objin.Loc_Code)
                                End If

                                objin.Invoice_Type = "Retail"

                            ElseIf clsCommon.CompairString(transtype, "I") = CompairStringResult.Equal Then
                                'If clsCommon.myLen(invoice) <= 0 Then
                                If clsCommon.myLen(invoice) <= 0 Then
                                    Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesRetailatMiscSale, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesRetailatMiscSale, trans)
                                    If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(obj.Is_Scrap, "Y") = CompairStringResult.Equal Then
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.SaleInvoiceInterstate, objin.Loc_Code)
                                        Else
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceInterstate, objin.Loc_Code)
                                        End If

                                    Else
                                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceRetail, objin.Loc_Code)
                                    End If
                                    'invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceRetail, objin.Loc_Code)
                                End If
                                objin.Invoice_Type = "Invoice"
                            End If
                        End If
                    End If
                End If

            End If


            '  Throw New Exception("Transaction Type not found for Customer:" + obj.cust_Name)




            '------------------------------------------------------------------------------------------------------------


            For Each objout As ClsScrapSaleDetail In obj.Arr
                Dim objtr As New scrapinvoicedetail()
                objtr.ItemwiseTaxCode = objout.ItemwiseTaxCode
                objtr.invoice_No = invoice
                objtr.Line_No = objout.Line_No
                objtr.Row_Type = objout.Row_Type
                objtr.Specification = objout.Specification
                objtr.Item_Code = objout.Item_Code
                objtr.Item_Desc = objout.Item_Desc
                objtr.Unit_Code = objout.Unit_Code
                objtr.shipped_Qty = objout.shipped_Qty

                objtr.FAT = objout.FAT
                objtr.SNF = objout.SNF

                objtr.price = objout.price
                objtr.DiscountPer = objout.DiscountPer
                objtr.DiscountAmt = objout.DiscountAmt
                objtr.NetPriceAmt = objout.NetPriceAmt
                objtr.ItemAmt = objout.ItemAmt
                objtr.TotalDiscountAmt = objout.TotalDiscountAmt
                objtr.TotalTaxAmt = objout.TotalTaxAmt
                objtr.ItemNetAmt = objout.ItemNetAmt
                objtr.TotalAmt = objout.TotalAmt
                objtr.TAX1 = objout.TAX1
                objtr.TAX1_Base_Amt = objout.TAX1_Base_Amt
                objtr.TAX1_Rate = objout.TAX1_Rate
                objtr.TAX1_Amt = objout.TAX1_Amt
                objtr.TAX2 = objout.TAX2
                objtr.TAX2_Base_Amt = objout.TAX2_Base_Amt
                objtr.TAX2_Rate = objout.TAX2_Rate
                objtr.TAX2_Amt = objout.TAX2_Amt
                objtr.TAX3 = objout.TAX3
                objtr.TAX3_Base_Amt = objout.TAX3_Base_Amt
                objtr.TAX3_Rate = objout.TAX3_Rate
                objtr.TAX3_Amt = objout.TAX3_Amt
                objtr.TAX4 = objout.TAX4
                objtr.TAX4_Base_Amt = objout.TAX4_Base_Amt
                objtr.TAX4_Rate = objout.TAX4_Rate
                objtr.TAX4_Amt = objout.TAX4_Amt
                objtr.TAX5 = objout.TAX5
                objtr.TAX5_Base_Amt = objout.TAX5_Base_Amt
                objtr.TAX5_Rate = objout.TAX5_Rate
                objtr.TAX5_Amt = objout.TAX5_Amt
                objtr.TAX6 = objout.TAX6
                objtr.TAX6_Base_Amt = objout.TAX6_Base_Amt
                objtr.TAX6_Rate = objout.TAX6_Rate
                objtr.TAX6_Amt = objout.TAX6_Amt
                objtr.TAX7 = objout.TAX7
                objtr.TAX7_Base_Amt = objout.TAX7_Base_Amt
                objtr.TAX7_Rate = objout.TAX7_Rate
                objtr.TAX7_Amt = objout.TAX7_Amt
                objtr.TAX8 = objout.TAX8
                objtr.TAX8_Base_Amt = objout.TAX8_Base_Amt
                objtr.TAX8_Rate = objout.TAX8_Rate
                objtr.TAX8_Amt = objout.TAX8_Amt
                objtr.TAX9 = objout.TAX9
                objtr.TAX9_Base_Amt = objout.TAX9_Base_Amt
                objtr.TAX9_Rate = objout.TAX9_Rate
                objtr.TAX9_Amt = objout.TAX9_Amt
                objtr.TAX10 = objout.TAX10
                objtr.TAX10_Base_Amt = objout.TAX10_Base_Amt
                objtr.TAX10_Rate = objout.TAX10_Rate
                objtr.TAX10_Amt = objout.TAX10_Amt
                objtr.Asset_Code = objout.Asset_Code
                If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                    For Each objq As ClsScrapSaleDetail In Arr
                        If clsCommon.CompairString(objq.Line_No, objtr.Line_No) = CompairStringResult.Equal Then
                            If clsCommon.CompairString(objq.Item_Code, objout.Item_Code) = CompairStringResult.Equal Then
                                objtr.arrBatchItem = (objq.arrBatchItem)
                            End If
                        End If

                    Next
                End If
                objin.ArrIn.Add(objtr)
            Next



            If (clsCommon.myLen(invoice) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "shipment_No", objin.shipment_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", objin.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "Status", objin.Status)
            clsCommon.AddColumnsForChange(coll, "Po_No", objin.Po_No)
            clsCommon.AddColumnsForChange(coll, "NRG_No", objin.NRG_No)
            clsCommon.AddColumnsForChange(coll, "cust_Code", objin.cust_Code)
            clsCommon.AddColumnsForChange(coll, "cust_Name", objin.cust_Name)
            clsCommon.AddColumnsForChange(coll, "shipment_Date", clsCommon.GetPrintDate(objin.shipment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "posting_Date", clsCommon.GetPrintDate(objin.posting_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "expship_Date", clsCommon.GetPrintDate(objin.expship_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", objin.Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Loc_Name", objin.Loc_Name)
            clsCommon.AddColumnsForChange(coll, "ToLoc_Code", objin.ToLoc_Code)
            clsCommon.AddColumnsForChange(coll, "CreateInvoice", objin.CreateInvoice)
            clsCommon.AddColumnsForChange(coll, "Description", objin.Description)
            clsCommon.AddColumnsForChange(coll, "reff", objin.reff)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", objin.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Tax_Desc", objin.Tax_Desc)
            clsCommon.AddColumnsForChange(coll, "Add_Amt", objin.Add_Amt)
            clsCommon.AddColumnsForChange(coll, "Before_Add_Amt", objin.Before_Add_Amt)
            clsCommon.AddColumnsForChange(coll, "Discount_Base", objin.Discount_Base)
            clsCommon.AddColumnsForChange(coll, "Discount_Amt", objin.Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", objin.Amount_Less_Discount)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", objin.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "ship_Total_Amt", objin.ship_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "doc_Amt", objin.doc_Amt)
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", objin.RoundOffAmount)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Id", objin.Vehicle_Id)
            clsCommon.AddColumnsForChange(coll, "Vehicle_code", objin.Vehicle_code)
            clsCommon.AddColumnsForChange(coll, "Freight_Distance", objin.Freight_Distance)
            clsCommon.AddColumnsForChange(coll, "Transport_code", objin.Transporter_code)
            clsCommon.AddColumnsForChange(coll, "ispost", 0)
            clsCommon.AddColumnsForChange(coll, "TAX1", objin.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", objin.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", objin.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", objin.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", objin.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", objin.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", objin.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", objin.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", objin.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", objin.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", objin.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", objin.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", objin.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", objin.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", objin.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", objin.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", objin.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", objin.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", objin.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", objin.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6", objin.TAX6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", objin.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", objin.TAX6_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6_Amt", objin.TAX6_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7", objin.TAX7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", objin.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", objin.TAX7_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7_Amt", objin.TAX7_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8", objin.TAX8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", objin.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", objin.TAX8_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8_Amt", objin.TAX8_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9", objin.TAX9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", objin.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", objin.TAX9_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9_Amt", objin.TAX9_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10", objin.TAX10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", objin.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", objin.TAX10_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10_Amt", objin.TAX10_Amt)
            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "AddCode1", objin.AddCode1)
            clsCommon.AddColumnsForChange(coll, "AddDesc1", objin.AddDesc1)
            clsCommon.AddColumnsForChange(coll, "AddAmt1", objin.AddAmt1)
            clsCommon.AddColumnsForChange(coll, "AddCode2", objin.AddCode2)
            clsCommon.AddColumnsForChange(coll, "AddDesc2", objin.AddDesc2)
            clsCommon.AddColumnsForChange(coll, "AddAmt2", objin.AddAmt2)
            clsCommon.AddColumnsForChange(coll, "AddCode3", objin.AddCode3)
            clsCommon.AddColumnsForChange(coll, "AddDesc3", objin.AddDesc3)
            clsCommon.AddColumnsForChange(coll, "AddAmt3", objin.AddAmt3)
            clsCommon.AddColumnsForChange(coll, "AddCode4", objin.AddCode4)
            clsCommon.AddColumnsForChange(coll, "AddDesc4", objin.AddDesc4)
            clsCommon.AddColumnsForChange(coll, "AddAmt4", objin.AddAmt4)
            clsCommon.AddColumnsForChange(coll, "AddCode5", objin.AddCode5)
            clsCommon.AddColumnsForChange(coll, "AddDesc5", objin.AddDesc5)
            clsCommon.AddColumnsForChange(coll, "AddAmt5", objin.AddAmt5)
            clsCommon.AddColumnsForChange(coll, "AddCode6", objin.AddCode6)
            clsCommon.AddColumnsForChange(coll, "AddDesc6", objin.AddDesc6)
            clsCommon.AddColumnsForChange(coll, "AddAmt6", objin.AddAmt6)
            clsCommon.AddColumnsForChange(coll, "AddCode7", objin.AddCode7)
            clsCommon.AddColumnsForChange(coll, "AddDesc7", objin.AddDesc7)
            clsCommon.AddColumnsForChange(coll, "AddAmt7", objin.AddAmt7)
            clsCommon.AddColumnsForChange(coll, "AddCode8", objin.AddCode8)
            clsCommon.AddColumnsForChange(coll, "AddDesc8", objin.AddDesc8)
            clsCommon.AddColumnsForChange(coll, "AddAmt8", objin.AddAmt8)
            clsCommon.AddColumnsForChange(coll, "AddCode9", objin.AddCode9)
            clsCommon.AddColumnsForChange(coll, "AddDesc9", objin.AddDesc9)
            clsCommon.AddColumnsForChange(coll, "AddAmt9", objin.AddAmt9)
            clsCommon.AddColumnsForChange(coll, "AddCode10", objin.AddCode10)
            clsCommon.AddColumnsForChange(coll, "AddDesc10", objin.AddDesc10)
            clsCommon.AddColumnsForChange(coll, "AddAmt10", objin.AddAmt10)
            clsCommon.AddColumnsForChange(coll, "Balance_Amt", objin.BalanceAmt)
            clsCommon.AddColumnsForChange(coll, "Inter_Branch", IIf(objin.Inter_Branch = True, 1, 0))

            If clsCommon.myLen(objin.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(objin.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Terms_Code", objin.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "invoice_No", invoice)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Invoice_Type", objin.Invoice_Type)
            clsCommon.AddColumnsForChange(coll, "is_Asset_Type", IIf(obj.is_Asset_Type, 1, 0))

            clsCommon.AddColumnsForChange(coll, "Total_Gross_Weight", objin.Total_Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Total_Net_Weight", objin.Total_Net_Weight)
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", IIf(objin.Is_Taxable, 1, 0))
            If isNewEntry = True Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPINVOICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPINVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_SCRAPINVOICE_HEAD.invoice_No='" + invoice + "'", trans)
            End If

            scrapinvoicedetail.SaveDatainvoice(invoice, objin.shipment_No, objin.ArrIn, objin.shipment_Date, objin.Loc_Code, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostDataInvoice(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Invoice No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

            Dim obj As ClsScrapInvoiceHead = ClsScrapInvoiceHead.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.invoice_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.ispost = 1) Then
                Throw New Exception("Already Post on :" + obj.posting_Date)
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Invoice No " + obj.invoice_No + " Is On Hold.Can't Post it")
            End If

            Dim qry As String = ""
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim IsRejectedItemFound As Boolean = False
            Dim totDrAmt As Double = 0
            Dim totCrAmt As Double = 0


            Dim strcust As String = " SELECT TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct as CustControlAcc FROM  TSPL_CUSTOMER_MASTER left outer JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_MASTER.Cust_Account = TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account where TSPL_CUSTOMER_MASTER.Cust_Code='" + obj.cust_Code + "'  "
            Dim strCustContAcc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strcust, trans))
            If clsCommon.myLen(strCustContAcc) <= 0 Then
                Throw New Exception("Please set Customer  Control Account for  Customer  Code")
            End If
            strCustContAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strCustContAcc, obj.Loc_Code, trans)
            Dim AccCust() As String = {strCustContAcc, obj.doc_Amt}
            ArryLstGLAC.Add(AccCust)
            totDrAmt = totDrAmt + (obj.doc_Amt)

            ' Dim addcost As String = "select account_code from TSPL_Additional_Charges where code='"+obj.+"'"


            If (clsCommon.myLen(obj.AddCode1) > 0) Then
                Dim Add1Acc As String = ClsScrapInvoiceHead.GetaddtionalAcc(obj.AddCode1, trans)
                Add1Acc = clsERPFuncationality.ChangeGLAccountLocationSegment(Add1Acc, obj.Loc_Code, trans)
                Dim Add1AccCr() As String = {Add1Acc, -1 * obj.AddAmt1}
                ArryLstGLAC.Add(Add1AccCr)
                totCrAmt = totCrAmt + (-1 * obj.AddAmt1)
            End If
            If (clsCommon.myLen(obj.AddCode2) > 0) Then
                Dim Add2Acc As String = ClsScrapInvoiceHead.GetaddtionalAcc(obj.AddCode2, trans)
                Add2Acc = clsERPFuncationality.ChangeGLAccountLocationSegment(Add2Acc, obj.Loc_Code, trans)
                Dim Add2AccCr() As String = {Add2Acc, -1 * obj.AddAmt2}
                ArryLstGLAC.Add(Add2AccCr)
                totCrAmt = totCrAmt + (-1 * obj.AddAmt2)
            End If
            If (clsCommon.myLen(obj.AddCode3) > 0) Then
                Dim Add3Acc As String = ClsScrapInvoiceHead.GetaddtionalAcc(obj.AddCode3, trans)
                Add3Acc = clsERPFuncationality.ChangeGLAccountLocationSegment(Add3Acc, obj.Loc_Code, trans)
                Dim Add3AccCr() As String = {Add3Acc, -1 * obj.AddAmt3}
                ArryLstGLAC.Add(Add3AccCr)
                totCrAmt = totCrAmt + (-1 * obj.AddAmt3)
            End If
            If (clsCommon.myLen(obj.AddCode4) > 0) Then
                Dim Add4Acc As String = ClsScrapInvoiceHead.GetaddtionalAcc(obj.AddCode4, trans)
                Add4Acc = clsERPFuncationality.ChangeGLAccountLocationSegment(Add4Acc, obj.Loc_Code, trans)
                Dim Add4AccCr() As String = {Add4Acc, -1 * obj.AddAmt4}
                ArryLstGLAC.Add(Add4AccCr)
                totCrAmt = totCrAmt + (-1 * obj.AddAmt4)
            End If
            If (clsCommon.myLen(obj.AddCode5) > 0) Then
                Dim Add5Acc As String = ClsScrapInvoiceHead.GetaddtionalAcc(obj.AddCode5, trans)
                Add5Acc = clsERPFuncationality.ChangeGLAccountLocationSegment(Add5Acc, obj.Loc_Code, trans)
                Dim Add5AccCr() As String = {Add5Acc, -1 * obj.AddAmt5}
                ArryLstGLAC.Add(Add5AccCr)
                totCrAmt = totCrAmt + (-1 * obj.AddAmt5)
            End If
            If (clsCommon.myLen(obj.AddCode6) > 0) Then
                Dim Add6Acc As String = ClsScrapInvoiceHead.GetaddtionalAcc(obj.AddCode6, trans)
                Add6Acc = clsERPFuncationality.ChangeGLAccountLocationSegment(Add6Acc, obj.Loc_Code, trans)
                Dim Add6AccCr() As String = {Add6Acc, -1 * obj.AddAmt6}
                ArryLstGLAC.Add(Add6AccCr)
                totCrAmt = totCrAmt + (-1 * obj.AddAmt6)
            End If
            If (clsCommon.myLen(obj.AddCode7) > 0) Then
                Dim Add7Acc As String = ClsScrapInvoiceHead.GetaddtionalAcc(obj.AddCode7, trans)
                Add7Acc = clsERPFuncationality.ChangeGLAccountLocationSegment(Add7Acc, obj.Loc_Code, trans)
                Dim Add7AccCr() As String = {Add7Acc, -1 * obj.AddAmt7}
                ArryLstGLAC.Add(Add7AccCr)
                totCrAmt = totCrAmt + (-1 * obj.AddAmt7)
            End If
            If (clsCommon.myLen(obj.AddCode8) > 0) Then
                Dim Add8Acc As String = ClsScrapInvoiceHead.GetaddtionalAcc(obj.AddCode8, trans)
                Add8Acc = clsERPFuncationality.ChangeGLAccountLocationSegment(Add8Acc, obj.Loc_Code, trans)
                Dim Add8AccCr() As String = {Add8Acc, -1 * obj.AddAmt8}
                ArryLstGLAC.Add(Add8AccCr)
                totCrAmt = totCrAmt + (-1 * obj.AddAmt8)
            End If
            If (clsCommon.myLen(obj.AddCode9) > 0) Then
                Dim Add9Acc As String = ClsScrapInvoiceHead.GetaddtionalAcc(obj.AddCode9, trans)
                Add9Acc = clsERPFuncationality.ChangeGLAccountLocationSegment(Add9Acc, obj.Loc_Code, trans)
                Dim Add9AccCr() As String = {Add9Acc, -1 * obj.AddAmt9}
                ArryLstGLAC.Add(Add9AccCr)
                totCrAmt = totCrAmt + (-1 * obj.AddAmt9)
            End If
            If (clsCommon.myLen(obj.AddCode10) > 0) Then
                Dim Add10Acc As String = ClsScrapInvoiceHead.GetaddtionalAcc(obj.AddCode10, trans)
                Add10Acc = clsERPFuncationality.ChangeGLAccountLocationSegment(Add10Acc, obj.Loc_Code, trans)
                Dim Add10AccCr() As String = {Add10Acc, -1 * obj.AddAmt10}
                ArryLstGLAC.Add(Add10AccCr)
                totCrAmt = totCrAmt + (-1 * obj.AddAmt10)
            End If

            For Each objTr As ClsScrapInvoiceDetail In obj.Arr
                qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Shipment_Clearing from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + objTr.Item_Code + "(" + objTr.Item_Desc + ")")
                End If

                Dim strqry As String = "select  convert(decimal(18,2),sum(case when  Item_Qty =0 or Amount=0 then 0 else  (Amount/Item_Qty )end)) as cost  from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.Loc_Code + "'"
                Dim itemcost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strqry, trans))

                Dim strshippingCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Shipment_Clearing"))
                If clsCommon.myLen(strshippingCtrlAC) <= 0 Then
                    Throw New Exception("Please set Shipment Clearing Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_Code + "(" + objTr.Item_Desc + ") ")
                End If
                strshippingCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strshippingCtrlAC, obj.Loc_Code, trans)
                Dim AccCr() As String = {strshippingCtrlAC, -1 * (itemcost * objTr.shipped_Qty)}
                ArryLstGLAC.Add(AccCr)
                totCrAmt = totCrAmt + (-1 * (itemcost * objTr.shipped_Qty))

                Dim AccSetQry As String = "SELECT TSPL_SALES_ACCOUNTS.Sales_Account, TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold,TSPL_SALES_ACCOUNTS.Cogs_InterBranch FROM TSPL_ITEM_MASTER INNER JOIN  TSPL_SALES_ACCOUNTS ON TSPL_ITEM_MASTER.Sale_Class_Code = TSPL_SALES_ACCOUNTS.Sales_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "' "
                Dim dr As DataTable

                dr = clsDBFuncationality.GetDataTable(AccSetQry)
                Dim SaleAcc As String = ""
                Dim CostOfGood As String = ""
                Dim strInterBranchCogs As String = ""

                If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    SaleAcc = dr.Rows(0)(0).ToString()
                    CostOfGood = dr.Rows(0)(1).ToString()
                    strInterBranchCogs = clsCommon.myCstr(dr.Rows(0)("Cogs_InterBranch"))
                End If


                If clsCommon.myLen(SaleAcc) <= 0 Then
                    Throw New Exception("Please set Sale Account Code ")
                End If
                If clsCommon.myLen(CostOfGood) <= 0 Then
                    Throw New Exception("Please set Cost of Good Account Code ")
                End If
                If obj.Inter_Branch AndAlso clsCommon.myLen(strInterBranchCogs) <= 0 Then
                    Throw New Exception("Please set Cogs Inter Branch Code of Sale Account Set")
                End If

                SaleAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(SaleAcc, obj.Loc_Code, trans)
                Dim AccSalCr() As String = {SaleAcc, -1 * (objTr.ItemNetAmt)}
                ArryLstGLAC.Add(AccSalCr)
                totCrAmt = totCrAmt + (-1 * (objTr.ItemNetAmt))

                If obj.Inter_Branch Then
                    strInterBranchCogs = clsERPFuncationality.ChangeGLAccountLocationSegment(strInterBranchCogs, obj.Loc_Code, trans)
                    Dim AccCostCr() As String = {strInterBranchCogs, itemcost * objTr.shipped_Qty}
                    ArryLstGLAC.Add(AccCostCr)
                    totDrAmt = totDrAmt + (itemcost * objTr.shipped_Qty)
                Else
                    CostOfGood = clsERPFuncationality.ChangeGLAccountLocationSegment(CostOfGood, obj.Loc_Code, trans)
                    Dim AccCostCr() As String = {CostOfGood, itemcost * objTr.shipped_Qty}
                    ArryLstGLAC.Add(AccCostCr)
                    totDrAmt = totDrAmt + (itemcost * objTr.shipped_Qty)
                End If

                If (clsCommon.myLen(objTr.TAX1) > 0) Then
                    Dim tax1acc As String = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX1, trans)
                    tax1acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax1acc, obj.Loc_Code, trans)
                    Dim tax1accCr() As String = {tax1acc, -1 * objTr.TAX1_Amt}
                    ArryLstGLAC.Add(tax1accCr)
                    totCrAmt = totCrAmt + (-1 * objTr.TAX1_Amt)
                End If

                If (clsCommon.myLen(objTr.TAX2) > 0) Then
                    Dim tax2acc As String = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX1, trans)
                    tax2acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax2acc, obj.Loc_Code, trans)
                    Dim tax2accCr() As String = {tax2acc, -1 * objTr.TAX2_Amt}
                    ArryLstGLAC.Add(tax2accCr)
                    totCrAmt = totCrAmt + (-1 * objTr.TAX2_Amt)
                End If
                If (clsCommon.myLen(objTr.TAX3) > 0) Then
                    Dim tax3acc As String = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX3, trans)
                    tax3acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax3acc, obj.Loc_Code, trans)
                    Dim tax3accCr() As String = {tax3acc, -1 * objTr.TAX3_Amt}
                    ArryLstGLAC.Add(tax3accCr)
                    totCrAmt = totCrAmt + (-1 * objTr.TAX3_Amt)
                End If
                If (clsCommon.myLen(objTr.TAX4) > 0) Then
                    Dim tax4acc As String = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX4, trans)
                    tax4acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax4acc, obj.Loc_Code, trans)
                    Dim tax4accCr() As String = {tax4acc, -1 * objTr.TAX4_Amt}
                    ArryLstGLAC.Add(tax4accCr)
                    totCrAmt = totCrAmt + (-1 * objTr.TAX4_Amt)
                End If
                If (clsCommon.myLen(objTr.TAX5) > 0) Then
                    Dim tax5acc As String = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX5, trans)
                    tax5acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax5acc, obj.Loc_Code, trans)
                    Dim tax5accCr() As String = {tax5acc, -1 * objTr.TAX5_Amt}
                    ArryLstGLAC.Add(tax5accCr)
                    totCrAmt = totCrAmt + (-1 * objTr.TAX5_Amt)
                End If
                If (clsCommon.myLen(objTr.TAX6) > 0) Then
                    Dim tax6acc As String = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX6, trans)
                    tax6acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax6acc, obj.Loc_Code, trans)
                    Dim tax6accCr() As String = {tax6acc, -1 * objTr.TAX6_Amt}
                    ArryLstGLAC.Add(tax6accCr)
                    totCrAmt = totCrAmt + (-1 * objTr.TAX6_Amt)
                End If
                If (clsCommon.myLen(objTr.TAX7) > 0) Then
                    Dim tax7acc As String = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX7, trans)
                    tax7acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax7acc, obj.Loc_Code, trans)
                    Dim tax7accCr() As String = {tax7acc, -1 * objTr.TAX7_Amt}
                    ArryLstGLAC.Add(tax7accCr)
                    totCrAmt = totCrAmt + (-1 * objTr.TAX7_Amt)
                End If
                If (clsCommon.myLen(objTr.TAX8) > 0) Then
                    Dim tax8acc As String = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX8, trans)
                    tax8acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax8acc, obj.Loc_Code, trans)
                    Dim tax8accCr() As String = {tax8acc, -1 * objTr.TAX8_Amt}
                    ArryLstGLAC.Add(tax8accCr)
                    totCrAmt = totCrAmt + (-1 * objTr.TAX8_Amt)
                End If
                If (clsCommon.myLen(objTr.TAX9) > 0) Then
                    Dim tax9acc As String = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX9, trans)
                    tax9acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax9acc, obj.Loc_Code, trans)
                    Dim tax9accCr() As String = {tax9acc, -1 * objTr.TAX9_Amt}
                    ArryLstGLAC.Add(tax9accCr)
                    totCrAmt = totCrAmt + (-1 * objTr.TAX9_Amt)
                End If
                If (clsCommon.myLen(objTr.TAX10) > 0) Then
                    Dim tax10acc As String = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX10, trans)
                    tax10acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax10acc, obj.Loc_Code, trans)
                    Dim tax10accCr() As String = {tax10acc, -1 * objTr.TAX10_Amt}
                    ArryLstGLAC.Add(tax10accCr)
                    totCrAmt = totCrAmt + (-1 * objTr.TAX10_Amt)
                End If
                Dim qryupdatepen As String = "update TSPL_SCRAPSALE_DETAIL set pending_qty='" + clsCommon.myCstr(objTr.pending_Qty) + "' where shipment_no='" + obj.shipment_No + "'  and item_code='" + objTr.Item_Code + "' "
                clsDBFuncationality.ExecuteNonQuery(qryupdatepen, trans)
            Next

            If Math.Abs(Math.Round(totDrAmt, 2)) <> Math.Abs(Math.Round(totCrAmt, 2)) Then
                Throw New Exception("Error in Posting: Total Debit Amount:" + clsCommon.myCstr(Math.Abs(totDrAmt)) + " and Total Credit Amount: " + clsCommon.myCstr(Math.Abs(totCrAmt)) + "")
            End If

            transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, strPostDate, "Against Scrap Sale " + obj.invoice_No, "SD-IN", "Scrap Invoice", obj.invoice_No, obj.Description, "C", obj.cust_Code, obj.cust_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
            Dim strRMDANo As String = ""
            qry = "Update TSPL_SCRAPINVOICE_HEAD set ispost=1, Posting_Date='" + clsCommon.GetPrintDate(strPostDate, "dd/MMM/yyyy ") + "',Modify_By='" + objCommonVar.CurrentUserCode + "'"
            qry += " where invoice_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveDatainvoiceReturn(ByVal shipment As String, ByVal strScrapSaleInvoiceNo As String, ByVal trans As SqlTransaction, Optional ByVal strInvoiceType As String = "", Optional ByVal Arr As List(Of ClsScrapSaleDetailReturn) = Nothing) As Boolean
        Try
            Dim obj As ClsScrapSaleHeadReturn = ClsScrapSaleHeadReturn.GetData(shipment, NavigatorType.Current, trans, False)
            Dim objin As scrapinvoicehead = New scrapinvoicehead()

            objin.shipment_No = obj.Document_No
            objin.invoice_No = obj.invoice_No
            objin.Status = obj.Status
            objin.Po_No = obj.Po_No
            objin.NRG_No = obj.NRG_No
            objin.cust_Code = obj.cust_Code
            objin.cust_Name = obj.cust_Name
            objin.shipment_Date = obj.shipment_Date
            objin.posting_Date = obj.posting_Date
            objin.expship_Date = obj.expship_Date
            objin.Loc_Code = obj.Loc_Code
            objin.Loc_Name = obj.Loc_Name
            objin.ToLoc_Code = obj.ToLoc_Code
            objin.Specification = obj.Specification
            objin.CreateInvoice = obj.CreateInvoice
            objin.Description = obj.Description
            objin.reff = obj.reff
            objin.Tax_Group = obj.Tax_Group
            objin.Tax_Desc = obj.Tax_Desc
            objin.Add_Amt = obj.Add_Amt
            objin.Before_Add_Amt = obj.Before_Add_Amt
            objin.Discount_Base = obj.Discount_Base
            objin.Discount_Amt = obj.Discount_Amt
            objin.Amount_Less_Discount = obj.Amount_Less_Discount
            objin.Total_Tax_Amt = obj.Total_Tax_Amt
            objin.ship_Total_Amt = obj.ship_Total_Amt
            objin.doc_Amt = obj.doc_Amt
            objin.RoundOffAmount = obj.RoundOffAmount

            objin.ispost = obj.ispost
            objin.TAX1 = obj.TAX1
            objin.TAX1_Rate = obj.TAX1_Rate
            objin.TAX1_Base_Amt = obj.TAX1_Base_Amt
            objin.TAX1_Amt = obj.TAX1_Amt

            objin.TAX2 = obj.TAX2
            objin.TAX2_Rate = obj.TAX2_Rate
            objin.TAX2_Base_Amt = obj.TAX2_Base_Amt
            objin.TAX2_Amt = obj.TAX2_Amt

            objin.TAX3 = obj.TAX3
            objin.TAX3_Rate = obj.TAX3_Rate
            objin.TAX3_Base_Amt = obj.TAX3_Base_Amt
            objin.TAX3_Amt = obj.TAX3_Amt

            objin.TAX4 = obj.TAX4
            objin.TAX4_Rate = obj.TAX4_Rate
            objin.TAX4_Base_Amt = obj.TAX4_Base_Amt
            objin.TAX4_Amt = obj.TAX4_Amt

            objin.TAX5 = obj.TAX5
            objin.TAX5_Rate = obj.TAX5_Rate
            objin.TAX5_Base_Amt = obj.TAX5_Base_Amt
            objin.TAX5_Amt = obj.TAX5_Amt

            objin.TAX6 = obj.TAX6
            objin.TAX6_Rate = obj.TAX6_Rate
            objin.TAX6_Base_Amt = obj.TAX6_Base_Amt
            objin.TAX6_Amt = obj.TAX6_Amt

            objin.TAX7 = obj.TAX7
            objin.TAX7_Rate = obj.TAX7_Rate
            objin.TAX7_Base_Amt = obj.TAX7_Base_Amt
            objin.TAX7_Amt = obj.TAX7_Amt

            objin.TAX8 = obj.TAX8
            objin.TAX8_Rate = obj.TAX8_Rate
            objin.TAX8_Base_Amt = obj.TAX8_Base_Amt
            objin.TAX8_Amt = obj.TAX8_Amt

            objin.TAX9 = obj.TAX9
            objin.TAX9_Rate = obj.TAX9_Rate
            objin.TAX9_Base_Amt = obj.TAX9_Base_Amt
            objin.TAX9_Amt = obj.TAX9_Amt

            objin.TAX10 = obj.TAX10
            objin.TAX10_Rate = obj.TAX10_Rate
            objin.TAX10_Base_Amt = obj.TAX10_Base_Amt
            objin.TAX10_Amt = obj.TAX10_Amt

            objin.AddCode1 = obj.AddCode1
            objin.AddDesc1 = obj.AddDesc1
            objin.AddAmt1 = obj.AddAmt1
            objin.AddCode2 = obj.AddCode2
            objin.AddDesc2 = obj.AddDesc2
            objin.AddAmt2 = obj.AddAmt2
            objin.AddCode3 = obj.AddCode3
            objin.AddDesc3 = obj.AddDesc3
            objin.AddAmt3 = obj.AddAmt3
            objin.AddCode4 = obj.AddCode4
            objin.AddDesc4 = obj.AddDesc4
            objin.AddAmt4 = obj.AddAmt4
            objin.AddCode5 = obj.AddCode5
            objin.AddDesc5 = obj.AddDesc5
            objin.AddAmt5 = obj.AddAmt5
            objin.AddCode6 = obj.AddCode6
            objin.AddDesc6 = obj.AddDesc6
            objin.AddAmt6 = obj.AddAmt6
            objin.AddCode7 = obj.AddCode7
            objin.AddDesc7 = obj.AddDesc7
            objin.AddAmt7 = obj.AddAmt7
            objin.AddCode8 = obj.AddCode8
            objin.AddDesc8 = obj.AddDesc8
            objin.AddAmt8 = obj.AddAmt8
            objin.AddCode9 = obj.AddCode9
            objin.AddDesc9 = obj.AddDesc9
            objin.AddAmt9 = obj.AddAmt9
            objin.AddCode10 = obj.AddCode10
            objin.AddDesc10 = obj.AddDesc10
            objin.AddAmt10 = obj.AddAmt10
            objin.Due_Date = obj.Due_Date
            objin.Terms_Code = obj.Terms_Code
            objin.BalanceAmt = obj.doc_Amt
            objin.Inter_Branch = obj.Inter_Branch
            objin.is_Asset_Type = obj.is_Asset_Type

            objin.Total_Gross_Weight = obj.Total_Gross_Weight
            objin.Total_Net_Weight = obj.Total_Net_Weight
            objin.Doc_Type = obj.Doc_Type

            objin.Is_Taxable = obj.Is_Taxable
            objin.ArrIn = New List(Of scrapinvoicedetail)

            '-----------------------------------For Generating Scrap Sale Invoice No--------------------------------------
            Dim invoice As String = ""
            If clsCommon.myLen(strScrapSaleInvoiceNo) > 0 Then
                invoice = strScrapSaleInvoiceNo
            End If
            Dim custState As String = ""
            Dim custTin As String = ""
            Dim transtype As String = ""
            Dim isNewEntry As Boolean = False
            Dim qry As String = "select State,Tin_No,transaction_type  from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.cust_Code + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                custState = clsCommon.myCstr(dt.Rows(0)("State"))
                custTin = clsCommon.myCstr(dt.Rows(0)("Tin_No"))
                transtype = clsCommon.myCstr(dt.Rows(0)("transaction_type"))
            End If

            Dim locstate As String = clsDBFuncationality.getSingleValue("select state from tspl_location_master where location_code='" + obj.Loc_Code + "'", trans)
            ''richa agarwal 17/03/2015 sale invoice series generation setting based
            transtype = strInvoiceType
            Dim Desc As String = String.Empty


            If clsCommon.myLen(invoice) <= 0 Then
                isNewEntry = True

                If clsCommon.CompairString(obj.Is_CashSale, "Y") = CompairStringResult.Equal Then
                    If clsCommon.myLen(invoice) <= 0 Then
                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.CashSale, objin.Loc_Code)
                    End If
                Else
                    If clsCommon.CompairString(obj.Doc_Type, "J") = CompairStringResult.Equal Then
                        If clsCommon.myLen(invoice) <= 0 Then
                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.JobWorkInvoice, "", objin.Loc_Code)
                        End If
                    Else
                        If clsERPFuncationality.GetGSTStatus(obj.shipment_Date) Then
                            If obj.Is_Taxable Then
                                Dim strTaxType = clsLocationWiseTax.TaxType(obj.Loc_Code, obj.cust_Code, "S", obj.shipment_Date, trans)
                                If clsCommon.CompairString(strTaxType, "L") = CompairStringResult.Equal Then
                                    invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.GSTLocal, objin.Loc_Code)
                                Else
                                    invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.GSTInterstate, objin.Loc_Code)
                                End If
                                objin.Invoice_Type = "Taxable"
                            Else
                                invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.GSTNonTaxable, objin.Loc_Code)
                                objin.Invoice_Type = "Non Taxable"
                            End If
                        Else
                            If clsCommon.CompairString(obj.Excisable, "Y") = CompairStringResult.Equal Then
                                If clsCommon.myLen(invoice) <= 0 Then
                                    Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesExciseatMiscSale, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesExciseatMiscSale, trans)
                                    If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(obj.Is_Scrap, "Y") = CompairStringResult.Equal Then
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.SaleInvoiceExcise, objin.Loc_Code)
                                        Else
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceExcise, objin.Loc_Code)
                                        End If
                                    Else
                                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SaleInvoice, clsDocTransactionType.SaleInvoiceExcise, objin.Loc_Code)
                                    End If
                                    ' invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SaleInvoice, clsDocTransactionType.SaleInvoiceExcise, objin.Loc_Code)
                                End If
                                objin.Invoice_Type = "Excise"
                            ElseIf clsCommon.CompairString(transtype, "T") = CompairStringResult.Equal Then
                                'If clsCommon.myLen(custState) >= 0 And clsCommon.CompairString(custState, locstate) = CompairStringResult.Equal And clsCommon.myLen(custTin) > 0 Then


                                If clsCommon.myLen(custTin) <= 0 Then
                                    Throw New Exception("Please Insert Tin No. for Customer:" + obj.cust_Name)
                                End If
                                If clsCommon.myLen(invoice) <= 0 Then
                                    Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesTaxatMiscSale, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesTaxatMiscSale, trans)
                                    If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(obj.Is_Scrap, "Y") = CompairStringResult.Equal Then
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceTax, objin.Loc_Code)
                                        Else
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceTax, objin.Loc_Code)
                                        End If

                                    Else
                                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceTax, objin.Loc_Code)
                                    End If
                                    'invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceTax, objin.Loc_Code)
                                End If
                                objin.Invoice_Type = "Tax"
                            ElseIf clsCommon.CompairString(transtype, "R") = CompairStringResult.Equal Then
                                'If clsCommon.myLen(invoice) <= 0 Then
                                If clsCommon.myLen(invoice) <= 0 Then
                                    Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesRetailatMiscSale, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesRetailatMiscSale, trans)
                                    If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(obj.Is_Scrap, "Y") = CompairStringResult.Equal Then
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceRetail, objin.Loc_Code)
                                        Else
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceRetail, objin.Loc_Code)
                                        End If

                                    Else
                                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceRetail, objin.Loc_Code)
                                    End If
                                    'invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceRetail, objin.Loc_Code)
                                End If

                                objin.Invoice_Type = "Retail"

                            ElseIf clsCommon.CompairString(transtype, "I") = CompairStringResult.Equal Then
                                'If clsCommon.myLen(invoice) <= 0 Then
                                If clsCommon.myLen(invoice) <= 0 Then
                                    Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesRetailatMiscSale, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesRetailatMiscSale, trans)
                                    If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(obj.Is_Scrap, "Y") = CompairStringResult.Equal Then
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.SaleInvoiceInterstate, objin.Loc_Code)
                                        Else
                                            invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceInterstate, objin.Loc_Code)
                                        End If

                                    Else
                                        invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceRetail, objin.Loc_Code)
                                    End If
                                    'invoice = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objin.shipment_Date), clsDocType.ScrapInvoice, clsDocTransactionType.ScrapInvoiceRetail, objin.Loc_Code)
                                End If
                                objin.Invoice_Type = "Invoice"
                            End If
                        End If
                    End If
                End If

            End If

            '  Throw New Exception("Transaction Type not found for Customer:" + obj.cust_Name)




            '------------------------------------------------------------------------------------------------------------


            For Each objout As ClsScrapSaleDetailReturn In obj.Arr1
                Dim objtr As New scrapinvoicedetail()
                objtr.ItemwiseTaxCode = objout.ItemwiseTaxCode
                objtr.invoice_No = invoice
                objtr.Line_No = objout.Line_No
                objtr.Specification = objout.Specification
                objtr.Item_Code = objout.Item_Code
                objtr.Item_Desc = objout.Item_Desc
                objtr.Unit_Code = objout.Unit_Code
                objtr.shipped_Qty = objout.shipped_Qty

                objtr.FAT = objout.FAT
                objtr.SNF = objout.SNF

                objtr.price = objout.price
                objtr.DiscountPer = objout.DiscountPer
                objtr.DiscountAmt = objout.DiscountAmt
                objtr.NetPriceAmt = objout.NetPriceAmt
                objtr.ItemAmt = objout.ItemAmt
                objtr.TotalDiscountAmt = objout.TotalDiscountAmt
                objtr.TotalTaxAmt = objout.TotalTaxAmt
                objtr.ItemNetAmt = objout.ItemNetAmt
                objtr.TotalAmt = objout.TotalAmt
                objtr.TAX1 = objout.TAX1
                objtr.TAX1_Base_Amt = objout.TAX1_Base_Amt
                objtr.TAX1_Rate = objout.TAX1_Rate
                objtr.TAX1_Amt = objout.TAX1_Amt
                objtr.TAX2 = objout.TAX2
                objtr.TAX2_Base_Amt = objout.TAX2_Base_Amt
                objtr.TAX2_Rate = objout.TAX2_Rate
                objtr.TAX2_Amt = objout.TAX2_Amt
                objtr.TAX3 = objout.TAX3
                objtr.TAX3_Base_Amt = objout.TAX3_Base_Amt
                objtr.TAX3_Rate = objout.TAX3_Rate
                objtr.TAX3_Amt = objout.TAX3_Amt
                objtr.TAX4 = objout.TAX4
                objtr.TAX4_Base_Amt = objout.TAX4_Base_Amt
                objtr.TAX4_Rate = objout.TAX4_Rate
                objtr.TAX4_Amt = objout.TAX4_Amt
                objtr.TAX5 = objout.TAX5
                objtr.TAX5_Base_Amt = objout.TAX5_Base_Amt
                objtr.TAX5_Rate = objout.TAX5_Rate
                objtr.TAX5_Amt = objout.TAX5_Amt
                objtr.TAX6 = objout.TAX6
                objtr.TAX6_Base_Amt = objout.TAX6_Base_Amt
                objtr.TAX6_Rate = objout.TAX6_Rate
                objtr.TAX6_Amt = objout.TAX6_Amt
                objtr.TAX7 = objout.TAX7
                objtr.TAX7_Base_Amt = objout.TAX7_Base_Amt
                objtr.TAX7_Rate = objout.TAX7_Rate
                objtr.TAX7_Amt = objout.TAX7_Amt
                objtr.TAX8 = objout.TAX8
                objtr.TAX8_Base_Amt = objout.TAX8_Base_Amt
                objtr.TAX8_Rate = objout.TAX8_Rate
                objtr.TAX8_Amt = objout.TAX8_Amt
                objtr.TAX9 = objout.TAX9
                objtr.TAX9_Base_Amt = objout.TAX9_Base_Amt
                objtr.TAX9_Rate = objout.TAX9_Rate
                objtr.TAX9_Amt = objout.TAX9_Amt
                objtr.TAX10 = objout.TAX10
                objtr.TAX10_Base_Amt = objout.TAX10_Base_Amt
                objtr.TAX10_Rate = objout.TAX10_Rate
                objtr.TAX10_Amt = objout.TAX10_Amt
                objtr.Asset_Code = objout.Asset_Code
                If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                    For Each objq As ClsScrapSaleDetailReturn In Arr
                        If clsCommon.CompairString(objq.Item_Code, objout.Item_Code) = CompairStringResult.Equal Then
                            objtr.arrBatchItem = objq.arrBatchItem
                        End If
                    Next
                End If
                objin.ArrIn.Add(objtr)
            Next



            If (clsCommon.myLen(invoice) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "shipment_No", objin.shipment_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", objin.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "Status", objin.Status)
            clsCommon.AddColumnsForChange(coll, "Po_No", objin.Po_No)
            clsCommon.AddColumnsForChange(coll, "NRG_No", objin.NRG_No)
            clsCommon.AddColumnsForChange(coll, "cust_Code", objin.cust_Code)
            clsCommon.AddColumnsForChange(coll, "cust_Name", objin.cust_Name)
            clsCommon.AddColumnsForChange(coll, "shipment_Date", clsCommon.GetPrintDate(objin.shipment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "posting_Date", clsCommon.GetPrintDate(objin.posting_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "expship_Date", clsCommon.GetPrintDate(objin.expship_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", objin.Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Loc_Name", objin.Loc_Name)
            clsCommon.AddColumnsForChange(coll, "ToLoc_Code", objin.ToLoc_Code)
            clsCommon.AddColumnsForChange(coll, "CreateInvoice", objin.CreateInvoice)
            clsCommon.AddColumnsForChange(coll, "Description", objin.Description)
            clsCommon.AddColumnsForChange(coll, "reff", objin.reff)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", objin.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Tax_Desc", objin.Tax_Desc)
            clsCommon.AddColumnsForChange(coll, "Add_Amt", objin.Add_Amt)
            clsCommon.AddColumnsForChange(coll, "Before_Add_Amt", objin.Before_Add_Amt)
            clsCommon.AddColumnsForChange(coll, "Discount_Base", objin.Discount_Base)
            clsCommon.AddColumnsForChange(coll, "Discount_Amt", objin.Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", objin.Amount_Less_Discount)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", objin.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "ship_Total_Amt", objin.ship_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "doc_Amt", objin.doc_Amt)
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", objin.RoundOffAmount)
            clsCommon.AddColumnsForChange(coll, "ispost", 0)
            clsCommon.AddColumnsForChange(coll, "TAX1", objin.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", objin.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", objin.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", objin.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", objin.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", objin.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", objin.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", objin.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", objin.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", objin.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", objin.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", objin.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", objin.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", objin.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", objin.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", objin.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", objin.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", objin.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", objin.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", objin.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6", objin.TAX6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", objin.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", objin.TAX6_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6_Amt", objin.TAX6_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7", objin.TAX7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", objin.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", objin.TAX7_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7_Amt", objin.TAX7_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8", objin.TAX8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", objin.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", objin.TAX8_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8_Amt", objin.TAX8_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9", objin.TAX9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", objin.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", objin.TAX9_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9_Amt", objin.TAX9_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10", objin.TAX10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", objin.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", objin.TAX10_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10_Amt", objin.TAX10_Amt)

            clsCommon.AddColumnsForChange(coll, "AddCode1", objin.AddCode1)
            clsCommon.AddColumnsForChange(coll, "AddDesc1", objin.AddDesc1)
            clsCommon.AddColumnsForChange(coll, "AddAmt1", objin.AddAmt1)
            clsCommon.AddColumnsForChange(coll, "AddCode2", objin.AddCode2)
            clsCommon.AddColumnsForChange(coll, "AddDesc2", objin.AddDesc2)
            clsCommon.AddColumnsForChange(coll, "AddAmt2", objin.AddAmt2)
            clsCommon.AddColumnsForChange(coll, "AddCode3", objin.AddCode3)
            clsCommon.AddColumnsForChange(coll, "AddDesc3", objin.AddDesc3)
            clsCommon.AddColumnsForChange(coll, "AddAmt3", objin.AddAmt3)
            clsCommon.AddColumnsForChange(coll, "AddCode4", objin.AddCode4)
            clsCommon.AddColumnsForChange(coll, "AddDesc4", objin.AddDesc4)
            clsCommon.AddColumnsForChange(coll, "AddAmt4", objin.AddAmt4)
            clsCommon.AddColumnsForChange(coll, "AddCode5", objin.AddCode5)
            clsCommon.AddColumnsForChange(coll, "AddDesc5", objin.AddDesc5)
            clsCommon.AddColumnsForChange(coll, "AddAmt5", objin.AddAmt5)
            clsCommon.AddColumnsForChange(coll, "AddCode6", objin.AddCode6)
            clsCommon.AddColumnsForChange(coll, "AddDesc6", objin.AddDesc6)
            clsCommon.AddColumnsForChange(coll, "AddAmt6", objin.AddAmt6)
            clsCommon.AddColumnsForChange(coll, "AddCode7", objin.AddCode7)
            clsCommon.AddColumnsForChange(coll, "AddDesc7", objin.AddDesc7)
            clsCommon.AddColumnsForChange(coll, "AddAmt7", objin.AddAmt7)
            clsCommon.AddColumnsForChange(coll, "AddCode8", objin.AddCode8)
            clsCommon.AddColumnsForChange(coll, "AddDesc8", objin.AddDesc8)
            clsCommon.AddColumnsForChange(coll, "AddAmt8", objin.AddAmt8)
            clsCommon.AddColumnsForChange(coll, "AddCode9", objin.AddCode9)
            clsCommon.AddColumnsForChange(coll, "AddDesc9", objin.AddDesc9)
            clsCommon.AddColumnsForChange(coll, "AddAmt9", objin.AddAmt9)
            clsCommon.AddColumnsForChange(coll, "AddCode10", objin.AddCode10)
            clsCommon.AddColumnsForChange(coll, "AddDesc10", objin.AddDesc10)
            clsCommon.AddColumnsForChange(coll, "AddAmt10", objin.AddAmt10)
            clsCommon.AddColumnsForChange(coll, "Balance_Amt", objin.BalanceAmt)
            clsCommon.AddColumnsForChange(coll, "Inter_Branch", IIf(objin.Inter_Branch = True, 1, 0))

            If clsCommon.myLen(objin.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(objin.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Terms_Code", objin.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "invoice_No", invoice)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Invoice_Type", objin.Invoice_Type)
            clsCommon.AddColumnsForChange(coll, "is_Asset_Type", IIf(obj.is_Asset_Type, 1, 0))

            clsCommon.AddColumnsForChange(coll, "Total_Gross_Weight", objin.Total_Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Total_Net_Weight", objin.Total_Net_Weight)
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", IIf(objin.Is_Taxable, 1, 0))


            If isNewEntry = True Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPINVOICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPINVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_SCRAPINVOICE_HEAD.invoice_No='" + invoice + "'", trans)
            End If

            scrapinvoicedetail.SaveDatainvoice(invoice, objin.shipment_No, objin.ArrIn, objin.shipment_Date, objin.Loc_Code, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

End Class

Public Class scrapinvoicedetail
#Region "Variables"
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public Specification As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public shipped_Qty As Double = 0
    Public price As Double = 0
    Public DiscountPer As Double = 0
    Public DiscountAmt As Double = 0
    Public Tax As Double = 0
    Public NetPriceAmt As String = Nothing
    Public ItemAmt As String = Nothing
    Public TotalDiscountAmt As Double = 0
    Public TotalTaxAmt As Double = 0
    Public ItemNetAmt As Double = 0
    Public TotalAmt As Double = 0 '
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
    Public Code As String = Nothing   'Additiona charges code
    Public Description As String = Nothing   'Additiona charges description
    Public invoice_Qty As Double = 0
    Public Asset_Code As String = Nothing
    Public FAT As Double = 0
    Public SNF As Double = 0
    Public ItemwiseTaxCode As String = Nothing
    Public invoice_No As String = Nothing
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
#End Region

    Public Shared Sub SaveDatainvoice(ByVal strDocNo As String, ByVal strShip As String, ByVal Arr As List(Of scrapinvoicedetail), ByVal dtDocDate As DateTime, ByVal strlocation As String, ByVal trans As SqlTransaction)

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As scrapinvoicedetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "invoice_No", obj.invoice_No)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                If clsCommon.myLen(obj.Row_Type) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Row_Type", "Item")
                Else
                    clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                End If
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "shipped_Qty", obj.shipped_Qty)
                clsCommon.AddColumnsForChange(coll, "invoice_Qty", obj.shipped_Qty)
                clsCommon.AddColumnsForChange(coll, "price", obj.price)
                clsCommon.AddColumnsForChange(coll, "DiscountPer", obj.DiscountPer)
                clsCommon.AddColumnsForChange(coll, "DiscountAmt", obj.DiscountAmt)
                clsCommon.AddColumnsForChange(coll, "TotalTaxAmt", obj.TotalTaxAmt)
                clsCommon.AddColumnsForChange(coll, "ItemAmt", obj.ItemAmt)
                clsCommon.AddColumnsForChange(coll, "NetPriceAmt", obj.NetPriceAmt)
                clsCommon.AddColumnsForChange(coll, "TotalDiscountAmt", obj.TotalDiscountAmt)
                clsCommon.AddColumnsForChange(coll, "ItemNetAmt", obj.ItemNetAmt)
                clsCommon.AddColumnsForChange(coll, "TotalAmt", obj.TotalAmt)


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
                ' ''clsCommon.AddColumnsForChange(coll, "Landed_Cost_Rate", obj.Landed_Cost_Rate)
                ' ''clsCommon.AddColumnsForChange(coll, "Landed_Cost_Amount", obj.Landed_Cost_Amount)
                clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code, True)

                clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                clsCommon.AddColumnsForChange(coll, "ItemwiseTaxCode", obj.ItemwiseTaxCode)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPINVOICE_detail", OMInsertOrUpdate.Insert, "", trans)
                Dim penqty As Double = 0
                penqty = obj.shipped_Qty - obj.invoice_Qty
                Dim penqry As String = "update tspl_scrapsale_detail set pending_qty='" + clsCommon.myCstr(penqty) + "' where shipment_No='" + strShip + "' and Item_Code='" + obj.Item_Code + "' "
                clsDBFuncationality.ExecuteNonQuery(penqry, trans)

                clsBatchInventory.SaveData("ScrapIn", obj.invoice_No, dtDocDate, "O", obj.Item_Code, strlocation, obj.Line_No, 0, obj.Unit_Code, obj.arrBatchItem, trans)

            Next
        End If

    End Sub
End Class