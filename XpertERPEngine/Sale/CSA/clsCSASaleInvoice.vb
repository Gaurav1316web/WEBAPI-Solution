'============BM00000003604========created by Monika
''BM00000008066
Imports common
Imports System.Data.SqlClient
Public Class clsCSASaleInvoice
#Region "variables"
    Public Excisable As String = Nothing
    Public CSA_Distributor_Code As String = Nothing
    Public Distributor_Name As String = Nothing
    Public Is_Approved As Integer = Nothing
    Public CSA_FOC_STATUS As Integer = Nothing
    Public plant_loc_code As String = Nothing
    Public plant_loc_name As String = Nothing
    Public total_commision As Decimal = Nothing
    Public Posting_Date As Date = Nothing
    Public docno As String = Nothing
    Public docdate As Date = Nothing
    Public descrptn As String = Nothing
    Public cust_code As String = Nothing
    Public cust_name As String = Nothing
    Public loc_code As String = Nothing
    Public loc_name As String = Nothing
    Public po_no As String = Nothing
    Public document_amt As Decimal = Nothing
    Public currency_code As String = Nothing
    Public cnvrsn_rate As Decimal = Nothing
    Public applicable_from As String = Nothing
    Public amt_with_disc As Decimal = Nothing
    Public disc_on_amt As String = Nothing
    Public disc_on_rate As String = Nothing
    Public disc_pers As Decimal = Nothing
    Public disc_amt As Decimal = Nothing
    Public inv_disc_amt As Decimal = Nothing
    Public lbldisc_amt As Decimal = Nothing
    Public amt_after_disc As Decimal = Nothing
    Public lbltaxamt As Decimal = Nothing
    Public total_add_chrg As Decimal = Nothing
    Public tax_group_code As String = Nothing
    Public tax_group_name As String = Nothing
    Public Tax_Calculation_Type As String = Nothing
    Public term_code As String = Nothing
    Public term_desc As String = Nothing
    Public due_date As Date = Nothing

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
    Public isPost As Integer = Nothing
    Public RoundOffAmount As Decimal = 0
    Public Total_Freight_Amt As Decimal = 0

    Public Arr_Item As List(Of clsCSASaleInvoiceItem) = Nothing
