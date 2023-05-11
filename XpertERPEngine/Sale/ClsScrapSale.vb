Imports System.Data.SqlClient

Public Class ClsScrapSaleHead
#Region "Variables"
    Public ActualTCSBaseAmount As Double = 0
    Public ChangedTCSBaseAmount As Double = 0
    Public shipment_No As String = Nothing
    Public Specification As String = Nothing
    Public invoice_No As String = Nothing
    Public Status As String = Nothing
    Public Po_No As String = Nothing
    Public NRG_No As String = Nothing
    Public cust_Code As String = Nothing
    Public cust_Name As String = Nothing
    Public shipment_Date As Date
    Public posting_Date As String = Nothing
    Public expship_Date As String = Nothing
    Public Loc_Code As String = Nothing
    Public Loc_Name As String = Nothing
    Public Vehicle_Id As String = Nothing
    Public ToLoc_Code As String = Nothing
    Public CreateInvoice As String = Nothing
    Public Excisable As String = Nothing
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
    Public Unit_Code As String = Nothing

    Public Inter_Branch As Boolean = False
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public strInvoiceNo As String = Nothing
    Public Invoice_Type As String = Nothing
    'Public ispost As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public is_Asset_Type As Boolean = False
    Public Arr As List(Of ClsScrapSaleDetail) = Nothing
    Public Transporter_code As String = Nothing
    Public Transporter_Name As String = Nothing
    Public Vehicle_code As String = Nothing
    Public VAT_InvoiceNo As String = Nothing
    Public VatInvoice_Type As String = Nothing
    Public Is_Scrap As String = "N"
    Public Is_CashSale As String = "N"
    Public InvoiceManualNowithPrefix As String = Nothing
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public RoundOffAmount As Double = 0

    Public Total_Gross_Weight As Double = 0
    Public Total_Net_Weight As Double = 0
    Public Doc_Type As String
    Public Is_Taxable As Boolean = False

    Public EWayBillNo As String
    Public EWayBillDate As Date? = Nothing
    Public Electronic_Ref_No As String
    Public Weighment_Code As String
    Public Freight_Distance As Integer = 0
