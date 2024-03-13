'********** 02:57 PM 07/11/2012
Imports common
Imports System.Data.SqlClient
Imports System.Data


Public Class ClsScrapInvoiceHead


#Region "Variables"
    Public shipment_No As String = Nothing
    Public invoice_No As String = Nothing
    Public Status As String = Nothing
    Public Po_No As String = Nothing
    Public cust_Code As String = Nothing
    Public cust_Name As String = Nothing
    Public shipment_Date As Date
    Public Is_Taxable As Integer = 0
    Public posting_Date As String = Nothing
    Public expship_Date As String = Nothing
    Public Loc_Code As String = Nothing
    Public Loc_Name As String = Nothing
    Public ToLoc_Code As String = Nothing
    '--------------added by usha
    Public LocationAR As String = Nothing


    Public EWayBillDate As DateTime? = Nothing
    Public EwayBillValidDate As DateTime? = Nothing
    Public EwayBillRemarks As String = Nothing
    Public EWayBillNo As String = Nothing
    Public EInvoiceIRNNo As String = Nothing
    Public EInvoiceAckNo As String = Nothing
    Public EInvoiceAckDate As DateTime? = Nothing
    Public EInvoiceQRCode As String = Nothing
    '--------
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
    Public Balance_Amt As Double = 0

    Public Terms_Code As String = Nothing
    Public Due_Date As String = Nothing
    Public Inter_Branch As Boolean = False
    Private isNewEntry As Boolean = False
    'Public ArrIn As List(Of clsCustomerInvoiceDetail) = Nothing
    Public Doc_Type As String = Nothing
    'Public ispost As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Arr As List(Of ClsScrapInvoiceDetail) = Nothing
    Public RoundOffAmount As Double = 0