#End Region

    Public Shared Function GetCSASALEDescrptn() As String
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Description from TSPL_SD_SALE_INVOICE_HEAD where Trans_Type='CSA' and isnull(Description,'')<>'' order by document_date desc"))

        Return str
    End Function

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
                    strPostAmt = System.Math.Round((strPostAmt - 100) / 100, 2)

                    LstDecml.Add(strPreAmt)
                    LstDecml.Add(strPostAmt)
                ElseIf strPostAmt < 50 Then
                    strPreAmt = strPreAmt
                    strPostAmt = System.Math.Round(strPostAmt / 100, 2)

                    LstDecml.Add(strPreAmt)
                    LstDecml.Add(strPostAmt)
                End If
            End If

            Return LstDecml
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean, ByVal arrLoc As String) As String
        Try
            Dim AllowDistibutorSale As Boolean = False
            AllowDistibutorSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDistributorSaleAtCSA_SaleInvoice, clsFixedParameterCode.AllowDistributorSaleAtCSA_SaleInvoice, Nothing)) = "1", True, False))


            Dim str As String = ""
            Dim qry As String = ""

            If AllowDistibutorSale Then
                qry = "select TSPL_SD_SALE_INVOICE_HEAD.document_code as Code,TSPL_SD_SALE_INVOICE_HEAD.document_date as [Doc Date],TSPL_SD_SALE_INVOICE_HEAD.csa_distributor_code as [CSA Code],CUSTMST.customer_name as [CSA],TSPL_SD_SALE_INVOICE_HEAD.customer_code as [Distributor Code],tspl_customer_master.customer_name as [Distributor Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],(case when TSPL_SD_SALE_INVOICE_HEAD.status=1 then'Approved' else 'Pending' end) as Status,TSPL_SD_SALE_INVOICE_HEAD.Description,TSPL_SD_SALE_INVOICE_HEAD.tax_group as [Tax Group],TSPL_SD_SALE_INVOICE_HEAD.bill_to_location as [Location],TSPL_SD_SALE_INVOICE_HEAD.discount_amt as [Discount Amount],TSPL_SD_SALE_INVOICE_HEAD.amount_less_discount as [Amount After Disc],TSPL_SD_SALE_INVOICE_HEAD.total_tax_amt as [Tax Amount],TSPL_SD_SALE_INVOICE_HEAD.total_amt as [Bill Amount]"
                qry += " from TSPL_SD_SALE_INVOICE_HEAD left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code " & _
                    " left outer join tspl_customer_master CUSTMST on CUSTMST.cust_code=TSPL_SD_SALE_INVOICE_HEAD.csa_distributor_code "
            Else
                qry = "select TSPL_SD_SALE_INVOICE_HEAD.document_code as Code,TSPL_SD_SALE_INVOICE_HEAD.document_date as [Doc Date],TSPL_SD_SALE_INVOICE_HEAD.customer_code as [CSA Code],tspl_customer_master.customer_name as [CSA],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],(case when TSPL_SD_SALE_INVOICE_HEAD.status=1 then'Approved' else 'Pending' end) as Status,TSPL_SD_SALE_INVOICE_HEAD.Description,TSPL_SD_SALE_INVOICE_HEAD.tax_group as [Tax Group],TSPL_SD_SALE_INVOICE_HEAD.bill_to_location as [Location],TSPL_SD_SALE_INVOICE_HEAD.discount_amt as [Discount Amount],TSPL_SD_SALE_INVOICE_HEAD.amount_less_discount as [Amount After Disc],TSPL_SD_SALE_INVOICE_HEAD.total_tax_amt as [Tax Amount],TSPL_SD_SALE_INVOICE_HEAD.total_amt as [Bill Amount]"
                qry += " from TSPL_SD_SALE_INVOICE_HEAD left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code"
            End If

            If clsCommon.myLen(whrCls) > 0 Then
                whrCls = whrCls + " and TSPL_SD_SALE_INVOICE_HEAD.trans_type='CSA'"
            Else
                whrCls = " TSPL_SD_SALE_INVOICE_HEAD.trans_type='CSA'"
            End If

            If clsCommon.myLen(arrLoc) > 0 Then
                If clsCommon.myLen(whrCls) > 0 Then
                    whrCls = whrCls + " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + arrLoc + ") "
                Else
                    whrCls = " TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + arrLoc + ") "
                End If
            End If
            str = clsCommon.myCstr(clsCommon.ShowSelectForm("SALFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked))

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsCSASaleInvoice, ByVal is_Post_Click As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(isNewEntry, obj, is_Post_Click, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsCSASaleInvoice, ByVal is_Post_Click As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSASaleInvoice, obj.plant_loc_code, obj.docdate, trans)
            Dim isSaved As Boolean = True

            clsBatchInventory.DeleteData("CSA-Sale", obj.docno, trans)

            Dim coll As New Hashtable()

            If isNewEntry Then
                Dim GSTStatus As Boolean = clsERPFuncationality.GetGSTStatus(obj.docdate)
                If GSTStatus Then
                    'If GST On
                    Dim strTaxType As String = clsCommon.myCstr(clsLocationWiseTax.TaxType(obj.plant_loc_code, obj.cust_code, "S", obj.docdate, trans))
                    If clsCommon.CompairString(strTaxType, "I") = CompairStringResult.Equal Then
                        obj.docno = clsERPFuncationality.GetNextCode(trans, obj.docdate, clsDocType.CSASaleInvoice, clsDocTransactionType.GSTTaxable, obj.plant_loc_code)
                    Else
                        obj.docno = clsERPFuncationality.GetNextCode(trans, obj.docdate, clsDocType.CSASaleInvoice, clsDocTransactionType.GSTNonTaxable, obj.plant_loc_code)
                    End If
                Else
                    obj.docno = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, obj.docdate, clsDocType.CSASaleInvoice, clsDocTransactionType.NA, obj.plant_loc_code))
                End If
            End If

                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.docno)
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.docdate, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.cust_code)
                clsCommon.AddColumnsForChange(coll, "CSA_Distributor_Code", obj.CSA_Distributor_Code)
                clsCommon.AddColumnsForChange(coll, "Status", obj.isPost)
                clsCommon.AddColumnsForChange(coll, "On_Hold", 0)
                clsCommon.AddColumnsForChange(coll, "Description", obj.descrptn)
                clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.tax_group_code)
                clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.loc_code)
                clsCommon.AddColumnsForChange(coll, "CSA_PLANT_LOCATION", obj.plant_loc_code)
                If is_Post_Click = True Then
                    obj.Is_Approved = 1
                End If
                clsCommon.AddColumnsForChange(coll, "Is_Approved", obj.Is_Approved)
                'clsCommon.AddColumnsForChange(coll, "CSA_FOC_STATUS", obj.CSA_FOC_STATUS)

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

                clsCommon.AddColumnsForChange(coll, "Discount_Base", obj.amt_with_disc)
                clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.lbldisc_amt)
                clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.amt_after_disc)
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.lbltaxamt)
                clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.document_amt)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.term_code)
                If clsCommon.myLen(obj.due_date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.due_date, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
                End If

                clsCommon.AddColumnsForChange(coll, "Modify_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Trans_Type", "CSA")

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

                clsCommon.AddColumnsForChange(coll, "Total_Add_Charge", obj.total_add_chrg)
                clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", obj.Tax_Calculation_Type)
                clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.currency_code, True)
                clsCommon.AddColumnsForChange(coll, "ConvRate", obj.cnvrsn_rate)
                If obj.applicable_from Is Nothing OrElse clsCommon.myLen(obj.applicable_from) <= 0 Then
                    obj.applicable_from = clsCommon.GETSERVERDATE(trans)
                End If
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(obj.applicable_from, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "HeadDisc_Per", obj.disc_pers)
                clsCommon.AddColumnsForChange(coll, "HeadDisc_PerAmt", obj.inv_disc_amt)
                clsCommon.AddColumnsForChange(coll, "HeadDisc_Amt", obj.disc_amt)
                clsCommon.AddColumnsForChange(coll, "Cust_PO_No", obj.po_no)
                clsCommon.AddColumnsForChange(coll, "comments", obj.descrptn)
                clsCommon.AddColumnsForChange(coll, "Total_Commision_Amt", obj.total_commision)
                clsCommon.AddColumnsForChange(coll, "Total_Freight_Amt", obj.Total_Freight_Amt)
                clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)

                clsCommon.AddColumnsForChange(coll, "Excisable", obj.Excisable)
                If clsCommon.CompairString(obj.Excisable, "E") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Invoice_Type", obj.Excisable)
                ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Excisable, "E") <> CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Invoice_Type", "T") ''02/02/017 (Ref By Amit Sir]
                End If

                If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_HEAD", OMInsertOrUpdate.Update, " Document_Code='" + obj.docno + "' and Trans_Type='CSA'", trans)
                End If

                isSaved = isSaved AndAlso clsCSASaleInvoiceItem.SaveData(obj.docno, obj.docdate, obj.loc_code, obj.Arr_Item, trans)


                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("delete from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + obj.docno + "'", trans)

                'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from CSA_SALE_TRANSFER", trans)) > 0 Then
                '    Dim qry As String = "insert into TSPL_CSA_SALE_TRANSFER_DETAIL(DOCUMENT_CODE,Line_No,Against_Transfer_Code,item_code,Item_Pack_Size,qty,transfer_rate,Conv_Factor,Transfer_Qty,Transfer_UOM,Balance_Qty,Sale_UOM,Alt_Qty,FOC) select '" + obj.docno + "',line_no,Against_Transfer_Code,item_code,Item_Pack_Size,qty,transfer_rate,Conv_Factor,Transfer_Qty,Transfer_UOM,Balance_Qty,Sale_UOM,Alt_Qty,FOC from CSA_SALE_TRANSFER "
                '    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'End If

                Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Try
            Dim isSaved As Boolean = True

            clsBatchInventory.DeleteData("CSA-Sale", strCode, trans)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,CSA_PLANT_LOCATION from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSASaleInvoice, clsCommon.myCstr(dt.Rows(0)("CSA_PLANT_LOCATION")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

            End If


            ''Delete from stock
            qry = "delete from tspl_inventory_movement where trans_type='csa-sale' and Source_Doc_No='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''-------end here====

            ''=============delete AR Invoice
            qry = "delete from TSPL_JOURNAL_DETAILS where Journal_No in (select TSPL_JOURNAL_MASTER.Journal_No from TSPL_JOURNAL_MASTER left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No " & _
                " where TSPL_JOURNAL_MASTER.Source_Code in ('AR-IN','AP-DN','AP-CN') and TSPL_Customer_Invoice_Head.trans_type='CSA' and TSPL_Customer_Invoice_Head. Against_Sale_No='" + strCode + "')"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Code in ('AR-IN','AP-DN','AP-CN') and Source_Doc_No in (select document_no from TSPL_Customer_Invoice_Head where trans_type='CSA' and Against_Sale_No='" + strCode + "')"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_REMITTANCE where Document_No in (select document_no from TSPL_Customer_Invoice_Head where trans_type='CSA' and Against_Sale_No='" + strCode + "')"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_Customer_Invoice_Head where trans_type='CSA' and Against_Sale_No='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''======================================

            ''--delete AP Invoice
            qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + strCode + "') "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_DETAILS where Journal_No in (select Journal_No from TSPL_JOURNAL_MASTER where Source_Code in ('AP-IN','AP-CN','AP-DN') and Source_Doc_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + strCode + "'))"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Code in ('AP-IN','AP-CN','AP-DN') and Source_Doc_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + strCode + "') "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_REMITTANCE where Document_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + strCode + "') "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''---------------------------------------------

            qry = "delete from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CSA_SALE_TRANSFER'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                qry = "delete from CSA_SALE_TRANSFER where document_code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            qry = "delete from TSPL_TRANSACTION_APPROVAL where Program_Code='" + clsUserMgtCode.frmCSASaleInvoice + "' and Document_No='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" + strCode + "' and Trans_Type='CSA'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, arrLoc, strCode, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsCSASaleInvoice()
        Try
            Dim isSaved As Boolean = True

            obj = clsCSASaleInvoice.GetData(strCode, arrLoc, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSASaleInvoice, obj.plant_loc_code, obj.docdate, trans)


            If obj Is Nothing OrElse clsCommon.myLen(obj.docno) <= 0 Then
                Throw New Exception("No Data Found.")
            End If

            If obj.isPost = "1" Then
                Throw New Exception("Record Already Posted On: " + obj.Posting_Date + "")
            End If

            'Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", obj.docno, trans)
            'If isResult = False Then
            '    trans.Commit()
            '    Return False
            'End If

            isSaved = isSaved AndAlso SendToInventoryMovement(obj, trans)

            'If obj.CSA_FOC_STATUS <> 1 Then
            createARInvoice(obj, trans)

            If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.FreightChargeOnCSASaleInvoice, clsFixedParameterCode.FreightChargeOnCSASaleInvoice, trans)) = "1", True, False)) = True Then
                ''itemwise
                SaveAPInvoice_Freight(obj, trans)
            Else
                SaveAPInvoice(obj, trans)
            End If

            'End If

            Dim strARInvNo = clsDBFuncationality.getSingleValue("Select Document_No from TSPL_Customer_Invoice_Head where Against_Sale_No='" + strCode + "'", trans)

            Dim qry As String = "Update TSPL_SD_SALE_INVOICE_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.docdate, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "'"
            qry += " where Document_Code='" + strCode + "' and trans_type='CSA'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Function

    Public Shared Function SetVendorTDSDetails(ByVal VendorCode As String, ByVal TotalCommision As Decimal, ByVal dtpdate As Date, ByVal trans As SqlTransaction) As clsRemittance
        Dim objRemittance As New clsRemittance()
        objRemittance = Nothing
        Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(VendorCode, trans)
        If objVendor IsNot Nothing Then
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objVendor.Nature_Of_Deduction, clsCommon.myCdbl(TotalCommision), trans)
            If (objDedDetails IsNot Nothing) Then
                objRemittance = New clsRemittance()
                objRemittance.Branch_Code = objVendor.Branch_Code
                objRemittance.Deduction_Code = objVendor.Nature_Of_Deduction
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
                objRemittance.IsTDSOverride = False
                objRemittance.IsApplyTDS = True
                objRemittance.Section_Code = objVendor.TDSSection
                objRemittance.Section_Description = objVendor.TDSSectionDescription
                objRemittance.Select_By = objVendor.VendorTypeCode

                objRemittance.Vendor_Code = objVendor.Vendor_Code
                objRemittance.Vendor_Name = clsVendorMaster.GetName(objVendor.Vendor_Code, trans)

                Dim qry As String = "select Year_Name from TSPL_TDS_FINANCIAL_YEAR where convert(date,'" + dtpdate + "',103)>=  convert(date,From_Date,103)  and convert(date,'" + dtpdate + "',103)<=convert(date,To_Date,103) "
                objRemittance.Fiscal_Year = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                objRemittance.Quarter = "First"
            End If
        End If

        Return objRemittance
    End Function

    Public Shared Function SaveAPInvoice(ByVal obj As clsCSASaleInvoice, ByVal trans As SqlTransaction) As Boolean
        Dim objVendorInvHead As New clsVedorInvoiceHead()
        Try
            Dim isSaved As Boolean = True

            If obj IsNot Nothing Then
                If obj.total_commision <= 0 Then
                    Return True ''when 0 amount for commission then,no AP invoice created.
                End If

                objVendorInvHead = New clsVedorInvoiceHead()
                objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.docdate, "dd/MM/yyyy")
                objVendorInvHead.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_code from tspl_location_master where location_code='" + obj.loc_code + "'", trans))
                objVendorInvHead.Vendor_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + objVendorInvHead.Vendor_Code + "'", trans))
                objVendorInvHead.Vendor_Invoice_No = obj.docno
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = obj.docdate
                objVendorInvHead.loc_code = clsCommon.myCstr(clsLocation.GetSegmentCode(obj.plant_loc_code, trans))
                objVendorInvHead.PROJECT_ID = ""
                objVendorInvHead.Form_ID = "CSA-SALE"
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                End If

                objVendorInvHead.Document_Type = "I" ''For Purchase Invoice Type
                objVendorInvHead.PO_Number = obj.po_no
                objVendorInvHead.Total_Tax = 0 ' obj.lbltaxamt

                objVendorInvHead.On_Hold = False

                objVendorInvHead.Description = "Vendor " + objVendorInvHead.Vendor_Code + "/" + objVendorInvHead.Vendor_Name + " .Against Invoice No " + obj.docno + ",dated: " + obj.docdate
                objVendorInvHead.Tax_Calculation_Type = obj.Tax_Calculation_Type
                objVendorInvHead.Tax_Group = obj.tax_group_code
                obj.TAX1 = ""
                obj.TAX2 = ""
                obj.TAX3 = ""
                obj.TAX4 = ""
                obj.TAX5 = ""
                obj.TAX6 = ""
                obj.TAX7 = ""
                obj.TAX8 = ""
                obj.TAX9 = ""
                obj.TAX10 = ""
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    objVendorInvHead.TAX1 = obj.TAX1
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
                        objVendorInvHead.TAX1_GLAC = clsCommon.myCstr(clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans))
                        objVendorInvHead.TAX1_GLAC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.plant_loc_code, trans))
                    End If
                    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
                    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
                End If
                If (clsCommon.myLen(obj.TAX2) > 0) Then
                    objVendorInvHead.TAX2 = obj.TAX2
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
                        objVendorInvHead.TAX2_GLAC = clsCommon.myCstr(clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans))
                        objVendorInvHead.TAX2_GLAC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.plant_loc_code, trans))
                    End If
                    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
                    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
                End If
                If (clsCommon.myLen(obj.TAX3) > 0) Then
                    objVendorInvHead.TAX3 = obj.TAX3
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
                        objVendorInvHead.TAX3_GLAC = clsCommon.myCstr(clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans))
                        objVendorInvHead.TAX3_GLAC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.plant_loc_code, trans))
                    End If
                    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
                    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
                End If
                If (clsCommon.myLen(obj.TAX4) > 0) Then
                    objVendorInvHead.TAX4 = obj.TAX4
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
                        objVendorInvHead.TAX4_GLAC = clsCommon.myCstr(clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans))
                        objVendorInvHead.TAX4_GLAC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.plant_loc_code, trans))
                    End If
                    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
                    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
                End If
                If (clsCommon.myLen(obj.TAX5) > 0) Then
                    objVendorInvHead.TAX5 = obj.TAX5
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
                        objVendorInvHead.TAX5_GLAC = clsCommon.myCstr(clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans))
                        objVendorInvHead.TAX5_GLAC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.plant_loc_code, trans))

                    End If
                    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
                    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
                End If
                If (clsCommon.myLen(obj.TAX6) > 0) Then
                    objVendorInvHead.TAX6 = obj.TAX6
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
                        objVendorInvHead.TAX6_GLAC = clsCommon.myCstr(clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans))
                        objVendorInvHead.TAX6_GLAC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.plant_loc_code, trans))
                    End If
                    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
                    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
                End If
                If (clsCommon.myLen(obj.TAX7) > 0) Then
                    objVendorInvHead.TAX7 = obj.TAX7
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
                        objVendorInvHead.TAX7_GLAC = clsCommon.myCstr(clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans))
                        objVendorInvHead.TAX7_GLAC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.loc_code, trans))

                    End If
                    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
                    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
                End If
                If (clsCommon.myLen(obj.TAX8) > 0) Then
                    objVendorInvHead.TAX8 = obj.TAX8
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
                        objVendorInvHead.TAX8_GLAC = clsCommon.myCstr(clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans))
                        objVendorInvHead.TAX8_GLAC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.loc_code, trans))
                    End If
                    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
                    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
                End If
                If (clsCommon.myLen(obj.TAX9) > 0) Then
                    objVendorInvHead.TAX9 = obj.TAX9
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
                        objVendorInvHead.TAX9_GLAC = clsCommon.myCstr(clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans))
                        objVendorInvHead.TAX9_GLAC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.loc_code, trans))
                    End If
                    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
                    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
                End If
                If (clsCommon.myLen(obj.TAX10) > 0) Then
                    objVendorInvHead.TAX10 = obj.TAX10
                    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
                        objVendorInvHead.TAX10_GLAC = clsCommon.myCstr(clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans))
                        objVendorInvHead.TAX10_GLAC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.loc_code, trans))
                    End If
                    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
                    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
                End If

                objVendorInvHead.Terms_Code = obj.term_code
                objVendorInvHead.Terms_Description = obj.term_desc
                objVendorInvHead.Due_Date = obj.due_date
                objVendorInvHead.Discount_Base = obj.total_commision
                objVendorInvHead.Discount_Amount = 0
                objVendorInvHead.Amount_Less_Discount = obj.total_commision
                objVendorInvHead.Document_Total = obj.total_commision
                objVendorInvHead.Balance_Amt = obj.total_commision
                objVendorInvHead.Against_POInvoice_No = "" 'obj.docno
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.plant_loc_code, trans))
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.plant_loc_code, trans))
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If

                '=============commision head=====================
                Dim qry As String = "select Commision_Acc from tspl_location_master where location_code='" + obj.loc_code + "' and vendor_code='" + objVendorInvHead.Vendor_Code + "'"
                Dim acc_code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                If clsCommon.myLen(acc_code) <= 0 OrElse clsCommon.CompairString(acc_code, """") = CompairStringResult.Equal Then
                    Throw New Exception("Please set Vendor Commision Account set for Vendor " + objVendorInvHead.Vendor_Code + "(" + objVendorInvHead.Vendor_Name + ") at location master.")
                End If

                acc_code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(acc_code, obj.plant_loc_code, trans))

                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                objVendorInvDetail.Detail_Line_No = 1
                objVendorInvDetail.GL_Account_Code = acc_code
                objVendorInvDetail.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + acc_code + "'", trans))
                objVendorInvDetail.Amount = obj.total_commision
                objVendorInvDetail.Discount_Per = 0 ' objPIDetail.Disc_Per
                objVendorInvDetail.Discount = 0 'objPIDetail.Disc_Amt
                objVendorInvDetail.Amount_less_Discount = obj.total_commision
                objVendorInvDetail.TAX1 = "" ' objPIDetail.TAX1
                objVendorInvDetail.TAX1_Rate = 0 ' objPIDetail.TAX1_Rate
                objVendorInvDetail.TAX1_Amt = 0 'objPIDetail.TAX1_Amt
                objVendorInvDetail.TAX1_Base_Amt = 0 ' objPIDetail.TAX1_Base_Amt
                objVendorInvDetail.TAX2 = "" 'objPIDetail.TAX2
                objVendorInvDetail.TAX2_Rate = 0 'objPIDetail.TAX2_Rate
                objVendorInvDetail.TAX2_Amt = 0 'objPIDetail.TAX2_Amt
                objVendorInvDetail.TAX2_Base_Amt = 0 ' objPIDetail.TAX2_Base_Amt
                objVendorInvDetail.TAX3 = "" 'objPIDetail.TAX3
                objVendorInvDetail.TAX3_Rate = 0 ' objPIDetail.TAX3_Rate
                objVendorInvDetail.TAX3_Amt = 0 ' objPIDetail.TAX3_Amt
                objVendorInvDetail.TAX3_Base_Amt = 0 ' objPIDetail.TAX3_Base_Amt
                objVendorInvDetail.TAX4 = "" ' objPIDetail.TAX4
                objVendorInvDetail.TAX4_Rate = 0 'objPIDetail.TAX4_Rate
                objVendorInvDetail.TAX4_Amt = 0 ' objPIDetail.TAX4_Amt
                objVendorInvDetail.TAX4_Base_Amt = 0 'objPIDetail.TAX4_Base_Amt
                objVendorInvDetail.TAX5 = "" ' objPIDetail.TAX5
                objVendorInvDetail.TAX5_Rate = 0 'objPIDetail.TAX5_Rate
                objVendorInvDetail.TAX5_Amt = 0 'objPIDetail.TAX5_Amt
                objVendorInvDetail.TAX5_Base_Amt = 0 ' objPIDetail.TAX5_Base_Amt
                objVendorInvDetail.TAX6 = "" ' objPIDetail.TAX6
                objVendorInvDetail.TAX6_Rate = 0 'objPIDetail.TAX6_Rate
                objVendorInvDetail.TAX6_Amt = 0 ' objPIDetail.TAX6_Amt
                objVendorInvDetail.TAX6_Base_Amt = 0 'objPIDetail.TAX6_Base_Amt
                objVendorInvDetail.TAX7 = "" 'objPIDetail.TAX7
                objVendorInvDetail.TAX7_Rate = 0 'objPIDetail.TAX7_Rate
                objVendorInvDetail.TAX7_Amt = 0 'objPIDetail.TAX7_Amt
                objVendorInvDetail.TAX7_Base_Amt = 0 'objPIDetail.TAX7_Base_Amt
                objVendorInvDetail.TAX8 = "" 'objPIDetail.TAX8
                objVendorInvDetail.TAX8_Rate = 0 'objPIDetail.TAX8_Rate
                objVendorInvDetail.TAX8_Amt = 0 'objPIDetail.TAX8_Amt
                objVendorInvDetail.TAX8_Base_Amt = 0 'objPIDetail.TAX8_Base_Amt
                objVendorInvDetail.TAX9 = "" ' objPIDetail.TAX9
                objVendorInvDetail.TAX9_Rate = 0 'objPIDetail.TAX9_Rate
                objVendorInvDetail.TAX9_Amt = 0 'objPIDetail.TAX9_Amt
                objVendorInvDetail.TAX9_Base_Amt = 0 'objPIDetail.TAX9_Base_Amt
                objVendorInvDetail.TAX10 = "" 'objPIDetail.TAX10
                objVendorInvDetail.TAX10_Rate = 0 'objPIDetail.TAX10_Rate
                objVendorInvDetail.TAX10_Amt = 0 'objPIDetail.TAX10_Amt
                objVendorInvDetail.TAX10_Base_Amt = 0 ' objPIDetail.TAX10_Base_Amt
                objVendorInvDetail.Total_Tax = 0 'objPIDetail.Total_Tax_Amt
                objVendorInvDetail.Total_Amount = obj.total_commision
                'objVendorInvDetail.Landed_Amount = obj.total_commision
                'objVendorInvHead.Total_Landed_Amt += obj.total_commision

                objVendorInvDetail.TAX1_Base_Amt = 0 ' objPIDetail.TAX1_Base_Amt
                objVendorInvDetail.TAX2_Base_Amt = 0 ' objPIDetail.TAX2_Base_Amt
                objVendorInvDetail.TAX3_Base_Amt = 0 'objPIDetail.TAX3_Base_Amt
                objVendorInvDetail.TAX4_Base_Amt = 0 'objPIDetail.TAX4_Base_Amt
                objVendorInvDetail.TAX5_Base_Amt = 0 'objPIDetail.TAX5_Base_Amt
                objVendorInvDetail.TAX6_Base_Amt = 0 'objPIDetail.TAX6_Base_Amt
                objVendorInvDetail.TAX7_Base_Amt = 0 '.TAX7_Base_Amt
                objVendorInvDetail.TAX8_Base_Amt = 0 'objPIDetail.TAX8_Base_Amt
                objVendorInvDetail.TAX9_Base_Amt = 0 'objPIDetail.TAX9_Base_Amt
                objVendorInvDetail.TAX10_Base_Amt = 0 '.TAX10_Base_Amt

                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If

                '==============TDS========================================
                Dim objRemittance As clsRemittance = SetVendorTDSDetails(objVendorInvHead.Vendor_Code, obj.total_commision, obj.docdate, trans)

                If objRemittance IsNot Nothing Then
                    '========================
                    Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objRemittance.Deduction_Code, clsCommon.myCdbl(obj.total_commision), trans)
                    If (objDedDetails IsNot Nothing AndAlso objRemittance.IsApplyTDS) Then
                        objRemittance.TDS_Per = objDedDetails.TDS
                        objRemittance.Surcharge_Per = objDedDetails.Surcharge
                        objRemittance.Edu_Cess_Per = objDedDetails.Educess
                        objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
                    End If
                    objRemittance.Calculated_TDS_Base = obj.total_commision
                    If Not objRemittance.IsTDSOverride Then
                        objRemittance.Actual_TDS_Base = obj.total_commision
                    End If

                    objRemittance.Calculated_TDS = (objRemittance.Calculated_TDS_Base * objRemittance.TDS_Per) / 100
                    objRemittance.Actual_TDS = (objRemittance.Actual_TDS_Base * objRemittance.TDS_Per) / 100

                    objRemittance.Calculated_Surcharge = (objRemittance.Calculated_TDS_Base * objRemittance.Surcharge_Per) / 100
                    objRemittance.Actual_Surcharge = (objRemittance.Actual_TDS_Base * objRemittance.Surcharge_Per) / 100

                    objRemittance.Calculated_Edu_Cess = (objRemittance.Calculated_TDS_Base * objRemittance.Edu_Cess_Per) / 100
                    objRemittance.Actual_Edu_Cess = (objRemittance.Actual_TDS_Base * objRemittance.Edu_Cess_Per) / 100

                    objRemittance.Calculated_Sec_Educess = (objRemittance.Calculated_TDS_Base * objRemittance.Sec_Educess_Per) / 100
                    objRemittance.Actual_Sec_Educess = (objRemittance.Actual_TDS_Base * objRemittance.Sec_Educess_Per) / 100

                    objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
                    objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess

                    objRemittance.Document_Type = objVendorInvHead.Document_Type
                    '======================

                    objVendorInvHead.RemittanceObject = New clsRemittance()
                    objVendorInvHead.RemittanceObject.Vendor_Code = objRemittance.Vendor_Code
                    objVendorInvHead.RemittanceObject.Vendor_Name = objRemittance.Vendor_Name
                    objVendorInvHead.RemittanceObject.Document_No = obj.docno
                    objVendorInvHead.RemittanceObject.Document_Date = obj.docdate
                    objVendorInvHead.RemittanceObject.Document_Type = objRemittance.Document_Type
                    objVendorInvHead.RemittanceObject.Document_Amount = objRemittance.Document_Amount
                    objVendorInvHead.RemittanceObject.Service_Type = objRemittance.Service_Type
                    objVendorInvHead.RemittanceObject.Actual_TDS_Base = objRemittance.Actual_TDS_Base
                    objVendorInvHead.RemittanceObject.Calculated_TDS_Base = objRemittance.Calculated_TDS_Base
                    objVendorInvHead.RemittanceObject.Actual_TDS = objRemittance.Actual_TDS
                    objVendorInvHead.RemittanceObject.Calculated_TDS = objRemittance.Calculated_TDS
                    objVendorInvHead.RemittanceObject.Actual_Surcharge = objRemittance.Actual_Surcharge
                    objVendorInvHead.RemittanceObject.Calculated_Surcharge = objRemittance.Calculated_Surcharge
                    objVendorInvHead.RemittanceObject.Actual_Edu_Cess = objRemittance.Actual_Edu_Cess
                    objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = objRemittance.Calculated_Edu_Cess
                    objVendorInvHead.RemittanceObject.Actual_Sec_Educess = objRemittance.Actual_Sec_Educess
                    objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = objRemittance.Calculated_Sec_Educess
                    objVendorInvHead.RemittanceObject.Actual_Total_TDS = objRemittance.Actual_Total_TDS
                    objVendorInvHead.RemittanceObject.Calculated_Total_TDS = objRemittance.Calculated_Total_TDS
                    objVendorInvHead.RemittanceObject.Fiscal_Year = objRemittance.Fiscal_Year
                    objVendorInvHead.RemittanceObject.Quarter = objRemittance.Quarter
                    objVendorInvHead.RemittanceObject.Section_Code = objRemittance.Section_Code
                    objVendorInvHead.RemittanceObject.Section_Description = objRemittance.Section_Description
                    objVendorInvHead.RemittanceObject.Branch_Code = objRemittance.Branch_Code
                    objVendorInvHead.RemittanceObject.Deduction_Code = objRemittance.Deduction_Code
                    objVendorInvHead.RemittanceObject.TDS_Per = objRemittance.TDS_Per
                    objVendorInvHead.RemittanceObject.Surcharge_Per = objRemittance.Surcharge_Per
                    objVendorInvHead.RemittanceObject.Edu_Cess_Per = objRemittance.Edu_Cess_Per
                    objVendorInvHead.RemittanceObject.Sec_Educess_Per = objRemittance.Sec_Educess_Per
                    objVendorInvHead.RemittanceObject.Select_By = objRemittance.Select_By
                    objVendorInvHead.RemittanceObject.IsTDSOverride = objRemittance.IsTDSOverride
                    objVendorInvHead.RemittanceObject.IsApplyTDS = objRemittance.IsApplyTDS

                    objVendorInvHead.TDS_Base_Actual_Amount = objRemittance.Actual_TDS_Base
                    objVendorInvHead.TDS_Base_Calculated_Amount = objRemittance.Calculated_TDS_Base
                    objVendorInvHead.TDS_Percentage = objRemittance.TDS_Per
                    objVendorInvHead.TDS_Actual_Amount = objRemittance.Actual_Total_TDS
                    objVendorInvHead.TDS_Calculated_Amount = objRemittance.Calculated_Total_TDS
                    objVendorInvHead.Nature_of_deduction = objRemittance.Deduction_Code
                    objVendorInvHead.Branch_Code = objRemittance.Branch_Code
                    objVendorInvHead.Balance_Amt = obj.total_commision - objRemittance.Actual_Total_TDS
                    objVendorInvHead.Section_Code = objRemittance.Section_Code
                End If
                '=============
                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If
                ''multicurrency
                objVendorInvHead.CURRENCY_CODE = obj.currency_code
                objVendorInvHead.ConvRate = 0 'obj.ConvRate
                objVendorInvHead.ApplicableFrom = obj.docdate  'obj.ApplicableFrom

                ''end multicurrency

                ''---------------check if there is already any AP is made,means entry is reposted after unpost
                qry = "select document_no from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + obj.docno + "'"
                Dim OLDAPNO As String = ""
                OLDAPNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                qry = "select voucher_no from TSPL_JOURNAL_MASTER where source_code='AP-IN' and source_doc_no='" + OLDAPNO + "' "
                Dim OLD_VoucherNO As String = ""
                OLD_VoucherNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                If clsCommon.myLen(OLDAPNO) > 0 Then
                    objVendorInvHead.Document_No = OLDAPNO
                    isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, False, trans, OLD_VoucherNO)
                Else
                    isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                End If
                ''---------------------------------------------------------------------------------------------

                'isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                'isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("CSA-SALE", objVendorInvHead.Document_No, "", trans, obj.docdate)

                'If objRemittance IsNot Nothing Then
                'Else
                'qry = "Update TSPL_PJV_HEAD set Status=1, Posting_Date='" + clsCommon.myCDate(obj.docdate, "dd/MM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Invoice_No='" + obj.docno + "'"
                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'End If

            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            objVendorInvHead = Nothing
        End Try
    End Function

    Public Shared Function SaveAPInvoice_Freight(ByVal obj As clsCSASaleInvoice, ByVal trans As SqlTransaction) As Boolean
        Dim objVendorInvHead As New clsVedorInvoiceHead()
        Dim dtMain As New DataTable()
        Try
            Dim isSaved As Boolean = True
            Dim taxvariable As String = " ,max(TSPL_SD_SALE_INVOICE_DETAIL.TAX1) as TAX1,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt,0)) as TAX1_Amt,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Base_Amt,0)) as TAX1_Base_Amt,max(TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate) as TAX1_Rate " & _
                    ",max(TSPL_SD_SALE_INVOICE_DETAIL.TAX2) as TAX2,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt,0)) as TAX2_Amt,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Base_Amt,0)) as TAX2_Base_Amt,max(TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate) as TAX2_Rate " & _
                    ",max(TSPL_SD_SALE_INVOICE_DETAIL.TAX3) as TAX3,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt,0)) as TAX3_Amt,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Base_Amt,0)) as TAX3_Base_Amt,max(TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate) as TAX3_Rate " & _
                    ",max(TSPL_SD_SALE_INVOICE_DETAIL.TAX4) as TAX4,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt,0)) as TAX4_Amt,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Base_Amt,0)) as TAX4_Base_Amt,max(TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate) as TAX4_Rate " & _
                    ",max(TSPL_SD_SALE_INVOICE_DETAIL.TAX5) as TAX5,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt,0)) as TAX5_Amt,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Base_Amt,0)) as TAX5_Base_Amt,max(TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate) as TAX5_Rate " & _
                    ",max(TSPL_SD_SALE_INVOICE_DETAIL.TAX6) as TAX6,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt,0)) as TAX6_Amt,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Base_Amt,0)) as TAX6_Base_Amt,max(TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate) as TAX6_Rate " & _
                    ",max(TSPL_SD_SALE_INVOICE_DETAIL.TAX7) as TAX7,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt,0)) as TAX7_Amt,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Base_Amt,0)) as TAX7_Base_Amt,max(TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate) as TAX7_Rate " & _
                    ",max(TSPL_SD_SALE_INVOICE_DETAIL.TAX8) as TAX8,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt,0)) as TAX8_Amt,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Base_Amt,0)) as TAX8_Base_Amt,max(TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate) as TAX8_Rate " & _
                    ",max(TSPL_SD_SALE_INVOICE_DETAIL.TAX9) as TAX9,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt,0)) as TAX9_Amt,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Base_Amt,0)) as TAX9_Base_Amt,max(TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate) as TAX9_Rate " & _
                    ",max(TSPL_SD_SALE_INVOICE_DETAIL.TAX10) as TAX10,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt,0)) as TAX10_Amt,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Base_Amt,0)) as TAX10_Base_Amt,max(TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate) as TAX10_Rate "

            ''============default no tax cal. so make all variables 0
            taxvariable = " ,'' as TAX1,0 as TAX1_Amt,0 as TAX1_Base_Amt,0 as TAX1_Rate " & _
                    ",'' as TAX2,0 as TAX2_Amt,0 as TAX2_Base_Amt,0 as TAX2_Rate " & _
                    ",'' as TAX3,0 as TAX3_Amt,0 as TAX3_Base_Amt,0 as TAX3_Rate " & _
                    ",'' as TAX4,0 as TAX4_Amt,0 as TAX4_Base_Amt,0 as TAX4_Rate " & _
                    ",'' as TAX5,0 as TAX5_Amt,0 as TAX5_Base_Amt,0 as TAX5_Rate " & _
                    ",'' as TAX6,0 as TAX6_Amt,0 as TAX6_Base_Amt,0 as TAX6_Rate " & _
                    ",'' as TAX7,0 as TAX7_Amt,0 as TAX7_Base_Amt,0 as TAX7_Rate " & _
                    ",'' as TAX8,0 as TAX8_Amt,0 as TAX8_Base_Amt,0 as TAX8_Rate " & _
                    ",'' as TAX9,0 as TAX9_Amt,0 as TAX9_Base_Amt,0 as TAX9_Rate " & _
                    ",'' as TAX10,0 as TAX10_Amt,0 as TAX10_Base_Amt,0 as TAX10_Rate "

            If obj IsNot Nothing Then
                Dim qry As String = "select TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Vendor_Code,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.commission_ac_code,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.freight_ac_code,max(tspl_vendor_master.vendor_name) as vendor_name,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.CSA_Commision_Amount,0)) as cmsn_amt,sum(isnull(freight_amt,0)) as frght_amt " & _
                    " " + taxvariable + " " & _
                    " from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL left outer join TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD on TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Doc_No=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Doc_No " & _
                    " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Item_Code left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
                    " left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Vendor_Code " & _
                    " where TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Cust_Code='" + obj.cust_code + "' and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='CSA' and TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" + obj.docno + "'  group by TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Vendor_Code,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.commission_ac_code,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.freight_ac_code "
                dtMain = clsDBFuncationality.GetDataTable(qry, trans)

                If dtMain IsNot Nothing AndAlso dtMain.Rows.Count > 0 Then
                    For Each dr As DataRow In dtMain.Rows
                        If clsCommon.myCdbl(dr("cmsn_amt")) > 0 Then
                            objVendorInvHead = New clsVedorInvoiceHead()

                            objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.docdate, "dd/MM/yyyy")
                            objVendorInvHead.Vendor_Code = clsCommon.myCstr(dr("vendor_code"))
                            objVendorInvHead.Vendor_Name = clsCommon.myCstr(dr("vendor_name"))
                            objVendorInvHead.Vendor_Invoice_No = obj.docno
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = obj.docdate
                            objVendorInvHead.loc_code = clsCommon.myCstr(clsLocation.GetSegmentCode(obj.plant_loc_code, trans))
                            objVendorInvHead.PROJECT_ID = ""
                            objVendorInvHead.Form_ID = "CSA-SALE"
                            objVendorInvHead.Addition_Doc_Type = "Commission"
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If

                            objVendorInvHead.Document_Type = "I" ''For Purchase Invoice Type
                            objVendorInvHead.PO_Number = obj.po_no
                            objVendorInvHead.Total_Tax = 0 ' obj.lbltaxamt

                            objVendorInvHead.On_Hold = False

                            objVendorInvHead.Description = "Vendor " + objVendorInvHead.Vendor_Code + "/" + objVendorInvHead.Vendor_Name + " .Against Invoice No " + obj.docno + ",dated: " + obj.docdate + " - " + objVendorInvHead.Addition_Doc_Type
                            objVendorInvHead.Tax_Calculation_Type = obj.Tax_Calculation_Type
                            objVendorInvHead.Tax_Group = obj.tax_group_code
                            obj.TAX1 = ""
                            obj.TAX2 = ""
                            obj.TAX3 = ""
                            obj.TAX4 = ""
                            obj.TAX5 = ""
                            obj.TAX6 = ""
                            obj.TAX7 = ""
                            obj.TAX8 = ""
                            obj.TAX9 = ""
                            obj.TAX10 = ""

                            objVendorInvHead.Terms_Code = obj.term_code
                            objVendorInvHead.Terms_Description = obj.term_desc
                            objVendorInvHead.Due_Date = obj.due_date
                            objVendorInvHead.Discount_Base = clsCommon.myCdbl(dr("cmsn_amt"))
                            objVendorInvHead.Discount_Amount = 0
                            objVendorInvHead.Amount_Less_Discount = clsCommon.myCdbl(dr("cmsn_amt"))
                            objVendorInvHead.Document_Total = clsCommon.myCdbl(dr("cmsn_amt"))
                            objVendorInvHead.Balance_Amt = clsCommon.myCdbl(dr("cmsn_amt"))
                            objVendorInvHead.Against_POInvoice_No = "" 'obj.docno
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.plant_loc_code, trans))
                                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.plant_loc_code, trans))
                                End If
                            End If
                            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                Throw New Exception("Please set the vendor payable Account")
                            End If

                            '=============commision head=====================
                            Dim acc_code As String = clsCommon.myCstr(dr("commission_ac_code"))

                            If clsCommon.myLen(acc_code) <= 0 OrElse clsCommon.CompairString(acc_code, """") = CompairStringResult.Equal Then
                                Throw New Exception("Please set Vendor Commision Account set for Vendor " + objVendorInvHead.Vendor_Code + "(" + objVendorInvHead.Vendor_Name + ") at Itemwise Freight/Commission master.")
                            End If

                            acc_code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(acc_code, obj.plant_loc_code, trans))

                            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                            objVendorInvDetail.Detail_Line_No = 1
                            objVendorInvDetail.GL_Account_Code = acc_code
                            objVendorInvDetail.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + acc_code + "'", trans))
                            objVendorInvDetail.Amount = clsCommon.myCdbl(dr("cmsn_amt"))
                            objVendorInvDetail.Discount_Per = 0 ' objPIDetail.Disc_Per
                            objVendorInvDetail.Discount = 0 'objPIDetail.Disc_Amt
                            objVendorInvDetail.Amount_less_Discount = clsCommon.myCdbl(dr("cmsn_amt"))
                            objVendorInvDetail.TAX1 = "" ' objPIDetail.TAX1
                            objVendorInvDetail.TAX1_Rate = 0 ' objPIDetail.TAX1_Rate
                            objVendorInvDetail.TAX1_Amt = 0 'objPIDetail.TAX1_Amt
                            objVendorInvDetail.TAX1_Base_Amt = 0 ' objPIDetail.TAX1_Base_Amt
                            objVendorInvDetail.TAX2 = "" 'objPIDetail.TAX2
                            objVendorInvDetail.TAX2_Rate = 0 'objPIDetail.TAX2_Rate
                            objVendorInvDetail.TAX2_Amt = 0 'objPIDetail.TAX2_Amt
                            objVendorInvDetail.TAX2_Base_Amt = 0 ' objPIDetail.TAX2_Base_Amt
                            objVendorInvDetail.TAX3 = "" 'objPIDetail.TAX3
                            objVendorInvDetail.TAX3_Rate = 0 ' objPIDetail.TAX3_Rate
                            objVendorInvDetail.TAX3_Amt = 0 ' objPIDetail.TAX3_Amt
                            objVendorInvDetail.TAX3_Base_Amt = 0 ' objPIDetail.TAX3_Base_Amt
                            objVendorInvDetail.TAX4 = "" ' objPIDetail.TAX4
                            objVendorInvDetail.TAX4_Rate = 0 'objPIDetail.TAX4_Rate
                            objVendorInvDetail.TAX4_Amt = 0 ' objPIDetail.TAX4_Amt
                            objVendorInvDetail.TAX4_Base_Amt = 0 'objPIDetail.TAX4_Base_Amt
                            objVendorInvDetail.TAX5 = "" ' objPIDetail.TAX5
                            objVendorInvDetail.TAX5_Rate = 0 'objPIDetail.TAX5_Rate
                            objVendorInvDetail.TAX5_Amt = 0 'objPIDetail.TAX5_Amt
                            objVendorInvDetail.TAX5_Base_Amt = 0 ' objPIDetail.TAX5_Base_Amt
                            objVendorInvDetail.TAX6 = "" ' objPIDetail.TAX6
                            objVendorInvDetail.TAX6_Rate = 0 'objPIDetail.TAX6_Rate
                            objVendorInvDetail.TAX6_Amt = 0 ' objPIDetail.TAX6_Amt
                            objVendorInvDetail.TAX6_Base_Amt = 0 'objPIDetail.TAX6_Base_Amt
                            objVendorInvDetail.TAX7 = "" 'objPIDetail.TAX7
                            objVendorInvDetail.TAX7_Rate = 0 'objPIDetail.TAX7_Rate
                            objVendorInvDetail.TAX7_Amt = 0 'objPIDetail.TAX7_Amt
                            objVendorInvDetail.TAX7_Base_Amt = 0 'objPIDetail.TAX7_Base_Amt
                            objVendorInvDetail.TAX8 = "" 'objPIDetail.TAX8
                            objVendorInvDetail.TAX8_Rate = 0 'objPIDetail.TAX8_Rate
                            objVendorInvDetail.TAX8_Amt = 0 'objPIDetail.TAX8_Amt
                            objVendorInvDetail.TAX8_Base_Amt = 0 'objPIDetail.TAX8_Base_Amt
                            objVendorInvDetail.TAX9 = "" ' objPIDetail.TAX9
                            objVendorInvDetail.TAX9_Rate = 0 'objPIDetail.TAX9_Rate
                            objVendorInvDetail.TAX9_Amt = 0 'objPIDetail.TAX9_Amt
                            objVendorInvDetail.TAX9_Base_Amt = 0 'objPIDetail.TAX9_Base_Amt
                            objVendorInvDetail.TAX10 = "" 'objPIDetail.TAX10
                            objVendorInvDetail.TAX10_Rate = 0 'objPIDetail.TAX10_Rate
                            objVendorInvDetail.TAX10_Amt = 0 'objPIDetail.TAX10_Amt
                            objVendorInvDetail.TAX10_Base_Amt = 0 ' objPIDetail.TAX10_Base_Amt
                            objVendorInvDetail.Total_Tax = 0 'objPIDetail.Total_Tax_Amt
                            objVendorInvDetail.Total_Amount = clsCommon.myCdbl(dr("cmsn_amt"))
                            'objVendorInvDetail.Landed_Amount = clsCommon.myCdbl(dr("cmsn_amt"))
                            'objVendorInvHead.Total_Landed_Amt += clsCommon.myCdbl(dr("cmsn_amt"))

                            objVendorInvDetail.TAX1_Base_Amt = 0 ' objPIDetail.TAX1_Base_Amt
                            objVendorInvDetail.TAX2_Base_Amt = 0 ' objPIDetail.TAX2_Base_Amt
                            objVendorInvDetail.TAX3_Base_Amt = 0 'objPIDetail.TAX3_Base_Amt
                            objVendorInvDetail.TAX4_Base_Amt = 0 'objPIDetail.TAX4_Base_Amt
                            objVendorInvDetail.TAX5_Base_Amt = 0 'objPIDetail.TAX5_Base_Amt
                            objVendorInvDetail.TAX6_Base_Amt = 0 'objPIDetail.TAX6_Base_Amt
                            objVendorInvDetail.TAX7_Base_Amt = 0 '.TAX7_Base_Amt
                            objVendorInvDetail.TAX8_Base_Amt = 0 'objPIDetail.TAX8_Base_Amt
                            objVendorInvDetail.TAX9_Base_Amt = 0 'objPIDetail.TAX9_Base_Amt
                            objVendorInvDetail.TAX10_Base_Amt = 0 '.TAX10_Base_Amt

                            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)

                            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                objVendorInvHead.Arr.Add(objVendorInvDetail)
                            End If

                            '==============TDS========================================
                            Dim objRemittance As clsRemittance = SetVendorTDSDetails(objVendorInvHead.Vendor_Code, clsCommon.myCdbl(dr("cmsn_amt")), obj.docdate, trans)

                            If objRemittance IsNot Nothing Then
                                '========================
                                Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objRemittance.Deduction_Code, clsCommon.myCdbl(dr("cmsn_amt")), trans)
                                If (objDedDetails IsNot Nothing AndAlso objRemittance.IsApplyTDS) Then
                                    objRemittance.TDS_Per = objDedDetails.TDS
                                    objRemittance.Surcharge_Per = objDedDetails.Surcharge
                                    objRemittance.Edu_Cess_Per = objDedDetails.Educess
                                    objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
                                End If
                                objRemittance.Calculated_TDS_Base = clsCommon.myCdbl(dr("cmsn_amt"))
                                If Not objRemittance.IsTDSOverride Then
                                    objRemittance.Actual_TDS_Base = clsCommon.myCdbl(dr("cmsn_amt"))
                                End If

                                objRemittance.Calculated_TDS = (objRemittance.Calculated_TDS_Base * objRemittance.TDS_Per) / 100
                                objRemittance.Actual_TDS = (objRemittance.Actual_TDS_Base * objRemittance.TDS_Per) / 100

                                objRemittance.Calculated_Surcharge = (objRemittance.Calculated_TDS_Base * objRemittance.Surcharge_Per) / 100
                                objRemittance.Actual_Surcharge = (objRemittance.Actual_TDS_Base * objRemittance.Surcharge_Per) / 100

                                objRemittance.Calculated_Edu_Cess = (objRemittance.Calculated_TDS_Base * objRemittance.Edu_Cess_Per) / 100
                                objRemittance.Actual_Edu_Cess = (objRemittance.Actual_TDS_Base * objRemittance.Edu_Cess_Per) / 100

                                objRemittance.Calculated_Sec_Educess = (objRemittance.Calculated_TDS_Base * objRemittance.Sec_Educess_Per) / 100
                                objRemittance.Actual_Sec_Educess = (objRemittance.Actual_TDS_Base * objRemittance.Sec_Educess_Per) / 100

                                objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
                                objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess

                                objRemittance.Document_Type = objVendorInvHead.Document_Type
                                '======================

                                objVendorInvHead.RemittanceObject = New clsRemittance()
                                objVendorInvHead.RemittanceObject.Vendor_Code = objRemittance.Vendor_Code
                                objVendorInvHead.RemittanceObject.Vendor_Name = objRemittance.Vendor_Name
                                objVendorInvHead.RemittanceObject.Document_No = obj.docno
                                objVendorInvHead.RemittanceObject.Document_Date = obj.docdate
                                objVendorInvHead.RemittanceObject.Document_Type = objRemittance.Document_Type
                                objVendorInvHead.RemittanceObject.Document_Amount = objRemittance.Document_Amount
                                objVendorInvHead.RemittanceObject.Service_Type = objRemittance.Service_Type
                                objVendorInvHead.RemittanceObject.Actual_TDS_Base = objRemittance.Actual_TDS_Base
                                objVendorInvHead.RemittanceObject.Calculated_TDS_Base = objRemittance.Calculated_TDS_Base
                                objVendorInvHead.RemittanceObject.Actual_TDS = objRemittance.Actual_TDS
                                objVendorInvHead.RemittanceObject.Calculated_TDS = objRemittance.Calculated_TDS
                                objVendorInvHead.RemittanceObject.Actual_Surcharge = objRemittance.Actual_Surcharge
                                objVendorInvHead.RemittanceObject.Calculated_Surcharge = objRemittance.Calculated_Surcharge
                                objVendorInvHead.RemittanceObject.Actual_Edu_Cess = objRemittance.Actual_Edu_Cess
                                objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = objRemittance.Calculated_Edu_Cess
                                objVendorInvHead.RemittanceObject.Actual_Sec_Educess = objRemittance.Actual_Sec_Educess
                                objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = objRemittance.Calculated_Sec_Educess
                                objVendorInvHead.RemittanceObject.Actual_Total_TDS = objRemittance.Actual_Total_TDS
                                objVendorInvHead.RemittanceObject.Calculated_Total_TDS = objRemittance.Calculated_Total_TDS
                                objVendorInvHead.RemittanceObject.Fiscal_Year = objRemittance.Fiscal_Year
                                objVendorInvHead.RemittanceObject.Quarter = objRemittance.Quarter
                                objVendorInvHead.RemittanceObject.Section_Code = objRemittance.Section_Code
                                objVendorInvHead.RemittanceObject.Section_Description = objRemittance.Section_Description
                                objVendorInvHead.RemittanceObject.Branch_Code = objRemittance.Branch_Code
                                objVendorInvHead.RemittanceObject.Deduction_Code = objRemittance.Deduction_Code
                                objVendorInvHead.RemittanceObject.TDS_Per = objRemittance.TDS_Per
                                objVendorInvHead.RemittanceObject.Surcharge_Per = objRemittance.Surcharge_Per
                                objVendorInvHead.RemittanceObject.Edu_Cess_Per = objRemittance.Edu_Cess_Per
                                objVendorInvHead.RemittanceObject.Sec_Educess_Per = objRemittance.Sec_Educess_Per
                                objVendorInvHead.RemittanceObject.Select_By = objRemittance.Select_By
                                objVendorInvHead.RemittanceObject.IsTDSOverride = objRemittance.IsTDSOverride
                                objVendorInvHead.RemittanceObject.IsApplyTDS = objRemittance.IsApplyTDS

                                objVendorInvHead.TDS_Base_Actual_Amount = objRemittance.Actual_TDS_Base
                                objVendorInvHead.TDS_Base_Calculated_Amount = objRemittance.Calculated_TDS_Base
                                objVendorInvHead.TDS_Percentage = objRemittance.TDS_Per
                                objVendorInvHead.TDS_Actual_Amount = objRemittance.Actual_Total_TDS
                                objVendorInvHead.TDS_Calculated_Amount = objRemittance.Calculated_Total_TDS
                                objVendorInvHead.Nature_of_deduction = objRemittance.Deduction_Code
                                objVendorInvHead.Branch_Code = objRemittance.Branch_Code
                                objVendorInvHead.Balance_Amt = clsCommon.myCdbl(dr("cmsn_amt")) - objRemittance.Actual_Total_TDS
                                objVendorInvHead.Section_Code = objRemittance.Section_Code
                            End If
                            '=============
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            ''multicurrency
                            objVendorInvHead.CURRENCY_CODE = obj.currency_code
                            objVendorInvHead.ConvRate = 0 'obj.ConvRate
                            objVendorInvHead.ApplicableFrom = obj.docdate  'obj.ApplicableFrom

                            ''end multicurrency

                            ''---------------check if there is already any AP is made,means entry is reposted after unpost
                            Dim OLDAPNO As String = ""
                            Dim OLD_VoucherNO As String = ""

                            qry = "select document_no from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + obj.docno + "' and vendor_code='" + objVendorInvHead.Vendor_Code + "' and Addition_Doc_Type='Commission'"
                            OLDAPNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                            qry = "select voucher_no from TSPL_JOURNAL_MASTER where source_code='AP-IN' and source_doc_no='" + OLDAPNO + "' "
                            OLD_VoucherNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                            If clsCommon.myLen(OLDAPNO) > 0 Then
                                objVendorInvHead.Document_No = OLDAPNO
                                isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, False, trans, OLD_VoucherNO)
                            Else
                                isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            End If
                            ''---------------------------------------------------------------------------------------------
                        End If ''end commission

                        ''==================freight
                        If clsCommon.myCdbl(dr("frght_amt")) > 0 Then
                            objVendorInvHead = New clsVedorInvoiceHead()

                            objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.docdate, "dd/MM/yyyy")
                            objVendorInvHead.Vendor_Code = clsCommon.myCstr(dr("vendor_code"))
                            objVendorInvHead.Vendor_Name = clsCommon.myCstr(dr("vendor_name"))
                            objVendorInvHead.Vendor_Invoice_No = obj.docno
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = obj.docdate
                            objVendorInvHead.loc_code = clsCommon.myCstr(clsLocation.GetSegmentCode(obj.plant_loc_code, trans))
                            objVendorInvHead.PROJECT_ID = ""
                            objVendorInvHead.Form_ID = "CSA-SALE"
                            objVendorInvHead.Addition_Doc_Type = "Freight"
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If

                            objVendorInvHead.Document_Type = "I" ''For Purchase Invoice Type
                            objVendorInvHead.PO_Number = obj.po_no
                            objVendorInvHead.Total_Tax = 0 ' obj.lbltaxamt

                            objVendorInvHead.On_Hold = False

                            objVendorInvHead.Description = "Vendor " + objVendorInvHead.Vendor_Code + "/" + objVendorInvHead.Vendor_Name + " .Against Invoice No " + obj.docno + ",dated: " + obj.docdate + " - " + objVendorInvHead.Addition_Doc_Type
                            objVendorInvHead.Tax_Calculation_Type = obj.Tax_Calculation_Type
                            objVendorInvHead.Tax_Group = obj.tax_group_code
                            obj.TAX1 = ""
                            obj.TAX2 = ""
                            obj.TAX3 = ""
                            obj.TAX4 = ""
                            obj.TAX5 = ""
                            obj.TAX6 = ""
                            obj.TAX7 = ""
                            obj.TAX8 = ""
                            obj.TAX9 = ""
                            obj.TAX10 = ""

                            objVendorInvHead.Terms_Code = obj.term_code
                            objVendorInvHead.Terms_Description = obj.term_desc
                            objVendorInvHead.Due_Date = obj.due_date
                            objVendorInvHead.Discount_Base = clsCommon.myCdbl(dr("frght_amt"))
                            objVendorInvHead.Discount_Amount = 0
                            objVendorInvHead.Amount_Less_Discount = clsCommon.myCdbl(dr("frght_amt"))
                            objVendorInvHead.Document_Total = clsCommon.myCdbl(dr("frght_amt"))
                            objVendorInvHead.Balance_Amt = clsCommon.myCdbl(dr("frght_amt"))
                            objVendorInvHead.Against_POInvoice_No = "" 'obj.docno
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.plant_loc_code, trans))
                                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.plant_loc_code, trans))
                                End If
                            End If
                            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                Throw New Exception("Please set the vendor payable Account")
                            End If

                            '=============commision head=====================
                            Dim acc_code As String = clsCommon.myCstr(dr("freight_ac_code"))

                            If clsCommon.myLen(acc_code) <= 0 OrElse clsCommon.CompairString(acc_code, """") = CompairStringResult.Equal Then
                                Throw New Exception("Please set Vendor Freight Account set for Vendor " + objVendorInvHead.Vendor_Code + "(" + objVendorInvHead.Vendor_Name + ") at Itemwise Freight/Commission master.")
                            End If

                            acc_code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(acc_code, obj.plant_loc_code, trans))

                            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                            objVendorInvDetail.Detail_Line_No = 1
                            objVendorInvDetail.GL_Account_Code = acc_code
                            objVendorInvDetail.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + acc_code + "'", trans))
                            objVendorInvDetail.Amount = clsCommon.myCdbl(dr("frght_amt"))
                            objVendorInvDetail.Discount_Per = 0 ' objPIDetail.Disc_Per
                            objVendorInvDetail.Discount = 0 'objPIDetail.Disc_Amt
                            objVendorInvDetail.Amount_less_Discount = clsCommon.myCdbl(dr("frght_amt"))
                            objVendorInvDetail.TAX1 = "" ' objPIDetail.TAX1
                            objVendorInvDetail.TAX1_Rate = 0 ' objPIDetail.TAX1_Rate
                            objVendorInvDetail.TAX1_Amt = 0 'objPIDetail.TAX1_Amt
                            objVendorInvDetail.TAX1_Base_Amt = 0 ' objPIDetail.TAX1_Base_Amt
                            objVendorInvDetail.TAX2 = "" 'objPIDetail.TAX2
                            objVendorInvDetail.TAX2_Rate = 0 'objPIDetail.TAX2_Rate
                            objVendorInvDetail.TAX2_Amt = 0 'objPIDetail.TAX2_Amt
                            objVendorInvDetail.TAX2_Base_Amt = 0 ' objPIDetail.TAX2_Base_Amt
                            objVendorInvDetail.TAX3 = "" 'objPIDetail.TAX3
                            objVendorInvDetail.TAX3_Rate = 0 ' objPIDetail.TAX3_Rate
                            objVendorInvDetail.TAX3_Amt = 0 ' objPIDetail.TAX3_Amt
                            objVendorInvDetail.TAX3_Base_Amt = 0 ' objPIDetail.TAX3_Base_Amt
                            objVendorInvDetail.TAX4 = "" ' objPIDetail.TAX4
                            objVendorInvDetail.TAX4_Rate = 0 'objPIDetail.TAX4_Rate
                            objVendorInvDetail.TAX4_Amt = 0 ' objPIDetail.TAX4_Amt
                            objVendorInvDetail.TAX4_Base_Amt = 0 'objPIDetail.TAX4_Base_Amt
                            objVendorInvDetail.TAX5 = "" ' objPIDetail.TAX5
                            objVendorInvDetail.TAX5_Rate = 0 'objPIDetail.TAX5_Rate
                            objVendorInvDetail.TAX5_Amt = 0 'objPIDetail.TAX5_Amt
                            objVendorInvDetail.TAX5_Base_Amt = 0 ' objPIDetail.TAX5_Base_Amt
                            objVendorInvDetail.TAX6 = "" ' objPIDetail.TAX6
                            objVendorInvDetail.TAX6_Rate = 0 'objPIDetail.TAX6_Rate
                            objVendorInvDetail.TAX6_Amt = 0 ' objPIDetail.TAX6_Amt
                            objVendorInvDetail.TAX6_Base_Amt = 0 'objPIDetail.TAX6_Base_Amt
                            objVendorInvDetail.TAX7 = "" 'objPIDetail.TAX7
                            objVendorInvDetail.TAX7_Rate = 0 'objPIDetail.TAX7_Rate
                            objVendorInvDetail.TAX7_Amt = 0 'objPIDetail.TAX7_Amt
                            objVendorInvDetail.TAX7_Base_Amt = 0 'objPIDetail.TAX7_Base_Amt
                            objVendorInvDetail.TAX8 = "" 'objPIDetail.TAX8
                            objVendorInvDetail.TAX8_Rate = 0 'objPIDetail.TAX8_Rate
                            objVendorInvDetail.TAX8_Amt = 0 'objPIDetail.TAX8_Amt
                            objVendorInvDetail.TAX8_Base_Amt = 0 'objPIDetail.TAX8_Base_Amt
                            objVendorInvDetail.TAX9 = "" ' objPIDetail.TAX9
                            objVendorInvDetail.TAX9_Rate = 0 'objPIDetail.TAX9_Rate
                            objVendorInvDetail.TAX9_Amt = 0 'objPIDetail.TAX9_Amt
                            objVendorInvDetail.TAX9_Base_Amt = 0 'objPIDetail.TAX9_Base_Amt
                            objVendorInvDetail.TAX10 = "" 'objPIDetail.TAX10
                            objVendorInvDetail.TAX10_Rate = 0 'objPIDetail.TAX10_Rate
                            objVendorInvDetail.TAX10_Amt = 0 'objPIDetail.TAX10_Amt
                            objVendorInvDetail.TAX10_Base_Amt = 0 ' objPIDetail.TAX10_Base_Amt
                            objVendorInvDetail.Total_Tax = 0 'objPIDetail.Total_Tax_Amt
                            objVendorInvDetail.Total_Amount = clsCommon.myCdbl(dr("frght_amt"))
                            'objVendorInvDetail.Landed_Amount = clsCommon.myCdbl(dr("frght_amt"))
                            'objVendorInvHead.Total_Landed_Amt += clsCommon.myCdbl(dr("frght_amt"))

                            objVendorInvDetail.TAX1_Base_Amt = 0 ' objPIDetail.TAX1_Base_Amt
                            objVendorInvDetail.TAX2_Base_Amt = 0 ' objPIDetail.TAX2_Base_Amt
                            objVendorInvDetail.TAX3_Base_Amt = 0 'objPIDetail.TAX3_Base_Amt
                            objVendorInvDetail.TAX4_Base_Amt = 0 'objPIDetail.TAX4_Base_Amt
                            objVendorInvDetail.TAX5_Base_Amt = 0 'objPIDetail.TAX5_Base_Amt
                            objVendorInvDetail.TAX6_Base_Amt = 0 'objPIDetail.TAX6_Base_Amt
                            objVendorInvDetail.TAX7_Base_Amt = 0 '.TAX7_Base_Amt
                            objVendorInvDetail.TAX8_Base_Amt = 0 'objPIDetail.TAX8_Base_Amt
                            objVendorInvDetail.TAX9_Base_Amt = 0 'objPIDetail.TAX9_Base_Amt
                            objVendorInvDetail.TAX10_Base_Amt = 0 '.TAX10_Base_Amt

                            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)

                            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                objVendorInvHead.Arr.Add(objVendorInvDetail)
                            End If

                            '==============TDS========================================
                            Dim objRemittance As clsRemittance = SetVendorTDSDetails(objVendorInvHead.Vendor_Code, clsCommon.myCdbl(dr("frght_amt")), obj.docdate, trans)

                            If objRemittance IsNot Nothing Then
                                '========================
                                Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objRemittance.Deduction_Code, clsCommon.myCdbl(dr("frght_amt")), trans)
                                If (objDedDetails IsNot Nothing AndAlso objRemittance.IsApplyTDS) Then
                                    objRemittance.TDS_Per = objDedDetails.TDS
                                    objRemittance.Surcharge_Per = objDedDetails.Surcharge
                                    objRemittance.Edu_Cess_Per = objDedDetails.Educess
                                    objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
                                End If
                                objRemittance.Calculated_TDS_Base = clsCommon.myCdbl(dr("frght_amt"))
                                If Not objRemittance.IsTDSOverride Then
                                    objRemittance.Actual_TDS_Base = clsCommon.myCdbl(dr("frght_amt"))
                                End If

                                objRemittance.Calculated_TDS = (objRemittance.Calculated_TDS_Base * objRemittance.TDS_Per) / 100
                                objRemittance.Actual_TDS = (objRemittance.Actual_TDS_Base * objRemittance.TDS_Per) / 100

                                objRemittance.Calculated_Surcharge = (objRemittance.Calculated_TDS_Base * objRemittance.Surcharge_Per) / 100
                                objRemittance.Actual_Surcharge = (objRemittance.Actual_TDS_Base * objRemittance.Surcharge_Per) / 100

                                objRemittance.Calculated_Edu_Cess = (objRemittance.Calculated_TDS_Base * objRemittance.Edu_Cess_Per) / 100
                                objRemittance.Actual_Edu_Cess = (objRemittance.Actual_TDS_Base * objRemittance.Edu_Cess_Per) / 100

                                objRemittance.Calculated_Sec_Educess = (objRemittance.Calculated_TDS_Base * objRemittance.Sec_Educess_Per) / 100
                                objRemittance.Actual_Sec_Educess = (objRemittance.Actual_TDS_Base * objRemittance.Sec_Educess_Per) / 100

                                objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
                                objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess
                                '======================

                                objVendorInvHead.RemittanceObject = New clsRemittance()
                                objVendorInvHead.RemittanceObject.Vendor_Code = objRemittance.Vendor_Code
                                objVendorInvHead.RemittanceObject.Vendor_Name = objRemittance.Vendor_Name
                                objVendorInvHead.RemittanceObject.Document_No = obj.docno
                                objVendorInvHead.RemittanceObject.Document_Date = obj.docdate
                                objVendorInvHead.RemittanceObject.Document_Type = objRemittance.Document_Type
                                objVendorInvHead.RemittanceObject.Document_Amount = objRemittance.Document_Amount
                                objVendorInvHead.RemittanceObject.Service_Type = objRemittance.Service_Type
                                objVendorInvHead.RemittanceObject.Actual_TDS_Base = objRemittance.Actual_TDS_Base
                                objVendorInvHead.RemittanceObject.Calculated_TDS_Base = objRemittance.Calculated_TDS_Base
                                objVendorInvHead.RemittanceObject.Actual_TDS = objRemittance.Actual_TDS
                                objVendorInvHead.RemittanceObject.Calculated_TDS = objRemittance.Calculated_TDS
                                objVendorInvHead.RemittanceObject.Actual_Surcharge = objRemittance.Actual_Surcharge
                                objVendorInvHead.RemittanceObject.Calculated_Surcharge = objRemittance.Calculated_Surcharge
                                objVendorInvHead.RemittanceObject.Actual_Edu_Cess = objRemittance.Actual_Edu_Cess
                                objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = objRemittance.Calculated_Edu_Cess
                                objVendorInvHead.RemittanceObject.Actual_Sec_Educess = objRemittance.Actual_Sec_Educess
                                objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = objRemittance.Calculated_Sec_Educess
                                objVendorInvHead.RemittanceObject.Actual_Total_TDS = objRemittance.Actual_Total_TDS
                                objVendorInvHead.RemittanceObject.Calculated_Total_TDS = objRemittance.Calculated_Total_TDS
                                objVendorInvHead.RemittanceObject.Fiscal_Year = objRemittance.Fiscal_Year
                                objVendorInvHead.RemittanceObject.Quarter = objRemittance.Quarter
                                objVendorInvHead.RemittanceObject.Section_Code = objRemittance.Section_Code
                                objVendorInvHead.RemittanceObject.Section_Description = objRemittance.Section_Description
                                objVendorInvHead.RemittanceObject.Branch_Code = objRemittance.Branch_Code
                                objVendorInvHead.RemittanceObject.Deduction_Code = objRemittance.Deduction_Code
                                objVendorInvHead.RemittanceObject.TDS_Per = objRemittance.TDS_Per
                                objVendorInvHead.RemittanceObject.Surcharge_Per = objRemittance.Surcharge_Per
                                objVendorInvHead.RemittanceObject.Edu_Cess_Per = objRemittance.Edu_Cess_Per
                                objVendorInvHead.RemittanceObject.Sec_Educess_Per = objRemittance.Sec_Educess_Per
                                objVendorInvHead.RemittanceObject.Select_By = objRemittance.Select_By
                                objVendorInvHead.RemittanceObject.IsTDSOverride = objRemittance.IsTDSOverride
                                objVendorInvHead.RemittanceObject.IsApplyTDS = objRemittance.IsApplyTDS

                                objVendorInvHead.TDS_Base_Actual_Amount = objRemittance.Actual_TDS_Base
                                objVendorInvHead.TDS_Base_Calculated_Amount = objRemittance.Calculated_TDS_Base
                                objVendorInvHead.TDS_Percentage = objRemittance.TDS_Per
                                objVendorInvHead.TDS_Actual_Amount = objRemittance.Actual_Total_TDS
                                objVendorInvHead.TDS_Calculated_Amount = objRemittance.Calculated_Total_TDS
                                objVendorInvHead.Nature_of_deduction = objRemittance.Deduction_Code
                                objVendorInvHead.Branch_Code = objRemittance.Branch_Code
                                objVendorInvHead.Balance_Amt = clsCommon.myCdbl(dr("frght_amt")) - objRemittance.Actual_Total_TDS
                                objVendorInvHead.Section_Code = objRemittance.Section_Code
                            End If
                            '=============
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            ''multicurrency
                            objVendorInvHead.CURRENCY_CODE = obj.currency_code
                            objVendorInvHead.ConvRate = 0 'obj.ConvRate
                            objVendorInvHead.ApplicableFrom = obj.docdate  'obj.ApplicableFrom

                            ''end multicurrency

                            ''---------------check if there is already any AP is made,means entry is reposted after unpost
                            Dim OLDAPNO As String = ""
                            Dim OLD_VoucherNO As String = ""

                            qry = "select document_no from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + obj.docno + "' and vendor_code='" + objVendorInvHead.Vendor_Code + "' and Addition_Doc_Type='Freight'"
                            OLDAPNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                            qry = "select voucher_no from TSPL_JOURNAL_MASTER where source_code='AP-IN' and source_doc_no='" + OLDAPNO + "' "
                            OLD_VoucherNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                            If clsCommon.myLen(OLDAPNO) > 0 Then
                                objVendorInvHead.Document_No = OLDAPNO
                                isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, False, trans, OLD_VoucherNO)
                            Else
                                isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            End If
                            ''---------------------------------------------------------------------------------------------
                        End If ''end freight

                    Next ''end main dr
                End If ''end main dt
            End If ''end obj cond.

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            objVendorInvHead = Nothing
        End Try
    End Function

    Public Shared Function SendToInventoryMovement(ByVal obj As clsCSASaleInvoice, Optional ByVal trans As SqlTransaction = Nothing) As Boolean

        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim isSaved As Boolean = True

        Dim strRgpNo As String = Nothing
        Dim intCounter As Integer = 0
        For Each objTr As clsCSASaleInvoiceItem In obj.Arr_Item
            intCounter = intCounter + 1

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
            objInventoryMovemnt.Location_Code = obj.loc_code
            objInventoryMovemnt.Item_Code = objTr.Item_Code
            objInventoryMovemnt.Item_Desc = objTr.Item_Desc
            objInventoryMovemnt.Qty = objTr.Qty  '+ objTr.Free_Qty
            objInventoryMovemnt.UOM = objTr.Unit_code
            objInventoryMovemnt.Basic_Cost = objTr.Total_Basic_Amt
            objInventoryMovemnt.MRP = objTr.sale_rate
            objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt
            objInventoryMovemnt.Net_Cost = objTr.Total_Tax_Amt
            objInventoryMovemnt.BatchSkipOnSetting = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.BatchSkipCSAReturn, clsFixedParameterCode.BatchSkipCSAReturn, trans)) = "1", True, False))
            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "RM"
            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "OT"
            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "FT"
            End If

            ''================calculate====cost=================================
            Dim cost As Decimal = 0

            cost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.loc_code, objTr.Qty, obj.docdate, clsCommon.GETSERVERDATE(trans), True, trans)
            objInventoryMovemnt.FIFO_Cost = cost

            cost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.loc_code, objTr.Qty, obj.docdate, clsCommon.GETSERVERDATE(trans), True, trans)
            objInventoryMovemnt.Avg_Cost = cost

            cost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, objTr.Item_Code, obj.loc_code, objTr.Qty, obj.docdate, clsCommon.GETSERVERDATE(trans), True, trans)
            objInventoryMovemnt.LIFO_Cost = cost
            '==================================================================

            objInventoryMovemnt.ItemType = strItemTypeToSave

            objInventoryMovemnt.Cust_Code = obj.cust_code
            objInventoryMovemnt.Cust_Name = obj.cust_name

            ArrInventoryMovement.Add(objInventoryMovemnt)
        Next
        isSaved = isSaved AndAlso clsInventoryMovement.SaveData("CSA-SALE", obj.docno, obj.docdate, clsCommon.GetPrintDate(obj.docdate, "dd/MM/yyyy"), ArrInventoryMovement, trans)

        ArrInventoryMovement = Nothing
        Return isSaved
    End Function

    Private Shared Function createARInvoice(ByVal obj As clsCSASaleInvoice, ByVal trans As SqlTransaction) As Boolean
        Try
            '=================Added by preeti Gupta==========================
            Dim ItemWiseCSAAccount As Boolean = False
            Dim AlwSaleMfgAcctng As Boolean = False

            ItemWiseCSAAccount = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowItemWiseCSAAccountingON_CSASale, clsFixedParameterCode.AllowItemWiseCSAAccountingON_CSASale, trans)) = "1", True, False))
            AlwSaleMfgAcctng = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow_SaleMfgACONCSAPatti, clsFixedParameterCode.Allow_SaleMfgACONCSAPatti, trans)) = "1", True, False))


            ''''''''''''''''''''''''''''''''''For Making AR Invoice
            Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)

            Dim StopGLForConsignment As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.StopGLEntryForConsignmentAtCSATransfer, clsFixedParameterCode.StopGLEntryForConsignmentAtCSATransfer, trans))
            If StopGLForConsignment = "1" Then
                isSkipCogsGL = False
            End If

            Dim stck_trans_value As Double = Nothing
            Dim GainLoss As Double = Nothing

            Dim objCustInv As New clsCustomerInvoiceHead()
            ''objCustInv.Document_No ''Will be Generateed
            objCustInv.Document_Date = obj.docdate
            objCustInv.Document_Type = "I"
            objCustInv.Document_Total = obj.document_amt

            objCustInv.RoundOffAmount = obj.RoundOffAmount

            objCustInv.Customer_Code = obj.cust_code
            objCustInv.Customer_Name = obj.cust_name
            objCustInv.Posting_Date = clsCommon.myCDate(clsCommon.GETSERVERDATE(trans))
            Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.cust_code + "'"
            objCustInv.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            ''objCustInv.Order_No
            objCustInv.loc_code = clsCommon.myCstr(clsLocation.GetSegmentCode(obj.plant_loc_code, trans))
            objCustInv.On_Hold = 0
            'objCustInv.Remarks = obj.Remarks
            objCustInv.Description = "AR Invoice against CSA Sale Patti no. " + obj.docno + ",dated: " + obj.docdate + ""
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                objCustInv.Tax_Group = obj.tax_group_code
                objCustInv.TAX1 = obj.TAX1
                objCustInv.TAX1_Rate = obj.TAX1_Rate
                objCustInv.TAX1_Amt = obj.TAX1_Amt
                objCustInv.TAX2 = obj.TAX2_Amt
                objCustInv.TAX2_Rate = obj.TAX2_Rate
                objCustInv.TAX2_Amt = obj.TAX2_Amt
                objCustInv.TAX3 = obj.TAX3
                objCustInv.TAX3_Rate = obj.TAX3_Rate
                objCustInv.TAX3_Amt = obj.TAX3_Amt
                objCustInv.TAX4 = obj.TAX4
                objCustInv.TAX4_Rate = obj.TAX4_Rate
                objCustInv.TAX4_Amt = obj.TAX4_Amt
                objCustInv.TAX5 = obj.TAX5
                objCustInv.TAX5_Rate = obj.TAX5_Rate
                objCustInv.TAX5_Amt = obj.TAX5_Amt
                objCustInv.TAX6 = obj.TAX6
                objCustInv.TAX6_Rate = obj.TAX6_Rate
                objCustInv.TAX6_Amt = obj.TAX6_Amt
                objCustInv.TAX7 = obj.TAX7
                objCustInv.TAX7_Rate = obj.TAX7_Rate
                objCustInv.TAX7_Amt = obj.TAX7_Amt
                objCustInv.TAX8 = obj.TAX8
                objCustInv.TAX8_Rate = obj.TAX8_Rate
                objCustInv.TAX8_Amt = obj.TAX8_Amt
                objCustInv.TAX9 = obj.TAX9
                objCustInv.TAX9_Rate = obj.TAX9_Rate
                objCustInv.TAX9_Amt = obj.TAX9_Amt
                objCustInv.TAX1 = obj.TAX1
                objCustInv.TAX1_Rate = obj.TAX1_Rate
                objCustInv.TAX1_Amt = obj.TAX1_Amt
                objCustInv.Total_Tax = obj.lbltaxamt
                objCustInv.Tax1_BAmount = obj.TAX1_Base_Amt
                objCustInv.Tax2_BAmount = obj.TAX2_Base_Amt
                objCustInv.Tax3_BAmount = obj.TAX3_Base_Amt
                objCustInv.Tax4_BAmount = obj.TAX4_Base_Amt
                objCustInv.Tax5_BAmount = obj.TAX5_Base_Amt
                objCustInv.Tax6_BAmount = obj.TAX6_Base_Amt
                objCustInv.Tax7_BAmount = obj.TAX7_Base_Amt
                objCustInv.Tax8_BAmount = obj.TAX8_Base_Amt
                objCustInv.Tax9_BAmount = obj.TAX9_Base_Amt
                objCustInv.Tax1_BAmount = obj.TAX1_Base_Amt

                objCustInv.Document_Total = obj.document_amt + obj.lbltaxamt ''done on 02/02/2017 ref by Amit sir
            Else
                objCustInv.Tax_Group = "" 'obj.tax_group_code
                objCustInv.TAX1 = "" 'obj.TAX1
                objCustInv.TAX1_Rate = 0 ' obj.TAX1_Rate
                objCustInv.TAX1_Amt = 0 ' obj.TAX1_Amt
                objCustInv.TAX2 = "" ' obj.TAX2_Amt
                objCustInv.TAX2_Rate = 0 'obj.TAX2_Rate
                objCustInv.TAX2_Amt = 0 'obj.TAX2_Amt
                objCustInv.TAX3 = "" ' obj.TAX3
                objCustInv.TAX3_Rate = 0 ' obj.TAX3_Rate
                objCustInv.TAX3_Amt = 0 ' obj.TAX3_Amt
                objCustInv.TAX4 = "" ' obj.TAX4
                objCustInv.TAX4_Rate = 0 'obj.TAX4_Rate
                objCustInv.TAX4_Amt = 0 ' obj.TAX4_Amt
                objCustInv.TAX5 = "" ' obj.TAX5
                objCustInv.TAX5_Rate = 0 'obj.TAX5_Rate
                objCustInv.TAX5_Amt = 0 'obj.TAX5_Amt
                objCustInv.TAX6 = "" ' obj.TAX6
                objCustInv.TAX6_Rate = 0 ' obj.TAX6_Rate
                objCustInv.TAX6_Amt = 0 'obj.TAX6_Amt
                objCustInv.TAX7 = "" ' obj.TAX7
                objCustInv.TAX7_Rate = 0 ' obj.TAX7_Rate
                objCustInv.TAX7_Amt = 0 ' obj.TAX7_Amt
                objCustInv.TAX8 = "" 'obj.TAX8
                objCustInv.TAX8_Rate = 0 ' obj.TAX8_Rate
                objCustInv.TAX8_Amt = 0 ' obj.TAX8_Amt
                objCustInv.TAX9 = "" ' obj.TAX9
                objCustInv.TAX9_Rate = 0 ' obj.TAX9_Rate
                objCustInv.TAX9_Amt = 0 ' obj.TAX9_Amt
                objCustInv.TAX10 = "" 'obj.TAX10
                objCustInv.TAX10_Rate = 0 ' obj.TAX10_Rate
                objCustInv.TAX10_Amt = 0 ' obj.TAX10_Amt
                objCustInv.Total_Tax = 0 ' obj.lbltaxamt
                objCustInv.Tax1_BAmount = 0 ' obj.TAX1_Base_Amt
                objCustInv.Tax2_BAmount = 0 ' obj.TAX2_Base_Amt
                objCustInv.Tax3_BAmount = 0 ' obj.TAX3_Base_Amt
                objCustInv.Tax4_BAmount = 0 ' obj.TAX4_Base_Amt
                objCustInv.Tax5_BAmount = 0 ' obj.TAX5_Base_Amt
                objCustInv.Tax6_BAmount = 0 'obj.TAX6_Base_Amt
                objCustInv.Tax7_BAmount = 0 ' obj.TAX7_Base_Amt
                objCustInv.Tax8_BAmount = 0 ' obj.TAX8_Base_Amt
                objCustInv.Tax9_BAmount = 0 ' obj.TAX9_Base_Amt
                objCustInv.Tax10_BAmount = 0 ' obj.TAX10_Base_Amt
            End If
            objCustInv.Balance_Amt = obj.document_amt
            objCustInv.Terms_Code = obj.term_code
            'objCustInv.PROJECT_ID = obj.PROJECT_ID

            '' currency details
            objCustInv.CURRENCY_CODE = obj.currency_code
            objCustInv.ConvRate = obj.cnvrsn_rate
            objCustInv.ApplicableFrom = obj.applicable_from
            objCustInv.Trans_Type = "CSA"

            qry = "select Terms_Code,Terms_Desc,No_Days from TSPL_TERMS_MASTER where Terms_Code='" + obj.term_code + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                objCustInv.Terms_Description = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
                objCustInv.Due_Date = obj.docdate.AddDays(clsCommon.myCdbl(dt.Rows(0)("No_Days")))
            End If

            If obj.amt_with_disc > 0 Then
                objCustInv.Discount_Percentage = IIf(obj.amt_with_disc > 0, obj.lbldisc_amt * 100 / obj.amt_with_disc, 0)
            Else
                objCustInv.Discount_Percentage = 0
            End If
            objCustInv.Discount_Base = obj.amt_with_disc
            objCustInv.Discount_Amount = obj.lbldisc_amt


            ''=======================UDL================================================
            If AlwSaleMfgAcctng Then
                objCustInv.Amount_Less_Discount = obj.amt_after_disc
            Else
                objCustInv.Amount_Less_Discount = 0
            End If
            ''========================end here================================================================



            dt = clsDBFuncationality.GetDataTable("select Receivable_Control_acct,Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
                If clsCommon.myCdbl(obj.lbldisc_amt) > 0 Then
                    objCustInv.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Receipts_Discount_acct"))
                End If
            End If

            If clsCommon.CompairString(obj.Excisable, "E") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then ''excisable
                objCustInv.TAX1 = obj.TAX1
                objCustInv.TAX1_Amt = obj.TAX1_Amt
                objCustInv.Tax1_BAmount = obj.TAX1_Base_Amt
                objCustInv.TAX1_Rate = obj.TAX1_Rate

                objCustInv.TAX2 = obj.TAX2
                objCustInv.TAX2_Amt = obj.TAX2_Amt
                objCustInv.Tax2_BAmount = obj.TAX2_Base_Amt
                objCustInv.TAX2_Rate = obj.TAX2_Rate

                objCustInv.TAX3 = obj.TAX3
                objCustInv.TAX3_Amt = obj.TAX3_Amt
                objCustInv.Tax3_BAmount = obj.TAX3_Base_Amt
                objCustInv.TAX3_Rate = obj.TAX3_Rate

                objCustInv.TAX4 = obj.TAX4
                objCustInv.TAX4_Amt = obj.TAX4_Amt
                objCustInv.Tax4_BAmount = obj.TAX4_Base_Amt
                objCustInv.TAX4_Rate = obj.TAX4_Rate

                objCustInv.TAX5 = obj.TAX5
                objCustInv.TAX5_Amt = obj.TAX5_Amt
                objCustInv.Tax5_BAmount = obj.TAX5_Base_Amt
                objCustInv.TAX5_Rate = obj.TAX5_Rate

                objCustInv.TAX6 = obj.TAX6
                objCustInv.TAX6_Amt = obj.TAX6_Amt
                objCustInv.Tax6_BAmount = obj.TAX6_Base_Amt
                objCustInv.TAX6_Rate = obj.TAX6_Rate

                objCustInv.TAX7 = obj.TAX7
                objCustInv.TAX7_Amt = obj.TAX7_Amt
                objCustInv.Tax7_BAmount = obj.TAX7_Base_Amt
                objCustInv.TAX7_Rate = obj.TAX7_Rate

                objCustInv.TAX8 = obj.TAX8
                objCustInv.TAX8_Amt = obj.TAX8_Amt
                objCustInv.Tax8_BAmount = obj.TAX8_Base_Amt
                objCustInv.TAX8_Rate = obj.TAX8_Rate

                objCustInv.TAX9 = obj.TAX9
                objCustInv.TAX9_Amt = obj.TAX9_Amt
                objCustInv.Tax9_BAmount = obj.TAX9_Base_Amt
                objCustInv.TAX9_Rate = obj.TAX9_Rate

                objCustInv.TAX10 = obj.TAX10
                objCustInv.TAX10_Amt = obj.TAX10_Amt
                objCustInv.Tax10_BAmount = obj.TAX10_Base_Amt
                objCustInv.TAX10_Rate = obj.TAX10_Rate
                If obj.TAX1_Amt > 0 AndAlso clsCommon.myLen(obj.TAX1) > 0 Then
                    objCustInv.TAX1_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
                    objCustInv.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX1_GLAC, obj.plant_loc_code, trans)
                End If
                If obj.TAX2_Amt > 0 AndAlso clsCommon.myLen(obj.TAX2) > 0 Then
                    objCustInv.TAX2_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
                    objCustInv.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX2_GLAC, obj.plant_loc_code, trans)
                End If
                If obj.TAX3_Amt > 0 AndAlso clsCommon.myLen(obj.TAX3) > 0 Then
                    objCustInv.TAX3_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
                    objCustInv.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX3_GLAC, obj.plant_loc_code, trans)
                End If
                If obj.TAX4_Amt > 0 AndAlso clsCommon.myLen(obj.TAX4) > 0 Then
                    objCustInv.TAX4_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
                    objCustInv.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX4_GLAC, obj.plant_loc_code, trans)
                End If
                If obj.TAX5_Amt > 0 AndAlso clsCommon.myLen(obj.TAX5) > 0 Then
                    objCustInv.TAX5_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
                    objCustInv.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX5_GLAC, obj.plant_loc_code, trans)
                End If
                If obj.TAX6_Amt > 0 AndAlso clsCommon.myLen(obj.TAX6) > 0 Then
                    objCustInv.TAX6_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
                    objCustInv.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX6_GLAC, obj.plant_loc_code, trans)
                End If
                If obj.TAX7_Amt > 0 AndAlso clsCommon.myLen(obj.TAX7) > 0 Then
                    objCustInv.TAX7_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
                    objCustInv.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX7_GLAC, obj.plant_loc_code, trans)
                End If
                If obj.TAX8_Amt > 0 AndAlso clsCommon.myLen(obj.TAX8) > 0 Then
                    objCustInv.TAX8_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
                    objCustInv.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX8_GLAC, obj.plant_loc_code, trans)
                End If
                If obj.TAX9_Amt > 0 AndAlso clsCommon.myLen(obj.TAX9) > 0 Then
                    objCustInv.TAX9_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
                    objCustInv.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX9_GLAC, obj.plant_loc_code, trans)
                End If
                If obj.TAX10_Amt > 0 AndAlso clsCommon.myLen(obj.TAX10) > 0 Then
                    objCustInv.TAX10_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
                    objCustInv.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX10_GLAC, obj.plant_loc_code, trans)
                End If
            Else
                If obj.TAX1_Amt > 0 AndAlso clsCommon.myLen(obj.TAX1) > 0 Then
                    objCustInv.TAX1_GLAC = "" ' clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
                    objCustInv.TAX1_GLAC = "" ' clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX1_GLAC, obj.loc_code, trans)
                End If
                If obj.TAX2_Amt > 0 AndAlso clsCommon.myLen(obj.TAX2) > 0 Then
                    objCustInv.TAX2_GLAC = "" ' clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
                    objCustInv.TAX2_GLAC = "" 'clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX2_GLAC, obj.loc_code, trans)
                End If
                If obj.TAX3_Amt > 0 AndAlso clsCommon.myLen(obj.TAX3) > 0 Then
                    objCustInv.TAX3_GLAC = "" ' clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
                    objCustInv.TAX3_GLAC = "" ' clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX3_GLAC, obj.loc_code, trans)
                End If
                If obj.TAX4_Amt > 0 AndAlso clsCommon.myLen(obj.TAX4) > 0 Then
                    objCustInv.TAX4_GLAC = "" ' clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
                    objCustInv.TAX4_GLAC = "" ' clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX4_GLAC, obj.loc_code, trans)
                End If
                If obj.TAX5_Amt > 0 AndAlso clsCommon.myLen(obj.TAX5) > 0 Then
                    objCustInv.TAX5_GLAC = "" ' clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
                    objCustInv.TAX5_GLAC = "" ' clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX5_GLAC, obj.loc_code, trans)
                End If
                If obj.TAX6_Amt > 0 AndAlso clsCommon.myLen(obj.TAX6) > 0 Then
                    objCustInv.TAX6_GLAC = "" 'clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
                    objCustInv.TAX6_GLAC = "" 'clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX6_GLAC, obj.loc_code, trans)
                End If
                If obj.TAX7_Amt > 0 AndAlso clsCommon.myLen(obj.TAX7) > 0 Then
                    objCustInv.TAX7_GLAC = "" ' clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
                    objCustInv.TAX7_GLAC = "" 'clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX7_GLAC, obj.loc_code, trans)
                End If
                If obj.TAX8_Amt > 0 AndAlso clsCommon.myLen(obj.TAX8) > 0 Then
                    objCustInv.TAX8_GLAC = "" ' clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
                    objCustInv.TAX8_GLAC = "" ' clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX8_GLAC, obj.loc_code, trans)
                End If
                If obj.TAX9_Amt > 0 AndAlso clsCommon.myLen(obj.TAX9) > 0 Then
                    objCustInv.TAX9_GLAC = "" 'clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
                    objCustInv.TAX9_GLAC = "" 'clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX9_GLAC, obj.loc_code, trans)
                End If
                If obj.TAX10_Amt > 0 AndAlso clsCommon.myLen(obj.TAX10) > 0 Then
                    objCustInv.TAX10_GLAC = "" ' clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
                    objCustInv.TAX10_GLAC = "" ' clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX10_GLAC, obj.loc_code, trans)
                End If
            End If


            objCustInv.RefDocType = "CSA SALE"
            objCustInv.RefDocNo = obj.docno
            objCustInv.Add_Charge_Code1 = obj.Add_Charge_Code1
            objCustInv.Add_Charge_Name1 = obj.Add_Charge_Name1
            objCustInv.Add_Charge_Amt1 = obj.Add_Charge_Amt1
            objCustInv.Add_Charge_Code2 = obj.Add_Charge_Code2
            objCustInv.Add_Charge_Name2 = obj.Add_Charge_Name2
            objCustInv.Add_Charge_Amt2 = obj.Add_Charge_Amt2
            objCustInv.Add_Charge_Code3 = obj.Add_Charge_Code3
            objCustInv.Add_Charge_Name3 = obj.Add_Charge_Name3
            objCustInv.Add_Charge_Amt3 = obj.Add_Charge_Amt3
            objCustInv.Add_Charge_Code4 = obj.Add_Charge_Code4
            objCustInv.Add_Charge_Name4 = obj.Add_Charge_Name4
            objCustInv.Add_Charge_Amt4 = obj.Add_Charge_Amt4
            objCustInv.Add_Charge_Code5 = obj.Add_Charge_Code5
            objCustInv.Add_Charge_Name5 = obj.Add_Charge_Name5
            objCustInv.Add_Charge_Amt5 = obj.Add_Charge_Amt5
            objCustInv.Add_Charge_Code6 = obj.Add_Charge_Code6
            objCustInv.Add_Charge_Name6 = obj.Add_Charge_Name6
            objCustInv.Add_Charge_Amt6 = obj.Add_Charge_Amt6
            objCustInv.Add_Charge_Code7 = obj.Add_Charge_Code7
            objCustInv.Add_Charge_Name7 = obj.Add_Charge_Name7
            objCustInv.Add_Charge_Amt7 = obj.Add_Charge_Amt7
            objCustInv.Add_Charge_Code8 = obj.Add_Charge_Code8
            objCustInv.Add_Charge_Name8 = obj.Add_Charge_Name8
            objCustInv.Add_Charge_Amt8 = obj.Add_Charge_Amt8
            objCustInv.Add_Charge_Code9 = obj.Add_Charge_Code9
            objCustInv.Add_Charge_Name9 = obj.Add_Charge_Name9
            objCustInv.Add_Charge_Amt9 = obj.Add_Charge_Amt9
            objCustInv.Add_Charge_Code10 = obj.Add_Charge_Code10
            objCustInv.Add_Charge_Name10 = obj.Add_Charge_Name10
            objCustInv.Add_Charge_Amt10 = obj.Add_Charge_Amt10
            objCustInv.Total_Add_Charge = obj.total_add_chrg
            objCustInv.Tax_Calculation_Type = obj.Tax_Calculation_Type
            ''objCustInv.Status
            ''objCustInv.AgainstScrap
            objCustInv.Against_Sale_No = obj.docno
            Dim counter As Integer = 1
            Dim counter_trans As Integer = 1
            Dim counter_GL As Integer = 1

            objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)

            Dim Item_Cost As Double = Nothing
            Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
            For Each objTr As clsCSASaleInvoiceItem In obj.Arr_Item

                '====================Item gl entry===============================
                If clsCommon.CompairString(objTr.FOC_Item, "1") = CompairStringResult.Equal Then
                    Continue For
                End If
                objCustInvTR = New clsCustomerInvoiceDetail()
                objCustInvTR.SNo = counter
                If clsCommon.CompairString(objTr.Row_Type, "Item") = CompairStringResult.Equal And clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal Then
                    dt = clsItemMaster.GetSaleAccGLAC(objTr.Item_Code, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set sale account for item" + objTr.Item_Code)
                    End If
                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Sales_Account"))
                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.plant_loc_code, trans))
                    objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans))
                Else ''for row type misl.
                    If clsCommon.CompairString(objTr.Scheme_Item, "N") = CompairStringResult.Equal Then
                        Dim objAC As clsAdditionalCharge = clsAdditionalCharge.GetData(objTr.Item_Code, NavigatorType.Current, trans)
                        If objAC Is Nothing Then
                            Throw New Exception("Please set GL Ac from addition charge" + objTr.Item_Code)
                        End If
                        objCustInvTR.GL_Account_Code = objAC.Account_Code
                        objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.plant_loc_code, trans))
                        objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans))
                    End If
                End If

                objCustInvTR.Amount = objTr.Amount
                
                objCustInvTR.Discount_Per = 0 ' objTr.Disc_Per
                objCustInvTR.Discount = 0 ' objTr.Disc_Amt


                ''================udl===================
                If AlwSaleMfgAcctng Then
                    objCustInvTR.Amount_less_Discount = objTr.Amount
                Else
                    objCustInvTR.Amount_less_Discount = 0
                End If
                ''''==================

                objCustInvTR.TAX1 = "" ' objTr.TAX1
                objCustInvTR.TAX1_Rate = 0 ' objTr.TAX1_Rate
                objCustInvTR.TAX1_Amt = 0 ' objTr.TAX1_Amt
                objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                objCustInvTR.TAX2 = "" ' objTr.TAX2
                objCustInvTR.TAX2_Rate = 0 'objTr.TAX2_Rate
                objCustInvTR.TAX2_Amt = 0 ' objTr.TAX2_Amt
                objCustInvTR.TAX2_Base_Amt = 0 ' objTr.TAX2_Base_Amt
                objCustInvTR.TAX3 = "" ' objTr.TAX3
                objCustInvTR.TAX3_Rate = 0 ' objTr.TAX3_Rate
                objCustInvTR.TAX3_Amt = 0 ' objTr.TAX3_Amt
                objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                objCustInvTR.TAX4 = "" ' objTr.TAX4
                objCustInvTR.TAX4_Rate = 0 ' objTr.TAX4_Rate
                objCustInvTR.TAX4_Amt = 0 'objTr.TAX4_Amt
                objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                objCustInvTR.TAX5 = "" 'objTr.TAX5
                objCustInvTR.TAX5_Rate = 0 'objTr.TAX5_Rate
                objCustInvTR.TAX5_Amt = 0 ' objTr.TAX5_Amt
                objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                objCustInvTR.TAX6 = "" ' objTr.TAX6
                objCustInvTR.TAX6_Rate = 0 ' objTr.TAX6_Rate
                objCustInvTR.TAX6_Amt = 0 'objTr.TAX6_Amt
                objCustInvTR.TAX6_Base_Amt = 0 'objTr.TAX6_Base_Amt
                objCustInvTR.TAX7 = "" 'objTr.TAX7
                objCustInvTR.TAX7_Rate = 0 'objTr.TAX7_Rate
                objCustInvTR.TAX7_Amt = 0 'objTr.TAX7_Amt
                objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                objCustInvTR.TAX8 = "" ' objTr.TAX8
                objCustInvTR.TAX8_Rate = 0 ' objTr.TAX8_Rate
                objCustInvTR.TAX8_Amt = 0 'objTr.TAX8_Amt
                objCustInvTR.TAX8_Base_Amt = 0 'objTr.TAX8_Base_Amt
                objCustInvTR.TAX9 = "" ' objTr.TAX9
                objCustInvTR.TAX9_Rate = 0 ' objTr.TAX9_Rate
                objCustInvTR.TAX9_Amt = 0 'objTr.TAX9_Amt
                objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                objCustInvTR.TAX10 = "" ' objTr.TAX10
                objCustInvTR.TAX10_Rate = 0 ' objTr.TAX10_Rate
                objCustInvTR.TAX10_Amt = 0 ' objTr.TAX10_Amt
                objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                objCustInvTR.Total_Tax = 0 ' objTr.Item_Tax
                objCustInvTR.Total_Amount = objTr.Item_Net_Amt
                objCustInvTR.Remarks = objTr.remarks
                objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                objCustInvTR.TAX2_Base_Amt = 0 'objTr.TAX2_Base_Amt
                objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                objCustInvTR.TAX6_Base_Amt = 0 ' objTr.TAX6_Base_Amt
                objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                objCustInvTR.TAX8_Base_Amt = 0 ' objTr.TAX8_Base_Amt
                objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt

                ''=================================02/02/2017 ref by Amit sir
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                    objCustInvTR.TAX1 = objTr.TAX1
                    objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
                    objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
                    objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
                    objCustInvTR.TAX2 = objTr.TAX2
                    objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
                    objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
                    objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
                    objCustInvTR.TAX3 = objTr.TAX3
                    objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
                    objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
                    objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
                    objCustInvTR.TAX4 = objTr.TAX4
                    objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
                    objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
                    objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
                    objCustInvTR.TAX5 = objTr.TAX5
                    objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
                    objCustInvTR.TAX5_Amt = objTr.TAX5_Amt
                    objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
                    objCustInvTR.TAX6 = objTr.TAX6
                    objCustInvTR.TAX6_Rate = objTr.TAX6_Rate
                    objCustInvTR.TAX6_Amt = objTr.TAX6_Amt
                    objCustInvTR.TAX6_Base_Amt = objTr.TAX6_Base_Amt
                    objCustInvTR.TAX7 = objTr.TAX7
                    objCustInvTR.TAX7_Rate = objTr.TAX7_Rate
                    objCustInvTR.TAX7_Amt = objTr.TAX7_Amt
                    objCustInvTR.TAX7_Base_Amt = objTr.TAX7_Base_Amt
                    objCustInvTR.TAX8 = objTr.TAX8
                    objCustInvTR.TAX8_Rate = objTr.TAX8_Rate
                    objCustInvTR.TAX8_Amt = objTr.TAX8_Amt
                    objCustInvTR.TAX8_Base_Amt = objTr.TAX8_Base_Amt
                    objCustInvTR.TAX9 = objTr.TAX9
                    objCustInvTR.TAX9_Rate = objTr.TAX9_Rate
                    objCustInvTR.TAX9_Amt = objTr.TAX9_Amt
                    objCustInvTR.TAX9_Base_Amt = objTr.TAX9_Base_Amt
                    objCustInvTR.TAX1 = objTr.TAX1
                    objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
                    objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
                    objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
                    objCustInvTR.Total_Tax = objTr.Item_Tax
                    objCustInvTR.Total_Amount = objTr.Item_Net_Amt
                    objCustInvTR.Remarks = objTr.remarks
                    objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
                    objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
                    objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
                    objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
                    objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
                    objCustInvTR.TAX6_Base_Amt = objTr.TAX6_Base_Amt
                    objCustInvTR.TAX7_Base_Amt = objTr.TAX7_Base_Amt
                    objCustInvTR.TAX8_Base_Amt = objTr.TAX8_Base_Amt
                    objCustInvTR.TAX9_Base_Amt = objTr.TAX9_Base_Amt
                    objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
                    objCustInvTR.Amount = objTr.Amount + objTr.Item_Tax
                End If
                ''=========================================================

                objCustInvTR.Comments = "CSA SALE"
                objCustInv.Arr.Add(objCustInvTR)
                counter += 1


                '=================stock transfer rate-----------
                stck_trans_value = stck_trans_value + objTr.stck_trans_value

                GainLoss = GainLoss + objTr.GainLoss

            Next

            '=================if stopgl entry setting is ON then cal. item cost------------------

            Dim ttotalitemwise_cost As Decimal = 0
            If StopGLForConsignment = "1" Then
                If ItemWiseCSAAccount Then
                    qry = "select (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=1 then Inv_Movement.Avg_Cost when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then Inv_Movement.FIFO_Cost" & _
                      " when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then Inv_Movement.LIFO_Cost end) as Item_Cost " & _
                      " ,TSPL_ITEM_MASTER.Item_Code,ItemCustAcc.Consignment_Acct,Consignment_Gl.Account_Group_Desc " & _
                      " from (select item_code,sum(FIFO_Cost) as FIFO_Cost,sum(Avg_Cost) as Avg_Cost ,sum(LIFO_Cost) as LIFO_Cost " & _
                  " from TSPL_INVENTORY_MOVEMENT where Source_Doc_No in (select Against_Transfer_Code from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + obj.docno + "') and item_code in (select item_code from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + obj.docno + "') and InOut='O' group by item_code)Inv_Movement " & _
                  " left join TSPL_ITEM_MASTER on Inv_Movement.Item_Code=TSPL_ITEM_MASTER.Item_Code left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " & _
                   " left join TSPL_CUSTOMER_ACCOUNT_SET as ItemCustAcc on tspl_item_master.Cust_Account=ItemCustAcc.Cust_Account left join TSPL_GL_ACCOUNTS as Consignment_Gl on ItemCustAcc.Consignment_Acct=Consignment_Gl.Account_Code "
                    dt = New DataTable()
                    dt = clsDBFuncationality.GetDataTable(qry, trans)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Item_Cost = clsCommon.myCdbl(dr("item_cost"))

                            ttotalitemwise_cost += Item_Cost

                            objCustInvTR = New clsCustomerInvoiceDetail()
                            objCustInvTR.SNo = counter + 1
                            '==============item cost-->credit in consingnment and debit in cogs=================================

                            objCustInvTR.GL_Account_Code = clsCommon.myCstr(dr("Consignment_Acct"))
                            If clsCommon.myLen(objCustInvTR.GL_Account_Code) <= 0 Then
                                Throw New Exception("Please set CSA Account of item " + clsCommon.myCstr(dr("item_code")) + "")
                            End If
                            objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.plant_loc_code, trans))
                            objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(dr("Account_Group_Desc"), trans))


                            objCustInvTR.Amount = Item_Cost
                            objCustInvTR.Discount_Per = 0 ' objTr.Disc_Per
                            objCustInvTR.Discount = 0 ' objTr.Disc_Amt
                            objCustInvTR.Amount_less_Discount = Item_Cost  'objTr.Amt_Less_Discount


                            objCustInvTR.TAX1 = "" ' objTr.TAX1
                            objCustInvTR.TAX1_Rate = 0 ' objTr.TAX1_Rate
                            objCustInvTR.TAX1_Amt = 0 ' objTr.TAX1_Amt
                            objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                            objCustInvTR.TAX2 = "" ' objTr.TAX2
                            objCustInvTR.TAX2_Rate = 0 'objTr.TAX2_Rate
                            objCustInvTR.TAX2_Amt = 0 ' objTr.TAX2_Amt
                            objCustInvTR.TAX2_Base_Amt = 0 ' objTr.TAX2_Base_Amt
                            objCustInvTR.TAX3 = "" ' objTr.TAX3
                            objCustInvTR.TAX3_Rate = 0 ' objTr.TAX3_Rate
                            objCustInvTR.TAX3_Amt = 0 ' objTr.TAX3_Amt
                            objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                            objCustInvTR.TAX4 = "" ' objTr.TAX4
                            objCustInvTR.TAX4_Rate = 0 ' objTr.TAX4_Rate
                            objCustInvTR.TAX4_Amt = 0 'objTr.TAX4_Amt
                            objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                            objCustInvTR.TAX5 = "" 'objTr.TAX5
                            objCustInvTR.TAX5_Rate = 0 'objTr.TAX5_Rate
                            objCustInvTR.TAX5_Amt = 0 ' objTr.TAX5_Amt
                            objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                            objCustInvTR.TAX6 = "" ' objTr.TAX6
                            objCustInvTR.TAX6_Rate = 0 ' objTr.TAX6_Rate
                            objCustInvTR.TAX6_Amt = 0 'objTr.TAX6_Amt
                            objCustInvTR.TAX6_Base_Amt = 0 'objTr.TAX6_Base_Amt
                            objCustInvTR.TAX7 = "" 'objTr.TAX7
                            objCustInvTR.TAX7_Rate = 0 'objTr.TAX7_Rate
                            objCustInvTR.TAX7_Amt = 0 'objTr.TAX7_Amt
                            objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                            objCustInvTR.TAX8 = "" ' objTr.TAX8
                            objCustInvTR.TAX8_Rate = 0 ' objTr.TAX8_Rate
                            objCustInvTR.TAX8_Amt = 0 'objTr.TAX8_Amt
                            objCustInvTR.TAX8_Base_Amt = 0 'objTr.TAX8_Base_Amt
                            objCustInvTR.TAX9 = "" ' objTr.TAX9
                            objCustInvTR.TAX9_Rate = 0 ' objTr.TAX9_Rate
                            objCustInvTR.TAX9_Amt = 0 'objTr.TAX9_Amt
                            objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                            objCustInvTR.TAX10 = "" ' objTr.TAX10
                            objCustInvTR.TAX10_Rate = 0 ' objTr.TAX10_Rate
                            objCustInvTR.TAX10_Amt = 0 ' objTr.TAX10_Amt
                            objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                            objCustInvTR.Total_Tax = 0 ' objTr.Item_Tax
                            objCustInvTR.Total_Amount = Item_Cost
                            objCustInvTR.Remarks = ""
                            objCustInvTR.TAX1_Base_Amt = 0 'objTr.TAX1_Base_Amt
                            objCustInvTR.TAX2_Base_Amt = 0 'objTr.TAX2_Base_Amt
                            objCustInvTR.TAX3_Base_Amt = 0 'objTr.TAX3_Base_Amt
                            objCustInvTR.TAX4_Base_Amt = 0 'objTr.TAX4_Base_Amt
                            objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                            objCustInvTR.TAX6_Base_Amt = 0 ' objTr.TAX6_Base_Amt
                            objCustInvTR.TAX7_Base_Amt = 0 'objTr.TAX7_Base_Amt
                            objCustInvTR.TAX8_Base_Amt = 0 ' objTr.TAX8_Base_Amt
                            objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                            objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt

                            objCustInvTR.Comments = "CSA SALE"
                            If objCustInvTR.Amount <> 0 Then
                                objCustInv.Arr.Add(objCustInvTR)
                            End If
                            counter += 1
                        Next
                    Else
                        Throw New Exception("Please set Consignment account for csa")
                    End If ''end dt


                Else
                    qry = "select (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=1 then Inv_Movement.Avg_Cost when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then Inv_Movement.FIFO_Cost" & _
                      " when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then Inv_Movement.LIFO_Cost end) as Item_Cost from (select item_code,sum(FIFO_Cost) as FIFO_Cost,sum(Avg_Cost) as Avg_Cost ,sum(LIFO_Cost) as LIFO_Cost " & _
                  " from TSPL_INVENTORY_MOVEMENT where Source_Doc_No in (select Against_Transfer_Code from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + obj.docno + "') and item_code in (select item_code from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + obj.docno + "') and InOut='O' group by item_code)Inv_Movement " & _
                  " left join TSPL_ITEM_MASTER on Inv_Movement.Item_Code=TSPL_ITEM_MASTER.Item_Code left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code "
                    Item_Cost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

                    objCustInvTR = New clsCustomerInvoiceDetail()
                    objCustInvTR.SNo = counter + 1
                    '==============item cost-->credit in consingnment and debit in cogs=================================

                    qry = "select Consignment_Acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account in (select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.cust_code + "')"
                    dt = New DataTable()
                    dt = clsDBFuncationality.GetDataTable(qry, trans)

                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set Consignment account for csa")
                    End If

                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Consignment_Acct"))
                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.plant_loc_code, trans))
                    objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans))


                    objCustInvTR.Amount = Item_Cost
                    objCustInvTR.Discount_Per = 0 ' objTr.Disc_Per
                    objCustInvTR.Discount = 0 ' objTr.Disc_Amt
                    objCustInvTR.Amount_less_Discount = Item_Cost  'objTr.Amt_Less_Discount
                    objCustInvTR.TAX1 = "" ' objTr.TAX1
                    objCustInvTR.TAX1_Rate = 0 ' objTr.TAX1_Rate
                    objCustInvTR.TAX1_Amt = 0 ' objTr.TAX1_Amt
                    objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                    objCustInvTR.TAX2 = "" ' objTr.TAX2
                    objCustInvTR.TAX2_Rate = 0 'objTr.TAX2_Rate
                    objCustInvTR.TAX2_Amt = 0 ' objTr.TAX2_Amt
                    objCustInvTR.TAX2_Base_Amt = 0 ' objTr.TAX2_Base_Amt
                    objCustInvTR.TAX3 = "" ' objTr.TAX3
                    objCustInvTR.TAX3_Rate = 0 ' objTr.TAX3_Rate
                    objCustInvTR.TAX3_Amt = 0 ' objTr.TAX3_Amt
                    objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                    objCustInvTR.TAX4 = "" ' objTr.TAX4
                    objCustInvTR.TAX4_Rate = 0 ' objTr.TAX4_Rate
                    objCustInvTR.TAX4_Amt = 0 'objTr.TAX4_Amt
                    objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                    objCustInvTR.TAX5 = "" 'objTr.TAX5
                    objCustInvTR.TAX5_Rate = 0 'objTr.TAX5_Rate
                    objCustInvTR.TAX5_Amt = 0 ' objTr.TAX5_Amt
                    objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                    objCustInvTR.TAX6 = "" ' objTr.TAX6
                    objCustInvTR.TAX6_Rate = 0 ' objTr.TAX6_Rate
                    objCustInvTR.TAX6_Amt = 0 'objTr.TAX6_Amt
                    objCustInvTR.TAX6_Base_Amt = 0 'objTr.TAX6_Base_Amt
                    objCustInvTR.TAX7 = "" 'objTr.TAX7
                    objCustInvTR.TAX7_Rate = 0 'objTr.TAX7_Rate
                    objCustInvTR.TAX7_Amt = 0 'objTr.TAX7_Amt
                    objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                    objCustInvTR.TAX8 = "" ' objTr.TAX8
                    objCustInvTR.TAX8_Rate = 0 ' objTr.TAX8_Rate
                    objCustInvTR.TAX8_Amt = 0 'objTr.TAX8_Amt
                    objCustInvTR.TAX8_Base_Amt = 0 'objTr.TAX8_Base_Amt
                    objCustInvTR.TAX9 = "" ' objTr.TAX9
                    objCustInvTR.TAX9_Rate = 0 ' objTr.TAX9_Rate
                    objCustInvTR.TAX9_Amt = 0 'objTr.TAX9_Amt
                    objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                    objCustInvTR.TAX10 = "" ' objTr.TAX10
                    objCustInvTR.TAX10_Rate = 0 ' objTr.TAX10_Rate
                    objCustInvTR.TAX10_Amt = 0 ' objTr.TAX10_Amt
                    objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                    objCustInvTR.Total_Tax = 0 ' objTr.Item_Tax
                    objCustInvTR.Total_Amount = Item_Cost
                    objCustInvTR.Remarks = ""
                    objCustInvTR.TAX1_Base_Amt = 0 'objTr.TAX1_Base_Amt
                    objCustInvTR.TAX2_Base_Amt = 0 'objTr.TAX2_Base_Amt
                    objCustInvTR.TAX3_Base_Amt = 0 'objTr.TAX3_Base_Amt
                    objCustInvTR.TAX4_Base_Amt = 0 'objTr.TAX4_Base_Amt
                    objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                    objCustInvTR.TAX6_Base_Amt = 0 ' objTr.TAX6_Base_Amt
                    objCustInvTR.TAX7_Base_Amt = 0 'objTr.TAX7_Base_Amt
                    objCustInvTR.TAX8_Base_Amt = 0 ' objTr.TAX8_Base_Amt
                    objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                    objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                    objCustInvTR.Comments = "CSA SALE"
                    If objCustInvTR.Amount <> 0 Then
                        objCustInv.Arr.Add(objCustInvTR)
                    End If
                    counter += 1
                End If

                ''===============Cogs entry
                objCustInvTR = New clsCustomerInvoiceDetail()
                objCustInvTR.SNo = counter + 1


                qry = "select Cost_Of_Goods_Sold from TSPL_SALES_ACCOUNTS where sales_class_code in (select top 1 Sale_Class_Code from tspl_item_master where item_code in (select item_code from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + obj.docno + "'))"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set COGS account for csa")
                End If

                objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Cost_Of_Goods_Sold"))
                objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.plant_loc_code, trans))
                objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans))


                objCustInvTR.Amount = IIf(ItemWiseCSAAccount = True, 0 - ttotalitemwise_cost, 0 - Item_Cost)
                objCustInvTR.Discount_Per = 0 ' objTr.Disc_Per
                objCustInvTR.Discount = 0 ' objTr.Disc_Amt
                objCustInvTR.Amount_less_Discount = IIf(ItemWiseCSAAccount = True, 0 - ttotalitemwise_cost, 0 - Item_Cost)  'objTr.Amt_Less_Discount
                objCustInvTR.TAX1 = "" ' objTr.TAX1
                objCustInvTR.TAX1_Rate = 0 ' o
                objCustInvTR.TAX1_Rate = 0
                objCustInvTR.TAX1_Amt = 0 ' objTr.TAX1_Amt
                objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                objCustInvTR.TAX2 = "" ' objTr.TAX2
                objCustInvTR.TAX2_Rate = 0 'objTr.TAX2_Rate
                objCustInvTR.TAX2_Amt = 0 ' objTr.TAX2_Amt
                objCustInvTR.TAX2_Base_Amt = 0 ' objTr.TAX2_Base_Amt
                objCustInvTR.TAX3 = "" ' objTr.TAX3
                objCustInvTR.TAX3_Rate = 0 ' objTr.TAX3_Rate
                objCustInvTR.TAX3_Amt = 0 ' objTr.TAX3_Amt
                objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                objCustInvTR.TAX4 = "" ' objTr.TAX4
                objCustInvTR.TAX4_Rate = 0 ' objTr.TAX4_Rate
                objCustInvTR.TAX4_Amt = 0 'objTr.TAX4_Amt
                objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                objCustInvTR.TAX5 = "" 'objTr.TAX5
                objCustInvTR.TAX5_Rate = 0 'objTr.TAX5_Rate
                objCustInvTR.TAX5_Amt = 0 ' objTr.TAX5_Amt
                objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                objCustInvTR.TAX6 = "" ' objTr.TAX6
                objCustInvTR.TAX6_Rate = 0 ' objTr.TAX6_Rate
                objCustInvTR.TAX6_Amt = 0 'objTr.TAX6_Amt
                objCustInvTR.TAX6_Base_Amt = 0 'objTr.TAX6_Base_Amt
                objCustInvTR.TAX7 = "" 'objTr.TAX7
                objCustInvTR.TAX7_Rate = 0 'objTr.TAX7_Rate
                objCustInvTR.TAX7_Amt = 0 'objTr.TAX7_Amt
                objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                objCustInvTR.TAX8 = "" ' objTr.TAX8
                objCustInvTR.TAX8_Rate = 0 ' objTr.TAX8_Rate
                objCustInvTR.TAX8_Amt = 0 'objTr.TAX8_Amt
                objCustInvTR.TAX8_Base_Amt = 0 'objTr.TAX8_Base_Amt
                objCustInvTR.TAX9 = "" ' objTr.TAX9
                objCustInvTR.TAX9_Rate = 0 ' objTr.TAX9_Rate
                objCustInvTR.TAX9_Amt = 0 'objTr.TAX9_Amt
                objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                objCustInvTR.TAX10 = "" ' objTr.TAX10
                objCustInvTR.TAX10_Rate = 0 ' objTr.TAX10_Rate
                objCustInvTR.TAX10_Amt = 0 ' objTr.TAX10_Amt
                objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                objCustInvTR.Total_Tax = 0 ' objTr.Item_Tax
                objCustInvTR.Total_Amount = IIf(ItemWiseCSAAccount = True, 0 - ttotalitemwise_cost, 0 - Item_Cost)
                objCustInvTR.Remarks = ""
                objCustInvTR.TAX1_Base_Amt = 0 'objTr.TAX1_Base_Amt
                objCustInvTR.TAX2_Base_Amt = 0 'objTr.TAX2_Base_Amt
                objCustInvTR.TAX3_Base_Amt = 0 'objTr.TAX3_Base_Amt
                objCustInvTR.TAX4_Base_Amt = 0 'objTr.TAX4_Base_Amt
                objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                objCustInvTR.TAX6_Base_Amt = 0 ' objTr.TAX6_Base_Amt
                objCustInvTR.TAX7_Base_Amt = 0 'objTr.TAX7_Base_Amt
                objCustInvTR.TAX8_Base_Amt = 0 ' objTr.TAX8_Base_Amt
                objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                objCustInvTR.Comments = "CSA SALE"
                If objCustInvTR.Amount <> 0 Then
                    objCustInv.Arr.Add(objCustInvTR)
                End If
                counter += 1
            End If
            '=====================end here==================================================================


            If ItemWiseCSAAccount Then
                qry = "select final.Consignment_Acct,final.Gain_Acct,final.Loss_Acct,sum(isnull(final.CSA_Stock_Transfer_Amt,0)) as CSA_Stock_Transfer_Amt,sum(isnull(final.CSA_Gain_Loss,0)) as CSA_Gain_Loss from (select TSPL_CUSTOMER_ACCOUNT_SET.Consignment_Acct,TSPL_CUSTOMER_ACCOUNT_SET.Gain_Acct,TSPL_CUSTOMER_ACCOUNT_SET.Loss_Acct,TSPL_SD_SALE_INVOICE_DETAIL.CSA_Stock_Transfer_Amt,TSPL_SD_SALE_INVOICE_DETAIL.CSA_Gain_Loss,TSPL_SD_SALE_INVOICE_DETAIL.item_code from TSPL_SD_SALE_INVOICE_DETAIL " & _
                    " left outer join tspl_sd_sale_invoice_head on tspl_sd_sale_invoice_head.document_code=tspl_sd_sale_invoice_detail.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code "
                If StopGLForConsignment <> "1" Then ''then stock transfer ac seen 
                    qry += " left outer join TSPL_CUSTOMER_ACCOUNT_SET on tspl_item_master.Cust_Account=TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account "
                ElseIf StopGLForConsignment = "1" Then
                    qry += " left outer join (select gsoc_acct as Consignment_Acct,Cust_Account,Gain_Acct,Loss_Acct from TSPL_CUSTOMER_ACCOUNT_SET)TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_ACCOUNT_SET.cust_account=tspl_item_master.Cust_Account "
                End If
                qry += " where tspl_sd_sale_invoice_head.trans_type='CSA' and TSPL_SD_SALE_INVOICE_DETAIL.document_code='" + obj.docno + "')final group by final.Consignment_Acct,final.Gain_Acct,final.Loss_Acct"

                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        objCustInvTR = New clsCustomerInvoiceDetail()
                        objCustInvTR.SNo = counter + 1

                        objCustInvTR.GL_Account_Code = clsCommon.myCstr(dr("Consignment_Acct"))

                        If clsCommon.myLen(objCustInvTR.GL_Account_Code) <= 0 Then
                            Throw New Exception("No consignment account found for CSA itemwise.")
                        End If

                        objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.plant_loc_code, trans))
                        objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans))
                        objCustInvTR.Amount = clsCommon.myCdbl(dr("CSA_Stock_Transfer_Amt"))
                        objCustInvTR.Discount_Per = 0 ' objTr.Disc_Per
                        objCustInvTR.Discount = 0 ' objTr.Disc_Amt
                        objCustInvTR.Amount_less_Discount = clsCommon.myCdbl(dr("CSA_Stock_Transfer_Amt"))  'objTr.Amt_Less_Discount
                        objCustInvTR.TAX1 = "" ' objTr.TAX1
                        objCustInvTR.TAX1_Rate = 0 ' objTr.TAX1_Rate
                        objCustInvTR.TAX1_Amt = 0 ' objTr.TAX1_Amt
                        objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                        objCustInvTR.TAX2 = "" ' objTr.TAX2
                        objCustInvTR.TAX2_Rate = 0 'objTr.TAX2_Rate
                        objCustInvTR.TAX2_Amt = 0 ' objTr.TAX2_Amt
                        objCustInvTR.TAX2_Base_Amt = 0 ' objTr.TAX2_Base_Amt
                        objCustInvTR.TAX3 = "" ' objTr.TAX3
                        objCustInvTR.TAX3_Rate = 0 ' objTr.TAX3_Rate
                        objCustInvTR.TAX3_Amt = 0 ' objTr.TAX3_Amt
                        objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                        objCustInvTR.TAX4 = "" ' objTr.TAX4
                        objCustInvTR.TAX4_Rate = 0 ' objTr.TAX4_Rate
                        objCustInvTR.TAX4_Amt = 0 'objTr.TAX4_Amt
                        objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                        objCustInvTR.TAX5 = "" 'objTr.TAX5
                        objCustInvTR.TAX5_Rate = 0 'objTr.TAX5_Rate
                        objCustInvTR.TAX5_Amt = 0 ' objTr.TAX5_Amt
                        objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                        objCustInvTR.TAX6 = "" ' objTr.TAX6
                        objCustInvTR.TAX6_Rate = 0 ' objTr.TAX6_Rate
                        objCustInvTR.TAX6_Amt = 0 'objTr.TAX6_Amt
                        objCustInvTR.TAX6_Base_Amt = 0 'objTr.TAX6_Base_Amt
                        objCustInvTR.TAX7 = "" 'objTr.TAX7
                        objCustInvTR.TAX7_Rate = 0 'objTr.TAX7_Rate
                        objCustInvTR.TAX7_Amt = 0 'objTr.TAX7_Amt
                        objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                        objCustInvTR.TAX8 = "" ' objTr.TAX8
                        objCustInvTR.TAX8_Rate = 0 ' objTr.TAX8_Rate
                        objCustInvTR.TAX8_Amt = 0 'objTr.TAX8_Amt
                        objCustInvTR.TAX8_Base_Amt = 0 'objTr.TAX8_Base_Amt
                        objCustInvTR.TAX9 = "" ' objTr.TAX9
                        objCustInvTR.TAX9_Rate = 0 ' objTr.TAX9_Rate
                        objCustInvTR.TAX9_Amt = 0 'objTr.TAX9_Amt
                        objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                        objCustInvTR.TAX10 = "" ' objTr.TAX10
                        objCustInvTR.TAX10_Rate = 0 ' objTr.TAX10_Rate
                        objCustInvTR.TAX10_Amt = 0 ' objTr.TAX10_Amt
                        objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                        objCustInvTR.Total_Tax = 0 ' objTr.Item_Tax
                        objCustInvTR.Total_Amount = clsCommon.myCdbl(dr("CSA_Stock_Transfer_Amt"))
                        objCustInvTR.Remarks = ""
                        objCustInvTR.TAX1_Base_Amt = 0 'objTr.TAX1_Base_Amt
                        objCustInvTR.TAX2_Base_Amt = 0 'objTr.TAX2_Base_Amt
                        objCustInvTR.TAX3_Base_Amt = 0 'objTr.TAX3_Base_Amt
                        objCustInvTR.TAX4_Base_Amt = 0 'objTr.TAX4_Base_Amt
                        objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                        objCustInvTR.TAX6_Base_Amt = 0 ' objTr.TAX6_Base_Amt
                        objCustInvTR.TAX7_Base_Amt = 0 'objTr.TAX7_Base_Amt
                        objCustInvTR.TAX8_Base_Amt = 0 ' objTr.TAX8_Base_Amt
                        objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                        objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                        objCustInvTR.Comments = "CSA SALE"
                        If objCustInvTR.Amount <> 0 Then
                            objCustInv.Arr.Add(objCustInvTR)
                        End If
                        counter += 1

                        ''=============gain-loss===================================================

                        '================gain loss gl entry-------------------------
                        objCustInvTR = New clsCustomerInvoiceDetail()
                        objCustInvTR.SNo = counter + 1
                        If clsCommon.myCdbl(dr("CSA_Gain_Loss")) > 0 Then
                            If clsCommon.myCstr(dr("Gain_Acct")) Is Nothing OrElse clsCommon.myLen(dr("Gain_Acct")) <= 0 Then
                                Throw New Exception("Please set Gain account for CSA Itemwise.")
                            End If
                            objCustInvTR.GL_Account_Code = clsCommon.myCstr(dr("Gain_Acct"))
                        ElseIf clsCommon.myCdbl(dr("CSA_Gain_Loss")) < 0 Then
                            If clsCommon.myCstr(dr("Loss_Acct")) Is Nothing OrElse clsCommon.myLen(dr("Loss_Acct")) <= 0 Then
                                Throw New Exception("Please set Loss account for CSA Itemwise.")
                            End If
                            objCustInvTR.GL_Account_Code = clsCommon.myCstr(dr("Loss_Acct"))
                        End If
                        objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.plant_loc_code, trans))
                        objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans))
                        objCustInvTR.Amount = clsCommon.myCdbl(dr("CSA_Gain_Loss"))
                        objCustInvTR.Discount_Per = 0 ' objTr.Disc_Per
                        objCustInvTR.Discount = 0 ' objTr.Disc_Amt
                        objCustInvTR.Amount_less_Discount = clsCommon.myCdbl(dr("CSA_Gain_Loss"))  'objTr.Amt_Less_Discount
                        objCustInvTR.TAX1 = "" ' objTr.TAX1
                        objCustInvTR.TAX1_Rate = 0 ' objTr.TAX1_Rate
                        objCustInvTR.TAX1_Amt = 0 ' objTr.TAX1_Amt
                        objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                        objCustInvTR.TAX2 = "" ' objTr.TAX2
                        objCustInvTR.TAX2_Rate = 0 'objTr.TAX2_Rate
                        objCustInvTR.TAX2_Amt = 0 ' objTr.TAX2_Amt
                        objCustInvTR.TAX2_Base_Amt = 0 ' objTr.TAX2_Base_Amt
                        objCustInvTR.TAX3 = "" ' objTr.TAX3
                        objCustInvTR.TAX3_Rate = 0 ' objTr.TAX3_Rate
                        objCustInvTR.TAX3_Amt = 0 ' objTr.TAX3_Amt
                        objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                        objCustInvTR.TAX4 = "" ' objTr.TAX4
                        objCustInvTR.TAX4_Rate = 0 ' objTr.TAX4_Rate
                        objCustInvTR.TAX4_Amt = 0 'objTr.TAX4_Amt
                        objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                        objCustInvTR.TAX5 = "" 'objTr.TAX5
                        objCustInvTR.TAX5_Rate = 0 'objTr.TAX5_Rate
                        objCustInvTR.TAX5_Amt = 0 ' objTr.TAX5_Amt
                        objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                        objCustInvTR.TAX6 = "" ' objTr.TAX6
                        objCustInvTR.TAX6_Rate = 0 ' objTr.TAX6_Rate
                        objCustInvTR.TAX6_Amt = 0 'objTr.TAX6_Amt
                        objCustInvTR.TAX6_Base_Amt = 0 'objTr.TAX6_Base_Amt
                        objCustInvTR.TAX7 = "" 'objTr.TAX7
                        objCustInvTR.TAX7_Rate = 0 'objTr.TAX7_Rate
                        objCustInvTR.TAX7_Amt = 0 'objTr.TAX7_Amt
                        objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                        objCustInvTR.TAX8 = "" ' objTr.TAX8
                        objCustInvTR.TAX8_Rate = 0 ' objTr.TAX8_Rate
                        objCustInvTR.TAX8_Amt = 0 'objTr.TAX8_Amt
                        objCustInvTR.TAX8_Base_Amt = 0 'objTr.TAX8_Base_Amt
                        objCustInvTR.TAX9 = "" ' objTr.TAX9
                        objCustInvTR.TAX9_Rate = 0 ' objTr.TAX9_Rate
                        objCustInvTR.TAX9_Amt = 0 'objTr.TAX9_Amt
                        objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                        objCustInvTR.TAX10 = "" ' objTr.TAX10
                        objCustInvTR.TAX10_Rate = 0 ' objTr.TAX10_Rate
                        objCustInvTR.TAX10_Amt = 0 ' objTr.TAX10_Amt
                        objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                        objCustInvTR.Total_Tax = 0 ' objTr.Item_Tax
                        objCustInvTR.Total_Amount = clsCommon.myCdbl(dr("CSA_Gain_Loss"))
                        objCustInvTR.Remarks = ""
                        objCustInvTR.TAX1_Base_Amt = 0 'objTr.TAX1_Base_Amt
                        objCustInvTR.TAX2_Base_Amt = 0 'objTr.TAX2_Base_Amt
                        objCustInvTR.TAX3_Base_Amt = 0 'objTr.TAX3_Base_Amt
                        objCustInvTR.TAX4_Base_Amt = 0 'objTr.TAX4_Base_Amt
                        objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                        objCustInvTR.TAX6_Base_Amt = 0 ' objTr.TAX6_Base_Amt
                        objCustInvTR.TAX7_Base_Amt = 0 'objTr.TAX7_Base_Amt
                        objCustInvTR.TAX8_Base_Amt = 0 ' objTr.TAX8_Base_Amt
                        objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                        objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                        objCustInvTR.Comments = "CSA SALE"
                        If objCustInvTR.Amount <> 0 Then
                            objCustInv.Arr.Add(objCustInvTR)
                        End If
                        ''=========================================================================================
                    Next
                Else
                    If StopGLForConsignment <> "1" Then ''then stock transfer ac seen 
                        Throw New Exception("Please set Consignment account for csa")
                    ElseIf StopGLForConsignment = "1" Then
                        Throw New Exception("Please set GSOC account for csa")
                    End If
                End If ''end dt

            Else
                objCustInvTR = New clsCustomerInvoiceDetail()
                objCustInvTR.SNo = counter + 1
                '==============stock transfer amount=================================
                If StopGLForConsignment <> "1" Then ''then stock transfer ac seen 
                    qry = "select Consignment_Acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account in (select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.cust_code + "')"
                ElseIf StopGLForConsignment = "1" Then
                    qry = "select gsoc_acct as Consignment_Acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account in (select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.cust_code + "')"
                End If
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    If StopGLForConsignment <> "1" Then ''then stock transfer ac seen 
                        Throw New Exception("Please set Consignment account for csa")
                    ElseIf StopGLForConsignment = "1" Then
                        Throw New Exception("Please set GSOC account for csa")
                    End If

                End If
                objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Consignment_Acct"))
                objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.plant_loc_code, trans))
                objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans))


                objCustInvTR.Amount = stck_trans_value
                objCustInvTR.Discount_Per = 0 ' objTr.Disc_Per
                objCustInvTR.Discount = 0 ' objTr.Disc_Amt
                objCustInvTR.Amount_less_Discount = stck_trans_value  'objTr.Amt_Less_Discount
                objCustInvTR.TAX1 = "" ' objTr.TAX1
                objCustInvTR.TAX1_Rate = 0 ' objTr.TAX1_Rate
                objCustInvTR.TAX1_Amt = 0 ' objTr.TAX1_Amt
                objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                objCustInvTR.TAX2 = "" ' objTr.TAX2
                objCustInvTR.TAX2_Rate = 0 'objTr.TAX2_Rate
                objCustInvTR.TAX2_Amt = 0 ' objTr.TAX2_Amt
                objCustInvTR.TAX2_Base_Amt = 0 ' objTr.TAX2_Base_Amt
                objCustInvTR.TAX3 = "" ' objTr.TAX3
                objCustInvTR.TAX3_Rate = 0 ' objTr.TAX3_Rate
                objCustInvTR.TAX3_Amt = 0 ' objTr.TAX3_Amt
                objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                objCustInvTR.TAX4 = "" ' objTr.TAX4
                objCustInvTR.TAX4_Rate = 0 ' objTr.TAX4_Rate
                objCustInvTR.TAX4_Amt = 0 'objTr.TAX4_Amt
                objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                objCustInvTR.TAX5 = "" 'objTr.TAX5
                objCustInvTR.TAX5_Rate = 0 'objTr.TAX5_Rate
                objCustInvTR.TAX5_Amt = 0 ' objTr.TAX5_Amt
                objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                objCustInvTR.TAX6 = "" ' objTr.TAX6
                objCustInvTR.TAX6_Rate = 0 ' objTr.TAX6_Rate
                objCustInvTR.TAX6_Amt = 0 'objTr.TAX6_Amt
                objCustInvTR.TAX6_Base_Amt = 0 'objTr.TAX6_Base_Amt
                objCustInvTR.TAX7 = "" 'objTr.TAX7
                objCustInvTR.TAX7_Rate = 0 'objTr.TAX7_Rate
                objCustInvTR.TAX7_Amt = 0 'objTr.TAX7_Amt
                objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                objCustInvTR.TAX8 = "" ' objTr.TAX8
                objCustInvTR.TAX8_Rate = 0 ' objTr.TAX8_Rate
                objCustInvTR.TAX8_Amt = 0 'objTr.TAX8_Amt
                objCustInvTR.TAX8_Base_Amt = 0 'objTr.TAX8_Base_Amt
                objCustInvTR.TAX9 = "" ' objTr.TAX9
                objCustInvTR.TAX9_Rate = 0 ' objTr.TAX9_Rate
                objCustInvTR.TAX9_Amt = 0 'objTr.TAX9_Amt
                objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                objCustInvTR.TAX10 = "" ' objTr.TAX10
                objCustInvTR.TAX10_Rate = 0 ' objTr.TAX10_Rate
                objCustInvTR.TAX10_Amt = 0 ' objTr.TAX10_Amt
                objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                objCustInvTR.Total_Tax = 0 ' objTr.Item_Tax
                objCustInvTR.Total_Amount = stck_trans_value
                objCustInvTR.Remarks = ""
                objCustInvTR.TAX1_Base_Amt = 0 'objTr.TAX1_Base_Amt
                objCustInvTR.TAX2_Base_Amt = 0 'objTr.TAX2_Base_Amt
                objCustInvTR.TAX3_Base_Amt = 0 'objTr.TAX3_Base_Amt
                objCustInvTR.TAX4_Base_Amt = 0 'objTr.TAX4_Base_Amt
                objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                objCustInvTR.TAX6_Base_Amt = 0 ' objTr.TAX6_Base_Amt
                objCustInvTR.TAX7_Base_Amt = 0 'objTr.TAX7_Base_Amt
                objCustInvTR.TAX8_Base_Amt = 0 ' objTr.TAX8_Base_Amt
                objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                objCustInvTR.Comments = "CSA SALE"
                If objCustInvTR.Amount <> 0 Then
                    objCustInv.Arr.Add(objCustInvTR)
                End If
                counter += 1

                ''=============for UDL only===============================
                If AlwSaleMfgAcctng Then
                    objCustInvTR = New clsCustomerInvoiceDetail()
                    objCustInvTR.SNo = counter + 1
                    '==============stock transfer amount=================================
                    If StopGLForConsignment = "1" Then ''then stock transfer ac seen 
                        qry = "select Consignment_Acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account in (select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.cust_code + "')"
                    ElseIf StopGLForConsignment <> "1" Then
                        qry = "select gsoc_acct as Consignment_Acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account in (select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.cust_code + "')"
                    End If
                    dt = New DataTable()
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        If StopGLForConsignment = "1" Then ''then stock transfer ac seen 
                            Throw New Exception("Please set Consignment account for csa")
                        ElseIf StopGLForConsignment <> "1" Then
                            Throw New Exception("Please set GSOC account for csa")
                        End If

                    End If
                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Consignment_Acct"))
                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.plant_loc_code, trans))
                    objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans))


                    objCustInvTR.Amount = 0 - stck_trans_value
                    objCustInvTR.Discount_Per = 0 ' objTr.Disc_Per
                    objCustInvTR.Discount = 0 ' objTr.Disc_Amt
                    objCustInvTR.Amount_less_Discount = 0 - stck_trans_value  'objTr.Amt_Less_Discount
                    objCustInvTR.TAX1 = "" ' objTr.TAX1
                    objCustInvTR.TAX1_Rate = 0 ' objTr.TAX1_Rate
                    objCustInvTR.TAX1_Amt = 0 ' objTr.TAX1_Amt
                    objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                    objCustInvTR.TAX2 = "" ' objTr.TAX2
                    objCustInvTR.TAX2_Rate = 0 'objTr.TAX2_Rate
                    objCustInvTR.TAX2_Amt = 0 ' objTr.TAX2_Amt
                    objCustInvTR.TAX2_Base_Amt = 0 ' objTr.TAX2_Base_Amt
                    objCustInvTR.TAX3 = "" ' objTr.TAX3
                    objCustInvTR.TAX3_Rate = 0 ' objTr.TAX3_Rate
                    objCustInvTR.TAX3_Amt = 0 ' objTr.TAX3_Amt
                    objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                    objCustInvTR.TAX4 = "" ' objTr.TAX4
                    objCustInvTR.TAX4_Rate = 0 ' objTr.TAX4_Rate
                    objCustInvTR.TAX4_Amt = 0 'objTr.TAX4_Amt
                    objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                    objCustInvTR.TAX5 = "" 'objTr.TAX5
                    objCustInvTR.TAX5_Rate = 0 'objTr.TAX5_Rate
                    objCustInvTR.TAX5_Amt = 0 ' objTr.TAX5_Amt
                    objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                    objCustInvTR.TAX6 = "" ' objTr.TAX6
                    objCustInvTR.TAX6_Rate = 0 ' objTr.TAX6_Rate
                    objCustInvTR.TAX6_Amt = 0 'objTr.TAX6_Amt
                    objCustInvTR.TAX6_Base_Amt = 0 'objTr.TAX6_Base_Amt
                    objCustInvTR.TAX7 = "" 'objTr.TAX7
                    objCustInvTR.TAX7_Rate = 0 'objTr.TAX7_Rate
                    objCustInvTR.TAX7_Amt = 0 'objTr.TAX7_Amt
                    objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                    objCustInvTR.TAX8 = "" ' objTr.TAX8
                    objCustInvTR.TAX8_Rate = 0 ' objTr.TAX8_Rate
                    objCustInvTR.TAX8_Amt = 0 'objTr.TAX8_Amt
                    objCustInvTR.TAX8_Base_Amt = 0 'objTr.TAX8_Base_Amt
                    objCustInvTR.TAX9 = "" ' objTr.TAX9
                    objCustInvTR.TAX9_Rate = 0 ' objTr.TAX9_Rate
                    objCustInvTR.TAX9_Amt = 0 'objTr.TAX9_Amt
                    objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                    objCustInvTR.TAX10 = "" ' objTr.TAX10
                    objCustInvTR.TAX10_Rate = 0 ' objTr.TAX10_Rate
                    objCustInvTR.TAX10_Amt = 0 ' objTr.TAX10_Amt
                    objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                    objCustInvTR.Total_Tax = 0 ' objTr.Item_Tax
                    objCustInvTR.Total_Amount = 0 - stck_trans_value
                    objCustInvTR.Remarks = ""
                    objCustInvTR.TAX1_Base_Amt = 0 'objTr.TAX1_Base_Amt
                    objCustInvTR.TAX2_Base_Amt = 0 'objTr.TAX2_Base_Amt
                    objCustInvTR.TAX3_Base_Amt = 0 'objTr.TAX3_Base_Amt
                    objCustInvTR.TAX4_Base_Amt = 0 'objTr.TAX4_Base_Amt
                    objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                    objCustInvTR.TAX6_Base_Amt = 0 ' objTr.TAX6_Base_Amt
                    objCustInvTR.TAX7_Base_Amt = 0 'objTr.TAX7_Base_Amt
                    objCustInvTR.TAX8_Base_Amt = 0 ' objTr.TAX8_Base_Amt
                    objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                    objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                    objCustInvTR.Comments = "CSA SALE"
                    If objCustInvTR.Amount <> 0 Then
                        objCustInv.Arr.Add(objCustInvTR)
                    End If
                    counter += 1
                End If
                ''============end here


                '================gain loss gl entry-------------------------
                objCustInvTR = New clsCustomerInvoiceDetail()
                objCustInvTR.SNo = counter + 1
                qry = "select Gain_Acct,Loss_Acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account in (select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.cust_code + "')"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Gain/Loss account for csa")
                End If

                If GainLoss > 0 Then
                    If dt.Rows(0)("Gain_Acct") Is Nothing OrElse clsCommon.myLen(dt.Rows(0)("Gain_Acct")) <= 0 Then
                        Throw New Exception("Please set Gain account for csa")
                    End If
                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Gain_Acct"))
                ElseIf GainLoss < 0 Then
                    If dt.Rows(0)("Loss_Acct") Is Nothing OrElse clsCommon.myLen(dt.Rows(0)("Loss_Acct")) <= 0 Then
                        Throw New Exception("Please set Loss account for csa")
                    End If
                    objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Loss_Acct"))
                End If
                objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.plant_loc_code, trans))
                objCustInvTR.GL_Account_Desc = clsCommon.myCstr(clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans))


                objCustInvTR.Amount = GainLoss
                objCustInvTR.Discount_Per = 0 ' objTr.Disc_Per
                objCustInvTR.Discount = 0 ' objTr.Disc_Amt
                objCustInvTR.Amount_less_Discount = GainLoss  'objTr.Amt_Less_Discount
                objCustInvTR.TAX1 = "" ' objTr.TAX1
                objCustInvTR.TAX1_Rate = 0 ' objTr.TAX1_Rate
                objCustInvTR.TAX1_Amt = 0 ' objTr.TAX1_Amt
                objCustInvTR.TAX1_Base_Amt = 0 ' objTr.TAX1_Base_Amt
                objCustInvTR.TAX2 = "" ' objTr.TAX2
                objCustInvTR.TAX2_Rate = 0 'objTr.TAX2_Rate
                objCustInvTR.TAX2_Amt = 0 ' objTr.TAX2_Amt
                objCustInvTR.TAX2_Base_Amt = 0 ' objTr.TAX2_Base_Amt
                objCustInvTR.TAX3 = "" ' objTr.TAX3
                objCustInvTR.TAX3_Rate = 0 ' objTr.TAX3_Rate
                objCustInvTR.TAX3_Amt = 0 ' objTr.TAX3_Amt
                objCustInvTR.TAX3_Base_Amt = 0 ' objTr.TAX3_Base_Amt
                objCustInvTR.TAX4 = "" ' objTr.TAX4
                objCustInvTR.TAX4_Rate = 0 ' objTr.TAX4_Rate
                objCustInvTR.TAX4_Amt = 0 'objTr.TAX4_Amt
                objCustInvTR.TAX4_Base_Amt = 0 ' objTr.TAX4_Base_Amt
                objCustInvTR.TAX5 = "" 'objTr.TAX5
                objCustInvTR.TAX5_Rate = 0 'objTr.TAX5_Rate
                objCustInvTR.TAX5_Amt = 0 ' objTr.TAX5_Amt
                objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                objCustInvTR.TAX6 = "" ' objTr.TAX6
                objCustInvTR.TAX6_Rate = 0 ' objTr.TAX6_Rate
                objCustInvTR.TAX6_Amt = 0 'objTr.TAX6_Amt
                objCustInvTR.TAX6_Base_Amt = 0 'objTr.TAX6_Base_Amt
                objCustInvTR.TAX7 = "" 'objTr.TAX7
                objCustInvTR.TAX7_Rate = 0 'objTr.TAX7_Rate
                objCustInvTR.TAX7_Amt = 0 'objTr.TAX7_Amt
                objCustInvTR.TAX7_Base_Amt = 0 ' objTr.TAX7_Base_Amt
                objCustInvTR.TAX8 = "" ' objTr.TAX8
                objCustInvTR.TAX8_Rate = 0 ' objTr.TAX8_Rate
                objCustInvTR.TAX8_Amt = 0 'objTr.TAX8_Amt
                objCustInvTR.TAX8_Base_Amt = 0 'objTr.TAX8_Base_Amt
                objCustInvTR.TAX9 = "" ' objTr.TAX9
                objCustInvTR.TAX9_Rate = 0 ' objTr.TAX9_Rate
                objCustInvTR.TAX9_Amt = 0 'objTr.TAX9_Amt
                objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                objCustInvTR.TAX10 = "" ' objTr.TAX10
                objCustInvTR.TAX10_Rate = 0 ' objTr.TAX10_Rate
                objCustInvTR.TAX10_Amt = 0 ' objTr.TAX10_Amt
                objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                objCustInvTR.Total_Tax = 0 ' objTr.Item_Tax
                objCustInvTR.Total_Amount = GainLoss
                objCustInvTR.Remarks = ""
                objCustInvTR.TAX1_Base_Amt = 0 'objTr.TAX1_Base_Amt
                objCustInvTR.TAX2_Base_Amt = 0 'objTr.TAX2_Base_Amt
                objCustInvTR.TAX3_Base_Amt = 0 'objTr.TAX3_Base_Amt
                objCustInvTR.TAX4_Base_Amt = 0 'objTr.TAX4_Base_Amt
                objCustInvTR.TAX5_Base_Amt = 0 ' objTr.TAX5_Base_Amt
                objCustInvTR.TAX6_Base_Amt = 0 ' objTr.TAX6_Base_Amt
                objCustInvTR.TAX7_Base_Amt = 0 'objTr.TAX7_Base_Amt
                objCustInvTR.TAX8_Base_Amt = 0 ' objTr.TAX8_Base_Amt
                objCustInvTR.TAX9_Base_Amt = 0 ' objTr.TAX9_Base_Amt
                objCustInvTR.TAX10_Base_Amt = 0 'objTr.TAX10_Base_Amt
                objCustInvTR.Comments = "CSA SALE"
                If objCustInvTR.Amount <> 0 Then
                    objCustInv.Arr.Add(objCustInvTR)
                End If
            End If



            ''---------------check if there is already any AR is made,means entry is reposted after unpost
            qry = "select document_no from TSPL_Customer_Invoice_Head where trans_type='CSA' and RefDocType='csa sale' and RefDocNo='" + obj.docno + "' and Against_Sale_No='" + obj.docno + "'"
            Dim OLDARNO As String = ""
            OLDARNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

            qry = "select voucher_no from TSPL_JOURNAL_MASTER where source_code='AR-IN' and source_doc_no='" + OLDARNO + "' " ' and Against_Sale_No='" + obj.docno + "'"
            Dim OLD_VoucherNO As String = ""
            OLD_VoucherNO = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

            If clsCommon.myLen(OLDARNO) > 0 Then
                objCustInv.Document_No = OLDARNO
                objCustInv.SaveData(objCustInv, False, trans, "CSA-SALE", Nothing, OLDARNO) '"CSA-SALE"
            Else
                objCustInv.SaveData(objCustInv, True, trans, "CSA-SALE") '"CSA-SALE"
            End If

            If clsCommon.myLen(OLD_VoucherNO) > 0 Then
                clsCustomerInvoiceHead.PostData("CSA-SALE", objCustInv.Document_No, "", trans, OLD_VoucherNO)
            Else
                clsCustomerInvoiceHead.PostData("CSA-SALE", objCustInv.Document_No, "", trans)
            End If
            ''---------------------------------------------------------------------------------------------


            objCustInvTR = Nothing
            objCustInv = Nothing
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As clsCSASaleInvoice
        Try
            Return GetData(strCode, arrLoc, NavType, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCSASaleInvoice
        Dim obj_GV As New clsCSAStockTransferDetail()
        Try
            Dim obj As New clsCSASaleInvoice()
            obj.Arr_Item = New List(Of clsCSASaleInvoiceItem)
            Dim whrCls As String = ""

            Dim qry As String = "select TSPL_SD_SALE_INVOICE_HEAD.*,tspl_customer_master.customer_name as dist_name from TSPL_SD_SALE_INVOICE_HEAD left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_HEAD.CSA_Distributor_Code where TSPL_SD_SALE_INVOICE_HEAD.trans_type='CSA' "
            If clsCommon.myLen(arrLoc) > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + arrLoc + ")"
                whrCls = " and Bill_To_Location in (" + arrLoc + ") "
            End If


            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.document_code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.document_code in (select min(document_code) from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' " + whrCls + ")"
                Case NavigatorType.Last
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.document_code in (select max(document_code) from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' " + whrCls + ")"
                Case NavigatorType.Next
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.document_code in (select min(document_code) from TSPL_SD_SALE_INVOICE_HEAD where document_code>'" + strCode + "' and trans_type='CSA' " + whrCls + ")"
                Case NavigatorType.Previous
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.document_code in (select max(document_code) from TSPL_SD_SALE_INVOICE_HEAD where document_code<'" + strCode + "' and trans_type='CSA' " + whrCls + ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.docno = clsCommon.myCstr(dt.Rows(0)("document_code"))
                obj.docdate = clsCommon.myCDate(dt.Rows(0)("document_date"))
                obj.Is_Approved = CInt(clsCommon.myCdbl(dt.Rows(0)("Is_Approved")))

                obj.descrptn = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.cust_code = clsCommon.myCstr(dt.Rows(0)("customer_code"))
                'obj.CSA_FOC_STATUS = CInt(dt.Rows(0)("CSA_FOC_STATUS"))
                obj.cust_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + obj.cust_code + "'", trans))
                obj.loc_code = clsCommon.myCstr(dt.Rows(0)("bill_to_location"))
                obj.loc_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + obj.loc_code + "'", trans))

                obj.CSA_Distributor_Code = clsCommon.myCstr(dt.Rows(0)("CSA_Distributor_Code"))
                obj.Distributor_Name = clsCommon.myCstr(dt.Rows(0)("dist_name"))

                obj.plant_loc_code = clsCommon.myCstr(dt.Rows(0)("CSA_PLANT_LOCATION"))
                obj.plant_loc_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + obj.plant_loc_code + "'", trans))
                obj.total_commision = clsCommon.myCdbl(dt.Rows(0)("Total_Commision_Amt"))
                obj.po_no = clsCommon.myCstr(dt.Rows(0)("Cust_PO_No"))
                obj.document_amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
                obj.isPost = CInt(dt.Rows(0)("status"))
                obj.tax_group_code = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
                obj.tax_group_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" + obj.tax_group_code + "' and Tax_Group_Type='S'", trans))

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

                obj.lbltaxamt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
                obj.amt_with_disc = clsCommon.myCdbl(dt.Rows(0)("Discount_Base"))
                obj.lbldisc_amt = clsCommon.myCdbl(dt.Rows(0)("Discount_Amt"))
                obj.amt_after_disc = clsCommon.myCdbl(dt.Rows(0)("Amount_Less_Discount"))
                obj.term_code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                obj.term_desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select terms_desc from TSPL_TERMS_MASTER where Terms_Code='" + obj.term_code + "'", trans))
                obj.due_date = clsCommon.myCDate(dt.Rows(0)("Due_Date"))
                If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If

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

                obj.total_add_chrg = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge"))
                obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
                obj.Total_Freight_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Freight_Amt"))

                obj.Tax_Calculation_Type = clsCommon.myCstr(IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, "0", "1"))
                obj.disc_amt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Amt"))
                obj.disc_pers = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Per"))
                obj.inv_disc_amt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_PerAmt"))

                obj.Excisable = clsCommon.myCstr(dt.Rows(0)("Excisable"))

                obj.currency_code = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
                obj.cnvrsn_rate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
                If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                    obj.applicable_from = Nothing
                Else
                    obj.applicable_from = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
                End If

                qry = "select * from TSPL_SD_SALE_INVOICE_DETAIL where document_code='" + obj.docno + "' order by line_no"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                obj.Arr_Item = New List(Of clsCSASaleInvoiceItem)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsCSASaleInvoiceItem()

                        objtr.Line_No = CInt(dr("Line_No"))
                        objtr.commision = clsCommon.myCdbl(dr("CSA_Commision_Rate"))
                        objtr.Other_Chrage = clsCommon.myCdbl(dr("CSA_Other_Chrage"))
                        objtr.Pack_Size = clsCommon.myCdbl(dr("CSA_Item_Pack_Size"))
                        objtr.Master_Pack_Size = clsCommon.myCdbl(dr("CSA_Item_Master_Pack_Size"))

                        objtr.Freight_Type = clsCommon.myCstr(dr("Freight_Type"))
                        objtr.Freight_Rate = clsCommon.myCdbl(dr("Freight_Rate"))
                        objtr.Freight_Amt = clsCommon.myCdbl(dr("Freight_Amt"))

                        objtr.Grid_Date = clsCommon.myCDate(dr("CSA_Booking_Date"))
                        objtr.Booking_no = clsCommon.myCstr(dr("CSA_Booking_no"))
                        objtr.Booking_type = clsCommon.myCstr(dr("CSA_Booking_Type"))
                        objtr.Conv_Factor = clsCommon.myCdbl(dr("Conv_Factor"))
                        objtr.colcommsn_amt = clsCommon.myCdbl(dr("CSA_Commision_Amount"))
                        objtr.CSA_Commission_RS_PERS = clsCommon.myCstr(dr("CSA_Commission_RS_PERS"))
                        objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objtr.Item_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objtr.Item_Code + "'", trans))
                        objtr.Item_type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objtr.Item_Code + "'", trans))
                        objtr.csa_type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select csa_type from tspl_item_master where item_code='" + objtr.Item_Code + "'", trans))
                        objtr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                        objtr.Qty = clsCommon.myCdbl(dr("Qty"))
                        objtr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                        objtr.alt_uom = clsCommon.myCstr(dr("Alt_UOM"))
                        objtr.alt_qty = clsCommon.myCdbl(dr("Alt_Qty"))
                        objtr.booking_rate = clsCommon.myCdbl(dr("CSA_Booking_Rate"))
                        objtr.Item_Cost = clsCommon.myCdbl(dr("ActualRate"))
                        objtr.including_tax = clsCommon.myCstr(dr("CSA_Including_Tax"))
                        objtr.tax_basis = clsCommon.myCstr(dr("CSA_Tax_Basis"))
                        objtr.Disc_Per = clsCommon.myCdbl(dr("Disc_Per"))

                        objtr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                        objtr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                        objtr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                        objtr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                        objtr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                        objtr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                        objtr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                        objtr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                        objtr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                        objtr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                        objtr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                        objtr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                        objtr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                        objtr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                        objtr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                        objtr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                        objtr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                        objtr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                        objtr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                        objtr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                        objtr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                        objtr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                        objtr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                        objtr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                        objtr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                        objtr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                        objtr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                        objtr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                        objtr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                        objtr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                        objtr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                        objtr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                        objtr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                        objtr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                        objtr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                        objtr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                        objtr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                        objtr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                        objtr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                        objtr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))

                        objtr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                        objtr.sale_rate = clsCommon.myCdbl(dr("Item_Cost"))
                        objtr.Amount = clsCommon.myCdbl(dr("amount"))
                        objtr.stck_trans_rate = clsCommon.myCdbl(dr("CSA_Stock_Transfer_Rate"))
                        objtr.stck_trans_value = clsCommon.myCdbl(dr("CSA_Stock_Transfer_Amt"))
                        objtr.Amt_Less_Discount = clsCommon.myCstr(dr("Amt_Less_Discount"))
                        objtr.remarks = clsCommon.myCstr(dr("remarks"))
                        objtr.Item_Tax = clsCommon.myCdbl(dr("Item_Tax"))
                        objtr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                        objtr.Total_Basic_Amt = clsCommon.myCdbl(dr("Total_Basic_Amt"))
                        objtr.GainLoss = clsCommon.myCdbl(dr("CSA_Gain_Loss"))

                        objtr.Scheme_Applicable = clsCommon.myCstr(dr("Scheme_Applicable"))
                        objtr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                        objtr.Scheme_Item = clsCommon.myCstr(dr("Scheme_Item"))
                        objtr.Scheme_Item_Code = clsCommon.myCstr(dr("Scheme_Item_Code"))
                        objtr.Scheme_Item_Line_No = clsCommon.myCstr(dr("Scheme_Item_Line_No"))
                        objtr.Scheme_Item_UOM = clsCommon.myCstr(dr("Scheme_Item_UOM"))
                        objtr.Scheme_Qty = clsCommon.myCdbl(dr("Scheme_Qty"))
                        objtr.Scheme_Type = clsCommon.myCstr(dr("Scheme_Type"))
                        objtr.FOC_Item = clsCommon.myCstr(dr("FOC_Item"))
                        objtr.Cash_Scheme_Code = clsCommon.myCstr(dr("Cash_Scheme_Code"))
                        objtr.Cash_Scheme_Type = clsCommon.myCstr(dr("Cash_Scheme_Type"))
                        objtr.Cash_Scheme_Pers = clsCommon.myCstr(dr("Cash_Scheme_Pers"))
                        objtr.Cash_Scheme_Amount = clsCommon.myCstr(dr("Cash_Scheme_Amount"))

                        objtr.GV_TAG_ARR = New List(Of clsCSAStockTransferDetail)
                        objtr.arrBatchItem = New List(Of clsBatchInventory)
                        objtr.arrBatchItem = clsBatchInventory.GetData("CSA-Sale", obj.docno, objtr.Item_Code, objtr.Line_No, trans)


                        objtr.MRP = clsCommon.myCdbl(dr("MRP"))
                        objtr.Abatement_Per = clsCommon.myCdbl(dr("Abatement_Per"))
                        objtr.Abatement_Amt = clsCommon.myCdbl(dr("Abatement_Amt"))
                        objtr.SetTransferQtytoZero = clsCommon.myCdbl(dr("SetTransferQtytoZero"))
                        '==========================get data for tagging row-wise in grid=================================
                        qry = "select * from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + obj.docno + "' and item_code='" + objtr.Item_Code + "' and line_no='" + clsCommon.myCstr(objtr.Line_No) + "'"
                        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                        If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                            For Each dr2 As DataRow In dt2.Rows
                                obj_GV = New clsCSAStockTransferDetail()

                                obj_GV.code = clsCommon.myCstr(dr2("document_code"))
                                obj_GV.lineno = clsCommon.myCdbl(dr2("line_no"))
                                obj_GV.transcode = clsCommon.myCstr(dr2("against_transfer_code"))
                                obj_GV.icode = clsCommon.myCstr(dr2("item_code"))
                                obj_GV.qty = clsCommon.myCdbl(dr2("qty"))
                                obj_GV.rate = clsCommon.myCdbl(dr2("transfer_rate"))
                                obj_GV.packsize = clsCommon.myCdbl(dr2("item_pack_size"))
                                obj_GV.act_qty = clsCommon.myCdbl(dr2("transfer_qty"))
                                obj_GV.uom = clsCommon.myCstr(dr2("transfer_uom"))
                                obj_GV.bal_qty = clsCommon.myCdbl(dr2("balance_qty"))
                                obj_GV.sale_uom = clsCommon.myCstr(dr2("sale_uom"))
                                obj_GV.conv_factor = clsCommon.myCdbl(dr2("conv_factor"))
                                obj_GV.alt_qty = clsCommon.myCdbl(dr2("alt_qty"))
                                obj_GV.FOC = clsCommon.myCstr(dr2("FOC"))
                                obj_GV.Transfer_Line_No = clsCommon.myCdbl(dr2("Transfer_Line_No"))

                                objtr.GV_TAG_ARR.Add(obj_GV)
                            Next
                        End If ''dt2 cond.
                        '====================================end here====================================================
                        obj.Arr_Item.Add(objtr)
                    Next
                End If

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + obj.docno + "'", trans)) > 0 Then
                    qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CSA_SALE_TRANSFER'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                    If check <= 0 Then
                        qry = "create table CSA_SALE_TRANSFER (document_code varchar(30),line_no integer,Against_Transfer_Code varchar(30),item_code varchar(50),Item_Pack_Size float,qty float,transfer_rate float,Conv_Factor float null,Transfer_Qty float null,Transfer_UOM varchar(12) null,Balance_Qty float null,Sale_UOM varchar(12) null,Alt_Qty float null,FOC char(1) not null default 'N',Transfer_Line_No integer)"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If

                    qry = "insert into CSA_SALE_TRANSFER (DOCUMENT_CODE,Line_No,Against_Transfer_Code,item_code,Item_Pack_Size,qty,transfer_rate,Conv_Factor,Transfer_Qty,Transfer_UOM,Balance_Qty,Sale_UOM,Alt_Qty,FOC,Transfer_Line_No) select DOCUMENT_CODE,Line_No,Against_Transfer_Code,item_code,Item_Pack_Size,qty,transfer_rate,Conv_Factor,Transfer_Qty,Transfer_UOM,Balance_Qty,Sale_UOM,Alt_Qty,FOC,Transfer_Line_No from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + obj.docno + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If

            'trans.Commit()
            Return obj
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            obj_GV = Nothing
        End Try
    End Function

#Region "Reverse/Unpost" ''BM00000009170
    Public Shared Function UnPostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            UnPostData(FormId, arrLoc, strCode, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function UnPostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsCSASaleInvoice()
        Dim qry As String = ""
        Try
            Dim isSaved As Boolean = True

            obj = clsCSASaleInvoice.GetData(strCode, arrLoc, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.frmCSASaleInvoice, obj.plant_loc_code, obj.docdate, trans)


            If obj Is Nothing OrElse clsCommon.myLen(obj.docno) <= 0 Then
                Throw New Exception("No Data Found.")
            End If

            If obj.isPost <> "1" Then
                Throw New Exception("Record is already Unposted.")
            End If

            ''===========check receipt or payment is done==============
            qry = "select count(receipt_no) from TSPL_RECEIPT_detail where document_no in (select document_no from TSPL_Customer_Invoice_Head where trans_type='CSA' and Against_Sale_No='" + strCode + "')"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                Throw New Exception("Record can not be unposted." + Environment.NewLine + "Receipt entry exist.")
            End If

            qry = "select count(payment_no) from TSPL_PAYMENT_DETAIL where Document_No in (select Document_No from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No ='" + strCode + "')"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                Throw New Exception("Record can not be unposted." + Environment.NewLine + "Payment entry exist.")
            End If
            ''===============end here========================


            ''Delete from stock
            qry = "update tspl_batch_item set against_inv_movement_trans_id=NULL where document_type='csa-sale' and document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement where trans_type='csa-sale' and Source_Doc_No='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''-------end here====



            ''Unpost AR Invoice====================
            qry = "update TSPL_JOURNAL_MASTER set Authorized='N',Modify_By='" + objCommonVar.CurrentUserCode + "',Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' " & _
                " where source_code='AR-IN' and source_doc_no in (select document_no from TSPL_Customer_Invoice_Head where trans_type='CSA' and RefDocType='csa sale' and RefDocNo='" + strCode + "' and Against_Sale_No='" + strCode + "') "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_Customer_Invoice_Head set status=0,Modify_By='" + objCommonVar.CurrentUserCode + "',Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
                    " where trans_type='CSA' and RefDocType='csa sale' and RefDocNo='" + strCode + "' and Against_Sale_No='" + strCode + "' "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.FreightChargeOnCSASaleInvoice, clsFixedParameterCode.FreightChargeOnCSASaleInvoice, trans)) = "1", True, False)) = True Then
                ''then document must be deleted
                'qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + strCode + "') "
                'isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                'qry = "delete from TSPL_JOURNAL_DETAILS where Journal_No in (select Journal_No from TSPL_JOURNAL_MASTER where Source_Code in ('AP-IN','AP-CN','AP-DN') and Source_Doc_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + strCode + "'))"
                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                'qry = "delete from TSPL_JOURNAL_MASTER where Source_Code in ('AP-IN','AP-CN','AP-DN') and Source_Doc_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + strCode + "') "
                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                'qry = "delete from TSPL_REMITTANCE where Document_No in (select document_no from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + strCode + "') "
                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                'qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + strCode + "'"
                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            ''---------------------------------------------------------------------------------------------


            ''Unpost AP Invoice==================
            qry = "update TSPL_JOURNAL_MASTER set Authorized='N',Modify_By='" + objCommonVar.CurrentUserCode + "' ,Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' " & _
                " where source_code='AP-IN' and source_doc_no in (select document_no from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + strCode + "') "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_VENDOR_INVOICE_HEAD set posting_date=NULL,Modify_By='" + objCommonVar.CurrentUserCode + "',Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' " & _
                " where Vendor_Invoice_No='" + strCode + "' "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            ''---------------------------------------------------------------------------------------------



            qry = "Update TSPL_SD_SALE_INVOICE_HEAD set Status=0,Modify_By='" + objCommonVar.CurrentUserCode + "', Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "'"
            qry += " where Document_Code='" + strCode + "' and trans_type='CSA'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL set is_reverse=1 where document_code='" + strCode + "' and trans_code='" + clsCommon.myCstr(clsUserMgtCode.frmCSASaleInvoice) + "' and is_reverse=0"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Function
#End Region
End Class

Public Class clsCSASaleInvoiceItem
#Region "variables"
    Public MRP As Double = Nothing
    Public Is_MRP_Mandatory As Integer = Nothing
    Public Abatement_Per As Double = Nothing
    Public Abatement_Amt As Double = Nothing
    Public SetTransferQtytoZero As Integer = 0
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public CSA_Commission_RS_PERS As String = Nothing
    Public commision As Decimal = Nothing
    Public Other_Chrage As Decimal = Nothing
    Public Pack_Size As Decimal = Nothing
    Public Master_Pack_Size As Decimal = Nothing
    Public GainLoss As Decimal = Nothing
    Public Document_Code As String = Nothing
    Public Grid_Date As Date = Nothing
    Public Booking_no As String = Nothing
    Public Booking_type As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing 'Not a Table Field
    Public Item_type As String = Nothing
    Public csa_type As String = Nothing
    Public Unit_code As String = Nothing '
    Public Qty As Double = 0
    Public alt_uom As String = Nothing
    Public alt_qty As String = Nothing
    Public booking_rate As Decimal = Nothing
    Public Balance_Qty As Double = 0
    Public Location As String = Nothing '
    Public LocationName As String = Nothing 'Not a Table Field
    Public Item_Cost As Double = 0
    Public Total_Basic_Amt As Double = 0
    Public including_tax As String = Nothing
    Public Disc_Per As Double = 0

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
    Public Amount As Double = 0 'sale amount

    Public Disc_Amt As Double = 0
    Public sale_rate As Decimal = Nothing
    'Public sale_value As Decimal = Nothing
    Public stck_trans_rate As Decimal = Nothing
    Public stck_trans_value As Decimal = Nothing
    Public Amt_Less_Discount As Double = 0
    Public remarks As String = Nothing
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public Status As Integer = 0
    Public tax_basis As String = Nothing 'back cal./forward cal.

    Public Assessable As Double = 0
    Public AssessableAmt As Double = 0

    Public Item_Tax As Double = 0


    Public Total_Disc_Amt As Double = 0
    Public Cust_Discount As Double = 0
    Public Total_Cust_Discount As Double = 0
    Public ActualRate As Double = 0
    Public Cust_DiscountQty As Double = 0
    Public Price_Date As String = Nothing
    Public Item_Weight As Double = 0
    Public Conv_Factor As Double = 0
    Public TotalItem_Weight As Double = 0
    Public colcommsn_amt As Decimal = Nothing
    Public colBookQty As Decimal = Nothing

    Public Landing_Cost As Double = 0
    Public HeadDiscAmt As Double = 0
    Public CustDiscPer As Double = 0
    Public CasdDiscScheme_Code As String = Nothing
    Public Purchase_Cost As Double = 0
    Public OrgRate As Double = 0
    Public HeadDiscPer As Double = 0
    Public HeadDiscPerAmt As Double = 0
    Public Scheme_Item As String = "N"
    Public Row_Type As String = "Item"
    Public Scheme_Applicable As String = Nothing
    Public FOC_Item As String = Nothing
    Public Scheme_Code As String = Nothing
    Public Scheme_Type As String = Nothing
    Public Scheme_Item_Code As String = Nothing
    Public Scheme_Item_Line_No As String = Nothing
    Public Scheme_Qty As Decimal = Nothing
    Public Scheme_Item_UOM As String = Nothing
    Public Cash_Scheme_Code As String = Nothing
    Public Cash_Scheme_Type As String = Nothing
    Public Cash_Scheme_Pers As Decimal = Nothing
    Public Cash_Scheme_Amount As Decimal = Nothing

    Public Freight_Type As String = Nothing
    Public Freight_Rate As Decimal = Nothing
    Public Freight_Amt As Decimal = Nothing

    Public GV_TAG_ARR As New List(Of clsCSAStockTransferDetail)()
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal strDocDate As Date, ByVal CSALocCode As String, ByVal arr As List(Of clsCSASaleInvoiceItem), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim Rate_Mismatch_Counter As Integer = 0

            Dim qry As String = "delete from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("delete from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + strCode + "'", trans)

            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsCSASaleInvoiceItem In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "DOCUMENT_CODE", strCode)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "CSA_Commision_Rate", obj.commision)
                    clsCommon.AddColumnsForChange(coll, "CSA_Commision_Amount", obj.colcommsn_amt)
                    clsCommon.AddColumnsForChange(coll, "CSA_Commission_RS_PERS", obj.CSA_Commission_RS_PERS)
                    clsCommon.AddColumnsForChange(coll, "CSA_Other_Chrage", obj.Other_Chrage)
                    clsCommon.AddColumnsForChange(coll, "Conv_Factor", obj.Conv_Factor)
                    clsCommon.AddColumnsForChange(coll, "CSA_Item_Pack_Size", obj.Pack_Size)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                    clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.sale_rate)

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

                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Disc_Per", obj.Disc_Per)
                    clsCommon.AddColumnsForChange(coll, "Disc_Amt", obj.Disc_Amt)
                    clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount", obj.Amt_Less_Discount)
                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.remarks)
                    clsCommon.AddColumnsForChange(coll, "Item_Tax", obj.Item_Tax)
                    clsCommon.AddColumnsForChange(coll, "Total_Basic_Amt", obj.Total_Basic_Amt)
                    clsCommon.AddColumnsForChange(coll, "Total_Disc_Amt", obj.Total_Disc_Amt)
                    clsCommon.AddColumnsForChange(coll, "ActualRate", obj.Item_Cost)
                    clsCommon.AddColumnsForChange(coll, "HeadDiscAmt", obj.HeadDiscAmt)
                    clsCommon.AddColumnsForChange(coll, "HeadDiscPer", obj.HeadDiscPer)
                    clsCommon.AddColumnsForChange(coll, "HeadDiscPerAmt", obj.HeadDiscPerAmt)
                    clsCommon.AddColumnsForChange(coll, "CSA_Booking_no", obj.Booking_no)
                    clsCommon.AddColumnsForChange(coll, "CSA_Booking_Type", obj.Booking_type)
                    clsCommon.AddColumnsForChange(coll, "Alt_UOM", obj.alt_uom)
                    clsCommon.AddColumnsForChange(coll, "Alt_Qty", obj.alt_qty)
                    clsCommon.AddColumnsForChange(coll, "CSA_Booking_Date", clsCommon.GetPrintDate(obj.Grid_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "CSA_Booking_Rate", obj.booking_rate)
                    clsCommon.AddColumnsForChange(coll, "CSA_Including_Tax", obj.including_tax)
                    clsCommon.AddColumnsForChange(coll, "CSA_Stock_Transfer_Rate", obj.stck_trans_rate)
                    clsCommon.AddColumnsForChange(coll, "CSA_Stock_Transfer_Amt", obj.stck_trans_value)
                    clsCommon.AddColumnsForChange(coll, "CSA_Tax_Basis", obj.tax_basis)
                    clsCommon.AddColumnsForChange(coll, "CSA_Gain_Loss", obj.GainLoss)
                    clsCommon.AddColumnsForChange(coll, "CSA_Item_Master_Pack_Size", obj.Master_Pack_Size)

                    clsCommon.AddColumnsForChange(coll, "Scheme_Item_UOM", obj.Scheme_Item_UOM)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Qty", obj.Scheme_Qty)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Item", obj.Scheme_Item)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Item_Code", obj.Scheme_Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Item_Line_No", obj.Scheme_Item_Line_No)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Type", obj.Scheme_Type)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)
                    clsCommon.AddColumnsForChange(coll, "FOC_Item", obj.FOC_Item)
                    clsCommon.AddColumnsForChange(coll, "Scheme_Applicable", obj.Scheme_Applicable)
                    clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Code", obj.Cash_Scheme_Code)
                    clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Type", obj.Cash_Scheme_Type)
                    clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Pers", obj.Cash_Scheme_Pers)
                    clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Amount", obj.Cash_Scheme_Amount)

                    clsCommon.AddColumnsForChange(coll, "Freight_Amt", obj.Freight_Amt)
                    clsCommon.AddColumnsForChange(coll, "Freight_Rate", obj.Freight_Rate)
                    clsCommon.AddColumnsForChange(coll, "Freight_Type", obj.Freight_Type)

                    clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                    clsCommon.AddColumnsForChange(coll, "Abatement_Amt", obj.Abatement_Amt)
                    clsCommon.AddColumnsForChange(coll, "Abatement_Per", obj.Abatement_Per)
                    clsCommon.AddColumnsForChange(coll, "SetTransferQtytoZero", obj.SetTransferQtytoZero)

                    If clsCommon.myCdbl(obj.Item_Cost) <> clsCommon.myCdbl(obj.booking_rate) Then
                        Rate_Mismatch_Counter += 1
                    End If

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    isSaved = isSaved AndAlso SaveTransferDetail(strCode, obj.Line_No, obj.GV_TAG_ARR, trans)

                    clsBatchInventory.SaveData("CSA-SALE", strCode, strDocDate, "O", obj.Item_Code, CSALocCode, obj.Line_No, 0, obj.Unit_code, obj.arrBatchItem, trans)
                Next
            End If

            '>>>>>>>>>>>>>>>>>if unit rate and booking rate is mismatch then this document is go for approval on approval transaction screen
            '>>>>>>>>>>>>>>>>>and from there user have to first approvedthe document only then document is posted.
            If Rate_Mismatch_Counter > 0 AndAlso Not clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.frmCSASaleInvoice, trans) Then
                Rate_Mismatch_Counter = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Program_Code='" & clsUserMgtCode.frmCSASaleInvoice & "' and Document_No='" & strCode & "' ", trans))
                If Rate_Mismatch_Counter = 0 Then
                    qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                 "values ('CSA Sale Invoice/Sale Patti','" & clsUserMgtCode.frmCSASaleInvoice & "','" & strCode & "','" & clsCommon.GetPrintDate(strDocDate, "dd-MMM-yyyy") & "','Rate',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            ElseIf Rate_Mismatch_Counter <= 0 Then
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("update tspl_sd_sale_invoice_head set Is_Approved=1 where document_code='" + strCode + "' and trans_type='CSA'", trans)
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveTransferDetail(ByVal strCode As String, ByVal LineNo As Integer, ByVal Arr As List(Of clsCSAStockTransferDetail), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try

            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each objtr As clsCSAStockTransferDetail In Arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "document_code", strCode)
                    clsCommon.AddColumnsForChange(coll, "line_no", LineNo)
                    clsCommon.AddColumnsForChange(coll, "Against_Transfer_Code", objtr.transcode)
                    clsCommon.AddColumnsForChange(coll, "item_code", objtr.icode)
                    clsCommon.AddColumnsForChange(coll, "qty", objtr.qty)
                    clsCommon.AddColumnsForChange(coll, "transfer_rate", objtr.rate)
                    clsCommon.AddColumnsForChange(coll, "Item_Pack_Size", objtr.packsize)
                    clsCommon.AddColumnsForChange(coll, "Transfer_Qty", objtr.act_qty)
                    clsCommon.AddColumnsForChange(coll, "Transfer_UOM", objtr.uom)
                    clsCommon.AddColumnsForChange(coll, "Balance_Qty", objtr.bal_qty)
                    clsCommon.AddColumnsForChange(coll, "Sale_UOM", objtr.sale_uom)
                    clsCommon.AddColumnsForChange(coll, "Conv_Factor", objtr.conv_factor)
                    clsCommon.AddColumnsForChange(coll, "Alt_Qty", objtr.alt_qty)
                    clsCommon.AddColumnsForChange(coll, "FOC", objtr.FOC)
                    clsCommon.AddColumnsForChange(coll, "Transfer_Line_No", objtr.Transfer_Line_No)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSA_SALE_TRANSFER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function

    Public Shared Function MandiTax(ByVal CSA_Type As String, ByVal TaxCode As String) As String
        Try
            Dim qry As String = "select tax_code from tspl_tax_master where tax_code='" + TaxCode + "' and is_mandi_tax='Y'"
            TaxCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

            If TaxCode IsNot Nothing AndAlso clsCommon.myLen(TaxCode) > 0 AndAlso clsCommon.CompairString(TaxCode, "") <> CompairStringResult.Equal Then
                If clsCommon.myLen(CSA_Type) > 0 Then
                    qry = "select count(*) from TSPL_TAX_CSA_DETAIL where tax_code='" + TaxCode + "' and csa_type='" + CSA_Type + "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                    If check > 0 Then
                        Return "Y"
                    Else
                        Return "N"
                    End If
                Else
                    Return "Y"
                End If
            Else
                Return "Y"
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsCSAStockTransferDetail
#Region "Variables"
    Public FOC As String = Nothing
    Public code As String = Nothing
    Public lineno As Integer = Nothing
    Public transcode As String = Nothing
    Public icode As String = Nothing
    Public qty As Decimal = Nothing
    Public rate As Decimal = Nothing
    Public packsize As Decimal = Nothing
    Public act_qty As Decimal = Nothing
    Public uom As String = Nothing
    Public bal_qty As Decimal = Nothing
    Public sale_uom As String = Nothing
    Public conv_factor As Decimal = Nothing
    Public alt_qty As Decimal = Nothing
    Public Transfer_Line_No As Decimal = Nothing

    Public arr As List(Of clsCSAStockTransferDetail) = Nothing
#End Region

    Public Shared Function SaveData(ByVal arr As List(Of clsCSAStockTransferDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(arr, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsCSAStockTransferDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim counter As Integer = 0
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsCSAStockTransferDetail In arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "document_code", objtr.code)
                    clsCommon.AddColumnsForChange(coll, "line_no", objtr.lineno)
                    clsCommon.AddColumnsForChange(coll, "Against_Transfer_Code", objtr.transcode)
                    clsCommon.AddColumnsForChange(coll, "item_code", objtr.icode)
                    clsCommon.AddColumnsForChange(coll, "qty", objtr.qty)
                    clsCommon.AddColumnsForChange(coll, "transfer_rate", objtr.rate)
                    clsCommon.AddColumnsForChange(coll, "Item_Pack_Size", objtr.packsize)
                    clsCommon.AddColumnsForChange(coll, "Transfer_Qty", objtr.act_qty)
                    clsCommon.AddColumnsForChange(coll, "Transfer_UOM", objtr.uom)
                    clsCommon.AddColumnsForChange(coll, "Balance_Qty", objtr.bal_qty)
                    clsCommon.AddColumnsForChange(coll, "Sale_UOM", objtr.sale_uom)
                    clsCommon.AddColumnsForChange(coll, "Conv_Factor", objtr.conv_factor)
                    clsCommon.AddColumnsForChange(coll, "Alt_Qty", objtr.alt_qty)
                    clsCommon.AddColumnsForChange(coll, "FOC", objtr.FOC)
                    clsCommon.AddColumnsForChange(coll, "Transfer_Line_No", objtr.Transfer_Line_No)

                    If counter = 0 Then
                        Dim qry As String = "delete from CSA_SALE_TRANSFER where document_code='" + objtr.code + "' and line_no='" + clsCommon.myCstr(objtr.lineno) + "' and "
                        qry += " item_code='" + objtr.icode + "' and FOC='" + objtr.FOC + "'" 'Against_Transfer_Code='" + objtr.transcode + "' and 
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If

                    clsCommonFunctionality.UpdateDataTable(coll, "CSA_SALE_TRANSFER", OMInsertOrUpdate.Insert, "", trans)
                    counter += 1


                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