#End Region

    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_SCRAPSALE_HEAD", "shipment_No", "TSPL_SCRAPSALE_DETAIL", "shipment_No", trans)
        Return True
    End Function

    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String, ByVal InvoiceNo As String, ByVal NavType As NavigatorType) As Boolean
        '' created by Sanjay
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim obj As ClsScrapSaleHead = ClsScrapSaleHead.GetData(Doc_No, NavType, trans, False)

            If obj Is Nothing OrElse clsCommon.myLen(obj.shipment_No) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            ''richa agarwal 24 Dec,2020
            Dim dtirn As DataTable = clsDBFuncationality.GetDataTable("select Einvoice_type,IRN_No,Is_Taxable,loc_code from TSPL_SCRAPINVOICE_HEAD where invoice_No='" & InvoiceNo & "'", trans)
            If dtirn IsNot Nothing AndAlso dtirn.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtirn.Rows(0)("Einvoice_type")), "BB") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(dtirn.Rows(0)("Is_Taxable")), "1") = CompairStringResult.Equal AndAlso clsERPFuncationality.GetEInvoiceStatus(obj.shipment_Date, trans) = True Then
                    If ClsEInvoiceOFAPIs.EInvoice_Cancellation(InvoiceNo, clsCommon.myCstr(dtirn.Rows(0)("IRN_No")), clsCommon.myCstr(dtirn.Rows(0)("loc_code")), trans) = True Then
                    Else
                        Throw New Exception("Invalid JSON Value")
                    End If
                End If
            End If
            ''----------

            clsItemLocationDetails.CheckCancelInventoryBalance(Form_Id, Doc_No, trans)
            '' transfer data into cancel table
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_SCRAPSALE_HEAD", "SHIPMENT_NO", "TSPL_SCRAPSALE_DETAIL", "SHIPMENT_NO", trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, InvoiceNo, "TSPL_SCRAPINVOICE_HEAD", "invoice_No", "TSPL_SCRAPINVOICE_DETAIL", "invoice_No", trans)


            '' cancel customer invoice data
            qry = "select Document_No from TSPL_Customer_Invoice_Head  where AgainstScrap='" & InvoiceNo & "'"
            Dim Document_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Document_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Document_No, "TSPL_Customer_Invoice_Head", "Document_No", "TSPL_Customer_Invoice_Detail", "Document_No", trans)
            End If

            '' cancel journal master data invoice
            qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No in (select Document_No from TSPL_Customer_Invoice_Head  where AgainstScrap='" & InvoiceNo & "')"
            Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Voucher_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            End If

            '' cancel journal master data shipment
            'qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No='" & Doc_No & "'"
            'Voucher_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            'If clsCommon.myLen(Voucher_No) > 0 Then
            '    clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            'End If

            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, InvoiceNo, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)


            ''delete data from multiple tables

            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Document_No from TSPL_Customer_Invoice_Head  where AgainstScrap='" & InvoiceNo & "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Document_No from TSPL_Customer_Invoice_Head  where AgainstScrap='" & InvoiceNo & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_Customer_Invoice_Detail where Document_No in (Select Document_No from TSPL_Customer_Invoice_Head  where AgainstScrap='" & InvoiceNo & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_Customer_Invoice_Head where AgainstScrap='" & InvoiceNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code in ('" & InvoiceNo & "','" & Doc_No & "') "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '----- shipment data

            qry = "delete from TSPL_BATCH_ITEM where  Document_Code='" & InvoiceNo & "' and Document_Type='ScrapIn'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & InvoiceNo & "' and Trans_Type='ScrapIn'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & Doc_No & "')"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No  ='" & Doc_No & "'"
            'clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SCRAPINVOICE_DETAIL where invoice_No='" & InvoiceNo & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SCRAPINVOICE_HEAD where invoice_No='" & InvoiceNo & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SCRAPSALE_DETAIL where SHIPMENT_NO='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SCRAPSALE_HEAD where SHIPMENT_NO='" & Doc_No & "' "
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

    Public Function SaveData(ByVal obj As ClsScrapSaleHead, ByVal strScrapSaleInvoiceNo As String, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim Desc As String = String.Empty
        Dim VatInvoiceType As String = Nothing
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.JobWorkDispatchProduction, obj.Loc_Code, obj.shipment_Date, trans)
            If Not isNewEntry Then
                HistoryUpdate(obj.shipment_No, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.strInvoiceNo), "TSPL_SCRAPINVOICE_HEAD", "invoice_No", "TSPL_SCRAPINVOICE_DETAIL", "invoice_No", trans)
            End If

            Dim qry As String = "delete from TSPL_SCRAPSALE_DETAIL where shipment_No='" + obj.shipment_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SCRAPINVOICE_DETAIL where invoice_No='" + obj.strInvoiceNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "delete from TSPL_SCRAPINVOICE_HEAD where invoice_No='" + obj.strInvoiceNo + "'"
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            clsBatchInventory.DeleteData("ScrapIn", obj.strInvoiceNo, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString("J", obj.Doc_Type) = CompairStringResult.Equal Then
                    obj.shipment_No = clsERPFuncationality.GetNextCode(trans, obj.shipment_Date, clsDocType.JobWorkDispatch, "", obj.Loc_Code)
                Else
                    obj.shipment_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.shipment_Date, "dd/MMM/yyyy  hh:mm tt"), clsDocType.Scrap, "", obj.Loc_Code)
                    ''-----------ravi---------
                    Dim isIncrementCounter As Boolean = True
                    'If clsCommon.myLen(obj.InvoiceManualNowithPrefix) > 0 Then
                    '    isIncrementCounter = False
                    'End If
                    If (obj.ToLoc_Code IsNot String.Empty) Then
                        Dim CreatVatSeriesOnExciseInvoice As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateVatSeriesForProductExciseinvoice, clsFixedParameterCode.CreateVatSeriesForProductExciseinvoice, trans))
                        If clsCommon.CompairString(obj.Invoice_Type, "E") = CompairStringResult.Equal AndAlso CreatVatSeriesOnExciseInvoice = 1 Then
                            Desc = clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerateSaleInvoiceSeriesExciseTypeatPS, clsFixedParameterCode.AllowToGenerateSaleInvoiceSeriesExciseTypeatPS, trans)
                            VatInvoiceType = InvoiceType(obj.ToLoc_Code, obj.cust_Code, trans)
                            If clsCommon.CompairString(VatInvoiceType, "R") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                                    obj.VAT_InvoiceNo = clsERPFuncationality.GetNextCode(trans, obj.shipment_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceRetail, obj.ToLoc_Code, False, isIncrementCounter)
                                Else
                                    obj.VAT_InvoiceNo = clsERPFuncationality.GetNextCode(trans, obj.shipment_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceRetail, obj.ToLoc_Code, False, isIncrementCounter)
                                End If
                                obj.VatInvoice_Type = VatInvoiceType
                            ElseIf clsCommon.CompairString(VatInvoiceType, "T") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                                    obj.VAT_InvoiceNo = clsERPFuncationality.GetNextCode(trans, obj.shipment_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceTax, obj.ToLoc_Code, False, isIncrementCounter)
                                Else
                                    obj.VAT_InvoiceNo = clsERPFuncationality.GetNextCode(trans, obj.shipment_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceTax, obj.ToLoc_Code, False, isIncrementCounter)
                                End If
                                obj.VatInvoice_Type = VatInvoiceType
                            ElseIf clsCommon.CompairString(VatInvoiceType, "I") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(Desc, "1") = CompairStringResult.Equal Then
                                    obj.VAT_InvoiceNo = clsERPFuncationality.GetNextCode(trans, obj.shipment_Date, clsDocType.SNSaleInvoice, clsDocTransactionType.SaleInvoiceInterstate, obj.ToLoc_Code, False, isIncrementCounter)
                                Else
                                    obj.VAT_InvoiceNo = clsERPFuncationality.GetNextCode(trans, obj.shipment_Date, clsDocType.frmSaleInvoiceProductSale, clsDocTransactionType.SaleInvoiceInterstate, obj.ToLoc_Code, False, isIncrementCounter)
                                End If
                                obj.VatInvoice_Type = VatInvoiceType
                            End If
                        End If
                    Else
                        Throw New Exception("Please Select Location")
                    End If
                End If
            End If
            If (clsCommon.myLen(obj.shipment_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            '-----------------------------
            Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "shipment_No", obj.shipment_No)
            clsCommon.AddColumnsForChange(coll, "invoice_No", obj.invoice_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Po_No", obj.Po_No)
            clsCommon.AddColumnsForChange(coll, "NRG_No", obj.NRG_No)
            clsCommon.AddColumnsForChange(coll, "cust_Code", obj.cust_Code)
            clsCommon.AddColumnsForChange(coll, "cust_Name", obj.cust_Name)
            clsCommon.AddColumnsForChange(coll, "shipment_Date", clsCommon.GetPrintDate(obj.shipment_Date, "dd/MMM/yyyy hh:mm tt "))
            clsCommon.AddColumnsForChange(coll, "posting_Date", clsCommon.GetPrintDate(obj.posting_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "expship_Date", clsCommon.GetPrintDate(obj.expship_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Loc_Name", obj.Loc_Name)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Id", obj.Vehicle_Id)
            clsCommon.AddColumnsForChange(coll, "ToLoc_Code", obj.ToLoc_Code)
            clsCommon.AddColumnsForChange(coll, "CreateInvoice", obj.CreateInvoice)
            clsCommon.AddColumnsForChange(coll, "Excisable", obj.Excisable)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            '============Add By Rohit on Apr,14-2015==============
            clsCommon.AddColumnsForChange(coll, "Transport_code", obj.Transporter_code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_code", obj.Vehicle_code)
            '===========================================================
            clsCommon.AddColumnsForChange(coll, "Freight_Distance", obj.Freight_Distance)
            clsCommon.AddColumnsForChange(coll, "reff", obj.reff)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Tax_Desc", obj.Tax_Desc)
            clsCommon.AddColumnsForChange(coll, "Add_Amt", obj.Add_Amt)
            clsCommon.AddColumnsForChange(coll, "Before_Add_Amt", obj.Before_Add_Amt)
            clsCommon.AddColumnsForChange(coll, "Discount_Base", obj.Discount_Base)
            clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "ship_Total_Amt", obj.ship_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "doc_Amt", obj.doc_Amt)
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)
            clsCommon.AddColumnsForChange(coll, "ispost", 0)
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
            clsCommon.AddColumnsForChange(coll, "AddCode1", obj.AddCode1)
            clsCommon.AddColumnsForChange(coll, "AddDesc1", obj.AddDesc1)
            clsCommon.AddColumnsForChange(coll, "AddAmt1", obj.AddAmt1)
            clsCommon.AddColumnsForChange(coll, "AddCode2", obj.AddCode2)
            clsCommon.AddColumnsForChange(coll, "AddDesc2", obj.AddDesc2)
            clsCommon.AddColumnsForChange(coll, "AddAmt2", obj.AddAmt2)
            clsCommon.AddColumnsForChange(coll, "AddCode3", obj.AddCode3)
            clsCommon.AddColumnsForChange(coll, "AddDesc3", obj.AddDesc3)
            clsCommon.AddColumnsForChange(coll, "AddAmt3", obj.AddAmt3)
            clsCommon.AddColumnsForChange(coll, "AddCode4", obj.AddCode4)
            clsCommon.AddColumnsForChange(coll, "AddDesc4", obj.AddDesc4)
            clsCommon.AddColumnsForChange(coll, "AddAmt4", obj.AddAmt4)
            clsCommon.AddColumnsForChange(coll, "AddCode5", obj.AddCode5)
            clsCommon.AddColumnsForChange(coll, "AddDesc5", obj.AddDesc5)
            clsCommon.AddColumnsForChange(coll, "AddAmt5", obj.AddAmt5)
            clsCommon.AddColumnsForChange(coll, "AddCode6", obj.AddCode6)
            clsCommon.AddColumnsForChange(coll, "AddDesc6", obj.AddDesc6)
            clsCommon.AddColumnsForChange(coll, "AddAmt6", obj.AddAmt6)
            clsCommon.AddColumnsForChange(coll, "AddCode7", obj.AddCode7)
            clsCommon.AddColumnsForChange(coll, "AddDesc7", obj.AddDesc7)
            clsCommon.AddColumnsForChange(coll, "AddAmt7", obj.AddAmt7)
            clsCommon.AddColumnsForChange(coll, "AddCode8", obj.AddCode8)
            clsCommon.AddColumnsForChange(coll, "AddDesc8", obj.AddDesc8)
            clsCommon.AddColumnsForChange(coll, "AddAmt8", obj.AddAmt8)
            clsCommon.AddColumnsForChange(coll, "AddCode9", obj.AddCode9)
            clsCommon.AddColumnsForChange(coll, "AddDesc9", obj.AddDesc9)
            clsCommon.AddColumnsForChange(coll, "AddAmt9", obj.AddAmt9)
            clsCommon.AddColumnsForChange(coll, "AddCode10", obj.AddCode10)
            clsCommon.AddColumnsForChange(coll, "AddDesc10", obj.AddDesc10)
            clsCommon.AddColumnsForChange(coll, "AddAmt10", obj.AddAmt10)
            clsCommon.AddColumnsForChange(coll, "Inter_Branch", IIf(obj.Inter_Branch = True, 1, 0))
            clsCommon.AddColumnsForChange(coll, "is_Asset_Type", IIf(obj.is_Asset_Type, 1, 0))
            ''richa agarwal 19/03/2015
            clsCommon.AddColumnsForChange(coll, "Invoice_Type", obj.Invoice_Type)
            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "VAT_InvoiceNo", obj.VAT_InvoiceNo)
            clsCommon.AddColumnsForChange(coll, "VatInvoice_Type", obj.VatInvoice_Type)
            clsCommon.AddColumnsForChange(coll, "Is_Scrap", obj.Is_Scrap)
            clsCommon.AddColumnsForChange(coll, "Is_CashSale", obj.Is_CashSale)

            clsCommon.AddColumnsForChange(coll, "Total_Gross_Weight", obj.Total_Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Total_Net_Weight", obj.Total_Net_Weight)

            clsCommon.AddColumnsForChange(coll, "Weighment_Code", obj.Weighment_Code, True)

            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", IIf(obj.Is_Taxable, 1, 0))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "shipment_No", obj.shipment_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPSALE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPSALE_HEAD", OMInsertOrUpdate.Update, "TSPL_SCRAPSALE_HEAD.shipment_No='" + obj.shipment_No + "'", trans)
            End If

            isSaved = isSaved AndAlso ClsScrapSaleDetail.SaveData(obj.shipment_No, Arr, trans, obj.shipment_Date, obj.Loc_Code, obj.strInvoiceNo)
            If (obj.CreateInvoice = 1) Then
                isSaved = isSaved AndAlso scrapinvoicehead.SaveDatainvoice(obj.shipment_No, strScrapSaleInvoiceNo, trans, obj.Invoice_Type, Arr)
            End If
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.shipment_No, obj.arrCustomFields, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    'Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As ClsScrapSaleHead
    '    Return GetData(strDocumentNo, NavType, Nothing)
    'End Function

    Public Function InvoiceType(ByVal Location As String, ByVal Customer As String, ByVal trans As SqlTransaction) As String
        Dim dt As DataTable
        Dim strloc As String = Nothing
        Dim qry As String = Nothing
        Dim strInvoiceType As String = Nothing
        strloc = Location
        qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " & _
          "TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " & _
          "TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " & _
          "FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' " & _
          "WHERE TSPL_LOCATION_MASTER.Location_Code = '" + strloc + "'"


        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strLocState As String = clsCommon.myCstr(dt.Rows(0)("State"))

        qry = "select Price_Code,price_CodeNon,State,Tin_No from TSPL_CUSTOMER_MASTER where Cust_Code='" + Customer + "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strCustState As String = clsCommon.myCstr(dt.Rows(0)("State"))
        Dim strTinNo As String = clsCommon.myCstr(dt.Rows(0)("Tin_No"))

        If clsCommon.myLen(strTinNo) > 0 AndAlso clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
            strInvoiceType = "T"
        Else
            If clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
                strInvoiceType = "R"
            Else
                strInvoiceType = "I" 'Interstate series
            End If

        End If

        Return strInvoiceType
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As ClsScrapSaleHead
        Return GetData(strDocumentNo, NavType, Nothing, False)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal isAssetType As Boolean) As ClsScrapSaleHead
        Return GetData(strPONo, NavType, trans, isAssetType, "")
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal isAssetType As Boolean, ByVal strDocType As String) As ClsScrapSaleHead
        Dim obj As ClsScrapSaleHead = Nothing
        Dim qry As String = "SELECT tspl_scrapsale_head.*,TSPL_LOCATION_MASTER.Location_Desc as ToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,(select invoice_No from TSPL_SCRAPINVOICE_HEAD where TSPL_SCRAPINVOICE_HEAD.shipment_No=tspl_scrapsale_head.shipment_No) as ScrapInvoiceNo,TSPL_TRANSPORT_MASTER.Transporter_name FROM tspl_scrapsale_head left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_scrapsale_head.loc_code  left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=Transport_code  left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=tspl_scrapsale_head.loc_code  left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=tspl_scrapsale_head.Terms_Code where 2=2"
        Dim whrCls As String = ""

        If isAssetType Then
            whrCls += " and is_Asset_Type=1 "
        End If
        If clsCommon.myLen(strDocType) > 0 Then
            whrCls += " and Doc_Type ='" + strDocType + "'"
        End If

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND loc_code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and tspl_scrapsale_head.shipment_No = (select MIN(shipment_No) from tspl_scrapsale_head WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and tspl_scrapsale_head.shipment_No = (select Max(shipment_No) from tspl_scrapsale_head WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and tspl_scrapsale_head.shipment_No = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and tspl_scrapsale_head.shipment_No = (select Min(shipment_No) from tspl_scrapsale_head where shipment_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and tspl_scrapsale_head.shipment_No = (select Max(shipment_No) from tspl_scrapsale_head where shipment_No<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsScrapSaleHead()
            obj.shipment_No = clsCommon.myCstr(dt.Rows(0)("shipment_No"))
            obj.invoice_No = clsCommon.myCstr(dt.Rows(0)("invoice_No"))
            obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
            obj.Po_No = clsCommon.myCstr(dt.Rows(0)("Po_No"))
            obj.NRG_No = clsCommon.myCstr(dt.Rows(0)("NRG_No"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.ispost = clsCommon.myCstr(dt.Rows(0)("ispost"))
            obj.cust_Code = clsCommon.myCstr(dt.Rows(0)("cust_Code"))
            obj.cust_Name = clsCommon.myCstr(dt.Rows(0)("cust_Name"))
            obj.shipment_Date = clsCommon.myCDate(dt.Rows(0)("shipment_Date"))
            obj.posting_Date = clsCommon.myCDate(dt.Rows(0)("posting_Date"))
            obj.expship_Date = clsCommon.myCDate(dt.Rows(0)("expship_Date"))
            obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.Loc_Name = clsCommon.myCstr(dt.Rows(0)("Loc_Name"))
            obj.Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Vehicle_Id"))
            obj.ToLoc_Code = clsCommon.myCstr(dt.Rows(0)("ToLoc_Code"))
            obj.CreateInvoice = clsCommon.myCstr(dt.Rows(0)("CreateInvoice"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.reff = clsCommon.myCstr(dt.Rows(0)("reff"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.Transporter_code = clsCommon.myCstr(dt.Rows(0)("Transport_code"))
            obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_name"))
            obj.Vehicle_code = clsCommon.myCstr(dt.Rows(0)("Vehicle_code"))
            obj.Freight_Distance = clsCommon.myCdbl(dt.Rows(0)("Freight_Distance"))
            obj.Tax_Desc = clsCommon.myCstr(dt.Rows(0)("Tax_Desc"))
            obj.Add_Amt = clsCommon.myCstr(dt.Rows(0)("Add_Amt"))
            obj.Before_Add_Amt = clsCommon.myCstr(dt.Rows(0)("Before_Add_Amt"))
            obj.Discount_Base = clsCommon.myCstr(dt.Rows(0)("Discount_Base"))
            obj.Discount_Amt = clsCommon.myCstr(dt.Rows(0)("Discount_Amt"))
            obj.Amount_Less_Discount = clsCommon.myCstr(dt.Rows(0)("Amount_Less_Discount"))
            obj.Total_Tax_Amt = clsCommon.myCstr(dt.Rows(0)("Total_tax_amt"))
            obj.ship_Total_Amt = clsCommon.myCstr(dt.Rows(0)("ship_total_amt"))
            obj.doc_Amt = clsCommon.myCstr(dt.Rows(0)("Doc_Amt"))
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            obj.Excisable = clsCommon.myCstr(dt.Rows(0)("EXCISABLE"))
            obj.Inter_Branch = IIf(clsCommon.myCdbl(dt.Rows(0)("Inter_Branch")) = 1, True, False)
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
            obj.ChangedTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ChangedTCSBaseAmount"))
            obj.ActualTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ActualTCSBaseAmount"))
            obj.AddCode1 = clsCommon.myCstr(dt.Rows(0)("AddCode1"))
            obj.AddDesc1 = clsCommon.myCstr(dt.Rows(0)("AddDesc1"))
            obj.AddAmt1 = clsCommon.myCdbl(dt.Rows(0)("AddAmt1"))
            obj.AddCode2 = clsCommon.myCstr(dt.Rows(0)("AddCode2"))
            obj.AddDesc2 = clsCommon.myCstr(dt.Rows(0)("AddDesc2"))
            obj.AddAmt2 = clsCommon.myCdbl(dt.Rows(0)("AddAmt2"))
            obj.AddCode3 = clsCommon.myCstr(dt.Rows(0)("AddCode3"))
            obj.AddDesc3 = clsCommon.myCstr(dt.Rows(0)("AddDesc3"))
            obj.AddAmt3 = clsCommon.myCdbl(dt.Rows(0)("AddAmt3"))
            obj.AddCode4 = clsCommon.myCstr(dt.Rows(0)("AddCode4"))
            obj.AddDesc4 = clsCommon.myCstr(dt.Rows(0)("AddDesc4"))
            obj.AddAmt4 = clsCommon.myCdbl(dt.Rows(0)("AddAmt4"))
            obj.AddCode5 = clsCommon.myCstr(dt.Rows(0)("AddCode5"))
            obj.AddDesc5 = clsCommon.myCstr(dt.Rows(0)("AddDesc5"))
            obj.AddAmt5 = clsCommon.myCdbl(dt.Rows(0)("AddAmt5"))
            obj.AddCode6 = clsCommon.myCstr(dt.Rows(0)("AddCode6"))
            obj.AddDesc6 = clsCommon.myCstr(dt.Rows(0)("AddDesc6"))
            obj.AddAmt6 = clsCommon.myCdbl(dt.Rows(0)("AddAmt6"))
            obj.AddCode7 = clsCommon.myCstr(dt.Rows(0)("AddCode7"))
            obj.AddDesc7 = clsCommon.myCstr(dt.Rows(0)("AddDesc7"))
            obj.AddAmt7 = clsCommon.myCdbl(dt.Rows(0)("AddAmt7"))
            obj.AddCode8 = clsCommon.myCstr(dt.Rows(0)("AddCode8"))
            obj.AddDesc8 = clsCommon.myCstr(dt.Rows(0)("AddDesc8"))
            obj.AddAmt8 = clsCommon.myCdbl(dt.Rows(0)("AddAmt8"))
            obj.AddCode9 = clsCommon.myCstr(dt.Rows(0)("AddCode9"))
            obj.AddDesc9 = clsCommon.myCstr(dt.Rows(0)("AddDesc9"))
            obj.AddAmt9 = clsCommon.myCdbl(dt.Rows(0)("AddAmt9"))
            obj.AddCode10 = clsCommon.myCstr(dt.Rows(0)("AddCode10"))
            obj.AddDesc10 = clsCommon.myCstr(dt.Rows(0)("AddDesc10"))
            obj.AddAmt10 = clsCommon.myCdbl(dt.Rows(0)("AddAmt10"))
            obj.Weighment_Code = clsCommon.myCstr(dt.Rows(0)("Weighment_Code"))
            obj.is_Asset_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("is_Asset_Type")) = 1, True, False)
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            ''richa agarwal 19/03/2015
            obj.Invoice_Type = clsCommon.myCstr(dt.Rows(0)("Invoice_Type"))
            ''-----------
            '------Ravi----------
            obj.VAT_InvoiceNo = clsCommon.myCstr(dt.Rows(0)("VAT_InvoiceNo"))
            obj.VatInvoice_Type = clsCommon.myCstr(dt.Rows(0)("VatInvoice_Type"))
            obj.Is_Scrap = clsCommon.myCstr(dt.Rows(0)("Is_Scrap"))
            ''-----------
            'done by stuti
            obj.Is_CashSale = clsCommon.myCstr(dt.Rows(0)("Is_CashSale"))

            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)

            obj.strInvoiceNo = clsCommon.myCstr(dt.Rows(0)("ScrapInvoiceNo"))


            obj.Total_Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Total_Gross_Weight"))
            obj.Total_Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Total_Net_Weight"))

            obj.Is_Taxable = If(clsCommon.myCdbl(dt.Rows(0)("Is_Taxable")) > 0, True, False)
            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            If dt.Rows(0)("EWayBillDate") IsNot DBNull.Value Then
                obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            End If
            obj.Electronic_Ref_No = clsCommon.myCstr(dt.Rows(0)("Electronic_Ref_No"))
            qry = "SELECT TSPL_SCRAPSALE_DETAIL.ItemRate,TSPL_SCRAPSALE_DETAIL.Row_Type,TSPL_SCRAPSALE_DETAIL.ItemwiseTaxCode,TSPL_SCRAPSALE_DETAIL.shipment_No,TSPL_SCRAPSALE_DETAIL.Specification,TSPL_SCRAPSALE_DETAIL.Line_No,TSPL_SCRAPSALE_DETAIL.Item_Code,TSPL_SCRAPSALE_DETAIL.Item_Desc,TSPL_SCRAPSALE_DETAIL.unit_code,TSPL_SCRAPSALE_DETAIL.Shipped_Qty,TSPL_SCRAPSALE_DETAIL.price,TSPL_SCRAPSALE_DETAIL.DiscountPer,TSPL_SCRAPSALE_DETAIL.DiscountAmt,TSPL_SCRAPSALE_DETAIL.TotalTaxAmt,TSPL_SCRAPSALE_DETAIL.NetPriceAmt,TSPL_SCRAPSALE_DETAIL.ItemAmt,TSPL_SCRAPSALE_DETAIL.TotalDiscountAmt,TSPL_SCRAPSALE_DETAIL.ItemNetAmt,TSPL_SCRAPSALE_DETAIL.TotalAmt,TSPL_SCRAPSALE_DETAIL.Specification,TSPL_SCRAPSALE_DETAIL.TAX1,TSPL_SCRAPSALE_DETAIL.TAX1_Rate,TSPL_SCRAPSALE_DETAIL.TAX1_Amt,TSPL_SCRAPSALE_DETAIL.TAX2,TSPL_SCRAPSALE_DETAIL.TAX2_Rate,TSPL_SCRAPSALE_DETAIL.TAX2_Amt,TSPL_SCRAPSALE_DETAIL.TAX3,TSPL_SCRAPSALE_DETAIL.TAX3_Rate,TSPL_SCRAPSALE_DETAIL.TAX3_Amt,TSPL_SCRAPSALE_DETAIL.TAX4,TSPL_SCRAPSALE_DETAIL.TAX4_Rate,TSPL_SCRAPSALE_DETAIL.TAX4_Amt,TSPL_SCRAPSALE_DETAIL.TAX5,TSPL_SCRAPSALE_DETAIL.TAX5_Rate,TSPL_SCRAPSALE_DETAIL.TAX5_Amt,TSPL_SCRAPSALE_DETAIL.TAX6,TSPL_SCRAPSALE_DETAIL.TAX6_Rate,TSPL_SCRAPSALE_DETAIL.TAX6_Amt,TSPL_SCRAPSALE_DETAIL.TAX7,TSPL_SCRAPSALE_DETAIL.TAX7_Rate,TSPL_SCRAPSALE_DETAIL.TAX7_Amt,TSPL_SCRAPSALE_DETAIL.TAX8,TSPL_SCRAPSALE_DETAIL.TAX8_Rate,TSPL_SCRAPSALE_DETAIL.TAX8_Amt,TSPL_SCRAPSALE_DETAIL.TAX9,TSPL_SCRAPSALE_DETAIL.TAX9_Rate,TSPL_SCRAPSALE_DETAIL.TAX9_Amt,TSPL_SCRAPSALE_DETAIL.TAX10,TSPL_SCRAPSALE_DETAIL.TAX10_Rate,TSPL_SCRAPSALE_DETAIL.TAX10_Amt,TSPL_SCRAPSALE_DETAIL.TAX1_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX2_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX3_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX4_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX5_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX6_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX7_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX8_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX9_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX10_Base_Amt,Asset_Code,TSPL_SCRAPSALE_DETAIL.FAT,TSPL_SCRAPSALE_DETAIL.SNF  FROM TSPL_SCRAPSALE_DETAIL  where TSPL_SCRAPSALE_DETAIL.shipment_No='" + obj.shipment_No + "' ORDER BY TSPL_SCRAPSALE_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of ClsScrapSaleDetail)
                Dim objTr As ClsScrapSaleDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsScrapSaleDetail
                    objTr.ItemwiseTaxCode = clsCommon.myCstr(dr("ItemwiseTaxCode"))
                    objTr.shipment_No = clsCommon.myCstr(dr("shipment_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.shipped_Qty = clsCommon.myCdbl(dr("shipped_Qty"))
                    objTr.ItemRate = clsCommon.myCdbl(dr("ItemRate"))
                    objTr.FAT = clsCommon.myCdbl(dr("FAT"))
                    objTr.SNF = clsCommon.myCdbl(dr("SNF"))

                    objTr.Asset_Code = clsCommon.myCstr(dr("Asset_Code"))
                    objTr.price = clsCommon.myCdbl(dr("price"))
                    objTr.DiscountPer = clsCommon.myCdbl(dr("DiscountPer"))
                    objTr.DiscountAmt = clsCommon.myCdbl(dr("DiscountAmt"))
                    objTr.TotalTaxAmt = clsCommon.myCdbl(dr("TotalTaxAmt"))
                    objTr.NetPriceAmt = clsCommon.myCdbl(dr("NetPriceAmt"))
                    objTr.ItemAmt = clsCommon.myCdbl(dr("ItemAmt"))
                    objTr.TotalDiscountAmt = clsCommon.myCdbl(dr("TotalDiscountAmt"))
                    objTr.ItemNetAmt = clsCommon.myCdbl(dr("ItemNetAmt"))
                    objTr.TotalAmt = clsCommon.myCdbl(dr("TotalAmt"))

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
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.arrBatchItem = clsBatchInventory.GetData("ScrapIn", obj.strInvoiceNo, objTr.Item_Code, objTr.Line_No, trans)
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

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim GSTStatus As Boolean = False
        Try
            Dim isSaved As Boolean = True
            Dim isscrap As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Shipment No not found to Post")
            End If
            Dim obj As ClsScrapSaleHead = ClsScrapSaleHead.GetData(strDocNo, NavigatorType.Current, trans, False)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.shipment_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.JobWorkDispatchProduction, obj.Loc_Code, obj.shipment_Date, trans)
            If (obj.ispost = 1) Then
                Throw New Exception("Already Post on :" + obj.posting_Date)
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Shipment No " + obj.shipment_No + " Is On Hold.Can't Post it")
            End If
            GSTStatus = clsERPFuncationality.GetGSTStatus(obj.shipment_Date)
            If (obj.Is_Taxable = True) AndAlso (clsCommon.myLen(obj.Tax_Group) <= 0) Then
                Throw New Exception("Tax Group not found :" + "Shipment No " + obj.shipment_No)
            End If
            If GSTStatus = True AndAlso obj.Is_Taxable = True Then
                clsLocationWiseTax.IsValidTaxGroup(obj.Tax_Group, obj.Loc_Code, obj.cust_Code, "S", obj.shipment_Date, trans)
            End If
            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                Dim dblBalQty As Decimal = 0
                Dim dblEnteredQty As Decimal = 0
                For Each objTr As ClsScrapSaleDetail In obj.Arr
                    dblEnteredQty = objTr.shipped_Qty
                    dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                    dblBalQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(objTr.Item_Code, obj.Loc_Code, obj.shipment_No, obj.shipment_Date, trans, objTr.Unit_Code)
                    If dblBalQty < dblEnteredQty Then
                        Throw New Exception("Item - " + clsCommon.myCstr(objTr.Item_Code) + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                    End If
                Next
            End If
            Dim qry As String = ""
            If obj.is_Asset_Type Then
                qry = "Update TSPL_SCRAPSALE_HEAD set ispost=1, Posting_Date='" + clsCommon.GetPrintDate(obj.shipment_Date, "dd/MMM/yyyy hh:mm tt") + "',Modify_By='" + objCommonVar.CurrentUserCode + "'"
                qry += " where Shipment_No='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "Update TSPL_SCRAPINVOICE_HEAD set ispost=1, Posting_Date='" + clsCommon.GetPrintDate(obj.shipment_Date, "dd/MMM/yyyy hh:mm tt ") + "',Modify_By='" + objCommonVar.CurrentUserCode + "'"
                qry += " where invoice_No='" + obj.strInvoiceNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                trans.Commit()
                Return True
            End If
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim IsRejectedItemFound As Boolean = False
            Dim totDrAmt As Double = 0
            Dim totCrAmt As Double = 0
            If clsCommon.myLen(obj.NRG_No) <= 0 Then
                ClsScrapInvoiceHead.PostData(obj.strInvoiceNo, True, trans)
            Else
                ClsScrapInvoiceHead.PostData(obj.strInvoiceNo, False, trans)
            End If

            qry = "Update TSPL_SCRAPSALE_HEAD set ispost=1, Posting_Date='" + clsCommon.GetPrintDate(obj.shipment_Date, "dd/MMM/yyyy hh:mm tt") + "',Modify_By='" + objCommonVar.CurrentUserCode + "'"
            qry += " where Shipment_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_SCRAPSALE_HEAD", "shipment_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.strInvoiceNo), "TSPL_SCRAPINVOICE_HEAD", "invoice_No", trans)
            If objCommonVar.InternalSMSEmailinPurchaseModule = True Then
                CreateInternalEmailSMS_ScrapSale(obj, trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Sub CreateInternalEmailSMS_ScrapSale(ByVal obj As ClsScrapSaleHead, ByVal trans As SqlTransaction)
        Dim itemName As String = ""
        Dim UOM As String = ""
        Dim qty As String = ""
        Dim ItemDetail As String = ""
        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.ScrapSale + "2" + "'", trans)

        Dim qry As String = "select TSPL_USER_MASTER.User_Code from TSPL_USER_MASTER "
        qry += " left join tspl_scrapsale_head on tspl_scrapsale_head.Created_By=TSPL_USER_MASTER.User_Code  "
        qry += " where tspl_scrapsale_head.shipment_no='" + obj.shipment_No + "'"
        Dim StrUserCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Dim arrMobileNo As New List(Of String)
        Dim arrMailID As List(Of String) = clsERPFuncationality.ReportingMailIdandPhone(StrUserCode, arrMobileNo, trans)

        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 AndAlso ((arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Or (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0)) Then


            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                For Each objdetail As ClsScrapSaleDetail In obj.Arr

                    itemName = clsCommon.myCstr(objdetail.Item_Desc)
                    UOM = clsCommon.myCstr(objdetail.Unit_Code)
                    qty = clsCommon.myCstr(objdetail.shipped_Qty)

                    ItemDetail += itemName + " " + UOM + "-" + qty + Environment.NewLine

                Next
            End If

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso (arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Then
                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.arrEMail = arrMailID

                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))

                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.shipment_No)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.shipment_Date, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.Doc_Amount, clsCommon.myFormat(obj.ship_Total_Amt))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.CustomerNo, obj.cust_Code)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.CustomerName, obj.cust_Name)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

                objEmailH.SaveData(clsUserMgtCode.ScrapSale, objEmailH, trans)
                objEmailH = Nothing

            End If

            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 AndAlso (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0) Then
                Dim objSMSH As New clsSMSHead()
                objSMSH.arrMobilNo = New List(Of String)()
                objSMSH.arrMobilNo = arrMobileNo

                objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.shipment_No)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.shipment_Date, "dd/MMM/yyyy"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.Doc_Amount, clsCommon.myFormat(obj.ship_Total_Amt))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.CustomerNo, obj.cust_Code)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.CustomerName, obj.cust_Name)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

                objSMSH.SaveData(clsUserMgtCode.ScrapSale, objSMSH, trans)
                objSMSH = Nothing
            End If
        End If


    End Sub

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("ShipmentNo not found to Delete")
        End If
        Dim obj As ClsScrapSaleHead = ClsScrapSaleHead.GetData(strCode, NavigatorType.Current)
        Dim frm As New FrmFreeTxtBox1
        frm.Text = "Remarks for Delete"
        frm.ShowDialog()
        If clsCommon.myLen(frm.strRmks) <= 0 Then
            Return False
        End If


        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.shipment_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.JobWorkDispatchProduction, obj.Loc_Code, obj.shipment_Date, trans)

                If (obj.ispost = 1) Then
                    Throw New Exception("Already Posted on :" + obj.posting_Date)
                End If

                'done by stuti on 15/12/2016
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.strInvoiceNo, "TSPL_SCRAPINVOICE_HEAD", "invoice_no", "TSPL_SCRAPINVOICE_DETAIL", "invoice_no", trans)
                '=====end here===========

                '------------Saving Data in cancel tables
                'Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_SCRAPINVOICE_HEAD", trans)
                'Dim qry As String = "INSERT INTO TSPL_SCRAPINVOICE_HEAD_CANCEL(" + strInvColumns + ",Cancel_By,Cancel_Date,Cancel_Remarks) SELECT " + strInvColumns + ",'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + frm.strRmks + "' FROM TSPL_SCRAPINVOICE_HEAD WHERE invoice_No='" + obj.strInvoiceNo + "'"
                'clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'qry = "INSERT INTO TSPL_SCRAPINVOICE_DETAIL_CANCEL SELECT * FROM TSPL_SCRAPINVOICE_DETAIL WHERE invoice_No='" + obj.strInvoiceNo + "'"
                'clsDBFuncationality.ExecuteNonQuery(qry, trans)
                '------------End of Saving Data in cancel tables
                HistoryUpdate(strCode, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.strInvoiceNo), "TSPL_SCRAPINVOICE_HEAD", "invoice_No", "TSPL_SCRAPINVOICE_DETAIL", "invoice_No", trans)
                Dim qry As String = "delete from TSPL_SCRAPSALE_DETAIL where shipment_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from tspl_scrapsale_head where shipment_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SCRAPINVOICE_DETAIL where invoice_No='" + obj.strInvoiceNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SCRAPINVOICE_HEAD where invoice_No='" + obj.strInvoiceNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)

                clsBatchInventory.DeleteData("ScrapIn", obj.strInvoiceNo, trans)

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

    Public Shared Function IsValidVendorForSRN(ByVal Arr As List(Of String), ByVal strVendorCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select SRN_No,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD where SRN_No in (" + clsCommon.GetMulcallString(Arr) + ") and Vendor_Code not in ('" + strVendorCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "SRN No:" + clsCommon.myCstr(dr("SRN_No")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Vendor_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Vendor_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function GetVehicleDesc(ByVal strVehicleId As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "Select Description from TSPL_GL_SEGMENT_CODE where Seg_No=2 AND Segment_code='" + strVehicleId + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    '=====Sanjeet (Round Off Calculation)===

    Public Shared Function Calculate_RoundOffAmt(ByVal DocumentAmt As Decimal, ByVal trans As SqlTransaction) As List(Of Decimal)
        Dim LstDecml As New List(Of Decimal)
        Try
            Dim proundoff As Decimal = 0
            Dim amt As Decimal = 0

            If DocumentAmt > 0 Then
                Dim strPreAmt As Decimal = 0
                Dim strPostAmt As Decimal = 0

                If clsCommon.myCstr(DocumentAmt).Contains(".") Then
                    strPreAmt = clsCommon.myCdbl(clsCommon.myCstr(DocumentAmt).Substring(0, clsCommon.myCstr(DocumentAmt).IndexOf(".")))
                    strPostAmt = clsCommon.myCdbl(DocumentAmt - strPreAmt) * 100
                Else
                    strPreAmt = DocumentAmt
                    strPostAmt = 0
                End If

                If strPostAmt >= 50 Then
                    strPreAmt = strPreAmt + 1
                    ' strPostAmt = System.Math.Round((strPostAmt - 100) / 100, 2)
                    strPostAmt = System.Math.Round((100 - strPostAmt) / 100, 2)

                    LstDecml.Add(strPreAmt)
                    LstDecml.Add(strPostAmt)
                ElseIf strPostAmt < 50 Then
                    strPreAmt = strPreAmt
                    'strPostAmt = System.Math.Round(strPostAmt / 100, 2)
                    strPostAmt = System.Math.Round((strPostAmt * -1) / 100, 2)

                    LstDecml.Add(strPreAmt)
                    LstDecml.Add(strPostAmt)
                End If
            End If

            Return LstDecml
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    '===================

    Public Shared Function UpdateAfterPosting(ByVal obj As ClsScrapSaleHead, ByVal ShipmentNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(ShipmentNo) > 0 Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "EWayBillNo", obj.EWayBillNo)
                If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", Nothing, True)
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPINVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_SCRAPINVOICE_HEAD.shipment_No='" + ShipmentNo + "'", trans)
                clsCommon.AddColumnsForChange(coll, "Electronic_Ref_No", obj.Electronic_Ref_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPSALE_HEAD", OMInsertOrUpdate.Update, "TSPL_SCRAPSALE_HEAD.shipment_No='" + ShipmentNo + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class ClsScrapSaleDetail
#Region "Variables"
    Public ItemwiseTaxCode As String = Nothing
    Public document_No As String = Nothing
    Public shipment_No As String = Nothing
    Public Specification As String = Nothing
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public shipped_Qty As Double = 0
    Public ItemRate As Double = 0
    Public price As Double = 0
    Public balance_Qty As Double = 0
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
    Public pending_Qty As Double = 0
    Public Unit_Code As String = ""
    Public Asset_Code As String = ""

    Public FAT As Double = 0
    Public SNF As Double = 0

    Public arrBatchItem As List(Of clsBatchInventory) = Nothing

#End Region


    Public Shared Function FinderItem(ByVal strCode As String, ByVal strItemType As String, ByVal isButtonClicked As Boolean) As ClsScrapSaleDetail
        Dim obj As ClsScrapSaleDetail = Nothing
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name ,TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category],TSPL_ITEM_MASTER.ITF_CODE as [ITF CODE] from  TSPL_ITEM_MASTER"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
        qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "

        Dim WhrCls As String = ""
        If clsCommon.myLen(strItemType) > 0 Then
            WhrCls = "Item_Type<>'" + strItemType + "'"
        End If
        strCode = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc,Unit_Code,cost from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New ClsScrapSaleDetail()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                obj.price = clsCommon.myCdbl(dt.Rows(0)("cost"))


            End If
        End If
        Return obj
    End Function

    Public Shared Function FinderItemGST(ByVal strCode As String, ByVal strItemType As String, ByVal isButtonClicked As Boolean, ByVal transDate As Date, ByVal isTaxable As Boolean) As ClsScrapSaleDetail
        Dim obj As ClsScrapSaleDetail = Nothing
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name ,TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category],TSPL_ITEM_MASTER.ITF_CODE as [ITF CODE] from  TSPL_ITEM_MASTER"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
        qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "

        Dim WhrCls As String = " 2=2 "
        If clsCommon.myLen(strItemType) > 0 Then
            WhrCls += " and Item_Type<>'" + strItemType + "'"
        End If
        If clsERPFuncationality.GetGSTStatus(transDate) Then
            WhrCls += " and TSPL_ITEM_MASTER.IsTaxable='" + IIf(isTaxable, "1", "0") + "'"

        End If

        strCode = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc,Unit_Code,cost from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New ClsScrapSaleDetail()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                obj.price = clsCommon.myCdbl(dt.Rows(0)("cost"))
            End If
        End If
        Return obj
    End Function

    Public Shared Function FinderItemwithBalanceQty(ByVal strCode As String, ByVal strItemType As String, ByVal isButtonClicked As Boolean, ByVal strLocation As String) As ClsScrapSaleDetail
        Dim obj As ClsScrapSaleDetail = Nothing
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name ,TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category],isnull(a.Qty,0) as AvailableQty,TSPL_ITEM_MASTER.ITF_CODE as [ITF Code] from  TSPL_ITEM_MASTER"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
        qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "

        Dim WhrCls As String = ""
        If clsCommon.myLen(strItemType) > 0 Then
            WhrCls = "Item_Type<>'" + strItemType + "'"
        End If
        Dim strBalance = clsItemLocationDetails.getBalanceWithUnapproveForRMOtherforFinder("", strLocation, "", clsCommon.GETSERVERDATE(), Nothing)
        qry = qry & "Left outer join ( " & strBalance & " ) a on TSPL_ITEM_MASTER.Item_Code=a.ICode"
        strCode = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc,Unit_Code,cost from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New ClsScrapSaleDetail()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                obj.price = clsCommon.myCdbl(dt.Rows(0)("cost"))


            End If
        End If
        Return obj
    End Function

    Public Shared Function FinderItemwithBalanceQtyLocationWise(ByVal strCode As String, ByVal strItemType As String, ByVal isButtonClicked As Boolean, ByVal strLocation As String) As ClsScrapSaleDetail
        Dim obj As ClsScrapSaleDetail = Nothing
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name ,TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category],isnull(a.Qty,0) as AvailableQty,TSPL_ITEM_MASTER.ITF_CODE as [ITF Code] from  TSPL_ITEM_MASTER"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
        qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "

        Dim WhrCls As String = ""
        If clsCommon.myLen(strItemType) > 0 Then
            WhrCls = "Item_Type<>'" + strItemType + "'"
        End If
        Dim strBalance = clsItemLocationDetails.getBalanceWithUnapproveForRMOtherforFinder("", strLocation, "", clsCommon.GETSERVERDATE(), Nothing)
        qry = qry & "Inner join ( " & strBalance & " ) a on TSPL_ITEM_MASTER.Item_Code=a.ICode"
        strCode = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc,Unit_Code,cost from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New ClsScrapSaleDetail()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                obj.price = clsCommon.myCdbl(dt.Rows(0)("cost"))
                obj.balance_Qty = clsCommon.myCdbl(clsItemLocationDetails.getBalanceWithUnapprove(obj.Item_Code, strLocation, Nothing, obj.Unit_Code, Nothing, clsCommon.GETSERVERDATE()))
            End If
        End If
        Return obj
    End Function

    Public Shared Function FinderItemBoth(ByVal strCode As String, ByVal strItemType As String, ByVal isButtonClicked As Boolean) As ClsScrapSaleDetail
        Dim obj As ClsScrapSaleDetail = Nothing
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name ,TSPL_Item_Category.Category_Name as [Item Category] ,"
        qry += " TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category],TSPL_ITEM_MASTER.ITF_CODE as [ITF Code] from  TSPL_ITEM_MASTER"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
        qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "

        Dim WhrCls As String = ""
        If clsCommon.myLen(strItemType) > 0 Then
            If strItemType = "F" Then
                WhrCls = "Item_Type='F'  and TSPL_ITEM_MASTER.Active=1"
            Else
                WhrCls = "Item_Type<>'F'  and TSPL_ITEM_MASTER.Active=1"
            End If


        End If
        strCode = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc,Unit_Code,cost from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New ClsScrapSaleDetail()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                obj.price = clsCommon.myCdbl(dt.Rows(0)("cost"))


            End If
        End If
        Return obj
    End Function

    Public Shared Function FinderAdditional(ByVal strCode As String, ByVal isButtonClicked As Boolean) As ClsScrapSaleDetail
        Dim obj As ClsScrapSaleDetail = Nothing
        Dim qry As String = "select Code ,description from TSPL_Additional_Charges"
        Dim WhrCls As String = ""

        strCode = clsCommon.ShowSelectForm("Additiona charges", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Code,description from TSPL_Additional_Charges where Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New ClsScrapSaleDetail()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))

            End If
        End If
        Return obj
    End Function


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsScrapSaleDetail), ByVal trans As SqlTransaction, ByVal dtDocDate As DateTime, ByVal strlocation As String, ByVal strinvno As String) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsScrapSaleDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "shipment_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "shipped_Qty", obj.shipped_Qty)
                clsCommon.AddColumnsForChange(coll, "ItemRate", obj.ItemRate)
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
                clsCommon.AddColumnsForChange(coll, "pending_Qty", obj.pending_Qty)
                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Asset_Code", obj.Asset_Code, True)

                clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                clsCommon.AddColumnsForChange(coll, "ItemwiseTaxCode", obj.ItemwiseTaxCode)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPSALE_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                'clsBatchInventory.SaveData("ScrapIn", strinvno, dtDocDate, "O", obj.Item_Code, strlocation, obj.Line_No, 0, obj.Unit_Code, obj.arrBatchItem, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceSRNQty(ByVal strSRNCode As String, ByVal strICode As String, ByVal strCurrPINNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.SRN_Qty as Qty,1 as RI from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.Status=0 and TSPL_SRN_HEAD.Status=1 and TSPL_SRN_DETAIL.SRN_No ='" + strSRNCode + "' and TSPL_SRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_SRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_SRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_SRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' " & _
            " union all " & _
            " select TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.PI_Qty as Qty,-1 as RI from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_DETAIL.SRN_Id='" + strSRNCode + "'   and TSPL_PI_DETAIL.Item_Code='" + strICode + "'  and  TSPL_PI_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_PI_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_PI_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'  and TSPL_PI_DETAIL.PI_No not in ('" + strCurrPINNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompleteSRN(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_SRN_DETAIL set Status=1 where SRN_No='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function

End Class