#End Region

    Public Function SaveData(ByVal obj As ClsScrapInvoiceHead, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As ClsScrapInvoiceHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = True


        Try
            Dim qry As String = "delete from TSPL_SCRAPINVOICE_DETAIL where invoice_No='" + obj.invoice_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            'If isNewEntry Then
            '    obj.invoice_No = clsERPFuncationality.GetNextCode(trans, clsDocType.ScrapInvoice, "", obj.Loc_Code)
            'End If


            If isNewEntry Then
                Dim strlocation As String = "select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + obj.Loc_Code + "'"
                Dim chk As String = ""
                Dim transType As String = clsDocTransactionType.SaleInvoiceExcise
                chk = clsDBFuncationality.getSingleValue(strlocation, trans)
                If chk = "T" Then
                    obj.invoice_No = clsERPFuncationality.GetNextCode(trans, obj.shipment_Date, clsDocType.SaleInvoice, transType, obj.Loc_Code)

                Else
                    obj.invoice_No = clsERPFuncationality.GetNextCode(trans, obj.shipment_Date, clsDocType.ScrapInvoice, "", obj.Loc_Code)
                End If

            End If


            If (clsCommon.myLen(obj.invoice_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "shipment_No", obj.shipment_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Po_No", obj.Po_No)
            clsCommon.AddColumnsForChange(coll, "cust_Code", obj.cust_Code)
            clsCommon.AddColumnsForChange(coll, "cust_Name", obj.cust_Name)
            clsCommon.AddColumnsForChange(coll, "shipment_Date", clsCommon.GetPrintDate(obj.shipment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "posting_Date", clsCommon.GetPrintDate(obj.posting_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "expship_Date", clsCommon.GetPrintDate(obj.expship_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Loc_Name", obj.Loc_Name)
            clsCommon.AddColumnsForChange(coll, "ToLoc_Code", obj.ToLoc_Code)
            'clsCommon.AddColumnsForChange(coll, "CreateInvoice", obj.CreateInvoice)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
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
            clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.doc_Amt)

            clsCommon.AddColumnsForChange(coll, "Inter_Branch", IIf(obj.Inter_Branch = 1, True, False))




            'clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            ' clsCommon.AddColumnsForChange(coll, "Discount_Base", obj.Discount_Base)
            'clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
            'clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)

            ''clsCommon.AddColumnsForChange(coll, "TotalUnit_Cost_Tax", obj.TotalUnit_Cost_Tax)
            'clsCommon.AddColumnsForChange(coll, "Transport_code", obj.Transporter_code)
            'clsCommon.AddColumnsForChange(coll, "Vehicle_code", obj.Vehicle_code)
            'clsCommon.AddColumnsForChange(coll, "Vehicle_Id", obj.Vehicle_Id)
            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If


            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))






            If isNewEntry Then

                clsCommon.AddColumnsForChange(coll, "invoice_No", obj.invoice_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPINVOICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPINVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_SCRAPINVOICE_HEAD.invoice_No='" + obj.invoice_No + "'", trans)
            End If


            isSaved = isSaved AndAlso ClsScrapInvoiceDetail.SaveData(obj.invoice_No, Arr, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As ClsScrapInvoiceHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsScrapInvoiceHead
        Dim obj As ClsScrapInvoiceHead = Nothing
        Dim qry As String = "SELECT TSPL_SCRAPINVOICE_HEAD.Is_Taxable,TSPL_SCRAPINVOICE_HEAD.Doc_Type,TSPL_SCRAPINVOICE_HEAD.Inter_Branch, TSPL_SCRAPINVOICE_HEAD.shipment_No,TSPL_SCRAPINVOICE_HEAD.shipment_Date,TSPL_SCRAPINVOICE_HEAD.cust_Code,TSPL_SCRAPINVOICE_HEAD.Po_No,TSPL_SCRAPINVOICE_HEAD.invoice_No,TSPL_SCRAPINVOICE_HEAD.cust_Name,TSPL_SCRAPINVOICE_HEAD.shipment_date,TSPL_SCRAPINVOICE_HEAD.posting_date,TSPL_SCRAPINVOICE_HEAD.expship_date,TSPL_SCRAPINVOICE_HEAD.Status,TSPL_SCRAPINVOICE_HEAD.CreateInvoice,TSPL_SCRAPINVOICE_HEAD.ispost,TSPL_SCRAPINVOICE_HEAD.Doc_Amt,TSPL_SCRAPINVOICE_HEAD.Description,TSPL_SCRAPINVOICE_HEAD.reff,TSPL_SCRAPINVOICE_HEAD.Tax_Group,TSPL_SCRAPINVOICE_HEAD.tax_desc,TSPL_SCRAPINVOICE_HEAD.loc_code,TSPL_SCRAPINVOICE_HEAD.loc_Name,TSPL_SCRAPINVOICE_HEAD.ToLoc_code,TSPL_SCRAPINVOICE_HEAD.TAX1,TSPL_SCRAPINVOICE_HEAD.TAX1_Rate,TSPL_SCRAPINVOICE_HEAD.TAX1_Amt,TSPL_SCRAPINVOICE_HEAD.TAX1_Base_Amt,TSPL_SCRAPINVOICE_HEAD.TAX2,TSPL_SCRAPINVOICE_HEAD.TAX2_Rate,TSPL_SCRAPINVOICE_HEAD.TAX2_Amt,TSPL_SCRAPINVOICE_HEAD.TAX2_Base_Amt,TSPL_SCRAPINVOICE_HEAD.TAX3,TSPL_SCRAPINVOICE_HEAD.TAX3_Rate,TSPL_SCRAPINVOICE_HEAD.TAX3_Amt,TSPL_SCRAPINVOICE_HEAD.TAX3_Base_Amt,TSPL_SCRAPINVOICE_HEAD.TAX4,TSPL_SCRAPINVOICE_HEAD.TAX4_Rate,TSPL_SCRAPINVOICE_HEAD.TAX4_Amt,TSPL_SCRAPINVOICE_HEAD.TAX4_Base_Amt,TSPL_SCRAPINVOICE_HEAD.TAX5,TSPL_SCRAPINVOICE_HEAD.TAX5_Rate,TSPL_SCRAPINVOICE_HEAD.TAX5_Amt,TSPL_SCRAPINVOICE_HEAD.TAX5_Base_Amt,TSPL_SCRAPINVOICE_HEAD.TAX6,TSPL_SCRAPINVOICE_HEAD.TAX6_Rate,TSPL_SCRAPINVOICE_HEAD.TAX6_Amt,TSPL_SCRAPINVOICE_HEAD.TAX6_Base_Amt,TSPL_SCRAPINVOICE_HEAD.TAX7,TSPL_SCRAPINVOICE_HEAD.TAX7_Rate,TSPL_SCRAPINVOICE_HEAD.TAX7_Amt,TSPL_SCRAPINVOICE_HEAD.TAX7_Base_Amt,TSPL_SCRAPINVOICE_HEAD.TAX8,TSPL_SCRAPINVOICE_HEAD.TAX8_Rate,TSPL_SCRAPINVOICE_HEAD.TAX8_Amt,TSPL_SCRAPINVOICE_HEAD.TAX8_Base_Amt,TSPL_SCRAPINVOICE_HEAD.TAX9,TSPL_SCRAPINVOICE_HEAD.TAX9_Rate,TSPL_SCRAPINVOICE_HEAD.TAX9_Amt,TSPL_SCRAPINVOICE_HEAD.TAX9_Base_Amt,TSPL_SCRAPINVOICE_HEAD.TAX10,TSPL_SCRAPINVOICE_HEAD.TAX10_Rate,TSPL_SCRAPINVOICE_HEAD.TAX10_Amt,TSPL_SCRAPINVOICE_HEAD.TAX10_Base_Amt,TSPL_SCRAPINVOICE_HEAD.Add_Amt,TSPL_SCRAPINVOICE_HEAD.Before_add_Amt,TSPL_SCRAPINVOICE_HEAD.Discount_Base,TSPL_SCRAPINVOICE_HEAD.Discount_Amt,TSPL_SCRAPINVOICE_HEAD.Amount_Less_Discount,TSPL_SCRAPINVOICE_HEAD.Total_Tax_Amt,TSPL_SCRAPINVOICE_HEAD.ship_total_Amt,TSPL_SCRAPINVOICE_HEAD.Comp_Code,TSPL_SCRAPINVOICE_HEAD.Terms_Code,TSPL_SCRAPINVOICE_HEAD.Due_Date ,TSPL_SCRAPINVOICE_HEAD.AddCode1,TSPL_SCRAPINVOICE_HEAD.AddDesc1,TSPL_SCRAPINVOICE_HEAD.AddAmt1,TSPL_SCRAPINVOICE_HEAD.AddCode2,TSPL_SCRAPINVOICE_HEAD.AddDesc2,TSPL_SCRAPINVOICE_HEAD.AddAmt2,TSPL_SCRAPINVOICE_HEAD.AddCode3,TSPL_SCRAPINVOICE_HEAD.AddDesc3,TSPL_SCRAPINVOICE_HEAD.AddAmt3,TSPL_SCRAPINVOICE_HEAD.AddCode4,TSPL_SCRAPINVOICE_HEAD.AddDesc4,TSPL_SCRAPINVOICE_HEAD.AddAmt4,TSPL_SCRAPINVOICE_HEAD.AddCode5,TSPL_SCRAPINVOICE_HEAD.AddDesc5,TSPL_SCRAPINVOICE_HEAD.AddAmt5,TSPL_SCRAPINVOICE_HEAD.AddCode6,TSPL_SCRAPINVOICE_HEAD.AddDesc6,TSPL_SCRAPINVOICE_HEAD.AddAmt6,TSPL_SCRAPINVOICE_HEAD.AddCode7,TSPL_SCRAPINVOICE_HEAD.AddDesc7,TSPL_SCRAPINVOICE_HEAD.AddAmt7,TSPL_SCRAPINVOICE_HEAD.AddCode8,TSPL_SCRAPINVOICE_HEAD.AddDesc8,TSPL_SCRAPINVOICE_HEAD.AddAmt8,TSPL_SCRAPINVOICE_HEAD.AddCode9,TSPL_SCRAPINVOICE_HEAD.AddDesc9,TSPL_SCRAPINVOICE_HEAD.AddAmt9,TSPL_SCRAPINVOICE_HEAD.AddCode10,TSPL_SCRAPINVOICE_HEAD.AddDesc10,TSPL_SCRAPINVOICE_HEAD.AddAmt10,TSPL_SCRAPINVOICE_HEAD.Balance_Amt,TSPL_LOCATION_MASTER.Location_Desc as ToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_SCRAPINVOICE_HEAD.RoundOffAmount,TSPL_SCRAPINVOICE_HEAD.IRN_No,TSPL_SCRAPINVOICE_HEAD.Ack_Date,TSPL_SCRAPINVOICE_HEAD.Ack_No,TSPL_SCRAPINVOICE_HEAD.QR_Code,TSPL_SCRAPINVOICE_HEAD.EWayBillNo,TSPL_SCRAPINVOICE_HEAD.EWayBillDate,TSPL_SCRAPINVOICE_HEAD.EWayBillValidDate,TSPL_SCRAPINVOICE_HEAD.EWayBillRemarks FROM TSPL_SCRAPINVOICE_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPINVOICE_HEAD.loc_code left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SCRAPINVOICE_HEAD.loc_code  left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_SCRAPINVOICE_HEAD.Terms_Code where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SCRAPINVOICE_HEAD.invoice_No = (select MIN(invoice_No) from TSPL_SCRAPINVOICE_HEAD)"
            Case NavigatorType.Last
                qry += " and TSPL_SCRAPINVOICE_HEAD.invoice_No = (select Max(invoice_No) from TSPL_SCRAPINVOICE_HEAD)"
            Case NavigatorType.Current
                qry += " and TSPL_SCRAPINVOICE_HEAD.invoice_No = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_SCRAPINVOICE_HEAD.invoice_No = (select Min(invoice_No) from TSPL_SCRAPINVOICE_HEAD where invoice_No>'" + strPONo + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_SCRAPINVOICE_HEAD.invoice_No = (select Max(invoice_No) from TSPL_SCRAPINVOICE_HEAD where invoice_No<'" + strPONo + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsScrapInvoiceHead()
            'If Not IsDBNull(dt.Rows(0)("EWayBillDate")) Then
            '    obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            'End If

            'obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            'If Not IsDBNull(dt.Rows(0)("EwayBillValidDate")) Then
            '    obj.EwayBillValidDate = clsCommon.myCDate(dt.Rows(0)("EwayBillValidDate"))
            'End If

            'obj.EwayBillRemarks = clsCommon.myCstr(dt.Rows(0)("EwayBillRemarks"))
            'obj.EInvoiceIRNNo = clsCommon.myCstr(dt.Rows(0)("IRN_No"))
            'obj.EInvoiceAckNo = clsCommon.myCstr(dt.Rows(0)("Ack_No"))
            'If Not IsDBNull(dt.Rows(0)("Ack_Date")) Then
            '    obj.EInvoiceAckDate = clsCommon.myCDate(dt.Rows(0)("Ack_Date"))
            'End If
            'obj.EInvoiceQRCode = clsCommon.myCstr(dt.Rows(0)("QR_COde"))
            obj.shipment_No = clsCommon.myCstr(dt.Rows(0)("shipment_No"))
            obj.invoice_No = clsCommon.myCstr(dt.Rows(0)("invoice_No"))
            obj.Po_No = clsCommon.myCstr(dt.Rows(0)("Po_No"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.ispost = clsCommon.myCstr(dt.Rows(0)("ispost"))
            obj.cust_Code = clsCommon.myCstr(dt.Rows(0)("cust_Code"))
            obj.cust_Name = clsCommon.myCstr(dt.Rows(0)("cust_Name"))
            obj.shipment_Date = clsCommon.myCDate(dt.Rows(0)("shipment_Date"))
            obj.posting_Date = clsCommon.myCDate(dt.Rows(0)("posting_Date"))
            obj.expship_Date = clsCommon.myCDate(dt.Rows(0)("expship_Date"))
            obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.Loc_Name = clsCommon.myCstr(dt.Rows(0)("Loc_Name"))
            obj.ToLoc_Code = clsCommon.myCstr(dt.Rows(0)("ToLoc_Code"))
            '-----------added by usha----
            obj.LocationAR = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.Loc_Code + "'", trans)
            '--------------ends  
            'obj.CreateInvoice = clsCommon.myCstr(dt.Rows(0)("CreateInvoice"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.reff = clsCommon.myCstr(dt.Rows(0)("reff"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.Tax_Desc = clsCommon.myCstr(dt.Rows(0)("Tax_Desc"))
            obj.Add_Amt = clsCommon.myCdbl(dt.Rows(0)("Add_Amt"))
            obj.Before_Add_Amt = clsCommon.myCdbl(dt.Rows(0)("Before_Add_Amt"))
            obj.Discount_Base = clsCommon.myCdbl(dt.Rows(0)("Discount_Base"))
            obj.Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("Discount_Amt"))
            obj.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("Amount_Less_Discount"))
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.ship_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("ship_Total_Amt"))
            obj.doc_Amt = clsCommon.myCdbl(dt.Rows(0)("Doc_Amt"))
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            obj.Is_Taxable = clsCommon.myCdbl(dt.Rows(0)("Is_Taxable"))
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
            obj.Balance_Amt = clsCommon.myCdbl(dt.Rows(0)("Balance_Amt"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))

            obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))

            qry = "SELECT TSPL_SCRAPINVOICE_DETAIL.invoice_No,TSPL_SCRAPINVOICE_DETAIL.Line_No,TSPL_SCRAPINVOICE_DETAIL.Row_Type,TSPL_SCRAPINVOICE_DETAIL.Item_Code,TSPL_SCRAPINVOICE_DETAIL.Item_Desc,TSPL_SCRAPINVOICE_DETAIL.Unit_code,TSPL_SCRAPINVOICE_DETAIL.Shipped_Qty,TSPL_SCRAPINVOICE_DETAIL.invoice_Qty,TSPL_SCRAPINVOICE_DETAIL.pending_Qty,TSPL_SCRAPINVOICE_DETAIL.price,TSPL_SCRAPINVOICE_DETAIL.DiscountPer,TSPL_SCRAPINVOICE_DETAIL.DiscountAmt,TSPL_SCRAPINVOICE_DETAIL.TotalTaxAmt,TSPL_SCRAPINVOICE_DETAIL.NetPriceAmt,TSPL_SCRAPINVOICE_DETAIL.ItemAmt,TSPL_SCRAPINVOICE_DETAIL.TotalDiscountAmt,TSPL_SCRAPINVOICE_DETAIL.ItemNetAmt,TSPL_SCRAPINVOICE_DETAIL.TotalAmt,TSPL_SCRAPINVOICE_DETAIL.TAX1,TSPL_SCRAPINVOICE_DETAIL.TAX1_Rate,TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX2,TSPL_SCRAPINVOICE_DETAIL.TAX2_Rate,TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX3,TSPL_SCRAPINVOICE_DETAIL.TAX3_Rate,TSPL_SCRAPINVOICE_DETAIL.TAX3_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX4,TSPL_SCRAPINVOICE_DETAIL.TAX4_Rate,TSPL_SCRAPINVOICE_DETAIL.TAX4_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX5,TSPL_SCRAPINVOICE_DETAIL.TAX5_Rate,TSPL_SCRAPINVOICE_DETAIL.TAX5_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX6,TSPL_SCRAPINVOICE_DETAIL.TAX6_Rate,TSPL_SCRAPINVOICE_DETAIL.TAX6_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX7,TSPL_SCRAPINVOICE_DETAIL.TAX7_Rate,TSPL_SCRAPINVOICE_DETAIL.TAX7_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX8,TSPL_SCRAPINVOICE_DETAIL.TAX8_Rate,TSPL_SCRAPINVOICE_DETAIL.TAX8_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX9,TSPL_SCRAPINVOICE_DETAIL.TAX9_Rate,TSPL_SCRAPINVOICE_DETAIL.TAX9_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX10,TSPL_SCRAPINVOICE_DETAIL.TAX10_Rate,TSPL_SCRAPINVOICE_DETAIL.TAX10_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX1_Base_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX2_Base_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX3_Base_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX4_Base_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX5_Base_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX6_Base_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX7_Base_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX8_Base_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX9_Base_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX10_Base_Amt FROM TSPL_SCRAPINVOICE_DETAIL  where TSPL_SCRAPINVOICE_DETAIL.invoice_No='" + obj.invoice_No + "' ORDER BY TSPL_SCRAPINVOICE_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of ClsScrapInvoiceDetail)
                Dim objTr As ClsScrapInvoiceDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsScrapInvoiceDetail
                    objTr.invoice_No = clsCommon.myCstr(dr("invoice_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.shipped_Qty = clsCommon.myCdbl(dr("shipped_Qty"))
                    objTr.invoice_Qty = clsCommon.myCdbl(dr("invoice_Qty"))
                    objTr.pending_Qty = clsCommon.myCdbl(dr("pending_Qty"))


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


                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function GetDataShipment(ByVal strPONO As String) As ClsScrapInvoiceHead
        Dim obj As ClsScrapInvoiceHead = Nothing
        Dim qry As String = "SELECT tspl_scrapsale_head.shipment_No,tspl_scrapsale_head.shipment_Date,tspl_scrapsale_head.cust_Code,tspl_scrapsale_head.Po_No,tspl_scrapsale_head.invoice_No,tspl_scrapsale_head.cust_Name,tspl_scrapsale_head.shipment_date,tspl_scrapsale_head.posting_date,tspl_scrapsale_head.expship_date,tspl_scrapsale_head.Status,tspl_scrapsale_head.CreateInvoice,tspl_scrapsale_head.ispost,tspl_scrapsale_head.Description,tspl_scrapsale_head.reff,tspl_scrapsale_head.Tax_Group,tspl_scrapsale_head.tax_desc,tspl_scrapsale_head.loc_code,tspl_scrapsale_head.loc_Name,tspl_scrapsale_head.ToLoc_code,tspl_scrapsale_head.TAX1,tspl_scrapsale_head.TAX1_Rate,tspl_scrapsale_head.TAX1_Amt,tspl_scrapsale_head.TAX1_Base_Amt,tspl_scrapsale_head.TAX2,tspl_scrapsale_head.TAX2_Rate,tspl_scrapsale_head.TAX2_Amt,tspl_scrapsale_head.TAX2_Base_Amt,tspl_scrapsale_head.TAX3,tspl_scrapsale_head.TAX3_Rate,tspl_scrapsale_head.TAX3_Amt,tspl_scrapsale_head.TAX3_Base_Amt,tspl_scrapsale_head.TAX4,tspl_scrapsale_head.TAX4_Rate,tspl_scrapsale_head.TAX4_Amt,tspl_scrapsale_head.TAX4_Base_Amt,tspl_scrapsale_head.TAX5,tspl_scrapsale_head.TAX5_Rate,tspl_scrapsale_head.TAX5_Amt,tspl_scrapsale_head.TAX5_Base_Amt,tspl_scrapsale_head.TAX6,tspl_scrapsale_head.TAX6_Rate,tspl_scrapsale_head.TAX6_Amt,tspl_scrapsale_head.TAX6_Base_Amt,tspl_scrapsale_head.TAX7,tspl_scrapsale_head.TAX7_Rate,tspl_scrapsale_head.TAX7_Amt,tspl_scrapsale_head.TAX7_Base_Amt,tspl_scrapsale_head.TAX8,tspl_scrapsale_head.TAX8_Rate,tspl_scrapsale_head.TAX8_Amt,tspl_scrapsale_head.TAX8_Base_Amt,tspl_scrapsale_head.TAX9,tspl_scrapsale_head.TAX9_Rate,tspl_scrapsale_head.TAX9_Amt,tspl_scrapsale_head.TAX9_Base_Amt,tspl_scrapsale_head.TAX10,tspl_scrapsale_head.TAX10_Rate,tspl_scrapsale_head.TAX10_Amt,tspl_scrapsale_head.TAX10_Base_Amt,tspl_scrapsale_head.Addcost,tspl_scrapsale_head.AddCostDesc,tspl_scrapsale_head.Add_Amt,tspl_scrapsale_head.Before_add_Amt,tspl_scrapsale_head.After_Add_Amt,tspl_scrapsale_head.Discount_Base,tspl_scrapsale_head.Discount_Amt,tspl_scrapsale_head.Amount_Less_Discount,tspl_scrapsale_head.Total_Tax_Amt,tspl_scrapsale_head.ship_total_Amt,tspl_scrapsale_head.Comp_Code,tspl_scrapsale_head.Terms_Code,tspl_scrapsale_head.Due_Date ,tspl_scrapsale_head.AddCode1,tspl_scrapsale_head.AddDesc1,tspl_scrapsale_head.AddAmt1,tspl_scrapsale_head.AddCode2,tspl_scrapsale_head.AddDesc2,tspl_scrapsale_head.AddAmt2,tspl_scrapsale_head.AddCode3,tspl_scrapsale_head.AddDesc3,tspl_scrapsale_head.AddAmt3,tspl_scrapsale_head.AddCode4,tspl_scrapsale_head.AddDesc4,tspl_scrapsale_head.AddAmt4,tspl_scrapsale_head.AddCode5,tspl_scrapsale_head.AddDesc5,tspl_scrapsale_head.AddAmt5,tspl_scrapsale_head.AddCode6,tspl_scrapsale_head.AddDesc6,tspl_scrapsale_head.AddAmt6,tspl_scrapsale_head.AddCode7,tspl_scrapsale_head.AddDesc7,tspl_scrapsale_head.AddAmt7,tspl_scrapsale_head.AddCode8,tspl_scrapsale_head.AddDesc8,tspl_scrapsale_head.AddAmt8,tspl_scrapsale_head.AddCode9,tspl_scrapsale_head.AddDesc9,tspl_scrapsale_head.AddAmt9,tspl_scrapsale_head.AddCode10,tspl_scrapsale_head.AddDesc10,tspl_scrapsale_head.AddAmt10,TSPL_LOCATION_MASTER.Location_Desc as ToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_SCRAPINVOICE_HEAD.IRN_No,TSPL_SCRAPINVOICE_HEAD.Ack_Date,TSPL_SCRAPINVOICE_HEAD.Ack_No,TSPL_SCRAPINVOICE_HEAD.QR_Code,TSPL_SCRAPINVOICE_HEAD.EWayBillNo,TSPL_SCRAPINVOICE_HEAD.EWayBillDate,TSPL_SCRAPINVOICE_HEAD.EWayBillValidDate,TSPL_SCRAPINVOICE_HEAD.EWayBillRemarks FROM tspl_scrapsale_head 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_scrapsale_head.loc_code 
left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.shipment_No=tspl_scrapsale_head.shipment_No
left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=tspl_scrapsale_head.loc_code  
left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=tspl_scrapsale_head.Terms_Code where TSPL_SCRAPINVOICE_HEAD.shipment_No='" + strPONO + "'"





        'Select Case NavType
        '    Case NavigatorType.First
        '        qry += " and tspl_scrapsale_head.shipment_No = (select MIN(shipment_No) from tspl_scrapsale_head)"
        '    Case NavigatorType.Last
        '        qry += " and tspl_scrapsale_head.shipment_No = (select Max(shipment_No) from tspl_scrapsale_head)"
        '    Case NavigatorType.Current
        '        qry += " and tspl_scrapsale_head.shipment_No = '" + strPONO + "'"
        '    Case NavigatorType.Next
        '        qry += " and tspl_scrapsale_head.shipment_No = (select Min(shipment_No) from tspl_scrapsale_head where shipment_No>'" + strPONO + "')"
        '    Case NavigatorType.Previous
        '        qry += " and tspl_scrapsale_head.shipment_No = (select Max(shipment_No) from tspl_scrapsale_head where shipment_No<'" + strPONO + "')"
        'End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(
            qry)



        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsScrapInvoiceHead()
            If Not IsDBNull(dt.Rows(0)("EWayBillDate")) Then
                obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            End If

            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            If Not IsDBNull(dt.Rows(0)("EwayBillValidDate")) Then
                obj.EwayBillValidDate = clsCommon.myCDate(dt.Rows(0)("EwayBillValidDate"))
            End If

            obj.EwayBillRemarks = clsCommon.myCstr(dt.Rows(0)("EwayBillRemarks"))
            obj.EInvoiceIRNNo = clsCommon.myCstr(dt.Rows(0)("IRN_No"))
            obj.EInvoiceAckNo = clsCommon.myCstr(dt.Rows(0)("Ack_No"))
            If Not IsDBNull(dt.Rows(0)("Ack_Date")) Then
                obj.EInvoiceAckDate = clsCommon.myCDate(dt.Rows(0)("Ack_Date"))
            End If
            obj.EInvoiceQRCode = clsCommon.myCstr(dt.Rows(0)("QR_COde"))
            obj.shipment_No = clsCommon.myCstr(dt.Rows(0)("shipment_No"))
            obj.invoice_No = clsCommon.myCstr(dt.Rows(0)("invoice_No"))
            obj.Po_No = clsCommon.myCstr(dt.Rows(0)("Po_No"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.ispost = clsCommon.myCstr(dt.Rows(0)("ispost"))
            obj.cust_Code = clsCommon.myCstr(dt.Rows(0)("cust_Code"))
            obj.cust_Name = clsCommon.myCstr(dt.Rows(0)("cust_Name"))
            obj.shipment_Date = clsCommon.myCDate(dt.Rows(0)("shipment_Date"))
            obj.posting_Date = clsCommon.myCDate(dt.Rows(0)("posting_Date"))
            obj.expship_Date = clsCommon.myCDate(dt.Rows(0)("expship_Date"))
            obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.Loc_Name = clsCommon.myCstr(dt.Rows(0)("Loc_Name"))
            obj.ToLoc_Code = clsCommon.myCstr(dt.Rows(0)("ToLoc_Code"))
            obj.CreateInvoice = clsCommon.myCstr(dt.Rows(0)("CreateInvoice"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.reff = clsCommon.myCstr(dt.Rows(0)("reff"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.Tax_Desc = clsCommon.myCstr(dt.Rows(0)("Tax_Desc"))
            obj.Add_Amt = clsCommon.myCstr(dt.Rows(0)("Add_Amt"))
            obj.Before_Add_Amt = clsCommon.myCstr(dt.Rows(0)("Before_Add_Amt"))
            obj.Discount_Base = clsCommon.myCstr(dt.Rows(0)("Discount_Base"))
            obj.Discount_Amt = clsCommon.myCstr(dt.Rows(0)("Discount_Amt"))
            obj.Amount_Less_Discount = clsCommon.myCstr(dt.Rows(0)("Before_Add_Amt"))
            obj.Total_Tax_Amt = clsCommon.myCstr(dt.Rows(0)("Before_Add_Amt"))
            obj.ship_Total_Amt = clsCommon.myCstr(dt.Rows(0)("Before_Add_Amt"))
            obj.doc_Amt = clsCommon.myCstr(dt.Rows(0)("Before_Add_Amt"))

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


            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))




            qry = "SELECT TSPL_SCRAPSALE_DETAIL.shipment_No,TSPL_SCRAPSALE_DETAIL.Line_No,TSPL_SCRAPSALE_DETAIL.Row_Type,TSPL_SCRAPSALE_DETAIL.Item_Code,TSPL_SCRAPSALE_DETAIL.Item_Desc,TSPL_SCRAPSALE_DETAIL.unit_code,TSPL_SCRAPSALE_DETAIL.Shipped_Qty,TSPL_SCRAPSALE_DETAIL.price,TSPL_SCRAPSALE_DETAIL.DiscountPer,TSPL_SCRAPSALE_DETAIL.DiscountAmt,TSPL_SCRAPSALE_DETAIL.TotalTaxAmt,TSPL_SCRAPSALE_DETAIL.NetPriceAmt,TSPL_SCRAPSALE_DETAIL.ItemAmt,TSPL_SCRAPSALE_DETAIL.TotalDiscountAmt,TSPL_SCRAPSALE_DETAIL.ItemNetAmt,TSPL_SCRAPSALE_DETAIL.TotalAmt,TSPL_SCRAPSALE_DETAIL.TAX1,TSPL_SCRAPSALE_DETAIL.TAX1_Rate,TSPL_SCRAPSALE_DETAIL.TAX1_Amt,TSPL_SCRAPSALE_DETAIL.TAX2,TSPL_SCRAPSALE_DETAIL.TAX2_Rate,TSPL_SCRAPSALE_DETAIL.TAX2_Amt,TSPL_SCRAPSALE_DETAIL.TAX3,TSPL_SCRAPSALE_DETAIL.TAX3_Rate,TSPL_SCRAPSALE_DETAIL.TAX3_Amt,TSPL_SCRAPSALE_DETAIL.TAX4,TSPL_SCRAPSALE_DETAIL.TAX4_Rate,TSPL_SCRAPSALE_DETAIL.TAX4_Amt,TSPL_SCRAPSALE_DETAIL.TAX5,TSPL_SCRAPSALE_DETAIL.TAX5_Rate,TSPL_SCRAPSALE_DETAIL.TAX5_Amt,TSPL_SCRAPSALE_DETAIL.TAX6,TSPL_SCRAPSALE_DETAIL.TAX6_Rate,TSPL_SCRAPSALE_DETAIL.TAX6_Amt,TSPL_SCRAPSALE_DETAIL.TAX7,TSPL_SCRAPSALE_DETAIL.TAX7_Rate,TSPL_SCRAPSALE_DETAIL.TAX7_Amt,TSPL_SCRAPSALE_DETAIL.TAX8,TSPL_SCRAPSALE_DETAIL.TAX8_Rate,TSPL_SCRAPSALE_DETAIL.TAX8_Amt,TSPL_SCRAPSALE_DETAIL.TAX9,TSPL_SCRAPSALE_DETAIL.TAX9_Rate,TSPL_SCRAPSALE_DETAIL.TAX9_Amt,TSPL_SCRAPSALE_DETAIL.TAX10,TSPL_SCRAPSALE_DETAIL.TAX10_Rate,TSPL_SCRAPSALE_DETAIL.TAX10_Amt,TSPL_SCRAPSALE_DETAIL.TAX1_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX2_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX3_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX4_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX5_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX6_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX7_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX8_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX9_Base_Amt,TSPL_SCRAPSALE_DETAIL.TAX10_Base_Amt,TSPL_SCRAPSALE_DETAIL.pending_Qty FROM TSPL_SCRAPSALE_DETAIL  where TSPL_SCRAPSALE_DETAIL.shipment_No='" + obj.shipment_No + "' ORDER BY TSPL_SCRAPSALE_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of ClsScrapInvoiceDetail)
                Dim objTr As ClsScrapInvoiceDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsScrapInvoiceDetail
                    objTr.shipment_No = clsCommon.myCstr(dr("shipment_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.shipped_Qty = clsCommon.myCdbl(dr("shipped_Qty"))
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
                    objTr.pending_Qty = clsCommon.myCdbl(dr("pending_Qty"))
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
        Try
            PostData(strDocNo, True, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function EInvoice_Implementation(ByVal strDocNo As String, ByVal strLocation As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If

            Dim strtoken As String = ClsEInvoiceOFAPIs.IsGenerateAuthTokenNo_Required(objCommonVar.CurrentCompanyCode, strLocation, trans)

            If clsCommon.myLen(strtoken) > 0 Then
                Dim strQry As String = "select TSPL_Customer_master .Cust_Code ,TSPL_SCRAPINVOICE_HEAD.invoice_No as DocNo,convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) as DocDate,case when TSPL_Customer_Invoice_Head.Document_Type='D' then 'DBN' when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CRN' else 'INV' end as DocType ,'B2B' as SupTyp, 'N'  as IgstOnIntra,Bill_To_Location.GSTNO as SellerGSTINNo ,Bill_To_Location.location_desc as SellerLglNm,TSPL_COMPANY_MASTER.Comp_Name as SellerTrdNm,Bill_To_Location.Add1 as SellerAdd1,Bill_To_Location.Add2 as SellerAdd2 ,Bill_To_Location.city_code as SellerLoc,Bill_To_Location.Pin_Code  as SellerPincode,BillToLocation_State_Master.GST_STATE_Code as SellerStcd,Bill_To_Location.Phone1 as SellerPhone,Bill_To_Location.Email as SellerEmail,TSPL_Customer_master.GSTNo as BuyerGSTINNo ,TSPL_Customer_master.Customer_Name as BuyerLglNm,TSPL_Customer_master.alies_name as BuyerTrdNm,case when isnull(TSPL_SCRAPINVOICE_HEAD.ToLoc_Code,'')='' then Customer_State_Master.GST_STATE_Code else Ship_To_Location_State_Master.GST_STATE_Code end as BuyerPOS,TSPL_Customer_master.Add1 as BuyerAdd1,TSPL_Customer_master.Add2 as BuyerAdd2 ,tspl_city_master.City_Name as BuyerLoc,cast(TSPL_Customer_master.PIN_NO as int) as BuyerPincode,Customer_State_Master.GST_STATE_Code as BuyerStcd,TSPL_Customer_master.Phone1 as BuyerPhone,TSPL_Customer_master.Email as BuyerEmail,TSPL_SCRAPINVOICE_DETAIL.Line_No as ItemSlNo, 'N' as ItemIsServc,TSPL_ITEM_MASTER.Item_Desc AS ItemPrdDesc,TSPL_ITEM_MASTER.HSN_Code AS ItemHsnCd,TSPL_SCRAPINVOICE_DETAIL.invoice_Qty as ItemQty,TSPL_SCRAPINVOICE_DETAIL.Unit_code as ItemUnit,TSPL_SCRAPINVOICE_DETAIL.price as ItemUnitPrice,TSPL_SCRAPINVOICE_DETAIL.ItemAmt as ItemTotAmt,TSPL_SCRAPINVOICE_DETAIL.DiscountAmt as ItemDiscount,TSPL_SCRAPINVOICE_DETAIL.ItemNetAmt as ItemAssAmt,case when ISNULL(TSPL_SCRAPINVOICE_DETAIL .tax1,'') ='IGST' THEN TSPL_SCRAPINVOICE_DETAIL.TAX1_Rate when ISNULL(TSPL_SCRAPINVOICE_DETAIL .tax1,'') ='CGST' AND   ISNULL(TSPL_SCRAPINVOICE_DETAIL .tax2,'') ='SGST'  THEN TSPL_SCRAPINVOICE_DETAIL.TAX1_Rate+TSPL_SCRAPINVOICE_DETAIL.TAX2_Rate  ELSE 0 end as ItemGstRt, case when TSPL_SCRAPINVOICE_DETAIL .TAX1 ='SGST' AND TSPL_SCRAPINVOICE_DETAIL .TAX2  ='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt when TSPL_SCRAPINVOICE_DETAIL .TAX1 ='CGST' AND TSPL_SCRAPINVOICE_DETAIL .TAX2  ='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt else 0 end ItemSgstAmt,case when TSPL_SCRAPINVOICE_DETAIL .TAX1 ='IGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt else 0 end ItemIgstAmt,case when TSPL_SCRAPINVOICE_DETAIL .TAX1 ='CGST' AND TSPL_SCRAPINVOICE_DETAIL .TAX2  ='SGST' then TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt when TSPL_SCRAPINVOICE_DETAIL .TAX1 ='SGST' AND TSPL_SCRAPINVOICE_DETAIL .TAX2  ='CGST' then TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt else 0 end ItemCgstAmt,0 as ItemOthChrg,TSPL_SCRAPINVOICE_DETAIL.TotalAmt-case when isnull(TCS1.is_tcs,'')='Y' THEN  TSPL_SCRAPINVOICE_DETAIL.TAX2_AMT when isnull(TCS2.is_tcs,'')='Y' THEN  TSPL_SCRAPINVOICE_DETAIL.TAX3_AMT ELSE 0 END as ItemTotItemVal,TSPL_SCRAPINVOICE_HEAD .Amount_Less_Discount as ValDtlsAssVal,case when TSPL_SCRAPINVOICE_HEAD .TAX1 ='CGST' AND TSPL_SCRAPINVOICE_HEAD .TAX2  ='SGST' then TSPL_SCRAPINVOICE_HEAD.TAX1_Amt when TSPL_SCRAPINVOICE_HEAD .TAX1 ='SGST' AND TSPL_SCRAPINVOICE_HEAD .TAX2  ='CGST' then TSPL_SCRAPINVOICE_HEAD.TAX2_Amt else 0 end ValDtlsCgstVal, case when TSPL_SCRAPINVOICE_HEAD .TAX1 ='SGST' AND TSPL_SCRAPINVOICE_HEAD .TAX2  ='CGST' then TSPL_SCRAPINVOICE_HEAD.TAX1_Amt when TSPL_SCRAPINVOICE_HEAD .TAX1 ='CGST' AND TSPL_SCRAPINVOICE_HEAD .TAX2  ='SGST' then TSPL_SCRAPINVOICE_HEAD.TAX2_Amt else 0 end ValDtlsSgstVal,case when TSPL_SCRAPINVOICE_HEAD .TAX1 ='IGST' then TSPL_SCRAPINVOICE_HEAD.TAX1_Amt else 0 end ValDtlsIgstVal,TSPL_SCRAPINVOICE_HEAD.Discount_Amt as ValDtlsDiscount,case when isnull(TCS1.is_tcs,'')='Y' THEN  TSPL_SCRAPINVOICE_HEAD.TAX2_AMT when isnull(TCS2.is_tcs,'')='Y' THEN  TSPL_SCRAPINVOICE_HEAD.TAX3_AMT ELSE 0 END as ValDtlsOthChrg,TSPL_SCRAPINVOICE_HEAD.Doc_Amt  as ValDtlsTotInvVal,TSPL_SCRAPINVOICE_HEAD.RoundOffAmount  as ValDtlsRndOffAmt
,ISNULL(tspl_vendor_master.GSTFinalNo,'') AS EwbTransId,ISNULL(tspl_vendor_master.Vendor_Name,'') AS EwbTransName,TSPL_SCRAPINVOICE_HEAD.Freight_Distance as EwbDistance,isnull(TSPL_VEHICLE_MASTER.Number,'') as EwbVehNo
from TSPL_SCRAPINVOICE_HEAD
Left Outer Join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No =TSPL_SCRAPINVOICE_HEAD.invoice_No
Left Outer Join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code  ='" & objCommonVar.CurrentCompanyCode & "'
Left Outer Join TSPL_Customer_master on TSPL_Customer_master.Cust_Code  =TSPL_SCRAPINVOICE_HEAD.Cust_Code
left Outer Join TSPL_LOCATION_MASTER as Bill_To_Location on Bill_To_Location.Location_Code =TSPL_SCRAPINVOICE_HEAD.Loc_Code 
left Outer Join TSPL_SHIP_TO_LOCATION as Ship_To_Location on Ship_To_Location.Ship_To_Code =TSPL_SCRAPINVOICE_HEAD.ToLoc_Code   
left outer join TSPL_SCRAPINVOICE_DETAIL on TSPL_SCRAPINVOICE_DETAIL.invoice_No=TSPL_SCRAPINVOICE_HEAD.invoice_No
left outer join tspl_item_master on tspl_item_master.Item_code=TSPL_SCRAPINVOICE_DETAIL.Item_code
left outer join TSPL_STATE_MASTER as BillToLocation_State_Master on BillToLocation_State_Master.STATE_CODE  =Bill_To_Location.State
left outer join TSPL_STATE_MASTER as Ship_To_Location_State_Master on Ship_To_Location_State_Master.STATE_CODE  =Ship_To_Location.State
left outer join TSPL_STATE_MASTER as Customer_State_Master on Customer_State_Master.STATE_CODE  =TSPL_Customer_master.State 
left outer join tspl_city_master on tspl_city_master.city_code=TSPL_Customer_master.City_Code 
left outer join tspl_tax_master as TCS1 on TCS1.Tax_Code =TSPL_SCRAPINVOICE_HEAD.Tax2
left outer join tspl_tax_master as TCS2 on TCS2.Tax_Code =TSPL_SCRAPINVOICE_HEAD.Tax3
Left Outer Join tspl_vendor_master on tspl_vendor_master.vendor_code  =TSPL_SCRAPINVOICE_HEAD.Transport_Code
Left Outer Join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_SCRAPINVOICE_HEAD.Vehicle_Id
 where TSPL_SCRAPINVOICE_HEAD.invoice_No ='" & strDocNo & "'"

                Dim objResult As Object = ClsEInvoiceOFAPIs.PostAuthTokenNo_withInvoiceData(objCommonVar.CurrentCompanyCode, strtoken, strQry, strLocation, trans)
                If objResult IsNot Nothing Then
                    'assign to variable
                    Dim AckNo As String = objResult.SelectToken("AckNo").ToString
                    Dim AckDt As String = objResult.SelectToken("AckDt").ToString
                    Dim Irn As String = objResult.SelectToken("Irn").ToString
                    Dim SignedQRCode As String = objResult.SelectToken("SignedQRCode").ToString
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_SCRAPINVOICE_HEAD set  IRN_No ='" & Irn & "',qr_code='" & SignedQRCode & "',ack_no='" & AckNo & "',ack_date='" & clsCommon.GetPrintDate(AckDt, "dd/MMM/yyyy hh:mm tt") & "' where invoice_No ='" & strDocNo & "'", trans)

                    Dim TempByte As Byte() = clsERPFuncationalityOLD.GenerateMyQCCode(SignedQRCode)
                    clsDBFuncationality.UpdateImage("BarCode_Img", TempByte, "TSPL_SCRAPINVOICE_HEAD", "TSPL_SCRAPINVOICE_HEAD.invoice_No='" & strDocNo & "'", trans)

                    If objCommonVar.GenerateEWayBillWithEInvoice = True Then
                        Dim EwbNo As String = objResult.SelectToken("EwbNo").ToString
                        Dim EwbDt As String = objResult.SelectToken("EwbDt").ToString
                        Dim EwbValidTill As String = objResult.SelectToken("EwbValidTill").ToString
                        Dim Remarks As String = objResult.SelectToken("Remarks").ToString
                        If clsCommon.myLen(EwbDt) > 0 Then
                            EwbDt = clsCommon.GetPrintDate(EwbDt, "dd/MMM/yyyy hh:mm tt")
                        End If
                        If clsCommon.myLen(EwbValidTill) > 0 Then
                            EwbValidTill = clsCommon.GetPrintDate(EwbValidTill, "dd/MMM/yyyy hh:mm tt")
                        End If
                        clsDBFuncationality.ExecuteNonQuery("update TSPL_SCRAPINVOICE_HEAD set  EWayBillNo ='" & EwbNo & "',EwayBillDate=(CASE WHEN LEN('" & EwbDt & "')>0   THEN '" & EwbDt & "' ELSE NULL END) ,EwayBillValidDate=(CASE WHEN LEN('" & EwbValidTill & "')>0   THEN '" & EwbValidTill & "' ELSE NULL END)  , EWayBillRemarks = '" & Remarks & "'  where invoice_No ='" & strDocNo & "' ", trans)
                    End If

                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal IsCreateInventory As Boolean, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreateOnly As String = Nothing, Optional ByVal strArInvNoForRecreateOnly As String = Nothing, Optional strARVoucherNoForRecreatedOnly As String = Nothing) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim istrue As Boolean = True
            Dim GSTStatus As Boolean = False
            Dim Is_Taxable As Double = 0
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Invoice No not found to Post")
            End If
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

            'Sanjay 02/July/2018 Check Tax Group
            GSTStatus = clsERPFuncationality.GetGSTStatus(obj.shipment_Date)
            Is_Taxable = clsDBFuncationality.getSingleValue("select isnull(Is_Taxable,0) from TSPL_SCRAPINVOICE_HEAD where invoice_No='" + obj.invoice_No + "'", trans)
            If (Is_Taxable = 1) AndAlso (clsCommon.myLen(obj.Tax_Group) <= 0) Then
                Throw New Exception("Tax Group not found :" + obj.invoice_No + "(Shipment No " + obj.shipment_No + ")")
            End If
            If GSTStatus = True AndAlso Is_Taxable = 1 Then
                clsLocationWiseTax.IsValidTaxGroup(obj.Tax_Group, obj.Loc_Code, obj.cust_Code, "S", obj.shipment_Date, trans)
            End If
            'Sanjay 02/July/2018 Check Tax Group

            Dim qry As String = ""
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim IsRejectedItemFound As Boolean = False



            For Each objTr As ClsScrapInvoiceDetail In obj.Arr
                If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    Dim objInventoryMovemnt As New clsInventoryMovement()
                    objInventoryMovemnt.InOut = "O"
                    objInventoryMovemnt.Location_Code = obj.Loc_Code
                    objInventoryMovemnt.Cust_Code = obj.cust_Code
                    objInventoryMovemnt.Cust_Name = obj.cust_Name
                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                    objInventoryMovemnt.Qty = objTr.shipped_Qty
                    objInventoryMovemnt.UOM = objTr.Unit_code
                    objInventoryMovemnt.Basic_Cost = objTr.price
                    objInventoryMovemnt.Add_Cost = objTr.TotalTaxAmt
                    objInventoryMovemnt.Net_Cost = objTr.TotalAmt
                    objInventoryMovemnt.ItemType = "RM"
                    If clsCommon.CompairString(obj.Doc_Type, "J") = CompairStringResult.Equal Then
                        objInventoryMovemnt.CalculateAvgCost = False
                        objInventoryMovemnt.Avg_Cost = 0
                    End If
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                        Dim item_Purchase_Class As String = clsDBFuncationality.getSingleValue("select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" & objTr.Item_Code & "'", trans)
                        Dim qry1 As String = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + obj.Loc_Code + "'"
                        Dim strLocatinSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                        If clsCommon.myLen(item_Purchase_Class) > 0 Then
                            Dim Inventory_Purchase_code As String = clsDBFuncationality.getSingleValue("select Inv_Control_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code='" & item_Purchase_Class & "'", trans)
                            If clsCommon.myLen(Inventory_Purchase_code) > 0 Then
                                objInventoryMovemnt.Inventory_CrAcc = Inventory_Purchase_code.Substring(0, Inventory_Purchase_code.Length - 3) + strLocatinSegment
                            End If
                        End If
                    End If
                    ArrInventoryMovement.Add(objInventoryMovemnt)

                    Dim qryupdatepen As String = "update TSPL_SCRAPSALE_DETAIL set pending_qty='" + clsCommon.myCstr(objTr.pending_Qty) + "' where shipment_no='" + obj.shipment_No + "'  and item_code='" + objTr.Item_Code + "' "
                    clsDBFuncationality.ExecuteNonQuery(qryupdatepen, trans)
                End If
            Next


            If IsCreateInventory Then
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("ScrapIn", obj.invoice_No, clsCommon.GetPrintDate(obj.shipment_Date, "dd/MM/yyyy"), clsCommon.GetPrintDate(obj.shipment_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If

            If istrue = False Then
                Return False
            End If

            Dim strRMDANo As String = ""
            Dim ECustomerType = clsERPFuncationality.GetCustomerEInvoiceType(obj.cust_Code, trans)
            qry = "Update TSPL_SCRAPINVOICE_HEAD set ispost=1, Posting_Date='" + clsCommon.GetPrintDate(obj.shipment_Date, "dd/MMM/yyyy hh:mm tt ") + "',Modify_By='" + objCommonVar.CurrentUserCode + "',EInvoice_Type='" + ECustomerType + "',EInvoice_Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "'"
            qry += " where invoice_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)



            '---------------------For AR-Invoice Entry-----------------------------
            ''richa agarwal 10 June,2020 ar invoice will not created in case of job work invoice
            If clsCommon.CompairString(obj.Doc_Type, "J") <> CompairStringResult.Equal Then
                Dim Auto_Gen_No As Boolean = True
                Dim DoNotCreateJournalVoucheronJobWorkDispatch As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotCreateJournalVoucheronJobWorkDispatch, clsFixedParameterCode.DoNotCreateJournalVoucheronJobWorkDispatch, trans))

                Dim objCust As New clsCustomerInvoiceHead()
                ' objCust.Document_No = "'"
                If strArInvNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strArInvNoForRecreateOnly) > 0 Then
                    Auto_Gen_No = False
                    objCust.Document_No = strArInvNoForRecreateOnly
                End If
                objCust.Document_Date = obj.shipment_Date
                objCust.Customer_Code = obj.cust_Code
                objCust.Customer_Name = obj.cust_Name
                objCust.loc_code = obj.LocationAR
                objCust.Posting_Date = obj.posting_Date
                objCust.Account_Set = clsDBFuncationality.getSingleValue("select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.cust_Code + "'", trans)
                If clsCommon.CompairString(obj.Doc_Type, "J") = CompairStringResult.Equal Then
                    objCust.Document_Type = "D"
                Else
                    objCust.Document_Type = "I"
                End If



                objCust.Order_No = ""
                objCust.Document_Total = obj.doc_Amt
                objCust.RoundOffAmount = obj.RoundOffAmount
                objCust.On_Hold = "0"
                objCust.Remarks = ""
                objCust.Description = ""
                objCust.Tax_Group = obj.Tax_Group
                objCust.RefDocType = ""
                objCust.RefDocNo = ""

                objCust.TAX1 = obj.TAX1
                objCust.TAX1_Rate = obj.TAX1_Rate
                objCust.Tax1_BAmount = obj.TAX1_Base_Amt
                objCust.TAX1_Amt = obj.TAX1_Amt

                objCust.TAX2 = obj.TAX2
                objCust.TAX2_Rate = obj.TAX2_Rate
                objCust.Tax2_BAmount = obj.TAX2_Base_Amt
                objCust.TAX2_Amt = obj.TAX2_Amt

                objCust.TAX3 = obj.TAX3
                objCust.TAX3_Rate = obj.TAX3_Rate
                objCust.Tax3_BAmount = obj.TAX3_Base_Amt
                objCust.TAX3_Amt = obj.TAX3_Amt

                objCust.TAX4 = obj.TAX4
                objCust.TAX4_Rate = obj.TAX4_Rate
                objCust.Tax4_BAmount = obj.TAX4_Base_Amt
                objCust.TAX4_Amt = obj.TAX4_Amt

                objCust.TAX5 = obj.TAX5
                objCust.TAX5_Rate = obj.TAX5_Rate
                objCust.Tax5_BAmount = obj.TAX5_Base_Amt
                objCust.TAX5_Amt = obj.TAX5_Amt

                objCust.TAX6 = obj.TAX6
                objCust.TAX6_Rate = obj.TAX6_Rate
                objCust.Tax6_BAmount = obj.TAX6_Base_Amt
                objCust.TAX6_Amt = obj.TAX6_Amt

                objCust.TAX7 = obj.TAX7
                objCust.TAX7_Rate = obj.TAX7_Rate
                objCust.Tax7_BAmount = obj.TAX7_Base_Amt
                objCust.TAX7_Amt = obj.TAX7_Amt

                objCust.TAX8 = obj.TAX8
                objCust.TAX8_Rate = obj.TAX8_Rate
                objCust.Tax8_BAmount = obj.TAX8_Base_Amt
                objCust.TAX8_Amt = obj.TAX8_Amt

                objCust.TAX9 = obj.TAX9
                objCust.TAX9_Rate = obj.TAX9_Rate
                objCust.Tax9_BAmount = obj.TAX9_Base_Amt
                objCust.TAX9_Amt = obj.TAX9_Amt

                objCust.TAX10 = obj.TAX10
                objCust.TAX10_Rate = obj.TAX10_Rate
                objCust.Tax10_BAmount = obj.TAX10_Base_Amt
                objCust.TAX10_Amt = obj.TAX10_Amt

                objCust.Total_Tax = obj.Total_Tax_Amt
                objCust.Terms_Code = obj.Terms_Code
                objCust.Terms_Description = clsDBFuncationality.getSingleValue("select Terms_Desc from TSPL_TERMS_MASTER where Terms_Code='" + obj.Terms_Code + "'", trans)
                objCust.Due_Date = obj.Due_Date
                objCust.Discount_Percentage = 0
                objCust.Discount_Base = obj.Discount_Base
                objCust.Discount_Amount = obj.Discount_Amt
                objCust.Amount_Less_Discount = obj.Amount_Less_Discount
                objCust.Comp_Code = obj.Comp_Code
                objCust.Balance_Amt = obj.Balance_Amt

                ''richa agarwal 25/06/2015 change location of account against BM00000007177
                '  objCust.Customer_Control_AC = clsDBFuncationality.getSingleValue("select Receivable_Control_acct from TSPL_CUSTOMER_ACCOUNT_SET left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account where TSPL_CUSTOMER_MASTER.Cust_Code='" + obj.cust_Code + "'", trans)
                Dim strCustomerCntrlAcc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Receivable_Control_acct from TSPL_CUSTOMER_ACCOUNT_SET left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account where TSPL_CUSTOMER_MASTER.Cust_Code='" + obj.cust_Code + "'", trans))
                objCust.Customer_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(strCustomerCntrlAcc, obj.Loc_Code, trans)
                '--------------------------------
                If obj.Discount_Amt <> 0 Then
                    ''richa agarwal 25/06/2015 change location of account against BM00000007177
                    'objCust.Discount_GL_AC = clsDBFuncationality.getSingleValue("select Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account where TSPL_CUSTOMER_MASTER.Cust_Code='" + obj.cust_Code + "'", trans)
                    Dim strDiscountGLAcc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account where TSPL_CUSTOMER_MASTER.Cust_Code='" + obj.cust_Code + "'", trans))
                    objCust.Customer_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(strDiscountGLAcc, obj.Loc_Code, trans)
                    ''-------------------------------------
                End If
                objCust.TAX1_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
                objCust.TAX2_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
                objCust.TAX3_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
                objCust.TAX4_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
                objCust.TAX5_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
                objCust.TAX6_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
                objCust.TAX7_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
                objCust.TAX8_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
                objCust.TAX9_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
                objCust.TAX10_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
                objCust.Add_Charge_Code1 = obj.AddCode1
                objCust.Add_Charge_Name1 = obj.AddDesc1
                objCust.Add_Charge_Amt1 = obj.AddAmt1

                objCust.Add_Charge_Code2 = obj.AddCode2
                objCust.Add_Charge_Name2 = obj.AddDesc2
                objCust.Add_Charge_Amt2 = obj.AddAmt2

                objCust.Add_Charge_Code3 = obj.AddCode3
                objCust.Add_Charge_Name3 = obj.AddDesc3
                objCust.Add_Charge_Amt3 = obj.AddAmt3

                objCust.Add_Charge_Code4 = obj.AddCode4
                objCust.Add_Charge_Name4 = obj.AddDesc4
                objCust.Add_Charge_Amt4 = obj.AddAmt4

                objCust.Add_Charge_Code5 = obj.AddCode5
                objCust.Add_Charge_Name5 = obj.AddDesc5
                objCust.Add_Charge_Amt5 = obj.AddAmt5

                objCust.Add_Charge_Code6 = obj.AddCode6
                objCust.Add_Charge_Name6 = obj.AddDesc6
                objCust.Add_Charge_Amt6 = obj.AddAmt6

                objCust.Add_Charge_Code7 = obj.AddCode7
                objCust.Add_Charge_Name7 = obj.AddDesc7
                objCust.Add_Charge_Amt7 = obj.AddAmt7

                objCust.Add_Charge_Code8 = obj.AddCode8
                objCust.Add_Charge_Name8 = obj.AddDesc8
                objCust.Add_Charge_Amt8 = obj.AddAmt8

                objCust.Add_Charge_Code9 = obj.AddCode9
                objCust.Add_Charge_Name9 = obj.AddDesc9
                objCust.Add_Charge_Amt9 = obj.AddAmt9

                objCust.Add_Charge_Code10 = obj.AddCode10
                objCust.Add_Charge_Name10 = obj.AddDesc10
                objCust.Add_Charge_Amt10 = obj.AddAmt10

                objCust.Total_Add_Charge = obj.Add_Amt
                objCust.Balance_Amt = obj.Balance_Amt
                objCust.Tax_Calculation_Type = "0"
                objCust.AgainstScrap = obj.invoice_No
                objCust.Arr = New List(Of clsCustomerInvoiceDetail)
                'Dim strFirstItemCode As String = GetFirstItemCode(obj.Arr)
                For Each objout As ClsScrapInvoiceDetail In obj.Arr
                    Dim strGLAcc As String = ""
                    If clsCommon.CompairString(objout.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        strGLAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Account_Code from tspl_Additional_Charges where code='" + objout.Item_Code + "'", trans))
                        If clsCommon.myLen(strGLAcc) <= 0 Then
                            Throw New Exception("Please set the GL Account of Addtion Charges [" + objout.Item_Code + "]")
                        End If
                    Else
                        strGLAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_SALES_ACCOUNTS.Sales_Account  from TSPL_SALES_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Sale_Class_Code=TSPL_SALES_ACCOUNTS.Sales_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objout.Item_Code + "'", trans))
                        If clsCommon.myLen(strGLAcc) <= 0 Then
                            Throw New Exception("Please set the Sales Account Item [" + objout.Item_Code + "]")
                        End If
                    End If

                    Dim objtr As New clsCustomerInvoiceDetail()
                    objtr.SNo = objout.Line_No

                    objtr.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(strGLAcc, obj.Loc_Code, trans)
                    objtr.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(objtr.GL_Account_Code) + "'", trans))
                    objtr.Reco_Control_Account = "S"
                    ''-------------------------------------
                    objtr.Amount = objout.ItemAmt
                    objtr.Discount_Per = objout.DiscountPer
                    objtr.Discount = objout.DiscountAmt
                    objtr.Amount_less_Discount = objout.ItemNetAmt

                    objtr.TAX1 = objout.TAX1
                    objtr.TAX1_Rate = objout.TAX1_Rate
                    objtr.TAX1_Amt = objout.TAX1_Amt
                    objtr.TAX1_Base_Amt = objout.TAX1_Base_Amt


                    objtr.TAX2 = objout.TAX2
                    objtr.TAX2_Rate = objout.TAX2_Rate
                    objtr.TAX2_Amt = objout.TAX2_Amt
                    objtr.TAX2_Base_Amt = objout.TAX2_Base_Amt

                    objtr.TAX3 = objout.TAX3
                    objtr.TAX3_Rate = objout.TAX3_Rate
                    objtr.TAX3_Amt = objout.TAX3_Amt
                    objtr.TAX3_Base_Amt = objout.TAX3_Base_Amt

                    objtr.TAX4 = objout.TAX4
                    objtr.TAX4_Rate = objout.TAX4_Rate
                    objtr.TAX4_Amt = objout.TAX4_Amt
                    objtr.TAX4_Base_Amt = objout.TAX4_Base_Amt

                    objtr.TAX5 = objout.TAX5
                    objtr.TAX5_Rate = objout.TAX5_Rate
                    objtr.TAX5_Amt = objout.TAX5_Amt
                    objtr.TAX5_Base_Amt = objout.TAX4_Base_Amt

                    objtr.TAX6 = objout.TAX6
                    objtr.TAX6_Rate = objout.TAX6_Rate
                    objtr.TAX6_Amt = objout.TAX6_Amt
                    objtr.TAX6_Base_Amt = objout.TAX6_Base_Amt

                    objtr.TAX7 = objout.TAX7
                    objtr.TAX7_Rate = objout.TAX7_Rate
                    objtr.TAX7_Amt = objout.TAX7_Amt
                    objtr.TAX7_Base_Amt = objout.TAX7_Base_Amt

                    objtr.TAX8 = objout.TAX8
                    objtr.TAX8_Rate = objout.TAX8_Rate
                    objtr.TAX8_Amt = objout.TAX8_Amt
                    objtr.TAX8_Base_Amt = objout.TAX8_Base_Amt

                    objtr.TAX9 = objout.TAX9
                    objtr.TAX9_Rate = objout.TAX9_Rate
                    objtr.TAX9_Amt = objout.TAX9_Amt
                    objtr.TAX9_Base_Amt = objout.TAX9_Base_Amt

                    objtr.TAX10 = objout.TAX10
                    objtr.TAX10_Rate = objout.TAX10_Rate
                    objtr.TAX10_Amt = objout.TAX10_Amt
                    objtr.TAX10_Base_Amt = objout.TAX10_Base_Amt



                    objtr.Total_Tax = objout.TotalTaxAmt
                    objtr.Total_Amount = objout.TotalAmt
                    objtr.Remarks = ""
                    objtr.Comments = ""
                    objCust.Arr.Add(objtr)
                Next

                ''richa agarwal 08/07/2015 BM00000007330
                'If objCust.SaveData(objCust, Auto_Gen_No, trans, "", strARVoucherNoForRecreatedOnly) Then
                '    qry = "Update TSPL_Customer_Invoice_Head set status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.shipment_Date, "dd/MMM/yyyy hh:mm tt ") + "',Modify_By='" + objCommonVar.CurrentUserCode + "'"
                '    qry += " where AgainstScrap='" + obj.invoice_No + "'"

                'End If
                If (clsCommon.CompairString("S", obj.Doc_Type) = CompairStringResult.Equal OrElse (clsCommon.CompairString("J", obj.Doc_Type) = CompairStringResult.Equal AndAlso DoNotCreateJournalVoucheronJobWorkDispatch = 0)) Then
                    objCust.SaveData(objCust, Auto_Gen_No, trans, "", strARVoucherNoForRecreatedOnly)
                    clsCustomerInvoiceHead.PostData("", objCust.Document_No, "", trans)
                End If
            End If

            ''richa agarwal 21 Dec,2020 check eInvoice Implementation
            If clsCommon.CompairString(ECustomerType, "BB") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(obj.Is_Taxable), "1") = CompairStringResult.Equal AndAlso clsERPFuncationality.GetEInvoiceStatus(obj.shipment_Date, trans) = True Then
                If ClsScrapInvoiceHead.EInvoice_Implementation(obj.invoice_No, obj.Loc_Code, trans) = True Then
                Else
                    Throw New Exception("Invalid JSON Value")
                End If
                '    If clsCommon.myLen(ClsScrapInvoiceHead.GetIRNNo(strDocNo, trans)) > 0 Then
                '        ClsScrapInvoiceHead.EInvoice_Implementation(obj.invoice_No, obj.Loc_Code, trans, False)
                '        If clsCommon.myLen(ClsScrapInvoiceHead.GetIRNNo(strDocNo, trans)) <= 0 Then
                '            Throw New Exception("IRN No For Sales Invoice No [" + strDocNo + "] is not generated")
                '        End If
                '    End If
                '    If objCommonVar.GenerateEWayBillWithEInvoice Then
                '        If clsCommon.myLen(ClsScrapInvoiceHead.GetEWayBillNo(strDocNo, trans)) > 0 Then
                '            ClsScrapInvoiceHead.EInvoice_Implementation(obj.invoice_No, obj.Loc_Code, trans, True)
                '            If clsCommon.myLen(clsDBFuncationality.getSingleValue("select  isnull(EWayBillNo,'') from TSPL_SCRAPINVOICE_HEAD where shipment_No='" + strDocNo + "'", trans)) <= 0 Then
                '                Throw New Exception("E-Way Bill For Sales Invoice No [" + strDocNo + "] is not generated")
                '            End If
                '        End If
                '    End If
            End If
            ''------------------------------
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    'Public Shared Function GetIRNNo(strDocNo As String, trans As SqlTransaction) As String
    '    Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(IRN_No,'') from TSPL_SCRAPINVOICE_HEAD where shipment_No='" + strDocNo + "'", trans))
    'End Function
    'Public Shared Function GetEWayBillNo(strDocNo As String, trans As SqlTransaction) As String
    '    Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(EWayBillNo,'') from TSPL_SCRAPINVOICE_HEAD where shipment_No='" + strDocNo + "'", trans))
    'End Function

    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of ClsScrapInvoiceDetail)) As String
        For Each objtr As ClsScrapInvoiceDetail In Arr
            If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Return objtr.Item_Code
            End If
        Next


        Return ""
    End Function

    Public Shared Function PostDataForUtility(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        'Try
        Dim isSaved As Boolean = True
        Dim istrue As Boolean = True
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Invoice No not found to Post")
        End If
        ''Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy hh:mm tt")

        Dim obj As ClsScrapInvoiceHead = ClsScrapInvoiceHead.GetData(strDocNo, NavigatorType.Current, trans)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.invoice_No) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If
        'If (obj.ispost = 1) Then
        '    Throw New Exception("Already Post on :" + obj.posting_Date)
        'End If
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

            '****************** Remove Shipment Clearing Account 23/10/2012 Added by manoj On request by rakesh sir 
            'Dim strshippingCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Shipment_Clearing"))
            'If clsCommon.myLen(strshippingCtrlAC) <= 0 Then
            '    Throw New Exception("Please set Shipment Clearing Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_Code + "(" + objTr.Item_Desc + ") ")
            'End If
            'strshippingCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strshippingCtrlAC, obj.Loc_Code, trans)
            'Dim AccCr() As String = {strshippingCtrlAC, -1 * (itemcost * objTr.shipped_Qty)}
            'ArryLstGLAC.Add(AccCr)
            'totCrAmt = totCrAmt + (-1 * (itemcost * objTr.shipped_Qty))


            '*************** End Manoj

            'Dim AccSetQry As String = "SELECT TSPL_SALES_ACCOUNTS.Sales_Account, TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold,TSPL_SALES_ACCOUNTS.Cogs_InterBranch FROM TSPL_ITEM_MASTER INNER JOIN  TSPL_SALES_ACCOUNTS ON TSPL_ITEM_MASTER.Sale_Class_Code = TSPL_SALES_ACCOUNTS.Sales_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "' "
            'Dim dr As SqlDataReader

            'dr = connectSql.RunSqlReturnDR(AccSetQry)
            'Dim SaleAcc As String
            'Dim CostOfGood As String
            'Dim strInterBranchCogs As String
            'While dr.Read()
            '    SaleAcc = dr(0).ToString()
            '    CostOfGood = dr(1).ToString()
            '    strInterBranchCogs = clsCommon.myCstr(dr("Cogs_InterBranch"))
            'End While
            '----------Added By--pankaj Kumar----------------------15/10/2012-------
            Dim AccSetQry As String = "SELECT TSPL_SALES_ACCOUNTS.Sales_Account, TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold,TSPL_SALES_ACCOUNTS.Cogs_InterBranch FROM TSPL_ITEM_MASTER INNER JOIN  TSPL_SALES_ACCOUNTS ON TSPL_ITEM_MASTER.Sale_Class_Code = TSPL_SALES_ACCOUNTS.Sales_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "' "
            Dim dtItm As DataTable = clsDBFuncationality.GetDataTable(AccSetQry, trans)
            'dr = connectSql.RunSqlReturnDR(AccSetQry)
            Dim SaleAcc As String = ""
            Dim CostOfGood As String = ""
            Dim strInterBranchCogs As String = ""
            'While dr.Read()
            If dtItm.Rows.Count > 0 Then
                For Each dr As DataRow In dtItm.Rows
                    SaleAcc = dr("Sales_Account").ToString()
                    CostOfGood = dr("Cost_Of_Goods_Sold").ToString()
                    strInterBranchCogs = clsCommon.myCstr(dr("Cogs_InterBranch"))
                Next
            End If
            '------------------------------------------------------------------------
            'End While
            If clsCommon.myLen(SaleAcc) <= 0 Then
                Throw New Exception("Please set Sale Account Code ")
            End If
            If clsCommon.myLen(CostOfGood) <= 0 Then
                Throw New Exception("Please set Cost of Good Account Code ")
            End If

            'If obj.Inter_Branch Then
            'Else
            SaleAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(SaleAcc, obj.Loc_Code, trans)
            Dim AccSalCr() As String = {SaleAcc, -1 * (objTr.ItemNetAmt)}
            ArryLstGLAC.Add(AccSalCr)
            totCrAmt = totCrAmt + (-1 * (objTr.ItemNetAmt))

            '****************** Remove Cogs Account 23/10/2012 Added by manoj On request by rakesh sir 
            'CostOfGood = clsERPFuncationality.ChangeGLAccountLocationSegment(CostOfGood, obj.Loc_Code, trans)
            'Dim AccCostCr() As String = {CostOfGood, itemcost * objTr.shipped_Qty}
            'ArryLstGLAC.Add(AccCostCr)
            'totDrAmt = totDrAmt + (itemcost * objTr.shipped_Qty)
            '*************** End manoj

            'End If

            If (clsCommon.myLen(objTr.TAX1) > 0) Then
                Dim tax1acc As String
                If obj.Inter_Branch Then
                    tax1acc = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX1, trans)
                Else
                    tax1acc = clsTaxMaster.GetTaxPayAC(objTr.TAX1, trans)
                End If
                tax1acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax1acc, obj.Loc_Code, trans)
                If objTr.TAX1_Amt > 0 Then
                    Dim tax1accCr() As String = {tax1acc, -1 * objTr.TAX1_Amt}
                    ArryLstGLAC.Add(tax1accCr)
                    totCrAmt = totCrAmt + (-1 * objTr.TAX1_Amt)
                End If
            End If

            If (clsCommon.myLen(objTr.TAX2) > 0) Then

                Dim tax2acc As String
                If obj.Inter_Branch Then
                    tax2acc = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX2, trans)
                Else
                    tax2acc = clsTaxMaster.GetTaxPayAC(objTr.TAX2, trans)
                End If

                tax2acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax2acc, obj.Loc_Code, trans)
                If objTr.TAX2_Amt > 0 Then
                    Dim tax2accCr() As String = {tax2acc, -1 * objTr.TAX2_Amt}
                    ArryLstGLAC.Add(tax2accCr)
                    totCrAmt = totCrAmt + (-1 * objTr.TAX2_Amt)
                End If
            End If
            If (clsCommon.myLen(objTr.TAX3) > 0) Then

                Dim tax3acc As String
                If obj.Inter_Branch Then
                    tax3acc = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX3, trans)
                Else
                    tax3acc = clsTaxMaster.GetTaxPayAC(objTr.TAX3, trans)
                End If
                tax3acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax3acc, obj.Loc_Code, trans)
                Dim tax3accCr() As String = {tax3acc, -1 * objTr.TAX3_Amt}
                ArryLstGLAC.Add(tax3accCr)
                totCrAmt = totCrAmt + (-1 * objTr.TAX3_Amt)
            End If
            If (clsCommon.myLen(objTr.TAX4) > 0) Then


                Dim tax4acc As String
                If obj.Inter_Branch Then
                    tax4acc = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX4, trans)
                Else
                    tax4acc = clsTaxMaster.GetTaxPayAC(objTr.TAX4, trans)
                End If

                tax4acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax4acc, obj.Loc_Code, trans)
                Dim tax4accCr() As String = {tax4acc, -1 * objTr.TAX4_Amt}
                ArryLstGLAC.Add(tax4accCr)
                totCrAmt = totCrAmt + (-1 * objTr.TAX4_Amt)
            End If
            If (clsCommon.myLen(objTr.TAX5) > 0) Then

                Dim tax5acc As String
                If obj.Inter_Branch Then
                    tax5acc = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX5, trans)
                Else
                    tax5acc = clsTaxMaster.GetTaxPayAC(objTr.TAX5, trans)
                End If

                tax5acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax5acc, obj.Loc_Code, trans)
                Dim tax5accCr() As String = {tax5acc, -1 * objTr.TAX5_Amt}
                ArryLstGLAC.Add(tax5accCr)
                totCrAmt = totCrAmt + (-1 * objTr.TAX5_Amt)
            End If
            If (clsCommon.myLen(objTr.TAX6) > 0) Then


                Dim tax6acc As String
                If obj.Inter_Branch Then
                    tax6acc = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX6, trans)
                Else
                    tax6acc = clsTaxMaster.GetTaxPayAC(objTr.TAX6, trans)
                End If


                tax6acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax6acc, obj.Loc_Code, trans)
                Dim tax6accCr() As String = {tax6acc, -1 * objTr.TAX6_Amt}
                ArryLstGLAC.Add(tax6accCr)
                totCrAmt = totCrAmt + (-1 * objTr.TAX6_Amt)
            End If
            If (clsCommon.myLen(objTr.TAX7) > 0) Then

                Dim tax7acc As String
                If obj.Inter_Branch Then
                    tax7acc = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX7, trans)
                Else
                    tax7acc = clsTaxMaster.GetTaxPayAC(objTr.TAX7, trans)
                End If
                tax7acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax7acc, obj.Loc_Code, trans)
                Dim tax7accCr() As String = {tax7acc, -1 * objTr.TAX7_Amt}
                ArryLstGLAC.Add(tax7accCr)
                totCrAmt = totCrAmt + (-1 * objTr.TAX7_Amt)
            End If
            If (clsCommon.myLen(objTr.TAX8) > 0) Then

                Dim tax8acc As String
                If obj.Inter_Branch Then
                    tax8acc = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX8, trans)
                Else
                    tax8acc = clsTaxMaster.GetTaxPayAC(objTr.TAX8, trans)
                End If
                tax8acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax8acc, obj.Loc_Code, trans)
                Dim tax8accCr() As String = {tax8acc, -1 * objTr.TAX8_Amt}
                ArryLstGLAC.Add(tax8accCr)
                totCrAmt = totCrAmt + (-1 * objTr.TAX8_Amt)
            End If
            If (clsCommon.myLen(objTr.TAX9) > 0) Then

                Dim tax9acc As String
                If obj.Inter_Branch Then
                    tax9acc = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX9, trans)
                Else
                    tax9acc = clsTaxMaster.GetTaxPayAC(objTr.TAX9, trans)
                End If
                tax9acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax9acc, obj.Loc_Code, trans)
                Dim tax9accCr() As String = {tax9acc, -1 * objTr.TAX9_Amt}
                ArryLstGLAC.Add(tax9accCr)
                totCrAmt = totCrAmt + (-1 * objTr.TAX9_Amt)
            End If
            If (clsCommon.myLen(objTr.TAX10) > 0) Then

                Dim tax10acc As String
                If obj.Inter_Branch Then
                    tax10acc = clsTaxMaster.GetTaxRecoverableAC(objTr.TAX10, trans)
                Else
                    tax10acc = clsTaxMaster.GetTaxPayAC(objTr.TAX10, trans)
                End If
                tax10acc = clsERPFuncationality.ChangeGLAccountLocationSegment(tax10acc, obj.Loc_Code, trans)
                Dim tax10accCr() As String = {tax10acc, -1 * objTr.TAX10_Amt}
                ArryLstGLAC.Add(tax10accCr)
                totCrAmt = totCrAmt + (-1 * objTr.TAX10_Amt)
            End If



            '--------------------------------------------------------------\


            Dim objInventoryMovemnt As New clsInventoryMovement()
            objInventoryMovemnt.InOut = "O"
            objInventoryMovemnt.Location_Code = obj.Loc_Code
            objInventoryMovemnt.Item_Code = objTr.Item_Code
            objInventoryMovemnt.Item_Desc = objTr.Item_Desc
            objInventoryMovemnt.Qty = objTr.shipped_Qty
            objInventoryMovemnt.UOM = objTr.Unit_code
            objInventoryMovemnt.Basic_Cost = objTr.ItemNetAmt

            objInventoryMovemnt.Add_Cost = objTr.TotalTaxAmt
            objInventoryMovemnt.Net_Cost = objTr.TotalAmt

            objInventoryMovemnt.ItemType = "RM"


            ArrInventoryMovement.Add(objInventoryMovemnt)





            '------------------------------------------------------------=-=





            'Dim qryupdatepen As String = "update TSPL_SCRAPSALE_DETAIL set pending_qty='" + clsCommon.myCstr(objTr.pending_Qty) + "' where shipment_no='" + obj.shipment_No + "'  and item_code='" + objTr.Item_Code + "' "
            'clsDBFuncationality.ExecuteNonQuery(qryupdatepen, trans)

        Next

        If obj.Inter_Branch Then
            strCustContAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strCustContAcc, obj.Loc_Code, trans)
            Dim AccCust() As String = {strCustContAcc, -1 * (totCrAmt)}
            ArryLstGLAC.Add(AccCust)
            totDrAmt = totDrAmt + (-1 * totCrAmt)
        Else
            strCustContAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strCustContAcc, obj.Loc_Code, trans)
            Dim AccCust() As String = {strCustContAcc, obj.doc_Amt}
            ArryLstGLAC.Add(AccCust)
            totDrAmt = totDrAmt + (obj.doc_Amt)
        End If

        If Math.Abs(Math.Round(totDrAmt, 2)) <> Math.Abs(Math.Round(totCrAmt, 2)) Then
            Throw New Exception("Error in Posting: Total Debit Amount:" + clsCommon.myCstr(Math.Abs(totDrAmt)) + " and Total Credit Amount: " + clsCommon.myCstr(Math.Abs(totCrAmt)) + "")
        End If
        istrue = clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.shipment_Date, "Against Scrap Invoice " + obj.invoice_No + "With Shipment No: " + obj.shipment_No, "AR-SI", "Scrap Invoice", obj.invoice_No, obj.Description, "C", obj.cust_Code, obj.cust_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
        Return True
    End Function
    Public Shared Function GetaddtionalAcc(ByVal Add_Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select account_code from TSPL_Additional_Charges where code='" + Add_Code + "'"
        Return clsDBFuncationality.getSingleValue(qry, trans)
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Invoice No not found to Delete")
        End If
        Dim obj As ClsScrapInvoiceHead = ClsScrapInvoiceHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.invoice_No) > 0) Then
            Try
                If (obj.ispost = 1) Then
                    Throw New Exception("Already Posted on :" + obj.posting_Date)
                End If
                Dim qry As String = "delete from TSPL_SCRAPINVOICE_DETAIL where invoice_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SCRAPINVOICE_HEAD where invoice_No='" + strCode + "'"
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

    Public Shared Function ReverseAndUnpost(ByVal strShipmentCode As String, ByVal strInvoiceCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select ispost from TSPL_SCRAPINVOICE_HEAD where invoice_No='" + strInvoiceCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select distinct Receipt_No  from TSPL_RECEIPT_DETAIL where Document_No in (select Document_No from TSPL_Customer_Invoice_Head where AgainstScrap in ('" + strInvoiceCode + "') ) "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current Scrap Invoice is used in following Receipt -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Receipt_No"))
                Next
                Throw New Exception(Qry)
            End If


            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AR-SI' and Source_Doc_No='" + strInvoiceCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "select InOut,Trans_Type,Item_Code,Item_Desc,Location_Code,case when InOut='I' then -1 else 1 end *Qty as Qty ,UOM,MRP,ItemType,case when InOut='I' then -1 else 1 end* Basic_Cost as Basic_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strInvoiceCode + "' and Trans_Type='ScrapIn'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)
            For Each objtr As DataRow In dt.Rows
                Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objtr("Item_Code")), clsCommon.myCstr(objtr("UOM")), trans)
                Dim objLocationDetails As New clsItemLocationDetails()
                objLocationDetails.Item_Code = clsCommon.myCstr(objtr("Item_Code"))
                objLocationDetails.Item_Desc = clsCommon.myCstr(objtr("Item_Desc"))
                objLocationDetails.Location_Code = clsCommon.myCstr(objtr("Location_Code"))
                objLocationDetails.Location_Desc = clsLocation.GetName(objLocationDetails.Location_Code, trans)
                objLocationDetails.Item_Qty = clsCommon.myCdbl(objtr("Qty")) / dblConvFac
                objLocationDetails.Amount = clsCommon.myCdbl(objtr("Basic_Cost"))
                objLocationDetails.MRP = clsCommon.myCdbl(objtr("MRP")) * dblConvFac
                objLocationDetails.ItemType = clsCommon.myCstr(objtr("ItemType"))
                ArrLocationDetails.Add(objLocationDetails)
            Next
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            clsItemLocationDetails.SaveData(strPostDate, ArrLocationDetails, trans)

            Qry = "update tspl_batch_item set against_inv_movement_trans_id=NULL where document_type='ScrapIn' and document_code='" + strInvoiceCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strInvoiceCode), "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strInvoiceCode + "' and Trans_Type='ScrapIn'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            ''--- Joun Entry Delete
            Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No " &
            " in (select Document_No from TSPL_Customer_Invoice_Head where AgainstScrap='" + strInvoiceCode + "')", trans))

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            Qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No='" & Voucher_No & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Delete from  TSPL_JOURNAL_MASTER where Voucher_No='" & Voucher_No & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            ''---- End
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strInvoiceCode), "TSPL_Customer_Invoice_Head", "AgainstScrap", trans)
            Qry = "Delete from TSPL_Customer_Invoice_Detail where Document_No in (select Document_No from TSPL_Customer_Invoice_Head where AgainstScrap='" + strInvoiceCode + "')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Delete from  TSPL_Customer_Invoice_Head where AgainstScrap='" + strInvoiceCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_SCRAPINVOICE_HEAD set ispost = 0 where invoice_No='" + strInvoiceCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_SCRAPSALE_HEAD set ispost = 0 where shipment_No='" + strShipmentCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strShipmentCode), "TSPL_SCRAPSALE_HEAD", "shipment_No", trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strInvoiceCode), "TSPL_SCRAPINVOICE_HEAD", "invoice_No", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class ClsScrapInvoiceDetail
#Region "Variables"
    Public shipment_No As String = Nothing
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public invoice_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_code As String = Nothing
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
    Public pending_Qty As Double = 0
#End Region

    Public Shared Function FinderItem(ByVal strCode As String, ByVal strItemType As String, ByVal isButtonClicked As Boolean) As ClsScrapInvoiceDetail
        Dim obj As ClsScrapInvoiceDetail = Nothing
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name from TSPL_ITEM_MASTER"
        Dim WhrCls As String = ""
        If clsCommon.myLen(strItemType) > 0 Then
            WhrCls = "Item_Type<>'" + strItemType + "'"
        End If
        strCode = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc,Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New ClsScrapInvoiceDetail()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))

            End If
        End If
        Return obj
    End Function

    Public Shared Function FinderAdditional(ByVal strCode As String, ByVal isButtonClicked As Boolean) As ClsScrapInvoiceDetail
        Dim obj As ClsScrapInvoiceDetail = Nothing
        Dim qry As String = "select Code ,description from TSPL_Additional_Charges"
        Dim WhrCls As String = ""

        strCode = clsCommon.ShowSelectForm("Additiona charges", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Code,description from TSPL_Additional_Charges where Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New ClsScrapInvoiceDetail()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))

            End If
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsScrapInvoiceDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsScrapInvoiceDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "invoice_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                If clsCommon.myLen(obj.Row_Type) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Row_Type", "Item")
                Else
                    clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                End If

                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "shipped_Qty", obj.shipped_Qty)
                clsCommon.AddColumnsForChange(coll, "invoice_Qty", obj.invoice_Qty)
                clsCommon.AddColumnsForChange(coll, "pending_Qty", obj.pending_Qty)
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



                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPINVOICE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class



